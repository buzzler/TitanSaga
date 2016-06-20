using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset								asset;
	private	Dictionary<TileObject, TileItemGroup>	_table;
	private	Transform								_containerMap;
	private	TileConfig								_config;
	private	TileMap									_map;
	private	Rect									_rect;

	public	Transform	container	{ get { return _containerMap; } }
	public	TileConfig	config		{ get { return _config; } }
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
		_config.Hashing ();

		ClearMap ();
	}

	void Start() {
		LoadMap (config.tutorial.FindItem ("tutorial_1"));
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

	public	void ClearMap() {
		_containerMap.gameObject.SetActive (false);

		for (int i = _containerMap.childCount - 1; i >= 0; i--) {
			var t = _containerMap.GetChild (0);
			if (t != null && t.gameObject != null) {
				DestroyImmediate (t.gameObject);
			}
		}
		_map = new TileMap ();
		_rect = new Rect ();
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
		_containerMap.gameObject.SetActive (true);
	}

	public	void AddTileObject(TileObject tile) {
		var coord = GetPixelCoord (tile.xf, tile.yf, tile.zf);
		var group = new GameObject (tile.item).AddComponent<TileItemGroup> ();
		group.SetTileObject (_config.library, tile as ITileObject, _containerMap);
		group.UpdateSprite (coord);

		if (!_table.ContainsKey (tile)) {
			_table.Add (tile, group);
		}
	}

	public	bool ConfirmTileObject(TileObject tile) {
		bool added = false;
		if (tile is TileTerrain) {
			added = _map.AddTerrain (tile as TileTerrain);
		} else if (tile is TileUnit) {
			added = _map.AddUnit (tile as TileUnit);
		}

		if (added) {
			for (int _x = -1 ; _x <= 1 ; _x++) {
				for (int _y = -1 ; _y <= 1 ; _y++) {
					for (int _z = -1 ; _z <= 1 ; _z++) {
						var obj = _map.GetTileObject (tile.x + _x, tile.y + _y, tile.z + _z);
						if (obj != null) {
							UpdateTileObject (obj);
						}
					}
				}
			}
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

		if (tile is TileTerrain) {
			_map.RemoveTerrain (tile as TileTerrain);
		} else if (tile is TileUnit) {
			_map.RemoveUnit (tile as TileUnit);
		}
	}

	public	void UpdateTileObject(TileObject tile) {
		if (_table.ContainsKey (tile)) {
			_table [tile].UpdateSprite (GetPixelCoord (tile.xf, tile.yf, tile.zf));
		}
	}

	public	Vector3 GetPixelCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.75f - (x * 0.25f + z * 0.25f);
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

	public	Rect GetTileBound() {
		var top = _map.height * 0.75f;
		var bottom = -((_map.width - 1) * 0.25f + (_map.depth - 1) * 0.25f);
		var left = (_map.depth - 1) * -0.5f;
		var right = (_map.width - 1) * 0.5f;
		return Rect.MinMaxRect (left, bottom, right, top);
	}
}
