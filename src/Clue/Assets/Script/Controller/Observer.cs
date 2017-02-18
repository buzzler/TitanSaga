using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Observer : MonoBehaviour {
	public	ActorController			actorCtr;
	public	AudioController			audioCtr;
	public	BackgroundController	backgroundCtr;
	public	BundleController		bundleCtr;
	public	CameraController		cameraCtr;
	public	DialogController		dialogCtr;
	public	UIController			uiCtr;

	void Awake() {
		actorCtr		= new ActorController (this);
		audioCtr		= new AudioController (this);
		backgroundCtr	= new BackgroundController (this);
		bundleCtr		= new BundleController (this);
		cameraCtr		= new CameraController (this);
		dialogCtr		= new DialogController (this);
		uiCtr			= new UIController (this);
	}

	void Start() {
		uiCtr.Add ("Debug");
	}
}
