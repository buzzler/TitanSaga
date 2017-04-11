using System;
using UnityEngine;

[Serializable]
public	class MansionData {
	public	string		name;
	public	RoomData[]	rooms;

	public	MansionData() {
		name = string.Empty;
		rooms = new RoomData[0];
	}

	public	RoomData GetRoomByName(string name) {
		for (int i = rooms.Length - 1; i >= 0; i--) {
			if (rooms [i].name == name)
				return rooms [i];
		}
		return null;
	}
}

[Serializable]
public class RoomData {
	public	string			name;
	public	Vector2			location;
	public	EvidenceData[]	evidences;

	public	RoomData() {
		name = string.Empty;
		location = Vector2.zero;
		evidences = new EvidenceData[0];
	}

	public	EvidenceData GetEvidenceByName(string name) {
		for (int i = evidences.Length - 1; i >= 0; i--) {
			if (evidences [i].name == name)
				return evidences [i];
		}
		return null;
	}
}

[Serializable]
public	class EvidenceData {
	public	string		name;
	public	string[]	data;
	public	bool		view;
	public	float		cost;

	public	EvidenceData() {
		name = string.Empty;
		data = new string[0];
		view = false;
		cost = 0f;
	}
}
