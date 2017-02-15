using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UIController : Controller, IController {
	private	Transform								canvas;
	private	Dictionary<string, Transform>			currentUI;
	private	Dictionary<string, CharacterAdapter>	currentCharacters;

	public	UIController(Observer observer) : base (observer) {
		canvas = GameObject.FindObjectOfType<Canvas> ().transform;
		currentUI = new Dictionary<string, Transform> ();
		currentCharacters = new Dictionary<string, CharacterAdapter> ();
	}

	public	void AttachListener() {
		observer.AddEventListener (Events.UI_ADD, OnAdd);
		observer.AddEventListener (Events.UI_REMOVE, OnRemove);
		observer.AddEventListener (Events.UI_REMOVEALL, OnRemoveAll);
	}

	public	void DetachListener() {
		observer.RemoveEventListener (Events.UI_ADD, OnAdd);
		observer.RemoveEventListener (Events.UI_REMOVE, OnRemove);
		observer.RemoveEventListener (Events.UI_REMOVEALL, OnRemoveAll);
	}

	private	void OnAdd(string value) {
		Add (value);
	}

	private	void OnRemove(string value) {
		Remove (value);
	}

	private void OnRemoveAll() {
		RemoveAll ();
	}

	private	Transform Add(string name) {
		try {
			if (currentUI.ContainsKey (name)) {
				return currentUI [name];
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

	private	void Remove(string name) {
		if (currentUI.ContainsKey (name)) {
			GameObject.DestroyImmediate (currentUI [name].gameObject);
			currentUI.Remove (name);
		}
	}

	private	void RemoveAll() {
		var total = canvas.childCount;
		for (var i = total - 1; i >= 0; i--) {
			GameObject.DestroyImmediate (canvas.GetChild (i).gameObject);
		}
		currentUI.Clear ();
	}

	public	void AddCharacter(string name, ActorPosition position, ActorEmotion emotion) {
		if (currentCharacters.ContainsKey (name)) {
			var adapter = currentCharacters [name];
			adapter.SetPosition (position);
			adapter.SetEmotion (emotion);
		} else {
			var model = GameObject.Instantiate (Resources.Load (name)) as GameObject;
			var adapter = model.AddComponent<CharacterAdapter> ();
			adapter.Init ();
			adapter.SetPosition (position);
			adapter.SetEmotion (emotion);
			currentCharacters.Add (name, adapter);
		}
	}

	public	void RemoveCharacter() {
	}

	public	void RemoveAllCharacter() {
	}

	public	void ShowDialog(string speaker, string comment, float speed = 0.03f) {
		var tr = Add ("Dialog");
		var view = tr.GetComponent<DialogView> ();
		view.SetDialog (speaker, comment, speed);
	}

	public	void HideDialog() {
		Remove ("Dialog");
	}
}
