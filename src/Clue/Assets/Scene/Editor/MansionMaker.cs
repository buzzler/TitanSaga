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
	private	string[]					_suspectsAry;
	private	Dictionary<string, string>	_suspectsDic;

	private bool _showRoom;
	private	bool _showEvidence;
	private	bool _showSuspect;
	private	Vector2 _scroll;

	public	MansionMaker() {
		_path = null;
		_global = null;
		_data = null;
		_scenes = new string[0];
		_roomsAry = new string[0];
		_roomsDic = new Dictionary<string, string> ();
		_evidencesAry = new string[0];
		_evidencesDic = new Dictionary<string, string> ();
		_suspectsAry = new string[0];
		_suspectsDic = new Dictionary<string, string> ();
		_showRoom = true;
		_showEvidence = true;
		_showSuspect = true;
		_scroll = Vector2.zero;
	}

	void OnGUI() {
//		try {
			Verify();
			DrawToolbar ();
			GUILayout.Space (2);
			DrawMansionData ();
			GUILayout.Space (2);
			DrawRoomData ();
			GUILayout.Space (2);
			DrawSuspectData();
			GUILayout.Space (2);
			DrawEvidenceData ();
//		} catch (System.Exception e) {
//			Debug.LogWarning (e.Message);
//		}
	}

	private	void Verify() {
		if (_global == null) {
			UpdateData ();
		}

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
		if (GUILayout.Button ("Align", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickAlign ();
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

		int oldIndex;
		int newIndex;

		GUILayout.BeginHorizontal ();
		GUILayout.Button ("prologue", EditorStyles.miniButton, GUILayout.Width(90));
		oldIndex = ArrayUtility.IndexOf<string> (_scenes, _data.prologueScenario);
		newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
		if (oldIndex != newIndex)
			_data.prologueScenario = _scenes [newIndex];
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Button ("epilogue", EditorStyles.miniButton, GUILayout.Width(90));
		oldIndex = ArrayUtility.IndexOf<string> (_scenes, _data.epilogueScenario);
		newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
		if (oldIndex != newIndex)
			_data.epilogueScenario = _scenes [newIndex];
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Button ("gameover", EditorStyles.miniButton, GUILayout.Width(90));
		oldIndex = ArrayUtility.IndexOf<string> (_scenes, _data.gameoverScenario);
		newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
		if (oldIndex != newIndex)
			_data.gameoverScenario = _scenes [newIndex];
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

			int oldIndex;
			int newIndex;
			for (int i = 0; i < _data.rooms.Length; i++) {
				var room = _data.rooms [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveRoom (i);
				oldIndex = ArrayUtility.IndexOf<string> (_roomsAry, _roomsDic.ContainsKey (room.id) ? _roomsDic [room.id] : string.Empty);
				EditorGUILayout.Popup (oldIndex, _roomsAry, GUILayout.Width (90));
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
		GUILayout.BeginVertical ();
		_scroll = GUILayout.BeginScrollView (_scroll);

		if (_showEvidence) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveEvidence (-1);
			GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("scenario", EditorStyles.miniButton);
			GUILayout.Button ("room", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Toggle (false, string.Empty, GUILayout.Width (20));
			GUILayout.EndHorizontal ();

			int oldIndex;
			int newIndex;
			for (int i = 0; i < _data.evidences.Length; i++) {
				var evidence = _data.evidences [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveEvidence (i);
				oldIndex = ArrayUtility.IndexOf<string> (_evidencesAry, _evidencesDic.ContainsKey (evidence.id) ? _evidencesDic [evidence.id] : string.Empty);
				EditorGUILayout.Popup (oldIndex, _evidencesAry, GUILayout.Width (90));
				oldIndex = ArrayUtility.IndexOf<string> (_scenes, evidence.scenario);
				newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
				if (oldIndex != newIndex)
					evidence.scenario = _scenes [newIndex];
				var info = _global.GetEvidenceById (evidence.id);
				oldIndex = (info != null) ? ArrayUtility.IndexOf<string> (_roomsAry, _roomsDic[info.room]) : -1;
				EditorGUILayout.Popup (oldIndex, _roomsAry, GUILayout.Width (90));
				evidence.available = GUILayout.Toggle (evidence.available, "", GUILayout.Width (20));
				GUILayout.EndHorizontal ();
			}
		}

		GUILayout.EndScrollView ();
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	public	void DrawSuspectData() {
		if (_global == null || _data == null)
			return;

		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showSuspect = GUILayout.Toggle (_showSuspect, "suspect");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddSuspect ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();
		if (_showSuspect) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveSuspect (-1);
			GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("scenario", EditorStyles.miniButton);
			GUILayout.Button ("room", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Toggle (false, string.Empty, GUILayout.Width (20));
			GUILayout.EndHorizontal ();

			int oldIndex;
			int newIndex;
			for (int i = 0; i < _data.suspects.Length; i++) {
				var suspect = _data.suspects [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveSuspect (i);
				oldIndex = ArrayUtility.IndexOf<string> (_suspectsAry, _suspectsDic.ContainsKey (suspect.id) ? _suspectsDic [suspect.id] : string.Empty);
				newIndex = EditorGUILayout.Popup (oldIndex, _suspectsAry, GUILayout.Width (90));
				if (oldIndex != newIndex) {
					foreach (var key in _suspectsDic.Keys) {
						if (_suspectsDic [key] == _suspectsAry [newIndex]) {
							suspect.id = key;
							break;
						}
					}
				}

				oldIndex = ArrayUtility.IndexOf<string> (_scenes, suspect.scenario);
				newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
				if (oldIndex != newIndex)
					suspect.scenario = _scenes [newIndex];

				oldIndex = _roomsDic.ContainsKey(suspect.room) ? ArrayUtility.IndexOf<string> (_roomsAry, _roomsDic[suspect.room]) : -1;
				newIndex = EditorGUILayout.Popup (oldIndex, _roomsAry, GUILayout.Width (90));
				if (oldIndex != newIndex) {
					var itr = _roomsDic.GetEnumerator ();
					while (itr.MoveNext ()) {
						if (itr.Current.Value == _roomsAry [newIndex])
							suspect.room = itr.Current.Key;
					}
				}
				suspect.available = GUILayout.Toggle (suspect.available, "", GUILayout.Width (20));
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
			if (_global != null) {
				OnClickAddRoom ();
				OnClickAddEvidence ();
				OnClickAlign ();
			}
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

	public	void OnClickAlign() {
		if (_global == null || _data == null)
			return;

		var rooms = _data.rooms;
		System.Array.Sort<RoomData> (rooms, (RoomData x, RoomData y) => {
			var infoX = ArrayUtility.Find<RoomInfo>(_global.rooms, (RoomInfo obj) => {
				return obj.id == x.id;
			});
			var infoY = ArrayUtility.Find<RoomInfo>(_global.rooms, (RoomInfo obj) => {
				return obj.id == y.id;
			});
			return string.Compare(infoX.name, infoY.name);
		});

		var evidences = _data.evidences;
		System.Array.Sort<EvidenceData>(evidences, (EvidenceData x, EvidenceData y) => {
			var infoX = ArrayUtility.Find<EvidenceInfo>(_global.evidences, (EvidenceInfo obj) => {
				return obj.id == x.id;
			});
			var infoY = ArrayUtility.Find<EvidenceInfo>(_global.evidences, (EvidenceInfo obj) => {
				return obj.id == y.id;
			});
			var infoRoomX = ArrayUtility.Find<RoomInfo>(_global.rooms, (RoomInfo obj) => {
				return obj.id == infoX.room;
			});
			var infoRoomY = ArrayUtility.Find<RoomInfo>(_global.rooms, (RoomInfo obj) => {
				return obj.id == infoY.room;
			});
			var compare = string.Compare(infoRoomX.name, infoRoomY.name);
			if (compare == 0) {
				return string.Compare(infoX.name, infoY.name);
			} else {
				return compare;
			}
		});
		_data.rooms = rooms;
		_data.evidences = evidences;
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

	public	void OnToggleRoom(int index) {
		if (index < 0) {
		} else {
		}
	}

	public	void OnClickAddEvidence() {
		List<EvidenceData> list = new List<EvidenceData> ();
		if (_global == null) {
			list.Add (new EvidenceData ());
		} else {
			foreach (var info in _global.evidences) {
				var newEvidence = new EvidenceData ();
				newEvidence.id = info.id;
				list.Add (newEvidence);
			}
		}
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

	public	void OnClickAddSuspect() {
		List<SuspectData> list = null;
		if (_data.rooms == null)
			list = new List<SuspectData> ();
		else
			list = new List<SuspectData> (_data.suspects);

		if (_global == null) {
			list.Add (new SuspectData ());
		} else {
			foreach (var info in _global.suspects) {
				int index = list.FindIndex ((SuspectData suspect) => {
					return suspect.id == info.id;
				});
				if (index < 0) {
					var newSuspect = new SuspectData ();
					newSuspect.id = info.id;
					list.Add (newSuspect);
				}
			}
		}
		_data.suspects = list.ToArray ();
	}

	public	void OnClickRemoveSuspect(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Suspect", "This will remove all suspect. Is it alright?", "Ok", "Cancel"))
				_data.suspects = new SuspectData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Suspect", "This will remove a selected suspect. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<SuspectData> (ref _data.suspects, index);
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

	private	void UpdateSuspect() {
		if (_global == null)
			return;

		_suspectsAry = null;
		_suspectsDic = new Dictionary<string, string> ();
		var list = new List<string> ();
		for (var i = _global.suspects.Length - 1; i >= 0; i--) {
			var suspect = _global.suspects [i];
			list.Add (suspect.name);
			_suspectsDic.Add (suspect.id, suspect.name);
		}
		_suspectsAry = list.ToArray ();
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
		UpdateSuspect ();
	}
}
