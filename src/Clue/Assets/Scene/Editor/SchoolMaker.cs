using UnityEngine;
using UnityEditor;

public class SchoolMaker : EditorWindow {
	[MenuItem("HyperLuna/SchoolMaker")]
	private	static void Open() {
		var window = CreateInstance<SchoolMaker> ();
		window.Show ();
	}
}
