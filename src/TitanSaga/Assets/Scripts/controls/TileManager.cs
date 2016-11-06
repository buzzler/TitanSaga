using UnityEngine;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset								asset;
	private	Dictionary<TileObject, TileItemGroup>	_table;
	private	Transform								_containerMap;
	private	TileConfig								_config;
	private	TileLibrary								_library;
	private	TileMap									_map;
	private	Rect									_rect;

	public	Transform	container	{ get { return _containerMap; } }
	public	TileConfig	config		{ get { return _config; } }
	public	TileLibrary	library		{ get { return _library; } }
	public	TileMap		map			{ get { return _map; } }
	public	string		mapXml		{ get { return TitanUtility.SaveToText<TileMap> (_map); } }
	public	Rect		bound		{ get { return _rect; } }
	public	int			width		{ get { return _map.width; } }
	public	int			height		{ get { return _map.height; } }
	public	int			depth		{ get { return _map.depth; } }

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
		_table = new Dictionary<TileObject, TileItemGroup> ();
		_containerMap = new GameObject ("Map").transform;
		_config = TitanUtility.LoadFromText<TileConfig> (asset.text);
		_library = new TileLibrary ();

		_config.Hashing ();
		ClearMap ();
	}

	void Start() {
		LoadDB ();
	}

	public	Vector3 GetTopCoord() {
		return GetPixelCoord (0, 0, 0);
	}

	public	Vector3 GetBottomCoord() {
		return GetPixelCoord (_map.width - 1, 0, _map.depth - 1);
	}

	public	Vector3 GetLeftCoord() {
		return GetPixelCoord (0, 0, _map.depth - 1);
	}

	public	Vector3 GetRightCoord() {
		return GetPixelCoord (_map.width - 1, 0, 0);
	}

	public	void HideMap() {
		_containerMap.gameObject.SetActive (false);
	}

	public	void ShowMap() {
		_containerMap.gameObject.SetActive (true);
	}

	public	void ClearMap() {
		for (int i = _containerMap.childCount - 1; i >= 0; i--) {
			var t = _containerMap.GetChild (0);
			if (t != null && t.gameObject != null) {
				DestroyImmediate (t.gameObject);
			}
		}
		if (_map != null) {
			_map = new TileMap (_map.width, _map.height, _map.depth);
		} else {
			_map = new TileMap ();
		}
		_rect = new Rect ();
	}

	public	void ClearMap(int width, int height, int depth) {
		for (int i = _containerMap.childCount - 1; i >= 0; i--) {
			var t = _containerMap.GetChild (0);
			if (t != null && t.gameObject != null) {
				DestroyImmediate (t.gameObject);
			}
		}
		_map = new TileMap (width, height, depth);
		_map.Hashing ();
		_rect = GetTileBound ();
	}

	public	void LoadDB() {
		var dbms = Observer.Instance.dbms;
		dbms.ExecuteReader ("SELECT * FROM tile_item", (IDataReader dr) => {
			while (dr.Read ()) {
				var item = new TileItem (dr);
				library.items.Add(item.id, item);
				library.allItems.Add(item.id, item);
			}
			dr.Dispose ();
		});
		dbms.ExecuteReader ("SELECT * FROM tile_itemlink", (IDataReader dr) => {
			while (dr.Read ()) {
				var link = new TileItemLink (dr, library.items);
				library.itemLinks.Add(link.id, link);
			}
			dr.Dispose ();
		});

		dbms.ExecuteReader ("SELECT * FROM tile_normal", (IDataReader dr) => {
			while (dr.Read()) {
				var norm = new TileNormal(dr, library.itemLinks);
				library.normals.Add(norm.id, norm);
				library.allItems.Add(norm.id, norm);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_motion", (IDataReader dr) => {
			while (dr.Read()) {
				var motion = new TileCharacterMotion(dr, library.itemLinks);
				library.motions.Add(motion.id, motion);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_character", (IDataReader dr) => {
			while (dr.Read()) {
				var character = new TileCharacter(dr, library.motions);
				library.characters.Add(character.id, character);
				library.allItems.Add(character.id, character);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_building", (IDataReader dr) => {
			while (dr.Read ()) {
				var building = new TileBuilding (dr, library.itemLinks);
				library.buildings.Add (building.id, building);
				library.allItems.Add(building.id, building);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_wall", (IDataReader dr) => {
			while (dr.Read ()) {
				var wall = new TileWall (dr, library.itemLinks);
				library.walls.Add (wall.id, wall);
				library.allItems.Add(wall.id, wall);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_simple", (IDataReader dr) => {
			while (dr.Read ()) {
				var simple = new TileSimple (dr, library.itemLinks);
				library.simples.Add (simple.id, simple);
				library.allItems.Add(simple.id, simple);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_complex", (IDataReader dr) => {
			while (dr.Read ()) {
				var complex = new TileComplex (dr, library.itemLinks);
				library.complexs.Add (complex.id, complex);
				library.allItems.Add(complex.id, complex);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_direction", (IDataReader dr) => {
			while (dr.Read ()) {
				var direnction = new TileDirection (dr, library.itemLinks);
				library.directions.Add (direnction.id, direnction);
				library.allItems.Add(direnction.id, direnction);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_perlin", (IDataReader dr) => {
			while (dr.Read ()) {
				var perlin = new TilePerlin (dr, library.itemLinks);
				library.perlins.Add (perlin.id, perlin);
				library.allItems.Add(perlin.id, perlin);
			}
		});

		dbms.ExecuteReader ("SELECT * FROM tile_random", (IDataReader dr) => {
			while (dr.Read ()) {
				var rand = new TileRandom (dr, library.itemLinks);
				library.randoms.Add (rand.id, rand);
				library.allItems.Add(rand.id, rand);
			}


			// complete!!
			OnLoadDBComplete ();
		});
	}

	private	void OnLoadDBComplete() {
		LoadMap (config.tutorial.FindItem ("tutorial_1"));
	}

	public	void LoadMap(TileMap map) {
		ClearMap ();
		_map = map.Clone ();
		_map.Hashing ();
		_rect = GetTileBound ();

		var terrains = _map.terrains;
		var total = terrains.Length;
		for (int i = 0; i < total; i++) {
			AddTileObject (terrains [i]);
		}
		var units = _map.units;
		total = units.Length;
		for (int i = 0; i < total; i++) {
			AddTileObject (units [i]);
		}
	}

	public	void AddTileObject(TileObject tile) {
		var coord = GetPixelCoord (tile.xf, tile.yf, tile.zf);
		var group = new GameObject (tile.item).AddComponent<TileItemGroup> ();
		group.SetTileObject (library, tile as ITileObject, _containerMap);
		group.UpdateSprite (coord);

		if (!_table.ContainsKey (tile)) {
			_table.Add (tile, group);
		}
	}

	public	bool ConfirmTileObject(TileObject tile) {
		if (_map.AddTileObject (tile)) {
			UpdateBoundary (tile.x, tile.y, tile.z);
			return true;
		}
		return false;
	}

	public	void RemoveTileObject(TileObject tile) {
		if (_table.ContainsKey (tile)) {
			var group = _table [tile];
			_table.Remove (tile);
			if (group != null && group.gameObject != null) {
				DestroyImmediate (group.gameObject);
			}
		}
		_map.RemoveTileObject (tile);
	}

	public	void UpdateSingle(TileObject tile) {
		if (_table.ContainsKey (tile)) {
			_table [tile].UpdateSprite (GetPixelCoord (tile.xf, tile.yf, tile.zf));
		}
	}

	public	void UpdateBoundary(int x, int y, int z) {
		for (int _x = -1 ; _x <= 1 ; _x++) {
			for (int _y = -1 ; _y <= 1 ; _y++) {
				for (int _z = -1 ; _z <= 1 ; _z++) {
					var obj = _map.GetTileObject (x + _x, y + _y, z + _z);
					if (obj != null) {
						UpdateSingle (obj);
					}
				}
			}
		}
	}

	public	Vector3 GetPixelCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.5f - (x * 0.25f + z * 0.25f);
		v3.z = -(x + z + y);
		return v3;
	}

	public	Vector3 GetTileCoord(float x, float y, bool snap = true) {
		var _x = x / 0.5f;
		var _y = y / 0.25f;
		Vector3 v3;
		v3.x = (_x - _y) / 2f;
		v3.y = 0f;
		v3.z = -(_x + _y) / 2f;

		if (_map != null) {
			v3.x = Mathf.Clamp (v3.x, 0, _map.width-1);
			v3.z = Mathf.Clamp (v3.z, 0, _map.depth-1);
		}

		if (snap) {
			v3.x = Mathf.Ceil (v3.x);
			v3.z = Mathf.Ceil (v3.z);
		}
		return v3;
	}

	public	Vector3 GetSurfaceCoord(float x, float z) {
		int _x = (int)x;
		int _z = (int)z;

		int max = _map.height;
		for (int y = 0; y < max; y++) {
			if (_map.GetTileObject (_x, y, _z) == null) {
				return new Vector3 (x, (float)y, z);
			}
		}
		return new Vector3 (x, (float)max, z);
	}

	public	Rect GetTileBound() {
		var top = _map.height * 0.75f;
		var bottom = -((_map.width - 1) * 0.25f + (_map.depth - 1) * 0.25f);
		var left = (_map.depth - 1) * -0.5f;
		var right = (_map.width - 1) * 0.5f;
		return Rect.MinMaxRect (left, bottom, right, top);
	}
}
