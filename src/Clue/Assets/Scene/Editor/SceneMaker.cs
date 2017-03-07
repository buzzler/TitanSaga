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
	private	List<ActorData>		_actors;
	private	List<DialogData>	_dialogs;

	public	SceneMaker() {
		_data = null;
		_actors = new List<ActorData> ();
		_dialogs = new List<DialogData> ();
	}

	void OnGUI() {
		DrawToolbar ();
		DrawSceneData ();
		DrawActorData ();
		DrawDialogData ();
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
		GUILayout.TextField (_data.background);
		EditorGUILayout.Popup (0, new string[] {"select", "B1", "B2", "B3", "B4", "B5"}, GUILayout.Width (90));
		GUILayout.EndHorizontal ();
		GUILayout.EndVertical ();
	}

	private	void DrawActorData() {
		if (_data == null)
			return;
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		GUILayout.Label ("actors");
		if (GUILayout.Button ("+", GUILayout.Width (20)))
			OnClickAddActor ();
		GUILayout.EndHorizontal ();
		GUILayout.BeginVertical ();

		int index = 0;
		foreach (var actor in _actors) {
			GUILayout.BeginHorizontal ();
			GUILayout.TextField (actor.label, GUILayout.Width (90));
			GUILayout.Label (actor.name, GUILayout.Width (90));
			GUILayout.TextField (actor.asset);
			if (GUILayout.Button ("-", GUILayout.Width (20)))
				OnClickRemoveActor (index);
			GUILayout.EndHorizontal ();
			index++;
		}

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	 void DrawDialogData() {
		if (_data == null)
			return;
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		GUILayout.Label ("dialog");
		if (GUILayout.Button ("+", GUILayout.Width (20)))
			OnClickAddDialog ();
		GUILayout.EndHorizontal ();
		GUILayout.BeginVertical ();

		int index = 0;
		foreach (var dialog in _dialogs) {
			GUILayout.BeginHorizontal ();
			EditorGUILayout.EnumPopup (dialog.command, GUILayout.Width (90));
			EditorGUILayout.Popup (0, new string[]{ dialog.actor }, GUILayout.Width (90));
			EditorGUILayout.Popup (0, new string[]{ "center", "left", "right" }, GUILayout.Width (90));
			EditorGUILayout.Popup (0, new string[]{ "idle", "smile", "shy", "sad", "angry" }, GUILayout.Width (90));
			GUILayout.TextField (dialog.dialog);
			if (GUILayout.Button ("-", GUILayout.Width (20)))
				OnClickRemoveDialog (index);
			GUILayout.EndHorizontal ();
			index++;
		}

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawStatusBar() {
		GUILayout.FlexibleSpace ();
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
			_actors.Clear ();
			_dialogs.Clear ();
			UpdateData ();
		}
	}

	private	void OnClickOpen() {
		string path = EditorUtility.OpenFilePanel ("Open Scenario Data", Application.dataPath, "json");
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
			_actors = new List<ActorData> (_data.actors);
			_dialogs = new List<DialogData> (_data.sequences);
			UpdateData ();
		}
	}

	private	void OnClickSave() {
		UpdateInput ();
		_data.actors = _actors.ToArray ();
		_data.sequences = _dialogs.ToArray ();
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

	private	void OnClickAddActor() {
	}

	private	void OnClickRemoveActor(int index) {
	}

	private	void OnClickAddDialog() {
	}

	private	void OnClickRemoveDialog(int index) {
	}

	private	void UpdateData() {
	}

	private	void UpdateInput() {
	}
}
