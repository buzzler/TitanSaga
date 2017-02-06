using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DebugView : MonoBehaviour {
	private	List<GameObject> characterCenter;
	private	List<GameObject> characterLeft;
	private	List<GameObject> characterRight;

	void Start() {
		// auto-add all background to dropdown
		var files = Directory.GetFiles (Application.dataPath, "BG_*.unity", SearchOption.AllDirectories);
		var list = new List<string> ();
		foreach (var file in files) {
			list.Add (Path.GetFileNameWithoutExtension (file));
		}

		if (list.Count > 0) {
			var dd = GameObject.Find ("Dropdown-Background").GetComponent<Dropdown> ();
			dd.AddOptions (list);
			OnChangeBackground (0);
		}

		// auto-add all character to dropdown
		files = Directory.GetFiles (Path.Combine(Application.dataPath, "Art/Character/Resources"), "*.prefab", SearchOption.AllDirectories);
		list = new List<string> ();
		foreach (var file in files) {
			list.Add (Path.GetFileNameWithoutExtension (file));
		}

		if (list.Count > 0) {
			var dd = GameObject.Find ("Dropdown-Character").GetComponent<Dropdown> ();
			dd.AddOptions (list);
		}
	}

	public	void OnChangeBackground(int index) {
		var dd = GameObject.Find ("Dropdown-Background").GetComponent<Dropdown> ();
		var item = dd.options [index];

		if (!string.IsNullOrEmpty (item.text)) {
			var observer = GameObject.FindObjectOfType<Observer> ();
			observer.uc.SetBackground (item.text);
		}
	}

	public	void OnClickApply() {
		var character = GameObject.Find ("Dropdown-Character").GetComponent<Dropdown> ();
		var position = GameObject.Find ("Dropdown-Position").GetComponent<Dropdown> ();
		var emotion = GameObject.Find ("Dropdown-Emotion").GetComponent<Dropdown> ();
		var observer = GameObject.FindObjectOfType<Observer> ();

		observer.uc.AddCharacter (
			character.captionText.text, 
			(ActorPosition)Enum.Parse (typeof(ActorPosition), position.captionText.text, true), 
			(ActorEmotion)Enum.Parse (typeof(ActorEmotion), emotion.captionText.text, true)
		);
	}
}