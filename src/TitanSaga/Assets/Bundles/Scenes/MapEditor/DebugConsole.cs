using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TileLib;

public class DebugConsole : MonoBehaviour {
	public	HorizontalLayoutor	scrollView;
	public	RectTransform		scrollPrefab;
	public	Button				btnCategory;
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
		btnCategory.onClick.AddListener (OnClickCategory);
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
		_library = Observer.Instance.tileManager.config.library;
		_param = Observer.Instance.uiManager.GetStateParam ("debugconsole");
		Invoke ("OnRestore", 0.5f);
	}

	void OnDisable() {
		OnBackup ();
		btnCategory.onClick.RemoveAllListeners ();
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
		case "category":
			OnClickCategory ();
			break;
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

	private	void OnClickCategory() {
		_param.SetString ("onclicked", "category");
		scrollView.Clear();
		int categories = _library.categories.Length;
		for (int c = 0 ; c < categories ; c++) {
			var category = _library.categories[c];
			int items = category.items.Length;
			for (int i = 0 ; i < items ; i++) {
				var item = category.items[i];
				AddContent(item, item);
			}
		}
		scrollView.Align();
	}

	private	void OnClickSimple() {
		_param.SetString ("onclicked", "simple");
		scrollView.Clear();
		int simples = _library.simples.Length;
		for (int s = 0 ; s < simples ; s++) {
			var simple = _library.simples[s];
			AddContent(_library.FindItem(simple.none.item) as TileItem, simple);
		}
		scrollView.Align();
	}

	private	void OnClickComplex() {
		_param.SetString ("onclicked", "complex");
		scrollView.Clear();
		int complexs = _library.complexs.Length;
		for (int c = 0 ; c < complexs ; c++) {
			var complex = _library.complexs[c];
			AddContent(_library.FindItem(complex.none.item) as TileItem, complex);
		}
		scrollView.Align();
	}

	private	void OnClickDirection() {
		_param.SetString ("onclicked", "direction");
		scrollView.Clear();
		int directs = _library.directions.Length;
		for (int d = 0 ; d < directs ; d++) {
			var direction = _library.directions[d];
			AddContent(_library.FindItem(direction.up.item) as TileItem, direction);
		}
		scrollView.Align();
	}

	private	void OnClickBuilding() {
		_param.SetString ("onclicked", "building");
		scrollView.Clear();
		int build = _library.buildings.Length;
		for (int d = 0 ; d < build ; d++) {
			var building = _library.buildings[d];
			AddContent(_library.FindItem(building.basic.item) as TileItem, building);
		}
		scrollView.Align();
	}

	private	void OnClickWall() {
		_param.SetString ("onclicked", "wall");
		scrollView.Clear();
		int count = _library.walls.Length;
		for (int d = 0 ; d < count ; d++) {
			var wall = _library.walls[d];
			AddContent(_library.FindItem(wall.none.item) as TileItem, wall);
		}
		scrollView.Align();
	}

	private	void OnClickNormal() {
		_param.SetString ("onclicked", "normal");
		scrollView.Clear();
		int norms = _library.normals.Length;
		for (int d = 0 ; d < norms ; d++) {
			var norm = _library.normals[d];
			AddContent(_library.FindItem(norm.link.item) as TileItem, norm);
		}
		scrollView.Align();
	}

	private	void OnClickCharacter() {
		_param.SetString ("onclicked", "character");
		scrollView.Clear ();
		int count = _library.characters.Length;
		for (int d = 0; d < count; d++) {
			var character = _library.characters [d];
			AddContent (_library.FindItem (character.GetMotion ().right.item) as TileItem, character);
		}
		scrollView.Align ();
	}

	private	void onClickPerlin() {
		_param.SetString ("onclicked", "perlin");
		scrollView.Clear ();
		int count = _library.perlins.Length;
		for (int d = 0; d < count; d++) {
			var perlin = _library.perlins [d];
			AddContent (_library.FindItem (perlin.links [0].item) as TileItem, perlin);
		}
		scrollView.Align ();
	}

	private	void OnClickRandom() {
		_param.SetString ("onclicked", "random");
		scrollView.Clear ();
		int count = _library.randoms.Length;
		for (int d = 0; d < count; d++) {
			var random = _library.randoms [d];
			AddContent (_library.FindItem (random.links [0].item) as TileItem, random);
		}
		scrollView.Align ();
	}

	private	void OnClickNew() {
		#if UNITY_EDITOR
		if (UnityEditor.EditorUtility.DisplayDialog("create new map", "really?", "OK", "Cancel")) {
			Observer.Instance.uiManager.OpenState ("newworld");
		}
		#endif
	}

	private	void OnClickLoad() {
		#if UNITY_EDITOR
		var path = UnityEditor.EditorUtility.OpenFilePanel("load map info", Application.dataPath, "xml");
		if (!string.IsNullOrEmpty(path)) {
			var xml = System.IO.File.ReadAllText(path);
			Observer.Instance.tileManager.LoadMap(TitanUtility.LoadFromText<TileMap>(xml));
		}
		#endif
	}

	private	void OnClickSave() {
		#if UNITY_EDITOR
		var path = UnityEditor.EditorUtility.SaveFilePanel("save map info", Application.dataPath, "map", "xml");
		if (!string.IsNullOrEmpty(path)) {
			System.IO.File.WriteAllText(path, Observer.Instance.tileManager.mapXml);
			UnityEditor.EditorUtility.RevealInFinder(path);
		}
		#endif
	}

	private	void OnClickClose() {
		#if UNITY_EDITOR
		if (UnityEditor.EditorUtility.DisplayDialog("exit map editor", "really?", "OK", "Cancel")) {
			Observer.Instance.uiManager.CloseAllState();
		}
		#endif
	}

	private	void AddContent(TileItem item, TileBase tilebase) {
		var t = Instantiate<RectTransform>(scrollPrefab);
		t.GetComponent<TerrainItem>().SetItem(item, tilebase);
		scrollView.AddItem(t);
	}
}
