using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MoveView : MonoBehaviour {
	public	Button[] buttons;
	public	Text roomName;
	public	Text[] evidences;
	public	Text suspect;

	public	int current;
	public	int selected;

	public	RoomInfo[] roomInfos;
	public	RoomData[] roomDatas;

	private	Observer _observer;

	void Start() {
		_observer = GameObject.FindObjectOfType<Observer> ();
		var now = _observer.mansionCtr.GetCurrentRoom ();

		roomInfos = _observer.globalCtr.GetAllRoom ();
		if (roomInfos.Length != buttons.Length) {
			Debug.LogError ("RoomInfo does not matched with Button!");
			return;
		}

		var list = new List<RoomData> ();
		for (int i = 0; i < roomInfos.Length; i++) {
			var roomInfo = roomInfos [i];
			list.Add (_observer.mansionCtr.GetRoomData (roomInfo.id));
			var button = buttons [i];
			button.GetComponentInChildren<Text> ().text = roomInfo.name;
			if (roomInfo.id == now) {
				current = i;
			}
		}
		roomDatas = list.ToArray ();
		if (roomDatas.Length != buttons.Length) {
			Debug.LogError ("RoomData does not matched with Button!");
			return;
		}

		if (current >= 0)
			OnClickRoom (current);
	}

	public	void OnClickBack() {
		_observer.uiCtr.Change ("Detective");
	}

	public	void OnClickCrimemap() {
		_observer.stateCtr.Backup ();
		var uv = _observer.uiCtr.Change ("Utility").GetComponent<UtilityView>();
		uv.SetCrimeMap ();
	}

	public	void OnClickNotes() {
		_observer.stateCtr.Backup ();
		var uv = _observer.uiCtr.Change ("Utility").GetComponent<UtilityView>();
		uv.SetNotes ();
	}

	public	void OnClickSuspicions() {
		_observer.stateCtr.Backup ();
		var uv = _observer.uiCtr.Change ("Utility").GetComponent<UtilityView>();
		uv.SetSuspicions ();
	}

	public	void OnClickRoom(int index) {
		if (selected == index && index != current && index >= 0) {
			_observer.mansionCtr.Visit (roomInfos [index].id);
			return;
		}
		selected = index;

		var roomInfo = roomInfos [index];
		var roomData = roomDatas [index];
		var evidenceDatas = _observer.mansionCtr.GetEvidenceDataByRoom (roomInfo.id);
		var suspectDatas = _observer.mansionCtr.GetSuspectDataByRoom (roomInfo.id);

		for (int i = 0; i < buttons.Length; i++) {
			if (i == index)
				buttons [i].GetComponentInChildren<Text> ().text = string.Format ("*\n{0}", roomInfo.name);
			else
				buttons [i].GetComponentInChildren<Text> ().text = roomInfos [i].name;
		}
		roomName.text = string.Format ("[{0}]", roomInfo.name);
		for (int i = 0 ; i < evidences.Length ; i++) {
			if (i < evidenceDatas.Length)
				evidences [i].text = string.Format ("{0} {1}", i + 1, _observer.globalCtr.GetEvidence (evidenceDatas [i].id).name);
			else
				evidences [i].text = string.Empty;
		}

		var sb = new System.Text.StringBuilder ();
		for (int i = 0 ; i< suspectDatas.Length ; i ++) {
			var suspectInfo = _observer.globalCtr.GetSuspect (suspectDatas [i].id);
			if (i == 0)
				sb.Append (suspectInfo.name);
			else
				sb.AppendFormat (", {0}", suspectInfo.name);
		}
		suspect.text = sb.ToString ();
	}
}
