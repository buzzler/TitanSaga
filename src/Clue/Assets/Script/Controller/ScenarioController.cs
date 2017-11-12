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
		_paused = false;

		bool fading = false;

		if (_current == null) {
			fading = true;
		} else if (_current.background != newScene.background) {
			if (string.IsNullOrEmpty (newScene.background))
				fading = false;
			else
				fading = true;
		} else {
			if (_current.ui == newScene.ui)
				fading = false;
			else
				fading = true;
		}

		if (fading) {
			_current = newScene;
			observer.faderCtr.FadeOut (OnFadeOut);
		} else {
			_current = newScene;
			observer.uiCtr.Change (_current.ui);
			observer.actorCtr.RemoveAll ();
			observer.dialogCtr.Hide ();
			Next ();
		}
	}

	public	void Resume() {
		if (IsPaused)
			_paused = false;
		else
			return;
		observer.uiCtr.HideAll ();
		Next ();
	}

	public	void Pause() {
		_paused = true;
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

		var suspect = observer.mansionCtr.GetMainSuspect ();
		if (suspect != null) {
			var info = observer.globalCtr.GetSuspect (suspect);
			observer.actorCtr.Add (info.asset, ActorPosition.RIGHT, ActorEmotion.IDLE);
		}
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
			
		ShotData sd = _current.shots [_index++];
		switch (sd.command) {
		case ShotCommand.ACTOR_CLEAR:
			ExecuteActorClear(sd);
			break;
		case ShotCommand.BREAK:
			ExecuteBreak(sd);
			break;
		case ShotCommand.UI_CHANGE:
			ExecuteUIChange(sd);
			break;
		case ShotCommand.UI_CLEAR:
			ExecuteUIClear(sd);
			break;
		case ShotCommand.UI_HIDE:
			ExecuteUIHide(sd);
			break;
		case ShotCommand.UI_SHOW:
			ExecuteUIShow(sd);
			break;
		case ShotCommand.NONE:
			ExecuteDefault(sd, Next);
			break;
		case ShotCommand.SCENE_CHANGE:
			ExecuteSceneChange(sd);
			break;
		case ShotCommand.MANSION_RANDOM:
			ExecuteMansionRandom(sd);
			break;
		case ShotCommand.STATE_PUSH:
			ExecuteStatePush (sd);
			break;
		case ShotCommand.STATE_POP:
			ExecuteStatePop (sd);
			break;
		case ShotCommand.SCENARIO_CHANGE_SUSPECT:
			ExecuteScenatioChangeRole (sd);
			break;
		case ShotCommand.SCENARIO_CHANGE_EVIDENCE:
			ExecuteScenatioChangeEvidence (sd);
			break;
		}
	}

	private	void ExecuteDefault(ShotData data, Action callback = null) {
		var suspect = observer.roleCtr.GetSuspectInfo (data.role);
		if (suspect != null)
			observer.actorCtr.Add (suspect.asset, data.position, data.emotion);
		if (!string.IsNullOrEmpty (data.comment)) {
			observer.dialogCtr.SetCallback (callback);
			observer.dialogCtr.Show ((suspect == null) ? "":suspect.name, data.comment);
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
		Pause ();
	}

	private	void ExecuteUIShow(ShotData data) {
		ExecuteDefault (data, () => {
			observer.uiCtr.Show (data.parameter);
		});
	}

	private	void ExecuteUIHide(ShotData data) {
		ExecuteDefault (data, () => {
			observer.uiCtr.Hide (data.parameter);
		});
	}

	private	void ExecuteUIChange(ShotData data) {
		ExecuteDefault (data, () => {
			observer.uiCtr.Change (data.parameter);
		});
	}

	private	void ExecuteUIClear(ShotData data) {
		ExecuteDefault (data, () => {
			observer.uiCtr.RemoveAll ();
		});
	}

	private	void ExecuteSceneChange(ShotData data) {
		ExecuteDefault (data, () => {
			observer.mansionCtr.ShowScenario(data.parameter);
		});
	}

	private	void ExecuteMansionRandom(ShotData data) {
		ExecuteDefault (data, () => {
			observer.mansionCtr.VisitRandom();
		});
	}

	private	void ExecuteStatePush(ShotData data) {
		ExecuteDefault (data, () => {
			observer.stateCtr.Backup();
		});
	}

	private	void ExecuteStatePop(ShotData data) {
		ExecuteDefault (data, () => {
			observer.stateCtr.Restore();
		});
	}

	private	void ExecuteScenatioChangeRole(ShotData data) {
		observer.mansionCtr.SetRoleDataScenario (data.parameter, "No_Evidence");
		ExecuteDefault (data, Next);
	}

	private	void ExecuteScenatioChangeEvidence(ShotData data) {
		observer.mansionCtr.SetEvidenceDataScenario (observer.mansionCtr.GetLastEvidence (), "No_Evidence");
		ExecuteDefault (data, Next);
	}
}
