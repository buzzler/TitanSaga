using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIController : IController {
	private	string									currentBackground;
	private	string									currentUI;
	private	Dictionary<string, CharacterAdapter>	currentCharacters;

	public	void OnInit() {
		System.Console.WriteLine ("UIController.OnInit");
		currentBackground = null;
		currentUI = null;
		currentCharacters = new Dictionary<string, CharacterAdapter> ();
	}

	public	void OnUpdate() {
	}

	public	void SetUI(string name) {
		if (!string.IsNullOrEmpty (currentUI)) {
			SceneManager.UnloadScene (currentUI);
			currentUI = null;
		}
		currentUI = name;
		SceneManager.LoadScene (currentUI, LoadSceneMode.Additive);
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

	public	void ShowDialog() {
	}

	public	void HideDialog() {
	}
}
