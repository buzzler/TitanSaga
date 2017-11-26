using UnityEngine;
using System.Collections.Generic;

public class BackgroundController : Controller {
	private	const int _CAPACITY = 5;
	private	Transform _group;
	private	Dictionary<string, BackgroundView> _views;
	private	Stack<BackupState[]> _backups;

	public BackgroundController (Observer observer) : base (observer) {
		_group = null;
		_views = new Dictionary<string, BackgroundView> (_CAPACITY);
		_backups = new Stack<BackupState[]> ();
		observer.OnInited += OnInited;
	}

	private	void OnInited() {
		_group = observer.cameraCtr.CreateBackgroundGroup ();
	}

	public	bool Contains(string name) {
		return (!string.IsNullOrEmpty(name)) && _views.ContainsKey (name);
	}

	public	void Add(string name) {
		if (string.IsNullOrEmpty (name))
			return;

		if (!Contains (name)) {
			var info = observer.globalCtr.GetRoom (name);
			var model = observer.bundleCtr.Instantiate<GameObject> ((info.asset));
			model.transform.SetParent (_group);
			var view = model.AddComponent<BackgroundView> ();
			_views.Add (name, view);
		}
	}

	public	void Change(string name) {
		if (Contains (name))
			return;
		RemoveAll ();
		Add (name);
	}

	public	void Remove(string name) {
		if (_views.ContainsKey (name)) {
			var tr = _views [name];
			_views.Remove (name);
			GameObject.Destroy (tr.gameObject);
		}
	}

	public	void RemoveAll() {
		if (_views.Count > 0) {
			_views.Clear ();
			var total = _group.childCount;
			for (int i = 0; i < total; i++) {
				GameObject.Destroy (_group.GetChild (i).gameObject);
			}
		}
	}

	public	void SetInteractivity(bool value) {
		var itr = _views.GetEnumerator ();
		while (itr.MoveNext ()) {
			itr.Current.Value.SetIntectivity (value);
		}
	}

	public	void Backup() {
		var states = new List<BackupState> ();
		var itr = _views.GetEnumerator ();
		while (itr.MoveNext ()) {
			var background = itr.Current.Key;
			var view = itr.Current.Value;
			var state = new BackupState ();
			state.background = background;
			state.interact = view.GetIntectivity ();
			states.Add (state);
		}
		_backups.Push (states.ToArray ());
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
			Add (backup [i].background);
		}
		SetInteractivity (false);
	}

	class BackupState {
		public	bool interact;
		public	string background;
	}
}
