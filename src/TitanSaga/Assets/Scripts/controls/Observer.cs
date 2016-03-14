using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileManager))]
[RequireComponent(typeof(UIManager))]
public class Observer : MonoBehaviour {
	private	static Observer	_instance;
	private	TileManager		_tile;
	private	UIManager		_ui;

	public	static Observer	Instance	{get{return _instance;}}
	public	TileManager		tileManager	{get{return _tile;}}
	public	UIManager		uiManager	{get{return _ui;}}

	void Awake() {
		if (_instance != null) {
			throw new MissingComponentException();
		}
		_instance = this;
		_tile = GetComponent<TileManager>();
		_ui = GetComponent<UIManager>();
	}
}
