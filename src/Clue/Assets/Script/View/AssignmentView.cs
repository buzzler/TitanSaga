using UnityEngine;

public class AssignmentView : MonoBehaviour {
	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.scenarioCtr.Play ("MainMenu");
	}
}
