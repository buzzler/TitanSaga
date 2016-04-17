using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset									asset;
	private	Dictionary<TileTerrain, List<GameObject>>	_table;
	private	Transform									_containerMap;
	private	Transform									_containerGrid;
	private	TileConfig									_config;
	private	TileMap										_map;
	private TileMap										_grid;
	public	Transform	container	{get{return _containerMap;}}
	public	TileConfig	config		{get{return _config;}}

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
		_table = new Dictionary<TileTerrain, List<GameObject>> ();
		_containerMap = new GameObject ("Map").transform;
		_containerGrid = new GameObject ("Grid").transform;
		_config = TitanUtility.LoadFromText<TileConfig> (asset.text);
		_map = new TileMap ();
		_grid = new TileMap ();

		_containerMap.gameObject.SetActive (false);
		_containerGrid.gameObject.SetActive (false);
		_containerGrid.transform.localPosition = new Vector3(0f, -0.25f, 499f);
		_config.Hashing ();
	}

	void Start() {
		LoadMap (config.tutorial.FindItem ("tutorial_1"));
	}

	public	void Grid(bool show) {
		_containerGrid.gameObject.SetActive (show);
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
		_containerGrid.gameObject.SetActive (false);

		for (int i = _containerMap.childCount - 1; i >= 0; i--) {
			DestroyImmediate (_containerMap.GetChild (0));
		}
		_map = new TileMap ();

		for (int i = _containerGrid.childCount - 1; i >= 0; i--) {
			DestroyImmediate (_containerGrid.GetChild (0));
		}
		_grid = new TileMap ();
	}

	public	void LoadMap(TileMap map) {
		_map = map.Clone ();
		_map.Hashing ();

		var terrains = _map.terrains;
		var total = terrains.Length;
		for (int i = 0; i < total; i++) {
			RenderTerrain (terrains [i], _containerMap);
		}

		_grid = new TileMap ();
		_grid.width = _map.width;
		_grid.height = _map.height;
		_grid.depth = _map.depth;
		_grid.Hashing ();
		List<TileTerrain> list = new List<TileTerrain> ();
		// floor
		for (int i = _map.width - 1; i >= 0; i--) {
			for (int j = _map.depth - 1; j >= 0; j--) {
				var t = new TileTerrain (config.library.unit, i, 0, j);
				list.Add (t);
				_grid.AddTerrain (t);
			}
		}
		// right
		for (int i = _map.width - 1; i > 0; i--) {
			for (int j = _map.height - 1; j > 0; j--) {
				var t = new TileTerrain (config.library.unit, i, j, 0);
				list.Add (t);
				_grid.AddTerrain (t);
			}
		}
		// left
		for (int i = _map.height - 1; i > 0; i--) {
			for (int j = _map.depth - 1; j > 0; j--) {
				var t = new TileTerrain (config.library.unit, 0, i, j);
				list.Add (t);
				_grid.AddTerrain (t);
			}
		}
		_grid.terrains = list.ToArray ();
		total = list.Count;
		for (int i = 0; i < total; i++) {
			RenderTerrain (list [i], _containerGrid);
		}

		_containerMap.gameObject.SetActive (true);
	}

	public	void AddTerrain(TileTerrain terrain, bool confirmed = false) {
		RenderTerrain (terrain, _containerMap);
		if (confirmed) {
			ConfirmTerrain (terrain);
		}
	}

	public	void ConfirmTerrain(TileTerrain terrain) {
		_map.AddTerrain (terrain);
	}

	public	void RemoveTerrain(TileTerrain terrain) {
		_map.RemoveTerrain (terrain);
		if (_table.ContainsKey (terrain)) {
			var list = _table [terrain];
			for (int i = list.Count - 1; i >= 0; i--) {
				DestroyImmediate (list [i]);
			}
		}
	}

	public	void UpdateTerrain(TileTerrain terrain) {
		if (_table.ContainsKey (terrain)) {
			var list = _table [terrain];
			var coord = GetPixelCoord (terrain.xf, terrain.yf, terrain.zf);
			for (int i = list.Count - 1; i >= 0; i--) {
				list [i].transform.localPosition = coord;
			}
		}
	}

	private	void RenderTerrain(TileTerrain terrain, Transform container) {
		var items = terrain.GetTileItem (_config.library);
		var count = items.Count;
		List<GameObject> list = new List<GameObject> (count);
		Vector3 coord = GetPixelCoord (terrain.xf, terrain.yf, terrain.zf);
		for (int j = 0; j < count; j++) {
			list.Add (RenderTileItem (container, items [j], coord));
		}
		_table.Add (terrain, list);
	}

	private	GameObject RenderTileItem(Transform container, TileItem item, Vector3 position) {
		var go = new GameObject (item.asset, typeof(SpriteRenderer));
		go.GetComponent<SpriteRenderer> ().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite> (item.assetPath);
		var tr = go.transform;
		tr.SetParent (container);
		tr.localPosition = position;
		return go;
	}

	public	Vector3 GetPixelCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.75f - (x * 0.25f + z * 0.25f);
		v3.z = -(x + z + y);
		return v3;
	}

	public	Vector3 GetVoxelCoord(float x, float y, bool snap = true) {
		var _x = x / 0.5f;
		var _y = y / 0.25f;
		Vector3 v3;
		v3.x = (_x - _y) / 2f;
		v3.y = 0f;
		v3.z = -(_x + _y) / 2f;

		if (_map != null) {
			v3.x = Mathf.Clamp (v3.x, 0, _map.width);
			v3.y = Mathf.Clamp (v3.y, 0, _map.height);
			v3.z = Mathf.Clamp (v3.z, 0, _map.depth);
		}

		if (snap) {
			v3.x = Mathf.Round (v3.x);
			v3.y = Mathf.Round (v3.y);
			v3.z = Mathf.Round (v3.z);
		}

		return v3;
	}
}
