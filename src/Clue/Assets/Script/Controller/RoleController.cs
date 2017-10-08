using UnityEngine;
using System.Collections.Generic;

public class RoleController : Controller {
	private	Dictionary<string, SuspectInfo> _dic;

	public RoleController (Observer observer) : base (observer) {
		_dic = new Dictionary<string, SuspectInfo> ();
	}

	public	SuspectInfo GetSuspectInfo(string role) {
		if (_dic.ContainsKey (role)) {
			return _dic [role];
		} else {
			var id = observer.mansionCtr.GetMainSuspect ();
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
