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

	public	MansionData() {
		name = string.Empty;
		prologueScenario = string.Empty;
		epilogueScenario = string.Empty;
		rooms = new RoomData[0];
		evidences = new EvidenceData[0];
	}

	#if UNITY_EDITOR
	public	RoomData GetRoomById(string id) {
		return UnityEditor.ArrayUtility.Find<RoomData> (rooms, (RoomData room) => {
			return room.id == id;
		});
	}

	public	EvidenceData GetEvidenceById(string id) {
		return UnityEditor.ArrayUtility.Find<EvidenceData> (evidences, (EvidenceData evidence) => {
			return evidence.id == id;
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
