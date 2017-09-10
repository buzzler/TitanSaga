using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UIController : Controller {
	private	Transform						_canvas;
	private	Dictionary<string, Transform>	_current;
	private	Dictionary<string, Transform>	_lock;
	private	Stack<BackupState[]>			_backups;

	public	UIController(Observer observer) : base (observer) {
		_canvas = GameObject.FindObjectOfType<Canvas> ().transform;
		_current = new Dictionary<string, Transform> ();
		_lock = new Dictionary<string, Transform> ();
		_backups = new Stack<BackupState[]> ();
	}

	public	bool Contains(string name) {
		return (!string.IsNullOrEmpty (name)) && _current.ContainsKey (name);
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
			tr.SetAsFirstSibling();
			_current.Add (name, tr);
			if (locking)
				_lock.Add(name, tr);
			return tr;
		} catch (Exception e) {
			Debug.LogError (e.Message);
			return null;
		}
	}

	public	Transform Show(string name) {
		if (_current.ContainsKey (name)) {
			var child = _current [name];
			child.gameObject.SetActive (true);
			return child;
		}
		return null;
	}

	public	void ShowAll(bool force = false) {
		ActiveAll (true, force);
	}

	public	Transform Hide(string name) {
		if (_current.ContainsKey (name)) {
			var child = _current [name];
			child.gameObject.SetActive (false);
			return child;
		}
		return null;
	}

	public	void HideAll(bool force = false) {
		ActiveAll (false, force);
	}

	private	void ActiveAll(bool value, bool force = false) {
		var itr = _current.GetEnumerator ();
		while (itr.MoveNext ()) {
			var locked = _lock.ContainsKey (itr.Current.Key);
			var change = false;
			if (locked) {
				if (force)
					change = true;
			} else {
				change = true;
			}

			if (change)
				itr.Current.Value.gameObject.SetActive (value);
		}
	}

	public	Transform Change(string name) {
		if (string.IsNullOrEmpty (name)) {
			RemoveAll ();
			return null;
		}

		RemoveAll ();
		if (Contains (name)) {
			return Show (name);
		}
		return Add (name);
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
		var list = new List<string> ();
		foreach (string name in _current.Keys) {
			list.Add (name);
		}
		for (int i = list.Count - 1; i >= 0; i--) {
			Remove (list [i], force);
		}
	}

	public	void Backup() {
		var list = new List<BackupState> ();
		var itr = _current.GetEnumerator ();
		while (itr.MoveNext ()) {
			var tr = itr.Current.Value;
			var key = itr.Current.Key;
			var state = new BackupState ();
			state.visible = tr.gameObject.activeSelf;
			state.name = key;
			state.locked = _lock.ContainsKey (key);
			list.Add (state);

			Debug.LogFormat ("ui {0} backup", key);
		}
		_backups.Push (list.ToArray ());
	}

	public	void Restore() {
		if (_backups == null)
			return;
		if (_backups.Count == 0)
			return;
		var backup = _backups.Pop ();
		if (backup == null)
			return;

		RemoveAll ();
		for (int i = 0; i < backup.Length; i++) {
			var state = backup [i];
			Add (state.name, state.locked);
			if (!state.visible)
				Hide (state.name);

			Debug.LogFormat ("ui {0} restore", state.name);
		}
	}

	class BackupState {
		public	bool	visible;
		public	string	name;
		public	bool	locked;
	}
}
