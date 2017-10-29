using System;
using UnityEngine;

[Serializable]
public	class MansionData {
	public	string			name;
	public	string			prologueScenario;
	public	string			epilogueScenario;
	public	string			gameoverScenario;
	public	RoomData[]		rooms;
	public	EvidenceData[]	evidences;
	public	SuspectData[]	suspects;
	public	RoleData[]		roles;

	public	MansionData() {
		name = string.Empty;
		prologueScenario = string.Empty;
		epilogueScenario = string.Empty;
		rooms = new RoomData[0];
		evidences = new EvidenceData[0];
		suspects = new SuspectData[0];
		roles = new RoleData[0];
	}

	#if UNITY_EDITOR
	public	RoomData GetRoomData(string id) {
		return UnityEditor.ArrayUtility.Find<RoomData> (rooms, (RoomData data) => {
			return data.id == id;
		});
	}

	public	EvidenceData GetEvidenceData(string id) {
		return UnityEditor.ArrayUtility.Find<EvidenceData> (evidences, (EvidenceData data) => {
			return data.id == id;
		});
	}

	public	SuspectData GetSuspectData(string id) {
		return UnityEditor.ArrayUtility.Find<SuspectData> (suspects, (SuspectData data) => {
			return data.id == id;
		});
	}

	public	RoleData GetRoleData(string role) {
		return UnityEditor.ArrayUtility.Find<RoleData> (roles, (RoleData data) => {
			return data.role == role;
		});
	}
	#endif
}

[Serializable]
public class RoomData {
	public	string	id;
	public	string	scenario;
	public	bool	available;

	public	RoomData() {
		id = string.Empty;
		scenario = string.Empty;
		available = false;
	}
}

[Serializable]
public	class EvidenceData {
	public	string	id;
	public	string	scenario;
	public	bool	available;

	public	EvidenceData() {
		id = string.Empty;
		scenario = string.Empty;
		available = false;
	}
}

[Serializable]
public	class SuspectData {
	public	string	id;
	public	string	room;
	public	bool	available;

	public	SuspectData() {
		id = string.Empty;
		room = string.Empty;
		available = false;
	}
}

[Serializable]
public	class RoleData {
	public	string role;
	public	string scenario;

	public	RoleData() {
		role = string.Empty;
		scenario = string.Empty;
	}
}