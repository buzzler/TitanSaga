using UnityEngine;
using System.Collections;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset		asset;
	private	TileConfig		_config;

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		_config = TileConfig.LoadFromText(asset.text);
	}

	void Start() {
		LoadMap("tutorial_1");
	}

	public	TileConfig config {
		get {
			return _config;
		}
	}

	public	void LoadMap(string id) {
		TileMap map = _config.tutorial.FindItem(id);
		if (map != null) {
			LoadMap(map);
		}
	}

	public	void LoadMap(TileMap map) {
		var terrains = map.terrains;
		var total = terrains.Length;
		for (int i = 0 ; i < total ; i++) {
			var terrain = terrains[i];
			var items = terrain.GetTileItem();
			for (int j = 0 ; j < items.Count ; j++) {
				TileItem item = items[j];
				var go = new GameObject(item.asset, typeof(SpriteRenderer));
				go.GetComponent<SpriteRenderer>().sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(item.assetPath);
				go.transform.Translate(GetCoord(terrain.xf, terrain.yf, terrain.zf));

			}
		}
	}

	public	Vector3 GetCoord(float x, float y, float z) {
		Vector3 v3;
		v3.x = x * 0.5f - z * 0.5f;
		v3.y = y * 0.75f - (x * 0.25f + z * 0.25f);
		v3.z = (x + z + y) * -1f;
		return v3;
	}
}
