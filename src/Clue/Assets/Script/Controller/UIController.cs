using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UIController : Controller {
	private	Transform						_canvas;
	private	Dictionary<string, Transform>	_current;
	private	Dictionary<string, Transform>	_lock;

	public	UIController(Observer observer) : base (observer) {
		_canvas = GameObject.FindObjectOfType<Canvas> ().transform;
		_current = new Dictionary<string, Transform> ();
		_lock = new Dictionary<string, Transform> ();
	}

	public	bool Contains(string name) {
		return string.IsNullOrEmpty (name) || _current.ContainsKey (name);
	}

	public	Transform Add(string name, bool locking = false) {
		try {
			if (_current.ContainsKey (name)) {
				Transform result = _current [name];
				if (result != null) {
					return result;
				} else {
					_current.Remove(name);
					if (_lock.ContainsKey(name))
						_lock.Remove(name);
				}
			}
				
			Transform tr = observer.bundleCtr.Instantiate<Transform> (name);
			tr.SetParent (_canvas, false);
			_current.Add (name, tr);
			if (locking)
				_lock.Add(name, tr);
			return tr;
		} catch (Exception e) {
			Debug.LogError (e.Message);
			return null;
		}
	}

	public	void Change(string name) {
		if (Contains (name))
			return;
		RemoveAll ();
		Add (name);
	}

	public	void Remove(string name, bool force = false) {
		if (_current.ContainsKey (name)) {
			var child = _current [name];
			var locked = _lock.ContainsKey (name);
			if (locked) {
				if (force)
					_lock.Remove (name);
				else
					return;
			}
			_current.Remove (name);
			GameObject.Destroy (child.gameObject);
		}
	}

	public	void RemoveAll(bool force = false) {
		foreach (string name in _current.Keys) {
			Remove (name, force);
		}
	}
}
