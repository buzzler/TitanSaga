using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class GlobalDatabase : EditorWindow {
	[MenuItem("HyperLuna/GlobalDatabase")]
	private	static void Open() {
		var window = CreateInstance<GlobalDatabase> ();
		window.ExternOpen (Path.Combine(Application.dataPath, "Scene/Resources/Global.json"));
		window.Show ();
	}

	private	string		_path;
	private	GlobalInfo	_data;

	private	bool _showSuspect;
	private	bool _showWeapon;
	private	bool _showRoom;
	private	bool _showEvidence;
	private string[]					_roomsAry;
	private Dictionary<string, string>	_roomsDic;

	public	GlobalDatabase() {
		_path = null;
		_data = null;
		_showSuspect = true;
		_showWeapon = true;
		_showRoom = true;
		_showEvidence = true;
		_roomsAry = new string[0];
		_roomsDic = new Dictionary<string, string> ();
	}

	void OnGUI() {
		try {
			DrawToolbar ();
			GUILayout.Space (2);
			DrawSuspectInfo ();
			GUILayout.Space (2);
			DrawWeaponInfo ();
			GUILayout.Space (2);
			DrawRoomInfo ();
			GUILayout.Space (2);
			DrawEvidenceInfo ();
		} catch (System.Exception e) {
			Debug.LogWarning (e.Message);
		}
	}

	private	void DrawToolbar() {
		GUILayout.BeginHorizontal (EditorStyles.toolbar);
		if (GUILayout.Button ("New", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickNew ();
		if (GUILayout.Button ("Open...", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickOpen ();
		if (!string.IsNullOrEmpty (_path))
		if (GUILayout.Button ("Save", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickSave ();
		if (_data != null)
		if (GUILayout.Button ("Save As...", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickSaveAs ();
		GUILayout.FlexibleSpace ();
		if (_data != null)
		if (GUILayout.Button ("Rescan", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickRescan ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawSuspectInfo() {
		if (_data == null)
			return;

		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showSuspect = GUILayout.Toggle (_showSuspect, "suspect");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddSuspect ();
		GUILayout.EndHorizontal ();
		GUILayout.BeginVertical ();
		if (_showSuspect) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveSuspect (-1);
			GUILayout.Button ("name", EditorStyles.miniButton);
			GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.EndHorizontal ();

			for (int i = 0; i < _data.suspects.Length; i++) {
				var suspect = _data.suspects [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveSuspect (i);
				suspect.name = GUILayout.TextField (suspect.name);
				suspect.id = GUILayout.TextField (suspect.id, GUILayout.Width (90));
				GUILayout.EndHorizontal ();
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawWeaponInfo() {
		if (_data == null)
			return;

		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showWeapon = GUILayout.Toggle (_showWeapon, "weapon");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddWeapon ();
		GUILayout.EndHorizontal ();
		GUILayout.BeginVertical ();
		if (_showWeapon) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveWeapon (-1);
			GUILayout.Button ("name", EditorStyles.miniButton);
			GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.EndHorizontal ();

			for (int i = 0 ; i< _data.weapons.Length ; i++) {
				var weapon = _data.weapons [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveWeapon (i);
				weapon.name = GUILayout.TextField (weapon.name);
				weapon.id = GUILayout.TextField (weapon.id, GUILayout.Width (90));
				GUILayout.EndHorizontal ();
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawRoomInfo() {
		if (_data == null)
			return;

		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showRoom = GUILayout.Toggle (_showRoom, "room");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddRoom ();
		GUILayout.EndHorizontal ();
		GUILayout.BeginVertical ();
		if (_showRoom) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveRoom (-1);
			GUILayout.Button ("name", EditorStyles.miniButton);
			GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.EndHorizontal ();
			for (int i = 0 ; i < _data.rooms.Length ; i++) {
				var room = _data.rooms [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveRoom (i);

				var oldName = room.name;
				var newName = GUILayout.TextField (oldName);
				if (oldName != newName) {
					room.name = newName;
					UpdateData ();
				}
				oldName = room.id;
				newName = GUILayout.TextField (oldName, GUILayout.Width (90));
				if (oldName != newName) {
					room.id = newName;
					UpdateData ();
				}
				GUILayout.EndHorizontal ();
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawEvidenceInfo() {
		if (_data == null)
			return;

		if (_roomsAry.Length != _roomsDic.Count)
			UpdateRoom();

		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showEvidence = GUILayout.Toggle (_showEvidence, "evidence");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddEvidence ();
		GUILayout.EndHorizontal ();
		GUILayout.BeginVertical ();
		if (_showEvidence) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveEvidence (-1);
			GUILayout.Button ("name", EditorStyles.miniButton);
			GUILayout.Button ("room", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.EndHorizontal ();

			for (int i = 0 ; i < _data.evidences.Length ; i++) {
				var evidence = _data.evidences [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveEvidence (i);
				evidence.name = GUILayout.TextField (evidence.name);
				var oldIndex = _roomsDic.ContainsKey (evidence.room) ? ArrayUtility.IndexOf<string> (_roomsAry, _roomsDic [evidence.room]) : -1;
				var newIndex = EditorGUILayout.Popup (oldIndex, _roomsAry, GUILayout.Width (90));
				if (oldIndex != newIndex)
					evidence.room = _data.GetRoomByName (_roomsAry [newIndex]).id;
				evidence.id = GUILayout.TextField (evidence.id, GUILayout.Width (90));
				GUILayout.EndHorizontal ();
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	public	void OnClickNew() {
		bool clear = _data == null;
		if (!clear)
			clear = EditorUtility.DisplayDialog ("Confirm", "All global infomation will be lost.\nIs it alright?", "Ok", "Cancel");

		if (clear) {
			_path = null;
			_data = new GlobalInfo ();
			UpdateData ();
		}
	}

	public	void OnClickOpen() {
		string path = EditorUtility.OpenFilePanel ("Open Global Info", Path.Combine(Application.dataPath, "Scene/Resources"), "json");
		if (string.IsNullOrEmpty (path))
			return;
		if (!File.Exists (path))
			return;
		bool clear = _data == null;
		if (!clear)
			clear = EditorUtility.DisplayDialog ("Confirm", "All global infomation will be lost.\nIs it alright?", "Ok", "Cancel");

		if (clear) {
			ExternOpen (path);
		}
	}

	public	void ExternOpen(string path) {
		_path = path;
		_data = GlobalInfo.LoadFromJson (path);
		UpdateData ();
	}

	public	void OnClickSave() {
		File.WriteAllText (_path, JsonUtility.ToJson (_data), System.Text.Encoding.UTF8);
		AssetDatabase.Refresh ();
	}

	public	void OnClickSaveAs() {
		string filepath = null;
		if (string.IsNullOrEmpty (_path)) {
			filepath = EditorUtility.SaveFilePanel ("Save Global Info", Path.Combine(Application.dataPath, "Scene/Resources"), "", "json");
		} else {
			var dir = Path.GetDirectoryName (_path);
			var filename = Path.GetFileNameWithoutExtension (_path);
			filepath = EditorUtility.SaveFilePanel ("Save Global Info", dir, filename, "json");
		}
		if (string.IsNullOrEmpty (filepath))
			return;
		_path = filepath;
		OnClickSave ();
	}

	public	void OnClickRescan() {
		UpdateRoom ();
	}

	public	void OnClickAddSuspect() {
		List<SuspectInfo> list = null;
		if (_data.suspects == null)
			list = new List<SuspectInfo> ();
		else
			list = new List<SuspectInfo> (_data.suspects);
		list.Add (new SuspectInfo ());
		_data.suspects = list.ToArray ();
	}

	public	void OnClickRemoveSuspect(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Suspects", "This will remove all suspects. Is it alright?", "Ok", "Cancel"))
				_data.suspects = new SuspectInfo[0];
		} else {
			ArrayUtility.RemoveAt<SuspectInfo> (ref _data.suspects, index);
		}
	}

	public	void OnClickAddWeapon() {
		List<WeaponInfo> list = null;
		if (_data.weapons == null)
			list = new List<WeaponInfo> ();
		else
			list = new List<WeaponInfo> (_data.weapons);
		list.Add (new WeaponInfo ());
		_data.weapons = list.ToArray ();
	}

	public	void OnClickRemoveWeapon(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Weapons", "This will remove all weapons. Is it alright?", "Ok", "Cancel"))
				_data.weapons = new WeaponInfo[0];
		} else {
			ArrayUtility.RemoveAt<WeaponInfo> (ref _data.weapons, index);
		}
	}

	public	void OnClickAddRoom() {
		List<RoomInfo> list = null;
		if (_data.rooms == null)
			list = new List<RoomInfo> ();
		else
			list = new List<RoomInfo> (_data.rooms);
		list.Add (new RoomInfo ());
		_data.rooms = list.ToArray ();
	}

	public	void OnClickRemoveRoom(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Rooms", "This will remove all rooms. Is it alright?", "Ok", "Cancel"))
				_data.rooms = new RoomInfo[0];
		} else {
			ArrayUtility.RemoveAt<RoomInfo> (ref _data.rooms, index);
		}
		UpdateRoom ();
	}

	public	void OnClickAddEvidence() {
		List<EvidenceInfo> list = null;
		if (_data.evidences == null)
			list = new List<EvidenceInfo> ();
		else
			list = new List<EvidenceInfo> (_data.evidences);
		list.Add (new EvidenceInfo ());
		_data.evidences = list.ToArray ();
	}

	public	void OnClickRemoveEvidence(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Evidences", "This will remove all evidences. Is it alright?", "Ok", "Cancel"))
				_data.evidences = new EvidenceInfo[0];
		} else {
			ArrayUtility.RemoveAt<EvidenceInfo> (ref _data.evidences, index);
		}
	}

	private	void UpdateRoom() {
		if (_data == null)
			return;

		_roomsAry = null;
		_roomsDic = new Dictionary<string, string> ();
		var list = new List<string> ();
		for (var i = _data.rooms.Length - 1; i >= 0; i--) {
			var room = _data.rooms [i];
			list.Add (room.name);
			_roomsDic.Add (room.id, room.name);
		}
		_roomsAry = list.ToArray ();
	}

	private	void UpdateData() {
		UpdateRoom ();
	}
}
