using UnityEngine;
using System.Collections;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset		asset;
	private	Transform		_container;
	private	TileConfig		_config;
	private	TileMap			_map;
	private TileMap			_grid;
	public	Transform	container	{get{return _container;}}
	public	TileConfig	config		{get{return _config;}}

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		_container = new GameObject("Map").transform;
		_config = TitanUtility.LoadFromText<TileConfig>(asset.text);
		_map = new TileMap();
		_grid = new TileMap();
		_config.Hashing();
	}

	void Start() {
		LoadMap(config.tutorial.FindItem("tutorial_1"));
	}

	public	Vector3 GetTopCoord() {
		return GetCoord(0, 0, 0);
	}

	public	Vector3 GetBottomCoord() {
		return GetCoord(_map.width-1, 0, _map.depth-1);
	}

	public	Vector3 GetLeftCoord() {
		return GetCoord(0, 0, _map.depth-1);
	}

	public	Vector3 GetRightCoord() {
		return GetCoord(_map.width-1, 0, 0);
	}

	public	void ClearMap() {
		for (int i = _container.childCount-1 ; i>=0 ; i--) {
			DestroyImmediate(_container.GetChild(0));
		}
		_map = new TileMap();
	}

	public	void LoadMap(TileMap map) {
		_map = map.Clone();
		_map.Hashing();

		_container.name = string.Format("Map ({0})", _map.id);
		var terrains = _map.terrains;
		var total = terrains.Length;
		for (int i = 0 ; i < total ; i++) {
			RenderTerrain(terrains[i]);
		}

		_grid = new TileMap();
		_grid.width = _map.width;
		_grid.height = _map.height;
		_grid.depth = _map.depth;
		for (int i = _map.width-1 ; i>=0 ; i--) {
			for (int j = _map.depth-1 ; j>=0 ;j--) {
				var t = new TileTerrain(config.library.unit, i, 0, j);
				_grid.AddTerrain(t);
			}
		}
		for (int i = _map.width-1 ; i>=0 ; i--) {
			for (int j = _map.height-1 ; j>=0 ;j--) {
				var t = new TileTerrain(config.library.unit, i, j, 0);
				_grid.AddTerrain(t);
			}
		}
		for (int i = _map.height-1 ; i>=0 ; i--) {
			for (int j = _map.depth-1 ; j>=0 ;j--) {
				var t = new TileTerrain(config.library.unit, 0, i, j);
				_grid.AddTerrain(t);
			}
		}
	}

	public	void AddTerrain(TileTerrain terrain) {
		_map.AddTerrain(terrain);
		RenderTerrain(terrain);
	}

	private	void RenderTerrain(TileTerrain terrain) {
		var items = terrain.GetTileItem(_config.library);
		var count = items.Count;
		for (int j = 0 ; j < count ; j++) {
			RenderTileItem(items[j], terrain.xf, terrain.yf, terrain.zf);
		}
	}

	private	void RenderTileItem(TileItem item, float x, float y, float z) {
		var go = new GameObject(item.asset, typeof(SpriteRenderer));
		go.GetComponent<SpriteRenderer>().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(item.assetPath);
		var tr = go.transform;
		tr.Translate(GetCoord(x, y, z));
		tr.SetParent(_container);
	}

	private	Vector3 GetCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.75f - (x * 0.25f + z * 0.25f);
		v3.z = (x + z + y) * -1f;
		return v3;
	}
}
