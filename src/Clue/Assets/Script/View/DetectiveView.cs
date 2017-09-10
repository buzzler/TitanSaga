﻿using UnityEngine;

public	class DetectiveView : MonoBehaviour {
	public	void OnClickPause() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Pause");
	}

	public	void OnClickEditor() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Editor");
	}

	public	void OnClickMove() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Move");
	}

	public	void OnClickSearch() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Search");
	}

	public	void OnClickTalk() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.mansionCtr.TalkSuspect (ob.mansionCtr.GetCurrentSuspect ());
//		ob.scenarioCtr.Resume ();
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
