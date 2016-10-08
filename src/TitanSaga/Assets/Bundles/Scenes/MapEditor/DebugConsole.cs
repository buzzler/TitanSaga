using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TileLib;

public class DebugConsole : MonoBehaviour {
	public	HorizontalLayoutor	scrollView;
	public	RectTransform		scrollPrefab;
	public	Button				btnSimple;
	public	Button				btnComplex;
	public	Button				btnDirection;
	public	Button				btnBuilding;
	public	Button				btnWall;
	public	Button				btnNormal;
	public	Button				btnCharacter;
	public	Button				btnPerlin;
	public	Button				btnRandom;
	public	Button				btnNew;
	public	Button				btnLoad;
	public	Button				btnSave;

	private	TileLibrary			_library;
	private	UIStateParam		_param;

	void OnEnable () {
		btnSimple.onClick.AddListener (OnClickSimple);
		btnComplex.onClick.AddListener (OnClickComplex);
		btnDirection.onClick.AddListener (OnClickDirection);
		btnBuilding.onClick.AddListener (OnClickBuilding);
		btnWall.onClick.AddListener (OnClickWall);
		btnNormal.onClick.AddListener (OnClickNormal);
		btnCharacter.onClick.AddListener (OnClickCharacter);
		btnPerlin.onClick.AddListener (onClickPerlin);
		btnRandom.onClick.AddListener (OnClickRandom);
		btnNew.onClick.AddListener (OnClickNew);
		btnLoad.onClick.AddListener (OnClickLoad);
		btnSave.onClick.AddListener (OnClickSave);
		_library = Observer.Instance.tileManager.library;
		_param = Observer.Instance.uiManager.GetStateParam ("debugconsole");
		Invoke ("OnRestore", 0.5f);
	}

	void OnDisable() {
		OnBackup ();
		btnSimple.onClick.RemoveAllListeners ();
		btnComplex.onClick.RemoveAllListeners ();
		btnDirection.onClick.RemoveAllListeners ();
		btnBuilding.onClick.RemoveAllListeners ();
		btnWall.onClick.RemoveAllListeners ();
		btnNormal.onClick.RemoveAllListeners ();
		btnCharacter.onClick.RemoveAllListeners ();
		btnPerlin.onClick.RemoveAllListeners ();
		btnRandom.onClick.RemoveAllListeners ();
		btnNew.onClick.RemoveAllListeners ();
		btnLoad.onClick.RemoveAllListeners ();
		btnSave.onClick.RemoveAllListeners ();
		_library = null;
		_param = null;
	}

	private	void OnBackup() {
		float scrolled = scrollView.transform.localPosition.x;
		_param.SetFloat ("scrolled", scrolled);
	}

	private	void OnRestore() {
		string clicked = _param.GetString ("onclicked");
		switch (clicked) {
		case "simple":
			OnClickSimple ();
			break;
		case "complex":
			OnClickComplex ();
			break;
		case "direction":
			OnClickDirection ();
			break;
		case "building":
			OnClickBuilding ();
			break;
		case "wall":
			OnClickWall ();
			break;
		case "normal":
			OnClickNormal ();
			break;
		case "character":
			OnClickCharacter ();
			break;
		case "perlin":
			onClickPerlin ();
			break;
		case "random":
			OnClickRandom ();
			break;
		}

		if (_param.HasKey ("scrolled")) {
			float scrolled = _param.GetFloat ("scrolled");
			scrollView.transform.localPosition = new Vector3 (scrolled, 0f, 0f);
		}
	}

	private	void OnClickItem(TileBase tilebase) {
		Observer.Instance.uiManager.OpenState ("addnewtile", tilebase);
	}

	private	void OnClickSimple() {
		_param.SetString ("onclicked", "simple");
		scrollView.Clear();
		foreach(var simple in _library.simples.Values) {
			AddContent(simple.basic[0].item, simple);
		}

		scrollView.Align();
	}

