using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Diagnostics;
using System.IO;

public class DBMSUtility {
	[MenuItem("HyperLuna/DataBase/Default", false, 1)]
	public	static void OpenDefaultDatabaseBrowser() {
		OpenDatabaseBrowser (DBMS.GetDummyFilepath ());
	}

	[MenuItem("HyperLuna/DataBase/Saved", false, 2)]
	public	static void OpenSavedDatabaseBrowser() {
		OpenDatabaseBrowser (DBMS.GetFilepath ());
	}

	[MenuItem("HyperLuna/DataBase/Delete Saved", false, 3)]
	public	static void OpenNewDatabaseBrowser() {
		if (!EditorUtility.DisplayDialog ("Really?", "Saved DB will be deleted", "ok", "cancel")) {
			return;
		}
		var spath = DBMS.GetFilepath ();
		if (File.Exists (spath)) {
			File.Delete (spath);
			EditorUtility.DisplayDialog ("Complete", "Saved DB was deleted", "ok");
		} else {
			EditorUtility.DisplayDialog ("Error", "Saved File not Found", "ok");
		}
	}

	[MenuItem("HyperLuna/DataBase/Default->Saved", false, 4)]
	public	static void TransferDtoS() {
		if (!EditorUtility.DisplayDialog ("Really?", "Saved DB will be deleted", "ok", "cancel")) {
			return;
		}
		var dpath = DBMS.GetDummyFilepath ();
		var spath = DBMS.GetFilepath ();
		if (Transfer (dpath, spath)) {
			EditorUtility.DisplayDialog ("Complete", "Default DB to Saved", "ok");
		} else {
			EditorUtility.DisplayDialog ("Error", "Default File not Found", "ok");
		}
	}

	[MenuItem("HyperLuna/DataBase/Saved->Default", false, 5)]
	public	static void TransferStoD() {
		if (!EditorUtility.DisplayDialog ("Really?", "Default DB will be deleted", "ok", "cancel")) {
			return;
		}
		var dpath = DBMS.GetDummyFilepath ();
		var spath = DBMS.GetFilepath ();
		if (Transfer (spath, dpath)) {
			EditorUtility.DisplayDialog ("Complete", "Saved DB to Default", "ok");
		} else {
			EditorUtility.DisplayDialog ("Error", "Saved File not Found", "ok");
		}
	}

	private	static void OpenDatabaseBrowser(string args = null) {
		#if UNITY_EDITOR_OSX
		Process process = new Process ();
		process.StartInfo.FileName = "/Applications/DB Browser for SQLite.app/Contents/MacOS/DB Browser for SQLite";
		process.StartInfo.UseShellExecute = false;
		if (args != null) {
			process.StartInfo.Arguments = DBMS.GetDummyFilepath ();
		}
		process.Start();
		#endif
	}

	private	static bool Transfer(string from, string to) {
		if (!File.Exists (from)) {
			return false;
		}

		if (File.Exists (to)) {
			File.Delete (to);
		}

		File.Copy (from, to);
		return true;
	}
}
