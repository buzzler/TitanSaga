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
	private	TileObject	_object;
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
		if (_object == null) {
			GenerateTile ();
		}
	}

	void Update() {
		if (_object != null) {
			var pos = _observer.cameraManager.GetCurrentTilePosition ();
			pos.y = _height;
			_object.SetPosition (pos);
			_observer.tileManager.UpdateTileObject (_object);
		}
	}

	void OnDisable() {
		if (_object != null) {
			_observer.tileManager.RemoveTileObject (_object);
		}

		btnUp.onClick.RemoveListener (OnClickUp);
		btnDown.onClick.RemoveListener (OnClickDown);
		btnCW.onClick.RemoveListener (OnClickCW);
		btnCCW.onClick.RemoveListener (OnClickCCW);
		btnDone.onClick.RemoveListener (OnClickCancel);
		btnLocate.onClick.RemoveListener (OnClickLocate);
		_observer = null;
		_base = null;
		_object = null;
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
		_object.face = _face;
		_observer.tileManager.UpdateTileObject (_object);
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
		_object.face = _face;
		_observer.tileManager.UpdateTileObject (_object);
	}

	private	void OnClickCancel() {
		_observer.uiManager.OpenState ("debugconsole");
	}

	private	void OnClickLocate() {
		if (_object != null) {
			if (_observer.tileManager.ConfirmTileObject (_object)) {
				GenerateTile ();
			}
		}
	}

	private	void GenerateTile() {
		var pos = _observer.cameraManager.GetCurrentTilePosition ();

		switch (_base.type) {
		case TileType.Building:
			GenerateUnit (pos);
			break;
		case TileType.Complex:
		case TileType.Direction:
		case TileType.Item:
		case TileType.Simple:
			GenerateTerrain (pos);
			break;
		}
	}

	private	void GenerateUnit(Vector3 posistion) {
		_object = new TileUnit (_base.id, posistion.x, posistion.y, posistion.z);
		_object.face = _face;
		_observer.tileManager.AddTileObject (_object);
	}

	private	void GenerateTerrain(Vector3 posistion) {
		_object = new TileTerrain (_base.id, posistion.x, posistion.y, posistion.z);
		_object.face = _face;
		_observer.tileManager.AddTileObject (_object);
	}
}
