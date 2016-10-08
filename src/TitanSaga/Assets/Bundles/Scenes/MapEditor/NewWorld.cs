using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TileLib;

public class NewWorld : MonoBehaviour {
	public	Button btnCancel;
	public	Button btnOk;
	public	Slider sldWidth;
	public	Slider sldHeight;
	public	Slider sldDepth;

	public	InputField inputWidth;
	public	InputField inputHeight;
	public	InputField inputDepth;
	public	Dropdown ddFill;

	void OnEnable() {
		btnCancel.onClick.AddListener (OnClickCancel);
		btnOk.onClick.AddListener (OnClickOk);
		sldWidth.onValueChanged.AddListener (OnWidthChange);
		sldHeight.onValueChanged.AddListener (OnHeightChange);
		sldDepth.onValueChanged.AddListener (OnDepthChange);
		inputWidth.onValueChanged.AddListener (OnWidthChangeText);
		inputHeight.onValueChanged.AddListener (OnHeightChangeText);
		inputDepth.onValueChanged.AddListener (OnDepthChangeText);

		sldWidth.value = 50;
		sldHeight.value = 7f;
		sldDepth.value = 50;

		var lib = Observer.Instance.tileManager.library;
		ddFill.options = new System.Collections.Generic.List<Dropdown.OptionData> ();
		foreach (var a in lib.perlins.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
		foreach (var a in lib.randoms.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
		foreach (var a in lib.simples.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
		foreach (var a in lib.complexs.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
		foreach (var a in lib.normals.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
		foreach (var a in lib.directions.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
		foreach (var a in lib.walls.Values) {
			ddFill.options.Add (new Dropdown.OptionData(a.id));
		}
	}

	void OnDisable() {
		btnCancel.onClick.RemoveAllListeners ();
		btnOk.onClick.RemoveAllListeners ();
		sldWidth.onValueChanged.RemoveAllListeners ();
		sldHeight.onValueChanged.RemoveAllListeners ();
		sldDepth.onValueChanged.RemoveAllListeners ();
		inputWidth.onValueChanged.RemoveAllListeners ();
		inputHeight.onValueChanged.RemoveAllListeners ();
		inputDepth.onValueChanged.RemoveAllListeners ();
		ddFill.options.Clear ();
	}

	private	void OnClickCancel() {
		Observer.Instance.uiManager.OpenState ("debugconsole");
	}

	private	void OnClickOk() {
		var tileManager = Observer.Instance.tileManager;
		var w = (int)sldWidth.value;
		var h = (int)sldHeight.value;
		var d = (int)sldDepth.value;
		tileManager.ClearMap (w, h , d);

		if (ddFill.value >= 0) {
			var selected = ddFill.options [(ddFill.value)].text;
			var tb = tileManager.library.FindItem (selected);

			int y = 0;
			for (int x = 0 ;  x < w ; x++) {
				for (int z = 0; z < d; z++) {
					var t = new TileTerrain (tb.id, x, y, z);
					tileManager.AddTileObject (t);
					tileManager.ConfirmTileObject (t);
				}
			}
		}
		Observer.Instance.uiManager.OpenState ("debugconsole");
	}

	private	void OnWidthChange(float value) {
		inputWidth.text = value.ToString ();
	}

	private	void OnHeightChange(float value) {
		inputHeight.text = value.ToString ();
	}

	private	void OnDepthChange(float value) {
		inputDepth.text = value.ToString ();
	}

	private	void OnWidthChangeText(string value) {
		sldWidth.value = Mathf.Clamp (float.Parse (value), sldWidth.minValue, sldWidth.maxValue);
	}

	private	void OnHeightChangeText(string value) {
		sldHeight.value = Mathf.Clamp (float.Parse (value), sldHeight.minValue, sldHeight.maxValue);
	}

	private	void OnDepthChangeText(string value) {
		sldDepth.value = Mathf.Clamp (float.Parse (value), sldDepth.minValue, sldDepth.maxValue);
	}

	public	void OnLock() {
		Observer.Instance.cameraManager.Lock ();
	}

	public	void OnUnlock() {
		Observer.Instance.cameraManager.Unlock ();
	}
}
