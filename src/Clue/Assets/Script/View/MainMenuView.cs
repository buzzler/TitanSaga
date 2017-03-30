using UnityEngine;

public class MainMenuView : MonoBehaviour {
	public	void OnClickPlay() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.scenarioCtr.Play ("Assignment");
	}

	public	void OnClickMore() {
	}

	public	void OnClickOptions() {
	}

	public	void OnClickExtras() {
	}

	public	void OnClickHelp() {
	}
}