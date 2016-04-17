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
	public	Button				btnSave;
	public	Button				btnClose;

	private	TileLibrary			_library;

	void Awake () {
		if (btnCategory == null || btnSave == null || btnClose == null) {
			throw new MissingComponentException();
		}
		btnCategory.onClick.AddListener(OnClickCategory);
		btnSimple.onClick.AddListener(OnClickSimple);
		btnComplex.onClick.AddListener(OnClickComplex);
		btnDirection.onClick.AddListener(OnClickDirection);
		btnSave.onClick.AddListener(OnClickSave);
		btnClose.onClick.AddListener(OnClickClose);
	}

	void Start() {
		_library = Observer.Instance.tileManager.config.library;
	}

	void OnEnable() {
		Observer.Instance.tileManager.Grid (false);
	}

	void OnDestroy() {
		btnCategory.onClick.RemoveAllListeners();
		btnSimple.onClick.RemoveAllListeners();
		btnComplex.onClick.RemoveAllListeners();
		btnDirection.onClick.RemoveAllListeners();
		btnSave.onClick.RemoveAllListeners();
		btnClose.onClick.RemoveAllListeners();
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

	private	void OnClickSave() {
		
	}

	private	void OnClickClose() {
		Observer.Instance.uiManager.CloseAllState();
	}

	private	void AddContent(TileItem item, TileBase tilebase) {
		var t = Instantiate<RectTransform>(scrollPrefab);
		t.GetComponent<TerrainItem>().SetItem(item, tilebase);
		scrollView.AddItem(t);
	}
}
