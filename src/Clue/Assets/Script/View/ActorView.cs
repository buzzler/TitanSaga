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

		var localNext = _tr.localPosition;
		var localNow = _tr.localPosition;

		localNow.y = -2f;
		if (_position == ActorPosition.NONE) {
			switch(position) {
			case ActorPosition.CENTER:
				localNow.x = 0f;
				localNow.y = -4f;
				break;
			case ActorPosition.LEFT:
				localNow.x = -4f;
				break;
			case ActorPosition.LEFTMOST:
				localNow.x = -5f;
				break;
			case ActorPosition.RIGHT:
				localNow.x = 4f;
				break;
			case ActorPosition.RIGHTMOST:
				localNow.x = 5f;
				break;
			}
		}

		switch (position) {
		case ActorPosition.CENTER:
			localNext.x = 0f;
			break;
		case ActorPosition.LEFT:
			localNext.x = -2f;
			break;
		case ActorPosition.LEFTMOST:
			localNext.x = -3f;
			break;
		case ActorPosition.RIGHT:
			localNext.x = 2f;
			break;
		case ActorPosition.RIGHTMOST:
			localNext.x = 3f;
			break;
		}
		localNext.y = -2f;

		_tr.localPosition = localNow;
		if (LeanTween.isTweening (gameObject))
			LeanTween.cancelAll ();
		LeanTween.moveLocal (gameObject, localNext, 0.2f).setEaseInOutCirc();
		_position = position;
	}

	public	void Remove() {
		var localNext = _tr.localPosition;
		if (localNext.x == 0) {
			localNext.y -= 2f;
		} else if (localNext.x < 0) {
			localNext.x -= 2f;
		} else {
			localNext.x += 2f;
		}
		if (LeanTween.isTweening (gameObject))
			LeanTween.cancelAll ();
		LeanTween.moveLocal (gameObject, localNext, 0.1f).setEaseInCirc ().setOnComplete (OnRemove);
	}

	private	void OnRemove() {
		Destroy (gameObject);
	}
}
