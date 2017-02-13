using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class UIController : IController {
	private	Transform								canvas;
	private	string									currentBackground;
	private	Dictionary<string, Transform>			currentUI;
	private	Dictionary<string, CharacterAdapter>	currentCharacters;

	public	void OnInit() {
		System.Console.WriteLine ("UIController.OnInit");
		canvas = GameObject.FindObjectOfType<Canvas> ().transform;
		currentBackground = null;
		currentUI = new Dictionary<string, Transform> ();
		currentCharacters = new Dictionary<string, CharacterAdapter> ();
	}

	public	void OnUpdate() {
	}

	public	Transform AddUI(string name, bool flush = false) {
		try {
			if (flush) {
				var total = canvas.childCount;
				for (var i = total - 1; i >= 0; i--) {
					GameObject.DestroyImmediate (canvas.GetChild (i).gameObject);
				}
				currentUI.Clear ();
			} else if (currentUI.ContainsKey (name)) {
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

	public	void RemoveUI(string name) {
		if (currentUI.ContainsKey (name)) {
			GameObject.DestroyImmediate (currentUI [name].gameObject);
			currentUI.Remove (name);
		}
	}

	public	void SetBackground(string name) {
		if (!string.IsNullOrEmpty (currentBackground)) {
			SceneManager.UnloadScene (currentBackground);
			currentBackground = null;
		}
		currentBackground = name;
		SceneManager.LoadScene (currentBackground, LoadSceneMode.Additive);
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
		var tr = AddUI ("Dialog");
		var view = tr.GetComponent<DialogView> ();
		view.SetDialog (speaker, comment, speed);
	}

	public	void HideDialog() {
		RemoveUI ("Dialog");
	}
}
