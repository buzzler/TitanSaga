using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SceneMaker : EditorWindow {
	[MenuItem("HyperLuna/SceneMaker")]
	private	static void Open() {
		var window = CreateInstance<SceneMaker> ();
		window.Show ();
	}
		
	private	string		_path;
	private	SceneData	_data;
	private GlobalInfo	_global;
	private	string[]	_scenes;
	private	string[]	_uis;
	private	string[]	_backgrounds;
	private	string[]	_positions;
	private	string[]	_emotions;

	private	string[]	_rolesAry;
	private	bool		_showDialog;
	private	Vector2		_scroll;

	private	string[] _bgNames;
	private	Dictionary<string, string> _bgNameId;

	public	SceneMaker() {
		_path		= null;
		_data		= null;
		_scenes		= new string[0];
		_uis		= new string[0];
		_backgrounds= new string[0];
		_positions	= new string[]{ ActorPosition.NONE, ActorPosition.CENTER, ActorPosition.LEFT, ActorPosition.LEFTMOST, ActorPosition.RIGHT, ActorPosition.RIGHTMOST};
		_emotions	= new string[]{ ActorEmotion.NONE, ActorEmotion.ANGRY, ActorEmotion.IDLE, ActorEmotion.SAD, ActorEmotion.SHY, ActorEmotion.SMILE };
		_rolesAry	= new string[0];
		_showDialog	= true;
		_scroll		= Vector2.zero;
		_bgNames	= new string[0];
		_bgNameId	= new Dictionary<string, string> ();
	}

	void OnGUI() {
//		try {
			DrawToolbar ();
			GUILayout.Space (2);
			DrawSceneData ();
			GUILayout.Space (2);
//			DrawActorData ();
			GUILayout.Space (2);
			DrawDialogData ();
			GUILayout.FlexibleSpace ();
			DrawStatusBar ();
//		} catch (System.Exception e) {
//			Debug.LogWarning (e.Message);
//		}
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
		if (_data != null)
		if (Application.isPlaying)
		if (_path != null)
		if (GUILayout.Button("Play", EditorStyles.toolbarButton, GUILayout.Width(90)))
			OnClickPlay ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawSceneData() {
		if (_data == null)
			return;
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		GUILayout.Button ("title", EditorStyles.miniButton, GUILayout.Width (90));
		var old_title = _data.title;
		var new_title = GUILayout.TextField (_data.title);
		if (old_title != new_title) {
			_data.title = new_title;
			this.titleContent = new GUIContent (new_title);
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("ui", EditorStyles.miniButton, GUILayout.Width (90)))
			OnClickUI ();
		if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickUIClear ();
		int oldUI = ArrayUtility.IndexOf<string> (_uis, _data.ui);
		int newUI = EditorGUILayout.Popup (oldUI, _uis);
		if (oldUI != newUI)
			_data.ui = _uis [newUI];
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("background", EditorStyles.miniButton, GUILayout.Width (90)))
			OnClickBackground ();
		if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickBackgroundClear ();
		
//		int indexBackground = ArrayUtility.IndexOf<string> (_backgrounds, _data.background);
//		int newBackground = EditorGUILayout.Popup (indexBackground, _backgrounds);
//		if (indexBackground != newBackground)
//			_data.background = _backgrounds [newBackground];
		RoomInfo rInfo = _global.GetRoomById(_data.background);
		int indexBackground = (rInfo != null) ? ArrayUtility.IndexOf<string> (_bgNames, rInfo.name) : -1;
		int newBackground = EditorGUILayout.Popup (indexBackground, _bgNames);
		if (indexBackground != newBackground)
			_data.background = _bgNameId [_bgNames [newBackground]];


		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}

	private	 void DrawDialogData() {
		if (_data == null)
			return;
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showDialog = GUILayout.Toggle (_showDialog, "shots");
		if (_showDialog)
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddDialog ();
		GUILayout.EndHorizontal ();

		_scroll = EditorGUILayout.BeginScrollView (_scroll);
		GUILayout.BeginVertical ();

		if (_showDialog) {
			GUILayout.BeginHorizontal ();
			GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("comment", EditorStyles.miniButton);
			GUILayout.Button ("position", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("emotion", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("command", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.EndHorizontal ();

			int index = 0;
			for (int i = 0; i < _data.shots.Length; i++) {
				var dialog = _data.shots [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveDialog (index);

				int oldLabel = ArrayUtility.IndexOf<string> (_rolesAry, dialog.role);
				int newLabel = EditorGUILayout.Popup (oldLabel, _rolesAry, GUILayout.Width (90));
				if (oldLabel != newLabel)
					dialog.role = _rolesAry [newLabel];

				dialog.comment = GUILayout.TextArea (dialog.comment);
				int oldIndex = ArrayUtility.IndexOf<string> (_positions, dialog.position);
				int newIndex = EditorGUILayout.Popup (oldIndex, _positions, GUILayout.Width (90));
				if (oldIndex != newIndex)
					dialog.position = _positions [newIndex];
				oldIndex = ArrayUtility.IndexOf<string> (_emotions, dialog.emotion);
				newIndex = EditorGUILayout.Popup (oldIndex, _emotions, GUILayout.Width (90));
				if (oldIndex != newIndex)
					dialog.emotion = _emotions [newIndex];
				dialog.command = (ShotCommand) EditorGUILayout.EnumPopup (dialog.command, GUILayout.Width (90));
				if (dialog.command == ShotCommand.SCENE_CHANGE) {
					oldIndex = ArrayUtility.IndexOf (_scenes, dialog.parameter);
					newIndex = EditorGUILayout.Popup (oldIndex, _scenes, GUILayout.Width (90));
					if (oldIndex != newIndex)
						dialog.parameter = _scenes [newIndex];
				} else if ((dialog.command == ShotCommand.UI_CHANGE)||
					(dialog.command == ShotCommand.UI_HIDE)||
					(dialog.command == ShotCommand.UI_SHOW)) {
					oldIndex = ArrayUtility.IndexOf (_uis, dialog.parameter);
					newIndex = EditorGUILayout.Popup (oldIndex, _uis, GUILayout.Width (90));
					if (oldIndex != newIndex)
						dialog.parameter = _uis [newIndex];
//				} else if (dialog.command == ShotCommand.SCENARIO_CHANGE_SUSPECT) {
//					oldIndex = ArrayUtility.IndexOf<string> (_rolesAry, dialog.parameter);
//					newIndex = EditorGUILayout.Popup (oldIndex, _rolesAry, GUILayout.Width (90));
//					if (oldIndex != newIndex)
//						dialog.parameter = _rolesAry [newIndex];
//				}
				} else if (dialog.command == ShotCommand.SCENARIO_CHANGE_SUSPECT) {
					oldIndex = ArrayUtility.IndexOf<string> (_scenes, dialog.parameter);
					newIndex = EditorGUILayout.Popup (oldIndex, _scenes, GUILayout.Width (90));
					if (oldIndex != newIndex)
						dialog.parameter = _scenes [newIndex];
				}
				GUILayout.EndHorizontal ();
				index++;
			}
		}

		GUILayout.EndVertical ();
		EditorGUILayout.EndScrollView ();

		GUILayout.EndHorizontal ();
	}

	private	void DrawStatusBar() {
		GUILayout.BeginHorizontal ();
		GUILayout.Box ("STATUS", GUILayout.Width (90));
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("Resume", GUILayout.Width (90))) {
			var observer = GameObject.FindObjectOfType<Observer> ();
			observer.scenarioCtr.Resume ();
		}
		GUILayout.EndHorizontal ();
	}
	 
	private	void OnClickNew() {
		bool clear = _data == null;
		if (!clear)
			clear = EditorUtility.DisplayDialog ("Confirm", "All scene's data will be lost.\nIs it alright?", "Ok", "Cancel");

		if (clear) {
			_path = null;
			_data = new SceneData ();
			_global = GlobalInfo.LoadFromJson (Path.Combine (Application.dataPath, "Scene/Resources/Global.json"));
			UpdateData ();
		}
	}

	private	void OnClickOpen() {
		string path = EditorUtility.OpenFilePanel ("Open Scenario Data", Path.Combine(Application.dataPath, "Scene/Resources"), "json");
		if (string.IsNullOrEmpty (path))
			return;
		if (!File.Exists (path))
			return;
		bool clear = _data == null;
		if (!clear)
			clear = EditorUtility.DisplayDialog ("Confirm", "All scene's data will be lost.\nIs it alright?", "Ok", "Cancel");

		if (clear) {
			_path = path;
			_data = JsonUtility.FromJson<SceneData> (File.ReadAllText (path));
			_global = GlobalInfo.LoadFromJson (Path.Combine (Application.dataPath, "Scene/Resources/Global.json"));
			UpdateData ();
		}
	}

	private	void OnClickSave() {
		File.WriteAllText (_path, JsonUtility.ToJson (_data), System.Text.Encoding.UTF8);
		AssetDatabase.Refresh ();
	}

	private	void OnClickSaveAs() {
		string filepath = null;
		if (string.IsNullOrEmpty (_path)) {
			filepath = EditorUtility.SaveFilePanel ("Save Scenario Data", Path.Combine(Application.dataPath, "Scene/Resources"), "", "json");
		} else {
			var dir = Path.GetDirectoryName (_path);
			var filename = Path.GetFileNameWithoutExtension (_path);
			filepath = EditorUtility.SaveFilePanel ("Save Scenario Data", dir, filename, "json");
		}
		if (string.IsNullOrEmpty (filepath))
			return;
		_path = filepath;
		OnClickSave ();
	}
		
	private	void OnClickRescan() {
		// scene
		var path = Path.Combine (Application.dataPath, "Scene/Resources");
		var files = Directory.GetFiles (path, "*.json", SearchOption.AllDirectories);
		var list = new List<string> ();
		for (int i = 0; i < files.Length; i++) {
			list.Add (Path.GetFileNameWithoutExtension (files [i]));
		}
		_scenes = list.ToArray ();

		// ui
		path = Path.Combine (Application.dataPath, "Art/UI/Resources");
		files = Directory.GetFiles (path, "*.prefab", SearchOption.AllDirectories);
		list = new List<string> ();
		for (int i = 0; i < files.Length; i++) {
			list.Add (Path.GetFileNameWithoutExtension (files [i]));
		}
		_uis = list.ToArray ();

		// backgrounds
		path = Path.Combine (Application.dataPath, "Art/Background/Resources");
		files = Directory.GetFiles (path, "*.prefab", SearchOption.AllDirectories);
		list = new List<string> ();
		for (int i = 0; i < files.Length; i++) {
			list.Add (Path.GetFileNameWithoutExtension (files [i]));
		}
		_backgrounds = list.ToArray ();

		_bgNameId.Clear ();
		var names = new List<string> ();
		foreach (var info in _global.rooms) {
			_bgNameId.Add (info.name, info.id);
			names.Add (info.name);
		}
		_bgNames = names.ToArray ();
	}

	private	void OnClickPlay() {
		if (!Application.isPlaying)
			return;

		var observer = GameObject.FindObjectOfType<Observer> ();
		if (observer == null)
			return;

		if (string.IsNullOrEmpty (_path))
			return;

		var filename = Path.GetFileNameWithoutExtension (_path);
		observer.scenarioCtr.Play (filename);
	}

	private	void OnClickUI() {
		if (Application.isPlaying)
			return;
		if (_data == null)
			return;
		if (string.IsNullOrEmpty (_data.ui))
			return;
		
		var canvas = GameObject.Find ("Canvas").transform;
		for (int i = canvas.childCount - 1; i >= 0; i--) {
			GameObject.DestroyImmediate (canvas.GetChild (0).gameObject);
		}
		var ui = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject> ("Assets/Art/UI/Resources/" + _data.ui + ".prefab");
		var go = GameObject.Instantiate<GameObject> (ui);
		go.transform.SetParent (canvas, false);
	}

	private	void OnClickUIClear() {
		if (_data == null)
			return;
		_data.ui = string.Empty;
	}

	private void OnClickBackground() {
	}

	private	void OnClickBackgroundClear() {
		if (_data == null)
			return;
		_data.background = string.Empty;
	}

	private	void OnClickAddDialog() {
		List<ShotData> list = null;
		if (_data.shots == null)
			list = new List<ShotData> ();
		else
			list = new List<ShotData> (_data.shots);
		list.Add (new ShotData ());
		_data.shots = list.ToArray ();
	}

	private	void OnClickRemoveDialog(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove a Dialog", "This will remove a selected dialog. Is it alright?", "Ok", "Cancel"))
				_data.shots = new ShotData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Dialog", "This will remove a selected dialog. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<ShotData> (ref _data.shots, index);
		}
	}

	private	void UpdateRole() {
		var list = new List<string> ();
		var fields = typeof(Role).GetFields ();
		foreach(var field in fields) {
			list.Add (field.GetValue (null) as string);
		}
		_rolesAry = list.ToArray ();
	}

	private	void UpdateData() {
		UpdateRole ();
	}
}
