using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DialogView : MonoBehaviour {
	public	Action onClick;
	public	Image panel;
	public	Text textSpeaker;
	public	Text textComment;
	public	Text textSystem;

	private string _SKIP = "skip";
	private string _CLICK = "touch";
	private string _speaker = string.Empty;
	private string _comment = string.Empty;
	private	float _timer = 0f;
	private	float _speed = 0f;
	private	int _length = 0;
	private	int _index = 0;

	void Start() {
		textSpeaker.text = string.Empty;
		textComment.text = string.Empty;
		textSystem.text = string.Empty;

		Color c = Color.white;
		c.a = 0f;
		panel.color = c;
	}

	void Update() {
		if (!IsPlaying ()) {
			return;
		}

		if (_index == 0 && _length > 0) {
			Color c = Color.white;
			c.a = 0.5f;
			panel.color = c;
			textSpeaker.text = _speaker;
			textSystem.text = _SKIP;
		}

		_timer += Time.deltaTime;
		if (_timer < _speed) {
			return;
		}

		_timer = 0f;
		_index++;
		textComment.text = _comment.Substring (0, _index);

		if (_index >= _length) {
			textSystem.text = _CLICK;
		}
	}

	public	void Dialog(string speaker, string comment, float speed) {
		panel.color = Color.white;
		textSpeaker.text = speaker;
		textComment.text = string.Empty;
		textSystem.text = _SKIP;

		_speaker = speaker;
		_comment = comment;
		_speed = speed;
		_length = comment.Length;
		_index = 0;
		_timer = 0f;
	}

	public	void Skip() {
		textComment.text = _comment;
		textSystem.text = _CLICK;
		_length = _index;
	}

	public	void SetCallback(Action callback) {
		onClick = callback;
	}
		
	public	bool IsPlaying() {
		return (_index < _length);
	}

	public	void OnClick() {
		if (onClick != null)
			onClick.Invoke ();
	}
}
