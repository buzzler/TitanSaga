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
	public	MansionController		mansionCtr;
	public	GlobalDBController		globalCtr;
	public	UIController			uiCtr;
	public	StateController			stateCtr;

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
		mansionCtr		= new MansionController (this);
		globalCtr		= new GlobalDBController (this);
		uiCtr			= new UIController (this);
		stateCtr		= new StateController (this);
	}

	void Start() {
		DontDestroyOnLoad (gameObject);
		if (OnInited != null)
			OnInited.Invoke ();

		scenarioCtr.Play ("MainMenu");
	}

	void Update() {
		if (OnLoop != null)
			OnLoop.Invoke ();
	}
}
