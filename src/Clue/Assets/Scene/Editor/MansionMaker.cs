using UnityEngine;
using UnityEditor;

public class MansionMaker : EditorWindow {
	[MenuItem("HyperLuna/MansionMaker")]
	private	static void Open() {
		var window = CreateInstance<MansionMaker> ();
		window.Show ();
	}

	private	string		_path;
	private	SceneData	_data;

	void OnGUI() {
		DrawToolbar ();
		GUILayout.Space (2);
		DrawMansionData ();
		GUILayout.Space (2);
		DrawRoomData ();
		GUILayout.Space (2);
		DrawEvidenceData ();
	}

	private	void DrawToolbar() {
		GUILayout.BeginHorizontal (EditorStyles.toolbar);
		if (GUILayout.Button ("New", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickNew ();
		if (GUILayout.Button ("Open...", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickOpen ();
		if (GUILayout.Button ("Save", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickSave ();
		if (GUILayout.Button ("Save As...", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickSaveAs ();
		GUILayout.FlexibleSpace ();
		if (GUILayout.Button ("Rescan", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickRescan ();
		if (GUILayout.Button ("Play", EditorStyles.toolbarButton, GUILayout.Width (90)))
			OnClickPlay ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawMansionData() {
		GUILayout.BeginHorizontal ();
		GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width(90));
		GUILayout.TextField ("");
		GUILayout.EndHorizontal ();
	}

	private	void DrawRoomData() {
		GUILayout.BeginHorizontal ();
		GUILayout.BeginHorizontal (GUILayout.Width (90));
		GUILayout.Toggle (true, "rooms");
		GUILayout.Button ("+", EditorStyles.miniButton, GUILayout.Width (20));
		GUILayout.EndHorizontal ();
		GUILayout.Space (4);
		GUILayout.BeginVertical ();

		GUILayout.BeginHorizontal ();
		GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20));
		GUILayout.Button ("name", EditorStyles.miniButton, GUILayout.Width (90));
		GUILayout.Button ("location", EditorStyles.miniButton);
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Button ("-", EditorStyles.miniButton, GUILayout.Width (20));
		GUILayout.TextField ("name", GUILayout.Width (90));
		EditorGUILayout.Vector2Field ("", Vector2.zero);
		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();
	}

	private	void DrawEvidenceData() {
	}

	public	void OnClickNew() {
	}

	public	void OnClickOpen() {
	}

	public	void OnClickSave() {
	}

	public	void OnClickSaveAs() {
	}

	public	void OnClickRescan() {
	}

	public	void OnClickPlay() {
	}
}
