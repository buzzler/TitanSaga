using UnityEngine;

public class ExitConfirm : MonoBehaviour {
	public	void OnClickYes() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.mansionCtr.Clear ();
		ob.scenarioCtr.Play ("MainMenu");
	}

	public	void OnClickNo() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Pause");
	}
}
