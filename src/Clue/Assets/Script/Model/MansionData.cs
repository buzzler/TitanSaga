using System;
using UnityEngine;

[Serializable]
public	class MansionData {
	public	string			name;
	public	RoomData[]		rooms;
	public	EvidenceData[]	evidences;

	public	MansionData() {
		name = string.Empty;
		rooms = new RoomData[0];
		evidences = new EvidenceData[0];
	}

	#if UNITY_EDITOR
	public	RoomData GetRoomById(string id) {
		return UnityEditor.ArrayUtility.Find<RoomData> (rooms, (RoomData room) => {
			return room.id == id;
		});
	}

	public	RoomData GetRoomByName(string name) {
		return UnityEditor.ArrayUtility.Find<RoomData> (rooms, (RoomData room) => {
			return room.name == name;
		});
	}

	public	EvidenceData GetEvidenceById(string id) {
		return UnityEditor.ArrayUtility.Find<EvidenceData> (evidences, (EvidenceData evidence) => {
			return evidence.id == id;
		});
	}

	public	EvidenceData GetEvidenceByName(string name) {
		return UnityEditor.ArrayUtility.Find<EvidenceData> (evidences, (EvidenceData evidence) => {
			return evidence.name == name;
		});
	}
	#endif
}

[Serializable]
public class RoomData {
	public	string	id;
	public	string	name;
	public	string	scenario;
	public	Vector2	location;

	public	RoomData() {
		id = string.Empty;
		name = string.Empty;
		location = Vector2.zero;
	}
}

[Serializable]
public	class EvidenceData {
	public	string	id;
	public	string	room;
	public	string	name;
	public	string	data;
	public	bool	view;
	public	float	cost;

	public	EvidenceData() {
		id = string.Empty;
		room = string.Empty;
		name = string.Empty;
		data = string.Empty;
		view = false;
		cost = 0f;
	}
}
