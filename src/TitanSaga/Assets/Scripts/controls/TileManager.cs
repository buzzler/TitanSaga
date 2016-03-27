using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset		asset;
	private	Transform		_containerMap;
	private	Transform		_containerGrid;
	private	TileConfig		_config;
	private	TileMap			_map;
	private TileMap			_grid;
	public	Transform	container	{get{return _containerMap;}}
	public	TileConfig	config		{get{return _config;}}

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
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
		return GetCoord (0, 0, 0);
	}

	public	Vector3 GetBottomCoord() {
		return GetCoord (_map.width - 1, 0, _map.depth - 1);
	}

	public	Vector3 GetLeftCoord() {
		return GetCoord (0, 0, _map.depth - 1);
	}

	public	Vector3 GetRightCoord() {
		return GetCoord (_map.width - 1, 0, 0);
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
			for (int j = _map.height - 1; j >= 0; j--) {
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

	public	void AddTerrain(TileTerrain terrain) {
		_map.AddTerrain (terrain);
		RenderTerrain (terrain, _containerGrid);
	}

	private	void RenderTerrain(TileTerrain terrain, Transform container) {
		var items = terrain.GetTileItem (_config.library);
		var count = items.Count;
		for (int j = 0; j < count; j++) {
			RenderTileItem (container, items [j], terrain.xf, terrain.yf, terrain.zf);
		}
	}

	private	void RenderTileItem(Transform container, TileItem item, float x, float y, float z) {
		var go = new GameObject (item.asset, typeof(SpriteRenderer));
		go.GetComponent<SpriteRenderer> ().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite> (item.assetPath);
		var tr = go.transform;
		tr.SetParent (container);
		tr.localPosition = GetCoord (x, y, z);
	}

	private	Vector3 GetCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.75f - (x * 0.25f + z * 0.25f);
		v3.z = (x + z + y) * -1f;
		return v3;
	}
}
