using UnityEngine;
using System;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Mono.Data.Sqlite;

enum ReturnType {
	ERROR = 0,
	NONQUERY = 1,
	SCALAR = 2,
	READER = 3
}

public class DBMS : MonoBehaviour {
	private	const string FILENAME		= "main.db";
	private	const string DUMMY_FILENAME	= "default.db";
	private	object						_locker;
	private Queue<ReturnType>			_order;
	private Queue<string>				_sql;
	private	Queue<Action<int>> 			_nonquery;
	private	Queue<Action<object>>		_scalar;
	private	Queue<Action<IDataReader>>	_reader;

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		SetDefault ();
		ClearQuery ();
	}

	void Update() {
		if (_order.Count > 0) {
			Flush ();
		}
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

	private	void ClearQuery() {
		if (_order != null) {
			_order.Clear ();
		}
		if (_sql != null) {
			_sql.Clear ();
		}
		if (_nonquery != null) {
			_nonquery.Clear ();
		}
		if (_scalar != null) {
			_scalar.Clear ();
		}
		if (_reader != null) {
			_reader.Clear ();
		}
		_locker = new object ();
		_order = new Queue<ReturnType> ();
		_sql = new Queue<string> ();
		_nonquery = new Queue<Action<int>> ();
		_scalar = new Queue<Action<object>> ();
		_reader = new Queue<Action<IDataReader>> ();
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

	public	void ExecuteNonQuery(string sql, Action<int> callback) {
		lock (_locker) {
			_sql.Enqueue (sql);
			_nonquery.Enqueue (callback);
			_order.Enqueue (ReturnType.NONQUERY);
		}
	}

	public	void ExecuteScalar(string sql, Action<object> callback) {
		lock (_locker) {
			_sql.Enqueue (sql);
			_scalar.Enqueue (callback);
			_order.Enqueue (ReturnType.SCALAR);
		}
	}

	public	void ExecuteReader(string sql, Action<IDataReader> callback) {
		lock (_locker) {
			_sql.Enqueue (sql);
			_reader.Enqueue (callback);
			_order.Enqueue (ReturnType.READER);
		}
	}

	public	void Flush() {
		if (_order == null || _order.Count == 0) {
			return;
		}

		lock (_locker) {
			var conn = GetConnectionFast ();
			using (IDbConnection dbconn = (IDbConnection)new SqliteConnection (conn)) {
				dbconn.Open ();
				using (IDbCommand dbcmd = dbconn.CreateCommand ()) {

					for (var i = _order.Count; i > 0; i--) {
						var rtype = _order.Dequeue ();
						dbcmd.CommandText = _sql.Dequeue ();
						switch (rtype) {
						case ReturnType.NONQUERY:
							var naction = _nonquery.Dequeue ();
							if (naction != null) {
								naction (dbcmd.ExecuteNonQuery ());
							}
							break;
						case ReturnType.READER:
							var raction = _reader.Dequeue ();
							if (raction != null) {
								raction (dbcmd.ExecuteReader ());
							}
							break;
						case ReturnType.SCALAR:
							var saction = _scalar.Dequeue ();
							if (saction != null) {
								saction (dbcmd.ExecuteScalar ());
							}
							break;
						}
					}
				}
			}
		}
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
