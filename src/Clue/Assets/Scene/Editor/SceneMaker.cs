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
	private	string[]	_scenes;
	private	string[]	_uis;
	private	string[]	_backgrounds;
	private	string[]	_prefabs;
	private	string[]	_positions;
	private	string[]	_emotions;

	private	string[]					_actorsAry;
	private	Dictionary<string, string>	_actorsDic;
	private bool _showActor;
	private	bool _showDialog;

	public	SceneMaker() {
		_path		= null;
		_data		= null;
		_scenes		= new string[0];
		_uis		= new string[0];
		_backgrounds= new string[0];
		_prefabs	= new string[0];
		_positions	= new string[]{ ActorPosition.NONE, ActorPosition.CENTER, ActorPosition.LEFT, ActorPosition.LEFTMOST, ActorPosition.RIGHT, ActorPosition.RIGHTMOST};
		_emotions	= new string[]{ ActorEmotion.NONE, ActorEmotion.ANGRY, ActorEmotion.IDLE, ActorEmotion.SAD, ActorEmotion.SHY, ActorEmotion.SMILE };
		_actorsAry	= new string[0];
		_actorsDic	= new Dictionary<string, string> ();
		_showActor	= true;
		_showDialog	= true;
	}

	void OnGUI() {
		try {
			DrawToolbar ();
			GUILayout.Space (2);
			DrawSceneData ();
			GUILayout.Space (2);
			DrawActorData ();
			GUILayout.Space (2);
			DrawDialogData ();
			GUILayout.FlexibleSpace ();
			DrawStatusBar ();
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
		int indexBackground = ArrayUtility.IndexOf<string> (_backgrounds, _data.background);
		int newBackground = EditorGUILayout.Popup (indexBackground, _backgrounds);
		if (indexBackground != newBackground)
			_data.background = _backgrounds [newBackground];
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}

	private	void DrawActorData() {
		if (_data == null)
			return;
		if (_actorsAry.Length != _actorsDic.Count)
			UpdateActor();

		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showActor = GUILayout.Toggle (_showActor, "actors");
		if (_showActor)
		if (GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20)))
			OnClickAddActor ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();
		if (_showActor) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveActor (-1);
			GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.Button ("asset", EditorStyles.miniButton);
			GUILayout.Button ("alias", EditorStyles.miniButton, GUILayout.Width (90));
			GUILayout.EndHorizontal ();

			int index = 0;
			for (int i = 0; i < _data.actors.Length; i++) {
				var actor = _data.actors [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
					OnClickRemoveActor (index);
				var oldLabel = actor.label;
				var newLabel = GUILayout.TextField (oldLabel, GUILayout.Width (90));
				if (oldLabel != newLabel) {
					actor.label = newLabel;
					UpdateActor ();
				}
				int indexActor = ArrayUtility.IndexOf<string> (_prefabs, actor.asset);
				int newActor = EditorGUILayout.Popup (indexActor, _prefabs);
				if (indexActor != newActor)
					actor.asset = _prefabs [newActor];
				var oldName = actor.name;
				var newName = GUILayout.TextField (oldName, GUILayout.Width (90));
				if (oldName != newName) {
					actor.name = newName;
					UpdateActor ();
				}
				GUILayout.EndHorizontal ();
				index++;
			}
		}
		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
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

		EditorGUILayout.BeginScrollView (Vector2.zero);
		GUILayout.BeginVertical ();

		if (_showDialog) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20)))
				OnClickRemoveActor (-1);
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
				int oldLabel = _actorsDic.ContainsKey (dialog.actor) ? ArrayUtility.IndexOf<string> (_actorsAry, _actorsDic [dialog.actor]) : -1;
				int newLabel = EditorGUILayout.Popup (oldLabel, _actorsAry, GUILayout.Width (90));
				if (oldLabel != newLabel)
					dialog.actor = _data.GetActorByLabel (_actorsAry[newLabel]).name;
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
//			if (_data != null && _data.title != null)
//				this.titleContent = new GUIContent(_data.title);
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
		UpdateActor ();

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

		// prefabs
		path = Path.Combine (Application.dataPath, "Art/Actor/Resources");
		files = Directory.GetFiles (path, "*.prefab", SearchOption.AllDirectories);
		list = new List<string> ();
		for (int i = 0; i < files.Length; i++) {
			list.Add (Path.GetFileNameWithoutExtension (files [i]));
		}
		_prefabs = list.ToArray ();
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

	private	void OnClickAddActor() {
		List<ActorData> list = null;
		if (_data.actors == null)
			list = new List<ActorData> ();
		else
			list = new List<ActorData>(_data.actors);
		list.Add (new ActorData ());
		_data.actors = list.ToArray ();
		UpdateActor ();
	}

	private	void OnClickRemoveActor(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Actors", "This will remove all actors. Is it alright?", "Ok", "Cancel"))
				_data.actors = new ActorData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Actor", "This will remove a selected actor. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<ActorData> (ref _data.actors, index);
		}
		UpdateActor ();
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

	private void UpdateActor() {
		if (_data == null)
			return;

		_actorsAry = null;
		_actorsDic = new Dictionary<string, string> ();
		var list = new List<string> ();
		for (var i = _data.actors.Length - 1; i >= 0; i--) {
			var actor = _data.actors [i];
			list.Add (actor.label);
			_actorsDic.Add (actor.name, actor.label);
		}
		_actorsAry = list.ToArray ();
	}

	private	void UpdateData() {
		UpdateActor ();
	}
}
