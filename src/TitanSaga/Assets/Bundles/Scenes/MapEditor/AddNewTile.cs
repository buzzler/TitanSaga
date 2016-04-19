using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TileLib;

public class AddNewTile : MonoBehaviour {
	public	Button	btnUp;
	public	Button	btnDown;
	public	Button	btnCW;
	public	Button	btnCCW;
	public	Button	btnDone;
	public	Button	btnLocate;

	private	Observer	_observer;
	private	TileBase	_base;
	private	TileTerrain _terrain;
	private	float		_height;
	private	TileFace	_face;

	void OnEnable() {
		btnUp.onClick.AddListener (OnClickUp);
		btnDown.onClick.AddListener (OnClickDown);
		btnCW.onClick.AddListener (OnClickCW);
		btnCCW.onClick.AddListener (OnClickCCW);
		btnDone.onClick.AddListener (OnClickCancel);
		btnLocate.onClick.AddListener (OnClickLocate);

		_observer = Observer.Instance;
		if (_base == null) {
			var data = _observer.uiManager.GetKeptData ();
			_base = data [0] as TileBase;
			_height = 0;
			_face = TileFace.Up;
		}
		if (_terrain == null) {
			GenerateTerrain ();
		}
	}

	void Update() {
		if (_terrain != null) {
			var pos = _observer.cameraManager.GetCurrentTilePosition ();
			pos.y = _height;
			_terrain.SetPosition (pos);
			_observer.tileManager.UpdateTerrain (_terrain);
		}
	}

	void OnDisable() {
		if (_terrain != null) {
			_observer.tileManager.RemoveTerrain (_terrain);
		}

		btnUp.onClick.RemoveListener (OnClickUp);
		btnDown.onClick.RemoveListener (OnClickDown);
		btnCW.onClick.RemoveListener (OnClickCW);
		btnCCW.onClick.RemoveListener (OnClickCCW);
		btnDone.onClick.RemoveListener (OnClickCancel);
		btnLocate.onClick.RemoveListener (OnClickLocate);
		_observer = null;
		_base = null;
		_terrain = null;
	}

	private	void OnClickUp() {
		_height = Mathf.Clamp (_height + 1, 0, _observer.tileManager.height - 1);
	}

	private	void OnClickDown() {
		_height = Mathf.Clamp (_height - 1, 0, _observer.tileManager.height - 1);
	}

	private	void OnClickCW() {
		switch (_face) {
		case TileFace.Up:
			_face = TileFace.Right;	break;
		case TileFace.Right:
			_face = TileFace.Down;	break;
		case TileFace.Down:
			_face = TileFace.Left;	break;
		case TileFace.Left:
			_face = TileFace.Up;	break;
		}
		_terrain.face = _face;
		_observer.tileManager.UpdateTerrain (_terrain, true, true);
	}

	private	void OnClickCCW() {
		switch (_face) {
		case TileFace.Up:
			_face = TileFace.Left;	break;
		case TileFace.Left:
			_face = TileFace.Down;	break;
		case TileFace.Down:
			_face = TileFace.Right;	break;
		case TileFace.Right:
			_face = TileFace.Up;	break;
		}
		_terrain.face = _face;
		_observer.tileManager.UpdateTerrain (_terrain, true, true);
	}

	private	void OnClickCancel() {
		_observer.uiManager.OpenState ("debugconsole");
	}

	private	void OnClickLocate() {
		if (_terrain != null) {
			if (_observer.tileManager.ConfirmTerrain (_terrain)) {
				GenerateTerrain ();
			}
		}
	}

	private	void GenerateTerrain() {
		var pos = _observer.cameraManager.GetCurrentTilePosition ();
		_terrain = new TileTerrain (_base.id, pos.x, pos.y, pos.z);
		_terrain.face = _face;
		_observer.tileManager.AddTerrain (_terrain);
	}
}
