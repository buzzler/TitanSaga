using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UIController : Controller {
	private	Transform						_canvas;
	private	Dictionary<string, Transform>	_current;

	public	UIController(Observer observer) : base (observer) {
		_canvas = GameObject.FindObjectOfType<Canvas> ().transform;
		_current = new Dictionary<string, Transform> ();
	}

	public	bool Contains(string name) {
		return _current.ContainsKey (name);
	}

	public	Transform Add(string name) {
		try {
			if (_current.ContainsKey (name)) {
				Transform result = _current [name];
				if (result != null)
					return result;
				else
					_current.Remove(name);
			}

			Transform tr = GameObject.Instantiate (Resources.Load<Transform> (name));
			tr.SetParent (_canvas, false);
			_current.Add (name, tr);
			return tr;
		} catch (Exception e) {
			Debug.LogError (e.Message);
			return null;
		}
	}

	public	void Remove(string name) {
		if (_current.ContainsKey (name)) {
			GameObject.Destroy (_current [name].gameObject);
			_current.Remove (name);
		}
	}

	public	void RemoveAll() {
		var total = _canvas.childCount;
		for (var i = total - 1; i >= 0; i--) {
			GameObject.Destroy (_canvas.GetChild (i).gameObject);
		}
		_current.Clear ();
	}
}
