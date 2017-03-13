using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class SceneMaker : EditorWindow {
	[MenuItem("FNF Games/SceneMaker")]
	private	static void Open() {
		var window = CreateInstance<SceneMaker> ();
		window.Show ();
	}

	private	string				_path;
	private	SceneData			_data;
	private	string[] _backgrounds;
	private	string[] _prefabs;
	private	string[] _positions;
	private	string[] _emotions;

	private bool _showActor;
	private	bool _showDialog;

	public	SceneMaker() {
		_path = null;
		_data = null;
		_backgrounds = new string[0];
		_prefabs = new string[0];
		_positions = new string[]{ "none", "center", "left", "leftmost", "right", "rightmost" };
		_emotions = new string[]{ "none", "angry", "idle", "sad", "shy", "smile" };
		_showActor = true;
		_showDialog = true;
	}

	void OnGUI() {
		DrawToolbar ();
		GUILayout.Space (2);
		DrawSceneData ();
		GUILayout.Space (2);
		DrawActorData ();
		GUILayout.Space (2);
		DrawDialogData ();
		GUILayout.FlexibleSpace ();
		DrawStatusBar ();
	}

	private	void DrawToolbar() {
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("New", GUILayout.Width (90)))
			OnClickNew ();
		if (GUILayout.Button ("Open...", GUILayout.Width (90)))
			OnClickOpen ();
		if (!string.IsNullOrEmpty (_path))
		if (GUILayout.Button ("Save", GUILayout.Width (90)))
			OnClickSave ();
		if (_data != null)
		if (GUILayout.Button ("Save As...", GUILayout.Width (90)))
			OnClickSaveAs ();
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("Rescan", GUILayout.Width (90)))
			OnClickRescan ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawSceneData() {
		if (_data == null)
			return;
		GUILayout.BeginVertical ();
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("title", GUILayout.Width (90));
		_data.title = GUILayout.TextField (_data.title);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		GUILayout.Label ("background", GUILayout.Width (90));
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
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		_showActor = GUILayout.Toggle (_showActor, "actors");
		if (_showActor)
		if (GUILayout.Button ("+", GUILayout.Width (20)))
			OnClickAddActor ();
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();
		if (_showActor) {
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("-", GUILayout.Width (20)))
				OnClickRemoveActor (-1);
			GUILayout.Button ("name", GUILayout.Width (90));
			GUILayout.Button ("asset");
			GUILayout.Button ("alias", GUILayout.Width (90));
			GUILayout.EndHorizontal ();

			int index = 0;
			for (int i = 0; i < _data.actors.Length; i++) {
				var actor = _data.actors [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", GUILayout.Width (20)))
					OnClickRemoveActor (index);
				actor.label = GUILayout.TextField (actor.label, GUILayout.Width (90));
				int indexActor = ArrayUtility.IndexOf<string> (_prefabs, actor.asset);
				int newActor = EditorGUILayout.Popup (indexActor, _prefabs);
				if (indexActor != newActor)
					actor.asset = _prefabs [newActor];
				actor.name = GUILayout.TextField (actor.name, GUILayout.Width (90));
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
		_showDialog = GUILayout.Toggle (_showDialog, "dialog");
		if (_showDialog)
		if (GUILayout.Button ("+", GUILayout.Width (20)))
			OnClickAddDialog ();
		GUILayout.EndHorizontal ();

		EditorGUILayout.BeginScrollView (Vector2.zero);
		GUILayout.BeginVertical ();

		if (_showDialog) {
			int index = 0;
			for (int i = 0; i < _data.sequences.Length; i++) {
				var dialog = _data.sequences [i];
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("-", GUILayout.Width (20)))
					OnClickRemoveDialog (index);
				EditorGUILayout.EnumPopup (dialog.command, GUILayout.Width (90));
				EditorGUILayout.Popup (0, new string[]{ dialog.actor }, GUILayout.Width (90));
				int oldIndex = ArrayUtility.IndexOf<string> (_positions, dialog.position);
				int newIndex = EditorGUILayout.Popup (oldIndex, _positions, GUILayout.Width (90));
				if (oldIndex != newIndex)
					dialog.position = _positions [newIndex];
				oldIndex = ArrayUtility.IndexOf<string> (_emotions, dialog.emotion);
				newIndex = EditorGUILayout.Popup (oldIndex, _emotions, GUILayout.Width (90));
				if (oldIndex != newIndex)
					dialog.emotion = _emotions [newIndex];
				GUILayout.TextField (dialog.dialog);
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
			UpdateData ();
		}
	}

	private	void OnClickSave() {
		UpdateInput ();
		File.WriteAllText (_path, JsonUtility.ToJson (_data), System.Text.Encoding.UTF8);
	}

	private	void OnClickSaveAs() {
		UpdateInput ();
		string filepath = null;
		if (string.IsNullOrEmpty (_path)) {
			filepath = EditorUtility.SaveFilePanel ("Save Scenario Data", Application.dataPath, "", "json");
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
		// backgrounds
		var path = Path.Combine (Application.dataPath, "Art/Background/Resources");
		var files = Directory.GetFiles (path, "*.prefab", SearchOption.AllDirectories);
		var list = new List<string> ();
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

	private	void OnClickAddActor() {
	}

	private	void OnClickRemoveActor(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove all Actors", "This will remove all actors. Is it alright?", "Ok", "Cancel"))
				_data.actors = new ActorData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Actor", "This will remove a selected actor. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<ActorData> (ref _data.actors, index);
		}
	}

	private	void OnClickAddDialog() {
	}

	private	void OnClickRemoveDialog(int index) {
		if (index < 0) {
			if (EditorUtility.DisplayDialog ("Remove a Dialog", "This will remove a selected dialog. Is it alright?", "Ok", "Cancel"))
				_data.sequences = new DialogData[0];
		} else if (EditorUtility.DisplayDialog ("Remove a Dialog", "This will remove a selected dialog. Is it alright?", "Ok", "Cancel")) {
			ArrayUtility.RemoveAt<DialogData> (ref _data.sequences, index);
		}
	}

	private	void UpdateData() {
	}

	private	void UpdateInput() {
	}
}
