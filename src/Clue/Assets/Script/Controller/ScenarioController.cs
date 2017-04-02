using UnityEngine;
using System;

public class ScenarioController : Controller {
	private	SceneData _current;
	private	int _index;
	private	bool _paused;
	private	Action _callback;

	public	ScenarioController(Observer observer) : base (observer) {
	}

	public	void Play(string bundlePath, Action callback = null) {
		var asset = observer.bundleCtr.Load<TextAsset> (bundlePath);
		if (asset == null)
			return;
		var newScene = JsonUtility.FromJson<SceneData> (asset.text);
		if (newScene == null)
			return;

		_index = 0;
		_current = newScene;
		_callback = callback;

		var containBackground = observer.backgroundCtr.Contains (_current.background);
		var containUI = observer.uiCtr.Contains (_current.ui);

		observer.faderCtr.FadeOut (OnFadeOut);
	}

	public	void Resume() {
		if (IsPaused)
			_paused = false;
		Next ();
	}

	public	bool IsPaused {
		get {
			if (_current != null)
			if (_index < _current.shots.Length)
			if (_paused)
				return true;
			return false;
		}
	}

	private	void OnFadeOut() {
		observer.uiCtr.Change (_current.ui);
		observer.actorCtr.RemoveAll ();
		observer.dialogCtr.Hide ();
		observer.backgroundCtr.Change (_current.background);
		observer.faderCtr.FadeIn (OnFadeIn);
	}

	private	void OnFadeIn() {
		Next ();
	}

	private	void Next() {
		if (IsPaused)
			return;
		if (_index >= _current.shots.Length) {
			observer.dialogCtr.Hide ();
			if (_callback != null)
				_callback.Invoke ();
			return;
		}

		ShotData sd = null;
		do {
			sd = _current.shots [_index++];

			// command
			if (sd.command == ShotCommand.ACTOR_CLEAR)
				observer.actorCtr.RemoveAll ();
			else if (sd.command == ShotCommand.BREAK)
				_paused = true;

			// actor
			if (!string.IsNullOrEmpty (sd.actor))
				observer.actorCtr.Add (_current.GetActorByName(sd.actor).asset, sd.position, sd.emotion);

			// dialog
			if (!string.IsNullOrEmpty (sd.comment)) {
				observer.dialogCtr.SetCallback (Next);
				observer.dialogCtr.Show (_current.GetActorByName(sd.actor).label, sd.comment);
			}
		} while (
			(!_paused) &&
			(_index < _current.shots.Length) &&
			(_current.shots [_index].actor == sd.actor) &&
			(_current.shots [_index].position == sd.position));
	}
}
