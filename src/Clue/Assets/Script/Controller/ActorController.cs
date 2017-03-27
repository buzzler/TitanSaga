using UnityEngine;
using System.Collections.Generic;

public class ActorController : Controller {
	private	const int _CAPACITY = 5;
	private	Transform _group;
	private	Dictionary<string, ActorView> _views;

	public	ActorController(Observer observer) : base (observer) {
		_group = null;
		_views = new Dictionary<string, ActorView> (_CAPACITY);
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
}