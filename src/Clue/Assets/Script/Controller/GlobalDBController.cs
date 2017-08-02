using UnityEngine;
using System.Collections.Generic;

public class GlobalDBController : Controller
{
	private	GlobalInfo _db;
	private Dictionary<string, SuspectInfo> _dicSuspect;
	private	Dictionary<string, WeaponInfo> _dicWeapon;
	private	Dictionary<string, RoomInfo> _dicRoom;
	private	Dictionary<string, EvidenceInfo> _dicEvidence;

	public	GlobalDBController(Observer observer) : base (observer) {
		Init ();
	}

	private	void Init() {
		var json = observer.bundleCtr.Load<TextAsset> ("Global");
		_db = JsonUtility.FromJson<GlobalInfo> (json.text);

		int count = _db.suspects.Length;
		_dicSuspect = new Dictionary<string, SuspectInfo> (count);
		for (int i = 0 ; i < count ; i++) {
			var suspect = _db.suspects [i];
			_dicSuspect.Add (suspect.id, suspect);
		}

		count = _db.weapons.Length;
		_dicWeapon = new Dictionary<string, WeaponInfo> (count);
		for (int i = 0 ; i < count ; i++) {
			var weapon = _db.weapons [i];
			_dicWeapon.Add (weapon.id, weapon);
		}

		count = _db.rooms.Length;
		_dicRoom = new Dictionary<string, RoomInfo> (count);
		for (int i = 0; i < count; i++) {
			var room = _db.rooms [i];
			_dicRoom.Add (room.id, room);
		}

		count = _db.evidences.Length;
		_dicEvidence = new Dictionary<string, EvidenceInfo> (count);
		for (int i = 0; i < count; i++) {
			var evidence = _db.evidences [i];
			_dicEvidence.Add (evidence.id, evidence);
		}
	}

	public	int GetSuspectCount() {
		return _db.suspects.Length;
	}

	public	SuspectInfo GetSuspect(string id) {
		return _dicSuspect [id];
	}

	public	SuspectInfo[] GetAllSuspects() {
		return _db.suspects;
	}

	public	int GetWeaponCount() {
		return _db.weapons.Length;
	}

	public	WeaponInfo GetWeapon(string id) {
		return _dicWeapon [id];
	}

	public	WeaponInfo[]  GetAllWeapon() {
		return _db.weapons;
	}

	public	int GetRoomCount() {
		return _db.rooms.Length;
	}

	public	RoomInfo GetRoom(string id) {
		return _dicRoom [id];
	}

	public	RoomInfo[] GetAllRoom() {
		return _db.rooms;
	}

	public	int GetEvidenceCount() {
		return _db.evidences.Length;
	}

	public	EvidenceInfo GetEvidence(string id) {
		return _dicEvidence [id];
	}

	public	EvidenceInfo[] GetAllEvidence() {
		return _db.evidences;
	}
}
