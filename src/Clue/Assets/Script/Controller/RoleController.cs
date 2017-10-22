using UnityEngine;
using System.Collections.Generic;

public class RoleController : Controller {
	private	Dictionary<string, SuspectInfo> _dic;

	public RoleController (Observer observer) : base (observer) {
		_dic = new Dictionary<string, SuspectInfo> ();
		var suspects = observer.globalCtr.GetAllSuspects ();
		for (int i = 0; i < suspects.Length; i++) {
			var s = suspects [i];
			if (s.role != Role.NONE)
				_dic.Add (s.role, s);
		}
	}

	public	SuspectInfo GetSuspectInfo(string role) {
		Debug.LogFormat ("Role : {0}", role);
		if (_dic.ContainsKey (role)) {
			return _dic [role];
		} else {
			var id = observer.mansionCtr.GetMainSuspect ();
			Debug.LogFormat ("MainSuspect : {0}", id);
			var info = observer.globalCtr.GetSuspect (id);

			if (Debug.isDebugBuild)
				Debug.LogFormat ("{0}({1})'s role assigned from {2} to {3}", info.name, info.id, info.role, role);
			if (info.role != Role.NONE && info.role != role)
				Debug.LogWarningFormat ("wrong assigned!");
			info.role = role;
			_dic.Add (role, info);
			return info;
		}
	}
}
