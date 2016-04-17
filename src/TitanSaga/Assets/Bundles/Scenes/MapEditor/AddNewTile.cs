using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TileLib;

public class AddNewTile : MonoBehaviour {
	public	Button btnCancel;
	public	Button btnLocate;
	public	Button btnDone;

	private	TileBase	_base;
	private	TileTerrain _terrain;

	void Awake() {
		btnCancel.onClick.AddListener (OnClickCancel);
		btnLocate.onClick.AddListener (OnClickLocate);
	}

	void OnEnable() {
		var observer = Observer.Instance;
		observer.tileManager.Grid (true);
		var data = observer.uiManager.GetKeptData ();
		if (data == null || data.Length == 0) {
			OnClickCancel ();
			return;
		}

		_base = data [0] as TileBase;
		OnClickLocate ();
	}

	void Update() {
		if (_terrain != null) {
			var observer = Observer.Instance;
			_terrain.SetPosition (observer.cameraManager.GetCurrentTilePosition());
			observer.tileManager.UpdateTerrain (_terrain);
		}
	}

	void OnDestory() {
		btnCancel.onClick.RemoveListener (OnClickCancel);
		btnLocate.onClick.RemoveListener (OnClickLocate);
	}

	private	void OnClickCancel() {
		Observer.Instance.tileManager.RemoveTerrain (_terrain);
		_base = null;
		_terrain = null;

		Observer.Instance.uiManager.OpenState ("debugconsole");
	}

	private	void OnClickLocate() {
		var observer = Observer.Instance;

		if (_terrain != null) {
			observer.tileManager.ConfirmTerrain (_terrain);
			_terrain = null;
		}
		_terrain = new TileTerrain (_base.id, 0f, 0f, 0f);		// change x,y,z to current center of camera
		observer.tileManager.AddTerrain (_terrain);
	}
}
