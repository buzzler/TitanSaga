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
		observer.backgroundCtr.Change (_current.background);
		Next ();
	}

	private	void Next() {
		if (_index >= _current.sequences.Length) {
			observer.dialogCtr.Hide ();
			if (_callback != null)
				_callback.Invoke ();
			return;
		}

		DialogData dd = null;
		do {
			dd = _current.sequences [_index++];

			// command
			if (dd.command == SceneCommand.ACTOR_CLEAR)
				observer.actorCtr.RemoveAll ();

			// actor
			if (!string.IsNullOrEmpty (dd.actor))
				observer.actorCtr.Add (_current.GetActor(dd.actor).asset, dd.position, dd.emotion);

			// dialog
			if (!string.IsNullOrEmpty (dd.dialog)) {
				observer.dialogCtr.SetCallback (Next);
				observer.dialogCtr.Show (_current.GetActor(dd.actor).label, dd.dialog);
			}
		} while (
			(_index < _current.sequences.Length) &&
			(_current.sequences [_index].actor == dd.actor) &&
			(_current.sequences [_index].position == dd.position));
	}
}
