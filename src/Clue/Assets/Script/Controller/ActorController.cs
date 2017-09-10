using UnityEngine;
using System.Collections.Generic;

public class ActorController : Controller {
	private	const int _CAPACITY = 5;
	private	Transform _group;
	private	Dictionary<string, ActorView> _views;
	private	Stack<BackupState[]> _backups;

	public	ActorController(Observer observer) : base (observer) {
		_group = null;
		_views = new Dictionary<string, ActorView> (_CAPACITY);
		_backups = new Stack<BackupState[]> ();
		observer.OnInited += OnInited;
	}

	private	void OnInited() {
		_group = observer.cameraCtr.CreateActorGroup ();
	}

	public	void Add(string name, string position, string emotion) {
		if (_views.ContainsKey (name)) {
			var view = _views [name];
			view.SetPosition (position);
			view.SetEmotion (emotion);
		} else {
			var model = observer.bundleCtr.Instantiate<GameObject> (name);
			model.transform.SetParent (_group);
			var view = model.AddComponent<ActorView> ();
			view.Init ();
			view.SetPosition (position);
			view.SetEmotion (emotion);
			_views.Add (name, view);
		}
	}

	public	void Change(string name, string position, string emotion) {
		RemoveAll ();
		Add (name, position, emotion);
	}

	public	void Remove(string name) {
		Debug.Log ("Remove");
		if (_views.ContainsKey (name)) {
			var view = _views [name];
			_views.Remove (name);
			view.Remove ();
		}
	}

	public	void RemoveAll() {
		if (_views.Count > 0) {
			_views.Clear ();
			var total = _group.childCount;
			for (int i = 0; i < total; i++) {
				_group.GetChild (i).GetComponent<ActorView> ().Remove ();
			}
		}
	}

	public	void Show() {
		SetActive (true);
	}

	public	void Hide() {
		SetActive (false);
	}

	private	void SetActive(bool value) {
		var itr = _views.GetEnumerator ();
		while (itr.MoveNext ()) {
			var actor = itr.Current.Value;
			actor.gameObject.SetActive (value);
		}
	}

	public	void Backup() {
		var states = new List<BackupState> ();
		var itr = _views.GetEnumerator ();
		while (itr.MoveNext ()) {
			var actor = itr.Current.Key;
			var view = itr.Current.Value;
			if (!view.gameObject.activeSelf)
				continue;

			var state = new BackupState ();
			state.visible = view.gameObject.activeSelf;
			state.actor = actor;
			state.emotion = view.GetEmotion();
			state.position = view.GetPosition ();
			states.Add (state);

			Debug.LogFormat ("actor {0} backup", state.actor);
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
		for (int i = 0 ; i < backup.Length ; i++) {
			var state = backup [i];
			Add (state.actor, state.position, state.emotion);

			Debug.LogFormat ("actor {0} restore", state.actor);
		}
	}

	class BackupState {
		public	bool visible;
		public	string actor;
		public	string emotion;
		public	string position;
	}
}

