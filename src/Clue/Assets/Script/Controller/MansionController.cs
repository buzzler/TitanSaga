using UnityEngine;
using System.Collections.Generic;

public	class MansionController : Controller {
	private	string							_id;
	private	MansionData						_masion;
	private	RoomData						_room;
	private	Dictionary<string, RoomData>	_dicRoom;
	private	List<string>					_listRoom;
	private	Dictionary<string, EvidenceData>_dicEvidence;

	public	MansionController(Observer observer) : base (observer) {
	}

	public	void Clear() {
		_id = null;
		_masion = null;
		_room = null;
		_dicRoom = null;
		_listRoom = null;
		_dicEvidence = null;
	}

	public	bool Load(string id) {
		try {
			if (_id == id)
				return false;

			var asset = observer.bundleCtr.Load<TextAsset> (id + "/Mansion");
			if (asset == null)
				return false;
			var data = JsonUtility.FromJson<MansionData> (asset.text);
			if (data == null)
				return false;

			_masion = data;
			_id = id;
			_dicEvidence = new Dictionary<string, EvidenceData>();
			_listRoom = new List<string>();
			_dicRoom = new Dictionary<string, RoomData>();
			for (int i = 0 ; i < _masion.evidences.Length ; i++) {
				var evidence = _masion.evidences[i];
				_dicEvidence.Add(evidence.id, evidence);
			}
			for (int i = 0 ; i < _masion.rooms.Length ; i++) {
				var room = _masion.rooms[i];
				_dicRoom.Add(room.id, room);
				_listRoom.Add(room.id);
			}
			return true;
		} catch (System.Exception e) {
			if (Debug.isDebugBuild)
				Debug.LogError (e.Message);
			return false;
		}
	}

	public	void ShowPrologue() {
		if (_masion == null)
			return;
		ShowScenario (_masion.prologueScenario);
	}

	public	void ShowEpilogue() {
		if (_masion == null)
			return;
		ShowScenario (_masion.epilogueScenario);
	}

	public	void VisitRandom() {
		var index = Random.Range (0, _listRoom.Count);
		Visit (_listRoom [index]);
	}

	public	void Visit(string room) {
		if (!_dicRoom.ContainsKey (room))
			return;

		var selected = _dicRoom [room];
		if (_room == selected)
			return;

		_room = selected;
		ShowScenario (_room.scenario);
	}

	public	string GetCurrentRoom() {
		if (_room != null)
			return _room.id;
		else
			return null;
	}

	public	void ShowScenario(string filename) {
		if (string.IsNullOrEmpty (_id))
			return;
		if (string.IsNullOrEmpty (filename))
			return;
		
		observer.scenarioCtr.Play (string.Format("{0}/{1}", _id, filename));
	}

	public	MansionData GetMansionData() {
		return _masion;
	}

	public	EvidenceData GetEvidenceData(string id) {
		if (_masion == null)
			return null;
		return _dicEvidence [id];
	}

	public	RoomData GetRoomData(string id) {
		if (_masion == null)
			return null;
		return _dicRoom [id];
	}

	public	EvidenceData[] GetEvidenceDataByRoom(string room) {
		var result = new List<EvidenceData> ();
		for (int i = 0 ; i < _masion.evidences.Length ; i++) {
			var evidenceData = _masion.evidences [i];
			var evidenceInfo = observer.globalCtr.GetEvidence (evidenceData.id);
			if (evidenceInfo.room == room)
				result.Add (evidenceData);
		}
		return result.ToArray ();
	}

	public	SuspectData[] GetSuspectDataByRoom(string room) {
		var result = new List<SuspectData> ();
		for (int i = 0 ; i < _masion.suspects.Length ; i++) {
			var suspect = _masion.suspects [i];
			if (suspect.room == room)
				result.Add (suspect);
		}
		return result.ToArray ();
	}
}
