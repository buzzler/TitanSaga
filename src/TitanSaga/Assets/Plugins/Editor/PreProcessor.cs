using UnityEngine;
using UnityEditor;

public class PreProcessor {
	public	static void Run() {
		if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android) {
		}
		if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS) {
		}
	}
}
