using UnityEngine;
using System;
using System.Collections.Generic;

public class ActorController : Controller {
	private	const int _MAX_ACTORS = 5;
	private	Dictionary<string, ActorView> _views;

	public	ActorController(Observer observer) : base (observer) {
		_views = new Dictionary<string, ActorView> (_MAX_ACTORS);
	}

	public	void Add(string name, string position, string emotion) {
		if (_views.ContainsKey (name)) {
			var view = _views [name];
			view.SetPosition (position);
			view.SetEmotion (emotion);
		} else {
			var model = GameObject.Instantiate (Resources.Load (name)) as GameObject;
			var view = model.AddComponent<ActorView> ();
			view.Init ();
			view.SetPosition (position);
			view.SetEmotion (emotion);
			_views.Add (name, view);
		}
	}

	public	void OnRemove(string name) {
		if (_views.ContainsKey (name)) {
			var view = _views [name];
			_views.Remove (name);
			GameObject.Destroy (view.gameObject);
		}
	}

	public	void OnRemoveAll() {
		var itr = _views.GetEnumerator ();
		while (itr.MoveNext ()) {
			var view = itr.Current.Value;
			GameObject.Destroy (view.gameObject);
		}
		_views.Clear ();
	}
}