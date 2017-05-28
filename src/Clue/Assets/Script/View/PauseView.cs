using UnityEngine;

public class PauseView : MonoBehaviour {
	public	void OnClickResume() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Detective");
	}

	public	void OnClickOptions() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Options");
	}

	public	void OnClickHelp() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Help");
	}

	public	void OnClickMainMenu() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("ExitConfirm");
	}
}
