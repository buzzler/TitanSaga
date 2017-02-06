using UnityEngine;
using System.Collections.Generic;

public class CharacterAdapter : MonoBehaviour {
	private	Transform tr;
	private	Dictionary<ActorEmotion, GameObject> hash;
	private	ActorEmotion currentEmotion;
	private	ActorPosition currentPosition;

	public	void Init() {
		tr = transform;
		hash = new Dictionary<ActorEmotion, GameObject> ();
		currentEmotion = ActorEmotion.NONE;
		currentPosition = ActorPosition.NONE;

		hash.Add (ActorEmotion.NONE, tr.Find ("none").gameObject);
		hash.Add (ActorEmotion.ANGRY, tr.Find ("angry").gameObject);
		hash.Add (ActorEmotion.IDLE, tr.Find ("idle").gameObject);
		hash.Add (ActorEmotion.SAD, tr.Find ("sad").gameObject);
		hash.Add (ActorEmotion.SHY, tr.Find ("shy").gameObject);
		hash.Add (ActorEmotion.SMILE, tr.Find ("smile").gameObject);
		var total = tr.childCount;
		for (int i = total-1 ; i >= 0 ; i--) {
			tr.GetChild (i).gameObject.SetActive (false);
		}
	}

	public	void SetEmotion(ActorEmotion emotion) {
		if (emotion == currentEmotion) {
			return;
		}

		if (hash.ContainsKey (currentEmotion)) {
			hash [currentEmotion].SetActive (false);
		}

		currentEmotion = emotion;

		if (hash.ContainsKey (currentEmotion)) {
			hash [currentEmotion].SetActive (true);
		}
	}

	public	void SetPosition(ActorPosition position) {
		if (position == currentPosition) {
			return;
		}
			
		currentPosition = position;

		var local = tr.localPosition;
		switch (position) {
		case ActorPosition.CENTER:
			local.x = 0f;
			break;
		case ActorPosition.LEFT:
			local.x = -2f;
			break;
		case ActorPosition.RIGHT:
			local.x = 2f;
			break;
		}
		local.y = -5f;
		tr.localPosition = local;
	}
}
