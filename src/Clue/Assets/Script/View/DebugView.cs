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
		var files = Directory.GetFiles (Path.Combine(Application.dataPath, "Art/Background/Resources"), "*.prefab", SearchOption.AllDirectories);
		var list = new List<string> ();
		foreach (var file in files) {
			list.Add (Path.GetFileNameWithoutExtension (file));
		}

		if (list.Count > 0) {
			var dd = GameObject.Find ("Dropdown-Background").GetComponent<Dropdown> ();
			dd.AddOptions (list);
			OnChangeBackground ();
		}

		// auto-add all character to dropdown
		files = Directory.GetFiles (Path.Combine(Application.dataPath, "Art/Actor/Resources"), "*.prefab", SearchOption.AllDirectories);
		list = new List<string> ();
		foreach (var file in files) {
			list.Add (Path.GetFileNameWithoutExtension (file));
		}

		if (list.Count > 0) {
			var dd = GameObject.Find ("Dropdown-Character").GetComponent<Dropdown> ();
			dd.AddOptions (list);
		}
	}

	public	void OnChangeBackground() {
		var dd = GameObject.Find ("Dropdown-Background").GetComponent<Dropdown> ();
		var item = dd.options [dd.value];

		if (!string.IsNullOrEmpty (item.text)) {
			var observer = GameObject.FindObjectOfType<Observer> ();
			observer.backgroundCtr.Change (item.text);
		}
	}

	public	void OnClickApply() {
		var character = GameObject.Find ("Dropdown-Character").GetComponent<Dropdown> ();
		var position = GameObject.Find ("Dropdown-Position").GetComponent<Dropdown> ();
		var emotion = GameObject.Find ("Dropdown-Emotion").GetComponent<Dropdown> ();
		var observer = GameObject.FindObjectOfType<Observer> ();

		observer.actorCtr.Add(
			character.captionText.text, 
			position.captionText.text,
			emotion.captionText.text
		);
	}

	public	void OnClickSpeak() {
		var speaker = GameObject.Find ("InputField-Speaker").GetComponent<InputField> ();
		var comment = GameObject.Find ("InputField-Comment").GetComponent<InputField> ();
		var observer = GameObject.FindObjectOfType<Observer> ();

		observer.dialogCtr.Show (speaker.text, comment.text);
	}
}