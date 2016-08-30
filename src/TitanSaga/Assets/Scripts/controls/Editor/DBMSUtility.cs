using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Diagnostics;

public class DBMSUtility {
	[MenuItem("HyperLuna/DataBase/Dummy", false, 1)]
	public	static void OpenDummyDatabaseBrowser() {
		OpenDatabaseBrowser (DBMS.GetDummyFilepath ());
	}

	[MenuItem("HyperLuna/DataBase/Saved", false, 2)]
	public	static void OpenSavedDatabaseBrowser() {
		OpenDatabaseBrowser (DBMS.GetFilepath ());
	}

	[MenuItem("HyperLuna/DataBase/New", false, 3)]
	public	static void OpenNewDatabaseBrowser() {
		OpenDatabaseBrowser ();
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
}
