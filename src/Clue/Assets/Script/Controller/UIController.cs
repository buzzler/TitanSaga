using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UIController : Controller {
	private	Transform								canvas;
	private	Dictionary<string, Transform>			currentUI;

	public	UIController(Observer observer) : base (observer) {
		canvas = GameObject.FindObjectOfType<Canvas> ().transform;
		currentUI = new Dictionary<string, Transform> ();
	}

	public	Transform Add(string name) {
		try {
			if (currentUI.ContainsKey (name)) {
				Transform result = currentUI [name];
				if (result != null)
					return result;
				else
					currentUI.Remove(name);
			}

			Transform tr = GameObject.Instantiate (Resources.Load<Transform> (name));
			tr.SetParent (canvas, false);
			currentUI.Add (name, tr);
			return tr;
		} catch (Exception e) {
			Debug.LogError (e.Message);
			return null;
		}
	}

	public	void Remove(string name) {
		if (currentUI.ContainsKey (name)) {
			GameObject.Destroy (currentUI [name].gameObject);
			currentUI.Remove (name);
		}
	}

	public	void RemoveAll() {
		var total = canvas.childCount;
		for (var i = total - 1; i >= 0; i--) {
			GameObject.Destroy (canvas.GetChild (i).gameObject);
		}
		currentUI.Clear ();
	}
}
