using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public	class SceneData {
	public	string		title;
	public	string		ui;
	public	string		background;
	public	ActorData[]	actors;
	public	ShotData[]	shots;

	public	SceneData() {
		title = string.Empty;
		ui = string.Empty;
		background = string.Empty;
		actors = new ActorData[0];
		shots = new ShotData[0];
	}

	public	ActorData GetActorByName(string name) {
		for (int i = actors.Length - 1; i >= 0; i--) {
			if (actors [i].id == name)
				return actors [i];
		}
		return null;
	}
}

[Serializable]
public	class ShotData {
	public	ShotCommand command;
	public	string actor;
	public	string position;
	public	string emotion;
	public	string comment;
	public	string parameter;

	public	ShotData() {
		command = ShotCommand.NONE;
		actor = string.Empty;
		position = string.Empty;
		emotion = string.Empty;
		comment = string.Empty;
		parameter = string.Empty;
	}
}

[Serializable]
public	class ActorData {
	public	string id;
	public	string asset;

	public	ActorData() {
		id = string.Empty;
		asset = string.Empty;
	}
}

public	enum ShotCommand {
	NONE = 0,
	ACTOR_CLEAR,
	SCENE_CHANGE,
	MANSION_RANDOM,
	UI_SHOW,
	UI_HIDE,
	UI_CHANGE,
	UI_CLEAR,
	STATE_PUSH,
	STATE_POP,
	BREAK
}
