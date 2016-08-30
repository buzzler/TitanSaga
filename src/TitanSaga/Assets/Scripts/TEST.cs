using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class TEST : MonoBehaviour {
	void Start() {
		var dbms = Observer.Instance.dbms;
		var conn = dbms.GetConnectionFast ();

		using (IDbConnection dbconn = (IDbConnection)new SqliteConnection (conn)) {
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ()) {
				dbcmd.CommandText = "SELECT id, name, sequence FROM firsttable";

				using (IDataReader reader = dbcmd.ExecuteReader ()) {

					while (reader.Read ()) {
						int id = reader.GetInt32 (0);
						string name = reader.GetString (1);
						int seq = reader.GetInt32 (2);

						Debug.Log ("id= " + id + " name =" + name + " seq =" + seq);
					}
				}
			}
		}

	}
}
