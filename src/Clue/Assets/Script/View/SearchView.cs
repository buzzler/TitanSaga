﻿using UnityEngine;
using System.Collections;

public class SearchView : MonoBehaviour {

	public	void Start() {
		var ob = GameObject.FindObjectOfType<Observer> ();
//		ob.actorCtr.Hide ();
		ob.actorCtr.RemoveAll ();
		ob.backgroundCtr.SetInteractivity (true);
	}

	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.backgroundCtr.SetInteractivity (false);
		ob.actorCtr.Restore ();
		ob.backgroundCtr.Restore ();
		ob.uiCtr.Restore ();
//		ob.actorCtr.Show ();
//		ob.uiCtr.Change ("Detective");
	}

	public	void OnClickCrimemap() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		var uv = ob.uiCtr.Change ("Utility").GetComponent<UtilityView>();
		uv.SetCrimeMap ();
	}

	public	void OnClickNotes() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		var uv = ob.uiCtr.Change ("Utility").GetComponent<UtilityView>();
		uv.SetNotes ();
	}

	public	void OnClickSuspicions() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		var uv = ob.uiCtr.Change ("Utility").GetComponent<UtilityView>();
		uv.SetSuspicions ();
	}
}
