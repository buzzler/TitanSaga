using UnityEngine;
using System.Collections;
using TileLib;

public class TileManager : MonoBehaviour {
	public	TextAsset	asset;
	private	TileConfig	_config;

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		_config = TileConfig.LoadFromText(asset.text);
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
		// TODO : Create Terrain
	}
}
