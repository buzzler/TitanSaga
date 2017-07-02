using UnityEngine;
using UnityEngine.UI;

public class MoveView : MonoBehaviour {
	public	Button[] buttons;
	public	Text roomName;
	public	Text[] evidences;
	public	Text suspect;

	public	int current;
	public	int selected;

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

	public	void OnClickRoom(int index) {
		Debug.LogFormat ("Select Room {0}", index);
	}
}

