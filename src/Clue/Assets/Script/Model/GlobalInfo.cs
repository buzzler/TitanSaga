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

	public	SuspectInfo GetSuspectById(string id) {
		return UnityEditor.ArrayUtility.Find<SuspectInfo> (suspects, (SuspectInfo suspect) => {
			return suspect.id == id;
		});
	}

	public	SuspectInfo GetSuspectByName(string name) {
		return UnityEditor.ArrayUtility.Find<SuspectInfo> (suspects, (SuspectInfo suspect) => {
			return suspect.name == name;
		});
	}

	public	RoomInfo GetRoomById(string id) {
		return UnityEditor.ArrayUtility.Find<RoomInfo> (rooms, (RoomInfo room) => {
			return room.id == id;
		});
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

	public	EvidenceInfo GetEvidenceById(string id) {
		return UnityEditor.ArrayUtility.Find<EvidenceInfo> (evidences, (EvidenceInfo evidence) => {
			return evidence.id == id;
		});
	}
	#endif
}

[Serializable]
public	class SuspectInfo {
	public	string	id;
	public	string	name;
	public	string	asset;
	public	string	gender;
	public	string	role;

	public	SuspectInfo() {
		id = string.Empty;
		name = string.Empty;
		asset = string.Empty;
		gender = ActorGender.NONE;
		role = Role.NONE;
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
	public	string		asset;

	public	RoomInfo() {
		id = string.Empty;
		name = string.Empty;
		asset = string.Empty;
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
