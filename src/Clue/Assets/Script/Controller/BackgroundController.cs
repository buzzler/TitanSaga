using UnityEngine;
using System.Collections.Generic;

public class BackgroundController : Controller {
	private	const int _CAPACITY = 5;
	private	Transform _group;
	private	Dictionary<string, BackgroundView> _views;

	public BackgroundController (Observer observer) : base (observer) {
		_group = null;
		_views = new Dictionary<string, BackgroundView> (_CAPACITY);
		observer.OnInited += OnInited;
	}

	private	void OnInited() {
		_group = observer.cameraCtr.CreateBackgroundGroup ();
	}

	public	bool Contains(string name) {
		return _views.ContainsKey (name);
	}

	public	void Add(string name) {
		if (!_views.ContainsKey (name)) {
			var model = GameObject.Instantiate (Resources.Load (name)) as GameObject;
			model.transform.SetParent (_group);
			var view = model.AddComponent<BackgroundView> ();
			_views.Add (name, view);
		}
	}

	public	void Change(string name) {
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
}
