using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TileLib;

public class DebugConsole : MonoBehaviour {

	public	RectTransform	scrollView;
	public	Button			btnTitle;
	public	Button			btnSave;
	public	Button			btnClose;

	private	TileLibrary		_library;

	void Awake () {
		if (btnTitle == null || btnSave == null || btnClose == null) {
			throw new MissingComponentException();
		}
		btnTitle.onClick.AddListener(OnClickTitle);
		btnSave.onClick.AddListener(OnClickSave);
		btnClose.onClick.AddListener(OnClickClose);
	}

	void Start() {
		_library = Observer.Instance.tileManager.config.library;
		int total = _library.categories.Length;
		for (var i = 0 ; i < total ; i++) {
			Debug.Log(_library.categories[i].name);
		}
		total = _library.simples.Length;
		for (var i = 0 ; i < total ; i++) {
			Debug.Log(_library.simples[i]);
		}
		total = _library.complexs.Length;
		for (var i = 0 ; i < total ; i++) {
			Debug.Log(_library.complexs[i]);
		}
		total = _library.directions.Length;
		for (var i = 0 ; i < total ; i++) {
			Debug.Log(_library.directions[i]);
		}
	}

	void OnDestroy() {
		btnTitle.onClick.RemoveAllListeners();
		btnSave.onClick.RemoveAllListeners();
		btnClose.onClick.RemoveAllListeners();
	}

	private	void OnClickTitle() {
		
	}

	private	void OnClickSave() {
		
	}

	private	void OnClickClose() {
		Observer.Instance.uiManager.CloseAllState();
	}
}
