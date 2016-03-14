using UnityEngine;
using System.Collections;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset		asset;
	private	Transform		_container;
	private	TileConfig		_config;

	public	Transform	container	{get{return _container;}}
	public	TileConfig	config		{get{return _config;}}

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		_container = new GameObject("Map").transform;
		_config = TitanUtility.LoadFromText<TileConfig>(asset.text);
		_config.Hashing();
	}

	void Start() {
		LoadMap("tutorial_1");
	}

	public	void LoadMap(string id) {
		TileMap map = _config.tutorial.FindItem(id);
		if (map != null) {
			LoadMap(map);
		}
	}

	public	void LoadMap(TileMap map) {
		_container.name = string.Format("Map ({0})", map.id);
		var terrains = map.terrains;
		var total = terrains.Length;
		for (int i = 0 ; i < total ; i++) {
			var terrain = terrains[i];
			var items = terrain.GetTileItem();
			var count = items.Count;
			for (int j = 0 ; j < count ; j++) {
				AddTileItem(items[j], terrain.xf, terrain.yf, terrain.zf);
			}
		}
	}

	public	void AddTileItem(TileItem item, float x, float y, float z) {
		var go = new GameObject(item.asset, typeof(SpriteRenderer));
		go.GetComponent<SpriteRenderer>().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(item.assetPath);
		var tr = go.transform;
		tr.Translate(GetCoord(x, y, z));
		tr.SetParent(_container);
	}

	public	Vector3 GetCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.75f - (x * 0.25f + z * 0.25f);
		v3.z = (x + z + y) * -1f;
		return v3;
	}
}
