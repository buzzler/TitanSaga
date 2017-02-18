using UnityEngine;
using System.Collections.Generic;

public class ActorView : MonoBehaviour {
	private	Transform _tr;
	private	Dictionary<string, GameObject> _hash;
	private	string _emotion;
	private	string _position;

	public	void Init() {
		_tr = transform;
		_hash = new Dictionary<string, GameObject> ();
		_emotion = ActorEmotion.NONE;
		_position = ActorPosition.NONE;

		_hash.Add (ActorEmotion.NONE,	_tr.Find ("none").gameObject);
		_hash.Add (ActorEmotion.ANGRY,	_tr.Find ("angry").gameObject);
		_hash.Add (ActorEmotion.IDLE,	_tr.Find ("idle").gameObject);
		_hash.Add (ActorEmotion.SAD,	_tr.Find ("sad").gameObject);
		_hash.Add (ActorEmotion.SHY,	_tr.Find ("shy").gameObject);
		_hash.Add (ActorEmotion.SMILE,	_tr.Find ("smile").gameObject);
		var total = _tr.childCount;
		for (int i = total-1 ; i >= 0 ; i--) {
			_tr.GetChild (i).gameObject.SetActive (false);
		}
	}

	public	void SetEmotion(string emotion) {
		if (emotion == _emotion) {
			return;
		}

		if (_hash.ContainsKey (_emotion)) {
			_hash [_emotion].SetActive (false);
		}

		_emotion = emotion;

		if (_hash.ContainsKey (_emotion)) {
			_hash [_emotion].SetActive (true);
		}
	}

	public	void SetPosition(string position) {
		if (position == _position) {
			return;
		}
			
		_position = position;

		var local = _tr.localPosition;
		switch (position) {
		case ActorPosition.CENTER:
			local.x = 0f;
			break;
		case ActorPosition.LEFT:
			local.x = -2f;
			break;
		case ActorPosition.LEFTMOST:
			local.x = -3f;
			break;
		case ActorPosition.RIGHT:
			local.x = 2f;
			break;
		case ActorPosition.RIGHTMOST:
			local.x = 3f;
			break;
		}
		local.y = -5f;
		_tr.localPosition = local;
	}
}
