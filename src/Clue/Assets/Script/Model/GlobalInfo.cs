using System;
using System.Collections.Generic;

[Serializable]
public class GlobalInfo {
	public	SuspectInfo[]	suspects;
	public	WeaponInfo[]	weapons;
	public	RoomInfo[]		rooms;
	public	EvidenceInfo[]	evidences;
}

[Serializable]
public	class SuspectInfo {
	public	string id;
	public	string name;
}

[Serializable]
public	class WeaponInfo {
	public	string id;
	public	string name;
}

[Serializable]
public class RoomInfo {
	public	string		id;
	public	string		name;
	public	string[]	evidences;

	public	RoomInfo() {
		id = string.Empty;
		name = string.Empty;
		evidences = new string[0];
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
