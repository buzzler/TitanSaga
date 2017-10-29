using UnityEngine;
using System.Collections.Generic;

public class RoleController : Controller {
	private	Dictionary<string, SuspectInfo> _dic;
	private	List<SuspectInfo> _males;
	private	List<SuspectInfo> _females;
	private	string[] _roleMales;
	private	string[] _roleFemales;

	public RoleController (Observer observer) : base (observer) {
		_dic = new Dictionary<string, SuspectInfo> ();
		_males = new List<SuspectInfo> ();
		_females = new List<SuspectInfo> ();
		_roleMales = new string[] {
			Role.MALE_1, Role.MALE_2, Role.MALE_3
		};
		_roleFemales = new string[] {
			Role.FEMALE_1, Role.FEMALE_2, Role.FEMALE_3
		};

		var suspects = observer.globalCtr.GetAllSuspects ();
		for (int i = 0; i < suspects.Length; i++) {
			var suspect = suspects [i];
			if (!string.IsNullOrEmpty(suspect.role) && suspect.role != Role.NONE)
				SetRole (suspects [i]);
		}
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

	public	void SetRole(SuspectInfo info) { 
		if (string.IsNullOrEmpty(info.role) || info.role != Role.NONE) {
			if (!_dic.ContainsKey (info.role))
				_dic.Add (info.role, info);
			return;
		}

		if (info.gender != ActorGender.NONE && info.role == Role.NONE) {
			if (info.gender == ActorGender.MALE) {
				_males.Add (info);
				info.role = _roleMales [_males.Count - 1];


			} else if (info.gender == ActorGender.FEMALE) {
				_females.Add (info);
				info.role = _roleFemales [_females.Count - 1];
			} else
				return;
			Debug.LogFormat ("{0}'s role assigned to {1}", info.name, info.role);
		}
	}
}
