using UnityEditor;
using UnityEngine;
using System.IO;

public class Builder {
	[MenuItem("HyperLuna/Build")]
	public	static void BuildAuto() {
		var path = EditorUtility.SaveFolderPanel ("", "", "");
		if (!string.IsNullOrEmpty (path)) {
			Build (Path.Combine (path, GetDefaultName ()), EditorUserBuildSettings.activeBuildTarget);
		}
	}

	public	static void BuildAutoInterface() {
		string path = Application.dataPath;
		var args = System.Environment.GetCommandLineArgs ();
		var total = args.Length;
		for (int i = 0; i < total; i++) {
			if ((args [i] == "-buildPath") && (i < total - 1)) {
				path = args [i + 1];
				break;
			}
		}
		Build (path, EditorUserBuildSettings.activeBuildTarget);
		EditorApplication.Exit (0);
	}

	private	static void Build(string path, BuildTarget target) {
		if (!string.IsNullOrEmpty (path)) {
			PreProcessor.Run ();
			BuildPipeline.BuildPlayer (GetScenes (), path, target, BuildOptions.Il2CPP);
			PostProcessor.Run (path);
		}
	}

	private	static string GetDefaultName() {
		switch (EditorUserBuildSettings.activeBuildTarget) {
		case BuildTarget.iOS:
			return "Clue_ios";
		case BuildTarget.Android:
			return "Clue_aos.apk";
		default:
			return "Clue";
		}
	}

	private	static EditorBuildSettingsScene[] GetScenes() {
		return EditorBuildSettings.scenes;
	}
}
