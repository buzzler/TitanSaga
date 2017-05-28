using UnityEngine;
using UnityEngine.UI;

public class OptionsView : MonoBehaviour {
	public	Toggle toggleGameMusic;
	public	Toggle toggleMyMusic;
	public	Toggle toggleSoundFx;
	public	Toggle toggleVibration;
	public	Toggle toggleGameTips;

	public	void OnClickGameMusic() {
		toggleGameMusic.isOn = !toggleGameMusic.isOn;
	}

	public	void OnClickMyMusic() {
		toggleMyMusic.isOn = !toggleMyMusic.isOn;
	}

	public	void OnClickSoundFX() {
		toggleSoundFx.isOn = !toggleSoundFx.isOn;
	}

	public	void OnClickVibration() {
		toggleVibration.isOn = !toggleVibration.isOn;
	}

	public	void OnClickGameTips() {
		toggleGameTips.isOn = !toggleGameTips.isOn;
	}

	public	void OnClickBack() {
		var ob = GameObject.FindObjectOfType<Observer> ();
		ob.uiCtr.Change ("Pause");
	}
}
