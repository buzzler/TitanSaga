using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class TEST : MonoBehaviour {
//	void Start() {
//		var dbms = Observer.Instance.dbms;
//		var conn = dbms.GetConnectionFast ();
//
//		using (IDbConnection dbconn = (IDbConnection)new SqliteConnection (conn)) {
//			dbconn.Open ();
//
//			using (IDbCommand dbcmd = dbconn.CreateCommand ()) {
//				dbcmd.CommandText = "SELECT * FROM tile_item";
//				using (IDataReader reader = dbcmd.ExecuteReader ()) {
//					while (reader.Read ()) {
//						string id = reader.GetString (0);
//						string asset = reader.GetString (1);
//						int x = reader.GetInt32 (2);
//						int y = reader.GetInt32 (3);
//						Debug.LogFormat ("id:{0}, asse:{1}, x:{2}, y:{3}", id, asset, x, y);
//					}
//				}
//			}
//		}
//
//	}
}
