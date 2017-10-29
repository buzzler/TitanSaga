using UnityEngine;
using System.Collections.Generic;

public	class MansionController : Controller {
	private	string							_id;
	private	MansionData						_masion;
	private	Dictionary<string, RoomData>	_dicRoom;
	private	List<string>					_listRoom;
	private	Dictionary<string, EvidenceData>_dicEvidence;
	private	Dictionary<string, SuspectData>	_dicSuspect;
	private Dictionary<string, RoleData>	_dicRole;
	private	RoomData						_lastRoom;
	private	EvidenceData					_lastEvidence;
	private SuspectData						_lastSuspect;

	public	MansionController(Observer observer) : base (observer) {
	}

	public	void Clear() {
		_id = null;
		_masion = null;
		_dicRoom = null;
		_listRoom = null;
		_dicEvidence = null;
		_dicSuspect = null;
		_dicRole = null;
		_lastRoom = null;
		_lastEvidence = null;
		_lastSuspect = null;
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
			_dicSuspect = new Dictionary<string, SuspectData>();
			_dicRole = new Dictionary<string, RoleData>();
			for (int i = 0 ; i < _masion.evidences.Length ; i++) {
				var evidence = _masion.evidences[i];
				_dicEvidence.Add(evidence.id, evidence);
			}
			for (int i = 0 ; i < _masion.rooms.Length ; i++) {
				var room = _masion.rooms[i];
				_dicRoom.Add(room.id, room);
				_listRoom.Add(room.id);
			}
			for (int i = 0 ; i < _masion.suspects.Length ; i++) {
				var suspect = _masion.suspects[i];
				_dicSuspect.Add(suspect.id, suspect);
			}
			for (int i = 0 ; i< _masion.roles.Length ; i++) {
				var role = _masion.roles[i];
				_dicRole.Add(role.role, role);
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
		if (_lastRoom == selected)
			return;

		_lastRoom = selected;
		ShowScenario (_lastRoom.scenario);
	}

	public	string GetLastRoom() {
		if (_lastRoom != null)
			return _lastRoom.id;
		else
			return null;
	}

	public	void SetEvidenceDataScenario(string evidence, string scenario) {
		if (_dicEvidence.ContainsKey (evidence)) {
			_dicEvidence [evidence].scenario = scenario;
			if (Debug.isDebugBuild)
				Debug.LogFormat ("evidence({0}) was re-defined to {1}", evidence, scenario);
		}
	}

	public	void SetRoleDataScenario(string role, string scenario) {
		if (_dicRole.ContainsKey (role)) {
			_dicRole [role].scenario = scenario;
			if (Debug.isDebugBuild)
				Debug.LogFormat ("role({0}) was re-defined to {1}", role, scenario);
		}
	}

	public	string GetMainSuspect() {
		if (_lastRoom == null)
			return null;

		var itr = _dicSuspect.GetEnumerator ();
		while (itr.MoveNext ()) {
			var suspect = itr.Current.Value;
			if (string.IsNullOrEmpty (suspect.room))
				continue;
			if (suspect.room == _lastRoom.id)
				return suspect.id;
		}
		return null;
	}

	public	void TalkSuspect(string suspect) {
		if (!_dicSuspect.ContainsKey (suspect))
			return;

		var suspectData = _dicSuspect [suspect];
		_lastSuspect = suspectData;

		var suspectInfo = observer.globalCtr.GetSuspect (suspectData.id);
		observer.roleCtr.SetRole(suspectInfo);
		// TODO: ScenarioController.Play with suspectInfo.role
		ShowScenario(GetRoleData(suspectInfo.role).scenario);
	}

	public	string GetLastSuspect() {
		if (_lastSuspect != null)
			return _lastSuspect.id;
		else
			return null;
	}

	public	void CheckEvidence(string evidence) {
		if (!_dicEvidence.ContainsKey (evidence))
			return;

		observer.stateCtr.Backup ();

		var selected = _dicEvidence [evidence];
		_lastEvidence = selected;
		ShowScenario (selected.scenario);
	}

	public	string GetLastEvidence() {
		if (_lastEvidence != null)
			return _lastEvidence.id;
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

	public	RoleData GetRoleData(string role) {
		if (_dicRole.ContainsKey (role))
			return _dicRole [role];
		return null;
	}
}
