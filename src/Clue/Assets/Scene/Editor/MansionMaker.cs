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
	private	MansionData	_data;
	private	string[]	_scenes;

	private	string[]					_roomsAry;
	private	Dictionary<string, string>	_roomsDic;

	public	MansionMaker() {
		_path = null;
		_data = null;
		_scenes = new string[0];
		_roomsAry = new string[0];
		_roomsDic = new Dictionary<string, string> ();
	}

	void OnGUI() {
		try {
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
//		if (GUILayout.Button ("Play", EditorStyles.toolbarButton, GUILayout.Width (90)))
//			OnClickPlay ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawMansionData() {
		if (_data == null)
			return;
		
		GUILayout.BeginHorizontal ();
		GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width(90));
		_data.name = GUILayout.TextField (_data.name);
		GUILayout.EndHorizontal ();
	}

	private	void DrawRoomData() {
		if (_data == null)
			return;
		if (_roomsAry.Length != _roomsDic.Count)
			UpdateRoom();
		
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		GUILayout.Toggle (true, "rooms");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddRoom ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickRemoveRoom (-1);
		GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.Button ("location", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.Button ("scenario", EditorStyles.miniButton);
		GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.EndHorizontal ();

		for (int i = 0; i < _data.rooms.Length; i++) {
			var room = _data.rooms [i];
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveRoom (i);
			room.name = GUILayout.TextField (room.name, GUILayout.Width (90));
			room.location = EditorGUILayout.Vector2Field ("", room.location, GUILayout.Width (90));
			var oldIndex = ArrayUtility.IndexOf<string> (_scenes, room.scenario);
			var newIndex = EditorGUILayout.Popup (oldIndex, _scenes);
			if (oldIndex != newIndex)
				room.scenario = _scenes [newIndex];
			room.id = GUILayout.TextField (room.id, GUILayout.Width (90));
			GUILayout.EndHorizontal ();
		}

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawEvidenceData() {
		if (_data == null)
			return;
		
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		GUILayout.Toggle (true, "objects");
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddEvidence ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickRemoveEvidence (-1);
		GUILayout.Button ("room", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.Button ("data", EditorStyles.miniButton);
		GUILayout.Button ("cost", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.EndHorizontal ();

		for (int i = 0; i < _data.evidences.Length; i++) {
			var evidence = _data.evidences [i];
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveEvidence (i);
			var oldIndex = _roomsDic.ContainsKey (evidence.room) ? ArrayUtility.IndexOf<string> (_roomsAry, _roomsDic [evidence.room]) : -1;
			var newIndex = EditorGUILayout.Popup (oldIndex, _roomsAry, GUILayout.Width (90));
			if (oldIndex != newIndex)
				evidence.room = _data.GetRoomByName (_roomsAry [newIndex]).id;
			evidence.name = GUILayout.TextField (evidence.name, GUILayout.Width (90));
			evidence.data = GUILayout.TextArea (evidence.data);
			evidence.cost = EditorGUILayout.FloatField (evidence.cost, GUILayout.Width (90));
			evidence.id = GUILayout.TextField (evidence.id, GUILayout.Width (90));
			GUILayout.EndHorizontal ();
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
//			if (_data != null && _data.name != null)
//				this.titleContent = new GUIContent(_data.name);
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
		UpdateRoom ();

		// scene
		var path = Path.Combine (Application.dataPath, "Scene/Resources");
		var files = Directory.GetFiles (path, "*.json", SearchOption.AllDirectories);
		var list = new List<string> ();
		for (int i = 0; i < files.Length; i++) {
			list.Add (Path.GetFileNameWithoutExtension (files [i]));
		}
		_scenes = list.ToArray ();
	}

//	public	void OnClickPlay() {
//	}

	public	void OnClickAddRoom() {
		List<RoomData> list = null;
		if (_data.rooms == null)
			list = new List<RoomData> ();
		else
			list = new List<RoomData> (_data.rooms);
		list.Add (new RoomData ());
		_data.rooms = list.ToArray ();
	}

	public	void OnClickRemoveRoom(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Rooms", "This will remove all rooms. Is it alright?", "Ok", "Cancel"))
				_data.rooms = new RoomData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Room", "This will remove a selected room. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<RoomData> (ref _data.rooms, index);
		}
		UpdateRoom ();
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
