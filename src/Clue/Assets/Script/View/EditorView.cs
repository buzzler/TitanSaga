using UnityEngine;

public class EditorView : MonoBehaviour {
	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Detective");
	}

	public	void OnClickAccusation() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Accusation");
	}

	public	void OnClickHint() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Hint");
	}

	public	void OnClickExit() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("ExitConfirm");
	}
}
