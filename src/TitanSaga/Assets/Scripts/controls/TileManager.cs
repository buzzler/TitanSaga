using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset									asset;
	private	Dictionary<TileTerrain, List<Transform>>	_table;
	private	Transform									_containerMap;
	private	TileConfig									_config;
	private	TileMap										_map;
	private	Rect										_rect;

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
		_table = new Dictionary<TileTerrain, List<Transform>> ();
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
			DestroyImmediate (_containerMap.GetChild (0).gameObject);
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
			RenderTerrain (terrains [i], _containerMap);
		}
		_containerMap.gameObject.SetActive (true);
	}

	public	void AddTerrain(TileTerrain terrain) {
		RenderTerrain (terrain, _containerMap);
	}

	public	bool ConfirmTerrain(TileTerrain terrain) {
		if (_map.AddTerrain (terrain)) {
			for (int _x = -1 ; _x <= 1 ; _x++) {
				for (int _y = -1 ; _y <= 1 ; _y++) {
					for (int _z = -1 ; _z <= 1 ; _z++) {
						var _terrain = _map.GetTerrain (terrain.x + _x, terrain.y + _y, terrain.z + _z);
						if (_terrain != null) {
							UpdateTerrain (_terrain, true, true);
						}
					}
				}
			}
			return true;
		}
		return false;
	}

	public	void RemoveTerrain(TileTerrain terrain) {
		_map.RemoveTerrain (terrain);
		if (_table.ContainsKey (terrain)) {
			var list = _table [terrain];
			for (int i = list.Count - 1; i >= 0; i--) {
				DestroyImmediate (list [i].gameObject);
			}
		}
	}

	public	void UpdateTerrain(TileTerrain terrain, bool updateCoord = true, bool updateSprite = false) {
		if (!(updateCoord || updateSprite)) {
			return;
		}
		if (_table.ContainsKey (terrain)) {
			var list = _table [terrain];
			int count = list.Count;
			List<TileItem> items = updateSprite ? terrain.GetTileItem (_config.library) : null;

			for (int i = 0; i < count; i++) {
				var tr = list [i];
				if (updateCoord) {
					tr.localPosition = GetPixelCoord (terrain.xf, terrain.yf, terrain.zf);;
				}
				if (updateSprite) {
					tr.GetComponent<SpriteRenderer> ().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite> (items [i].assetPath);
				}
			}
		}
	}

	private	void RenderTerrain(TileTerrain terrain, Transform container) {
		var items = terrain.GetTileItem (_config.library);
		var count = items.Count;
		List<Transform> list = new List<Transform> (count);
		Vector3 coord = GetPixelCoord (terrain.xf, terrain.yf, terrain.zf);
		for (int j = 0; j < count; j++) {
			list.Add (RenderTileItem (container, items [j], coord));
		}
		_table.Add (terrain, list);
	}

	private	Transform RenderTileItem(Transform container, TileItem item, Vector3 position) {
		var go = new GameObject (item.asset, typeof(SpriteRenderer));
		go.GetComponent<SpriteRenderer> ().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite> (item.assetPath);
		var tr = go.transform;
		tr.SetParent (container);
		tr.localPosition = position;
		tr.Translate (-item.pivotX,-item.pivotY, 0);
		return tr;
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
