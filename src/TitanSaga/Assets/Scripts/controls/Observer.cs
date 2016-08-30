using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(CameraManager))]
public class Observer : MonoBehaviour {
	private	static Observer	_instance;
	private	TileManager		_tile;
	private	UIManager		_ui;
	private	CameraManager	_camera;
	private	DBMS			_dbms;

	public	static Observer	Instance		{ get { return _instance; } }
	public	TileManager		tileManager		{ get { return _tile; } }
	public	UIManager		uiManager		{ get { return _ui; } }
	public	CameraManager	cameraManager	{ get { return _camera; } }
	public	DBMS dbms						{ get { return _dbms; } }

	void Awake() {
		if (_instance != null) {
			throw new MissingComponentException ();
		}
		_instance = this;
		_tile = GetComponent<TileManager> ();
		_ui = GetComponent<UIManager> ();
		_camera = GetComponent<CameraManager> ();
		_dbms = GetComponent<DBMS> ();
	}
}
