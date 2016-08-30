using UnityEngine;
using System;
using System.Data;
using System.IO;
using System.Diagnostics;
using Mono.Data.Sqlite;

public class DBMS : MonoBehaviour {
	private	const string FILENAME		= "main.db";
	private	const string DUMMY_FILENAME	= "default.db";

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		SetDefault ();
	}

	public	void SetDefault(bool forced = false) {
		var filepath = GetFilepath ();
		if (File.Exists (filepath)) {
			if (forced) {
				File.Delete (filepath);
			} else {
				return;
			}
		}

		var dummy = GetDummyFilepath ();
		if (Application.platform == RuntimePlatform.Android) {
			using (WWW www = new WWW (dummy)) {
				while (!www.isDone) {
				}
				File.WriteAllBytes (filepath, www.bytes);
			}
		} else {
			File.Copy(dummy, filepath);
		}
	}

	public	string GetConnectionSimple() {
		return string.Format ("URI=file:{0}", GetFilepath ());
	}

	public	string GetConnectionFast() {
		SqliteConnectionStringBuilder sb = new SqliteConnectionStringBuilder();
		sb.DataSource = GetFilepath ();
		sb.DefaultTimeout = 5000;
		sb.SyncMode = SynchronizationModes.Off;
		sb.JournalMode = SQLiteJournalModeEnum.Off;
		sb.PageSize = 65536;
		sb.CacheSize = 16777216;
		sb.FailIfMissing = false;
		sb.ReadOnly = false;
		return sb.ConnectionString;
	}

	public	static string GetFilepath() {
		return string.Format ("{0}/{1}", GetRoot(), FILENAME);
	}

	public	static string GetDummyFilepath() {
		return string.Format ("{0}/{1}", GetDummyRoot(), DUMMY_FILENAME);
	}

	public	static string GetRoot() {
		if (Application.platform == RuntimePlatform.Android) {
			return Application.persistentDataPath;
		} else {
			return Application.temporaryCachePath;
		}
	}

	public	static string GetDummyRoot() {
		switch (Application.platform) {
		case RuntimePlatform.Android:
			return string.Format ("jar:file://{0}!/assets", Application.dataPath);
		case RuntimePlatform.IPhonePlayer:
			return string.Format ("{0}/Raw", Application.dataPath);
		default:
			return Application.streamingAssetsPath;
		}
	}
}
