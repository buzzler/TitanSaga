using UnityEngine;

public class UtilityView : MonoBehaviour {
	public	GameObject tabCrimeMap;
	public	GameObject tabNotes;
	public	GameObject tabSuspicions;

	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.stateCtr.Restore ();
	}

	public	void SetCrimeMap() {
		tabCrimeMap.SetActive (true);
		tabNotes.SetActive (false);
		tabSuspicions.SetActive (false);
	}

	public	void SetNotes() {
		tabCrimeMap.SetActive (false);
		tabNotes.SetActive (true);
		tabSuspicions.SetActive (false);
	}

	public	void SetSuspicions() {
		tabCrimeMap.SetActive (false);
		tabNotes.SetActive (false);
		tabSuspicions.SetActive (true);
	}
}
