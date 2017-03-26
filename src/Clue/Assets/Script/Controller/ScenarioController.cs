using UnityEngine;
using System;

public class ScenarioController : Controller {
	private	SceneData _current;
	private	int _index;
	private	Action _callback;

	public	ScenarioController(Observer observer) : base (observer) {
	}

	public	void Play(string bundlePath, Action callback = null) {
		var asset = Resources.Load<TextAsset> (bundlePath);
		if (asset == null)
			return;
		_current = JsonUtility.FromJson<SceneData> (asset.text);
		_index = 0;
		if (_current == null)
			return;

		_callback = callback;
		if (observer.backgroundCtr.Contains (_current.background) && observer.uiCtr.Contains (_current.ui)) {
			Next ();
		} else {
			observer.faderCtr.FadeOut (OnFadeOut);
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
		if (_index >= _current.shots.Length) {
			observer.dialogCtr.Hide ();
			if (_callback != null)
				_callback.Invoke ();
			return;
		}

		ShotData dd = null;
		do {
			dd = _current.shots [_index++];

			// command
			if (dd.command == ShotCommand.ACTOR_CLEAR)
				observer.actorCtr.RemoveAll ();

			// actor
			if (!string.IsNullOrEmpty (dd.actor))
				observer.actorCtr.Add (_current.GetActorByName(dd.actor).asset, dd.position, dd.emotion);

			// dialog
			if (!string.IsNullOrEmpty (dd.comment)) {
				observer.dialogCtr.SetCallback (Next);
				observer.dialogCtr.Show (_current.GetActorByName(dd.actor).label, dd.comment);
			}
		} while (
			(_index < _current.shots.Length) &&
			(_current.shots [_index].actor == dd.actor) &&
			(_current.shots [_index].position == dd.position));
	}
}
