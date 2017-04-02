using UnityEngine;

public class AssignmentView : MonoBehaviour {
	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.scenarioCtr.Play ("MainMenu");
	}

	public	void OnClickAssignment(int v) {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.scenarioCtr.Play ("Prologue" + v.ToString ());
	}
}
