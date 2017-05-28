using UnityEngine;
using System.Collections;

public class SearchView : MonoBehaviour {
	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Detective");
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
