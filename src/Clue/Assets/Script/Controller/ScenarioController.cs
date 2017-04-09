using UnityEngine;
using System;

public class ScenarioController : Controller {
	private	SceneData _current;
	private	int _index;
	private	bool _paused;

	public	ScenarioController(Observer observer) : base (observer) {
	}

	public	void Play(string bundlePath) {
		var asset = observer.bundleCtr.Load<TextAsset> (bundlePath);
		if (asset == null)
			return;
		var newScene = JsonUtility.FromJson<SceneData> (asset.text);
		if (newScene == null)
			return;

		_index = 0;
		_current = newScene;

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
			return;
		}

		ShotData sd = null;
		do {
			sd = _current.shots [_index++];

			switch (sd.command) {
			case ShotCommand.ACTOR_CLEAR:
				ExecuteActorClear(sd);
				break;
			case ShotCommand.BREAK:
				ExecuteBreak(sd);
				break;
			case ShotCommand.NONE:
				ExecuteDefault(sd, Next);
				break;
			case ShotCommand.SCENE_CHANGE:
				ExecuteSceneChange(sd);
				break;
			}
		} while (
			(!_paused) &&
			(_index < _current.shots.Length) &&
			(_current.shots [_index].actor == sd.actor) &&
			(_current.shots [_index].position == sd.position));
	}

	private	void ExecuteDefault(ShotData data, Action callback = null) {
		if (!string.IsNullOrEmpty (data.actor)) {
			observer.actorCtr.Add (_current.GetActorByName (data.actor).asset, data.position, data.emotion);
		}
		if (!string.IsNullOrEmpty (data.comment)) {
			observer.dialogCtr.SetCallback (callback);
			observer.dialogCtr.Show (_current.GetActorByName (data.actor).label, data.comment);
		} else if (callback != null) {
			callback.Invoke ();
		}
	}

	private	void ExecuteActorClear(ShotData data) {
		observer.actorCtr.RemoveAll ();
		ExecuteDefault (data, Next);
	}

	private	void ExecuteBreak(ShotData data) {
		ExecuteDefault (data);
	}

	private	void ExecuteSceneChange(ShotData data) {
		ExecuteDefault (data, () => {
			this.Play (data.parameter);
		});
	}
}
