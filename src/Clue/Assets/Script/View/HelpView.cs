using UnityEngine;

public class HelpView : MonoBehaviour {
	public	void OnClickGameRules() {
	}

	public	void OnClickGameControls() {
	}

	public	void OnClickAbout() {
	}

	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Pause");
	}
}
