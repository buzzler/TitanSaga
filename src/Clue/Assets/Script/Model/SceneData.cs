using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public	class SceneData {
	public	string			title;
	public	string			background;
	public	ActorData[]		actors;
	public	DialogData[]	sequences;

	public	SceneData() {
		title = string.Empty;
		background = string.Empty;
		actors = new ActorData[0];
		sequences = new DialogData[0];
	}

	public	ActorData GetActor(string name) {
		for (int i = actors.Length - 1; i >= 0; i--) {
			if (actors [i].name == name)
				return actors [i];
		}
		return null;
	}
}

[Serializable]
public	class DialogData {
	public	SceneCommand command;
	public	string actor;
	public	string position;
	public	string emotion;
	public	string dialog;
}

[Serializable]
public	class ActorData {
	public	string name;
	public	string asset;
	public	string label;
}

public	enum SceneCommand {
	NONE = 0,
	ACTOR_CLEAR = 1
}
