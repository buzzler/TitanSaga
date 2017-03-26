using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Observer : MonoBehaviour {
	public	delegate void EventDelegate();

	public	ActorController			actorCtr;
	public	AudioController			audioCtr;
	public	BackgroundController	backgroundCtr;
	public	BundleController		bundleCtr;
	public	CameraController		cameraCtr;
	public	DialogController		dialogCtr;
	public	FaderController			faderCtr;
	public	FileController			fileCtr;
	public	NetworkController		networkCtr;
	public	ScenarioController		scenarioCtr;
	public	UIController			uiCtr;

	public	EventDelegate OnInited;
	public	EventDelegate OnLoop;

	void Awake() {
		actorCtr		= new ActorController (this);
		audioCtr		= new AudioController (this);
		backgroundCtr	= new BackgroundController (this);
		bundleCtr		= new BundleController (this);
		cameraCtr		= new CameraController (this);
		dialogCtr		= new DialogController (this);
		faderCtr		= new FaderController (this);
		fileCtr			= new FileController (this);
		networkCtr		= new NetworkController (this);
		scenarioCtr		= new ScenarioController (this);
		uiCtr			= new UIController (this);
	}

	void Start() {
		DontDestroyOnLoad (gameObject);
		if (OnInited != null)
			OnInited.Invoke ();
	}

	void Update() {
		if (OnLoop != null)
			OnLoop.Invoke ();
	}
}
