using System;
using UnityEngine;

[Serializable]
public class RoomData {
	public	string		name;
	public	Vector2		location;
	public	Evidence[]	evidences;
}

[Serializable]
public	class Evidence {
	public	string		name;
	public	string[]	data;
	public	bool		view;
	public	float		cost;
}