	private	void OnClickComplex() {
		_param.SetString ("onclicked", "complex");
		scrollView.Clear();
		foreach (var complex in _library.complexs.Values) {
			AddContent(complex.basic[0].item, complex);
		}
		scrollView.Align();
	}

	private	void OnClickDirection() {
		_param.SetString ("onclicked", "direction");
		scrollView.Clear();
		foreach (var direction in _library.directions.Values) {
			AddContent(direction.u[0].item, direction);
		}
		scrollView.Align();
	}

	private	void OnClickBuilding() {
		_param.SetString ("onclicked", "building");
		scrollView.Clear();
		foreach(var building in _library.buildings.Values) {
			AddContent(building.basic[0].item, building);
		}
		scrollView.Align();
	}

	private	void OnClickWall() {
		_param.SetString ("onclicked", "wall");
		scrollView.Clear();
		foreach (var wall in _library.walls.Values) {
			AddContent(wall.basic[0].item, wall);
		}
		scrollView.Align();
	}

	private	void OnClickNormal() {
		_param.SetString ("onclicked", "normal");
		scrollView.Clear();
		foreach(var norm in _library.normals.Values) {
			AddContent(norm.link[0].item, norm);
		}
		scrollView.Align();
	}

	private	void OnClickCharacter() {
		_param.SetString ("onclicked", "character");
		scrollView.Clear ();
		foreach(var character in _library.characters.Values) {
			AddContent (character.basic.right[0].item, character);
		}
		scrollView.Align ();
	}

	private	void onClickPerlin() {
		_param.SetString ("onclicked", "perlin");
		scrollView.Clear ();
		foreach (var perlin in _library.perlins.Values) {
			AddContent (perlin.GetTileItemLInk(0)[0].item, perlin);
		}
		scrollView.Align ();
	}

	private	void OnClickRandom() {
		_param.SetString ("onclicked", "random");
		scrollView.Clear ();
		foreach (var random in this._library.randoms.Values) {
			AddContent (random.GetTileItemLink()[0].item, random);
		}
		scrollView.Align ();
	}

	private	void OnClickNew() {
		#if UNITY_EDITOR
		if (UnityEditor.EditorUtility.DisplayDialog("create new map", "really?", "OK", "Cancel")) {
			Observer.Instance.uiManager.OpenState ("newworld");
		}
		#else
		Observer.Instance.uiManager.OpenState ("newworld");
		#endif
	}

	private	void OnClickLoad() {
		#if UNITY_EDITOR
		var path = UnityEditor.EditorUtility.OpenFilePanel("load map info", Application.dataPath, "xml");
		if (!string.IsNullOrEmpty(path)) {
			var xml = System.IO.File.ReadAllText(path);
			Observer.Instance.tileManager.LoadMap(TitanUtility.LoadFromText<TileMap>(xml));
		}
		#else
		var save = PlayerPrefs.GetString("save", "");
		Observer.Instance.tileManager.LoadMap(TitanUtility.LoadFromText<TileMap>(save));
		#endif
	}

	private	void OnClickSave() {
		#if UNITY_EDITOR
		var path = UnityEditor.EditorUtility.SaveFilePanel("save map info", Application.dataPath, "map", "xml");
		if (!string.IsNullOrEmpty(path)) {
			System.IO.File.WriteAllText(path, Observer.Instance.tileManager.mapXml);
			UnityEditor.EditorUtility.RevealInFinder(path);
		}
		#else
		PlayerPrefs.SetString("save", Observer.Instance.tileManager.mapXml);
		#endif
	}

	private	void OnClickClose() {
		#if UNITY_EDITOR
		if (UnityEditor.EditorUtility.DisplayDialog("exit map editor", "really?", "OK", "Cancel")) {
			Observer.Instance.uiManager.CloseAllState();
		}
		#else
		Observer.Instance.uiManager.CloseAllState();
		#endif
	}

	private	void AddContent(TileItem item, TileBase tilebase) {
		var t = Instantiate<RectTransform>(scrollPrefab);
		t.GetComponent<TerrainItem>().SetItem(item, tilebase);
		scrollView.AddItem(t);
	}
}
