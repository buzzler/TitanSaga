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
	public	Button				btnClear;
	public	Button				btnLoad;
	public	Button				btnSave;
	public	Button				btnClose;

	private	TileLibrary			_library;

	void OnEnable () {
		btnCategory.onClick.AddListener (OnClickCategory);
		btnSimple.onClick.AddListener (OnClickSimple);
		btnComplex.onClick.AddListener (OnClickComplex);
		btnDirection.onClick.AddListener (OnClickDirection);
		btnBuilding.onClick.AddListener (OnClickBuilding);
		btnClear.onClick.AddListener (OnClickClear);
		btnLoad.onClick.AddListener (OnClickLoad);
		btnSave.onClick.AddListener (OnClickSave);
		btnClose.onClick.AddListener (OnClickClose);
		_library = Observer.Instance.tileManager.config.library;
	}

	void OnDisable() {
		btnCategory.onClick.RemoveAllListeners ();
		btnSimple.onClick.RemoveAllListeners ();
		btnComplex.onClick.RemoveAllListeners ();
		btnDirection.onClick.RemoveAllListeners ();
		btnBuilding.onClick.RemoveAllListeners ();
		btnClear.onClick.RemoveAllListeners ();
		btnLoad.onClick.RemoveAllListeners ();
		btnSave.onClick.RemoveAllListeners ();
		btnClose.onClick.RemoveAllListeners ();
		_library = null;
	}

	private	void OnClickItem(TileBase tilebase) {
		Observer.Instance.uiManager.OpenState ("addnewtile", tilebase);
	}

	private	void OnClickCategory() {
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
		scrollView.Clear();
		int simples = _library.simples.Length;
		for (int s = 0 ; s < simples ; s++) {
			var simple = _library.simples[s];
			AddContent(_library.FindItem(simple.none.item) as TileItem, simple);
		}
		scrollView.Align();
	}

	private	void OnClickComplex() {
		scrollView.Clear();
		int complexs = _library.complexs.Length;
		for (int c = 0 ; c < complexs ; c++) {
			var complex = _library.complexs[c];
			AddContent(_library.FindItem(complex.none.item) as TileItem, complex);
		}
		scrollView.Align();
	}

	private	void OnClickDirection() {
		scrollView.Clear();
		int directs = _library.directions.Length;
		for (int d = 0 ; d < directs ; d++) {
			var direction = _library.directions[d];
			AddContent(_library.FindItem(direction.up.item) as TileItem, direction);
		}
		scrollView.Align();
	}

	private	void OnClickBuilding() {
		scrollView.Clear();
		int build = _library.buildings.Length;
		for (int d = 0 ; d < build ; d++) {
			var building = _library.buildings[d];
			AddContent(_library.FindItem(building.basic.item) as TileItem, building);
		}
		scrollView.Align();
	}

	private	void OnClickClear() {
		#if UNITY_EDITOR
		if (UnityEditor.EditorUtility.DisplayDialog("clear map info", "really?", "OK", "Cancel")) {
			Observer.Instance.tileManager.ClearMap();
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
