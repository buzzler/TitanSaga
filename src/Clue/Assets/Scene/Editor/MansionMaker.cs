using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class MansionMaker : EditorWindow {
	[MenuItem("HyperLuna/MansionMaker")]
	private	static void Open() {
		var window = CreateInstance<MansionMaker> ();
		window.Show ();
	}

	private	string		_path;
	private	GlobalInfo	_global;
	private	MansionData	_data;
	private	string[]	_scenes;

	private	string[]					_roomsAry;
	private	Dictionary<string, string>	_roomsDic;
	private	string[]					_evidencesAry;
	private	Dictionary<string, string>	_evidencesDic;

	private bool _showRoom;
	private	bool _showEvidence;

	public	MansionMaker() {
		_path = null;
		_global = null;
		_data = null;
		_scenes = new string[0];
		_roomsAry = new string[0];
		_roomsDic = new Dictionary<string, string> ();
		_evidencesAry = new string[0];
		_evidencesDic = new Dictionary<string, string> ();
		_showRoom = true;
		_showEvidence = true;
	}

	void OnGUI() {
		try {
			Verify();
			DrawToolbar ();
			GUILayout.Space (2);
			DrawMansionData ();
			GUILayout.Space (2);
			DrawRoomData ();
			GUILayout.Space (2);
			DrawEvidenceData ();
		} catch (System.Exception e) {
			Debug.LogWarning (e.Message);
		}
	}

	private	void Verify() {
		if (_global == null) {
			EditorUtility.DisplayDialog ("Error", "Can't find Global.json\nYou must open 'Global Database'", "Ok");
			this.Close ();
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

	private	void DrawMansionData() {
		if (_global == null || _data == null)
			return;
		
		GUILayout.BeginHorizontal ();
		GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width(90));
		_data.name = GUILayout.TextField (_data.name);
		GUILayout.EndHorizontal ();
	}

	private	void DrawRoomData() {
		if (_global == null || _data == null)
			return;
		if (_roomsAry.Length != _roomsDic.Count)
			UpdateRoom();
		if (_evidencesAry.Length != _evidencesDic.Count)
			UpdateEvidence ();
		
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showRoom = GUILayout.Toggle (_showRoom, "room");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddRoom ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();

		if (_showRoom) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveRoom (-1);
			GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("scenario", EditorStyles.miniButton);
			GUILayout.Toggle (false, string.Empty, GUILayout.Width (20));
			GUILayout.EndHorizontal ();

			for (int i = 0; i < _data.rooms.Length; i++) {
				var room = _data.rooms [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveRoom (i);
				int oldIndex;
				int newIndex;
				oldIndex = ArrayUtility.IndexOf<string> (_roomsAry, _roomsDic.ContainsKey (room.id) ? _roomsDic [room.id] : string.Empty);
				newIndex = EditorGUILayout.Popup (oldIndex, _roomsAry, GUILayout.Width (90));
				if (oldIndex != newIndex)
					room.id = _global.GetRoomByName (_roomsAry [newIndex]).id;
				oldIndex = ArrayUtility.IndexOf<string> (_scenes, room.scenario);
				newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
				if (oldIndex != newIndex)
					room.scenario = _scenes [newIndex];
				room.available = GUILayout.Toggle (room.available, "", GUILayout.Width (20));
				GUILayout.EndHorizontal ();
			}
		}

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawEvidenceData() {
		if (_global == null || _data == null)
			return;
		
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showEvidence = GUILayout.Toggle (_showEvidence, "evidence");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddEvidence ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();


		if (_showEvidence) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveEvidence (-1);
			GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("scenario", EditorStyles.miniButton);
			GUILayout.Toggle (false, string.Empty, GUILayout.Width (20));
			GUILayout.EndHorizontal ();

			for (int i = 0; i < _data.evidences.Length; i++) {
				var evidence = _data.evidences [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveEvidence (i);
				int oldIndex;
				int newIndex;
				oldIndex = ArrayUtility.IndexOf<string> (_evidencesAry, _evidencesDic.ContainsKey (evidence.id) ? _evidencesDic [evidence.id] : string.Empty);
				newIndex = EditorGUILayout.Popup (oldIndex, _evidencesAry, GUILayout.Width (90));
				if (oldIndex != newIndex)
					evidence.id = _global.GetEvidenceByName (_evidencesAry [newIndex]).id;
				oldIndex = ArrayUtility.IndexOf<string> (_scenes, evidence.scenario);
				newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
				if (oldIndex != newIndex)
					evidence.scenario = _scenes [newIndex];
				evidence.available = GUILayout.Toggle (evidence.available, "", GUILayout.Width (20));
				GUILayout.EndHorizontal ();
			}
		}

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	public	void OnClickNew() {
		bool clear = _data == null;
		if (!clear)
			clear = EditorUtility.DisplayDialog ("Confirm", "All mansion's data will be lost.\nIs it alright?", "Ok", "Cancel");

		if (clear) {
			_path = null;
			_data = new MansionData ();
			UpdateData ();
		}
	}

	public	void OnClickOpen() {
		string path = EditorUtility.OpenFilePanel ("Open Mansion Data", Path.Combine(Application.dataPath, "Scene/Resources"), "json");
		if (string.IsNullOrEmpty (path))
			return;
		if (!File.Exists (path))
			return;
		bool clear = _data == null;
		if (!clear)
			clear = EditorUtility.DisplayDialog ("Confirm", "All mansion's data will be lost.\nIs it alright?", "Ok", "Cancel");

		if (clear) {
			_path = path;
			_data = JsonUtility.FromJson<MansionData> (File.ReadAllText (path));
			UpdateData ();
		}
	}

	public	void OnClickSave() {
		File.WriteAllText (_path, JsonUtility.ToJson (_data), System.Text.Encoding.UTF8);
		AssetDatabase.Refresh ();
	}

	public	void OnClickSaveAs() {
		string filepath = null;
		if (string.IsNullOrEmpty (_path)) {
			filepath = EditorUtility.SaveFilePanel ("Save Mansion Data", Path.Combine(Application.dataPath, "Scene/Resources"), "", "json");
		} else {
			var dir = Path.GetDirectoryName (_path);
			var filename = Path.GetFileNameWithoutExtension (_path);
			filepath = EditorUtility.SaveFilePanel ("Save Mansion Data", dir, filename, "json");
		}
		if (string.IsNullOrEmpty (filepath))
			return;
		_path = filepath;
		OnClickSave ();
	}

	public	void OnClickRescan() {
		UpdateData ();
	}

	public	void OnClickAddRoom() {
		List<RoomData> list = null;
		if (_data.rooms == null)
			list = new List<RoomData> ();
		else
			list = new List<RoomData> (_data.rooms);

		if (_global == null) {
			list.Add (new RoomData ());
		} else {
			foreach (var info in _global.rooms) {
				int index = list.FindIndex ((RoomData room) => {
					return room.id == info.id;
				});
				if (index < 0) {
					var newRoom = new RoomData ();
					newRoom.id = info.id;
					list.Add (newRoom);
				}
			}
		}
		_data.rooms = list.ToArray ();
	}

	public	void OnClickRemoveRoom(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Rooms", "This will remove all rooms. Is it alright?", "Ok", "Cancel"))
				_data.rooms = new RoomData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Room", "This will remove a selected room. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<RoomData> (ref _data.rooms, index);
		}
	}

	public	void OnClickAddEvidence() {
		List<EvidenceData> list = null;
		if (_data.evidences == null)
			list = new List<EvidenceData> ();
		else
			list = new List<EvidenceData> (_data.evidences);
		list.Add (new EvidenceData ());
		_data.evidences = list.ToArray ();
	}

	public	void OnClickRemoveEvidence(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Evidences", "This will remove all evidences. Is it alright?", "Ok", "Cancel"))
				_data.evidences = new EvidenceData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Evidence", "This will remove a selected evidence. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<EvidenceData> (ref _data.evidences, index);
		}
	}

	private	void UpdateRoom() {
		if (_global == null)
			return;

		_roomsAry = null;
		_roomsDic = new Dictionary<string, string> ();
		var list = new List<string> ();
		for (var i = _global.rooms.Length - 1; i >= 0; i--) {
			var room = _global.rooms [i];
			list.Add (room.name);
			_roomsDic.Add (room.id, room.name);
		}
		_roomsAry = list.ToArray ();
	}

	private	void UpdateEvidence() {
		if (_global == null)
			return;

		_evidencesAry = null;
		_evidencesDic = new Dictionary<string, string> ();
		var list = new List<string> ();
		for (var i = _global.evidences.Length - 1; i >= 0; i--) {
			var evidence = _global.evidences [i];
			list.Add (evidence.name);
			_evidencesDic.Add (evidence.id, evidence.name);
		}
		_evidencesAry = list.ToArray ();
	}

	private	void UpdateScenario() {
		var path = Path.Combine (Application.dataPath, "Scene/Resources");
		var files = Directory.GetFiles (path, "*.json", SearchOption.AllDirectories);
		var list = new List<string> ();
		for (int i = 0; i < files.Length; i++) {
			list.Add (Path.GetFileNameWithoutExtension (files [i]));
		}
		_scenes = list.ToArray ();
	}

	private	void UpdateData() {
		_global = GlobalInfo.LoadFromJson (Path.Combine (Application.dataPath, "Scene/Resources/Global.json"));
		UpdateRoom ();
		UpdateEvidence ();
		UpdateScenario ();
	}
}
