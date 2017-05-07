using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GlobalInfo {
	public	SuspectInfo[]	suspects;
	public	WeaponInfo[]	weapons;
	public	RoomInfo[]		rooms;
	public	EvidenceInfo[]	evidences;

	public	GlobalInfo() {
		suspects = new SuspectInfo[0];
		weapons = new WeaponInfo[0];
		rooms = new RoomInfo[0];
		evidences = new EvidenceInfo[0];
	}

	#if UNITY_EDITOR
	public	static GlobalInfo LoadFromJson(string path) {
		if (!System.IO.File.Exists (path))
			return null;
		return JsonUtility.FromJson<GlobalInfo> (System.IO.File.ReadAllText (path));
	}

	public	RoomInfo GetRoomByName(string name) {
		return UnityEditor.ArrayUtility.Find<RoomInfo> (rooms, (RoomInfo room) => {
			return room.name == name;
		});
	}

	public	EvidenceInfo GetEvidenceByName(string name) {
		return UnityEditor.ArrayUtility.Find<EvidenceInfo> (evidences, (EvidenceInfo evidence) => {
			return evidence.name == name;
		});
	}
	#endif
}

[Serializable]
public	class SuspectInfo {
	public	string id;
	public	string name;

	public	SuspectInfo() {
		id = string.Empty;
		name = string.Empty;
	}
}

[Serializable]
public	class WeaponInfo {
	public	string id;
	public	string name;

	public	WeaponInfo() {
		id = string.Empty;
		name = string.Empty;
	}
}

[Serializable]
public class RoomInfo {
	public	string		id;
	public	string		name;

	public	RoomInfo() {
		id = string.Empty;
		name = string.Empty;
	}
}

[Serializable]
public	class EvidenceInfo {
	public	string	id;
	public	string	room;
	public	string	name;

	public	EvidenceInfo() {
		id = string.Empty;
		room = string.Empty;
		name = string.Empty;
	}
}
