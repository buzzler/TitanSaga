using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public	class SceneData {
	public	string		title;
	public	string		ui;
	public	string		background;
	public	ShotData[]	shots;

	public	SceneData() {
		title = string.Empty;
		ui = string.Empty;
		background = string.Empty;
		shots = new ShotData[0];
	}
}

[Serializable]
public	class ShotData {
	public	ShotCommand command;
	public	string role;
	public	string position;
	public	string emotion;
	public	string comment;
	public	string parameter;

	public	ShotData() {
		command = ShotCommand.NONE;
		role = string.Empty;
		position = string.Empty;
		emotion = string.Empty;
		comment = string.Empty;
		parameter = string.Empty;
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
	SCENARIO_CHANGE_SUSPECT,
	SCENARIO_CHANGE_EVIDENCE,
	BREAK
}
