using UnityEngine;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour {
	public	AudioController		audioCtr;
	public	BundleController	bundleCtr;
	public	UIController		uiCtr;
	public	CameraController	cameraCtr;
	public	EventController		eventCtr;
	private	IController[]		_controllers;

	void Awake() {
		_controllers	= new IController[] {
			audioCtr	= new AudioController (this),
			bundleCtr	= new BundleController (this),
			cameraCtr	= new CameraController (this),
			eventCtr	= new EventController (this),
			uiCtr		= new UIController (this)
		};
	}

	void OnEnable() {
		int total = _controllers.Length;
		for (int i = 0; i < total; i++) {
			_controllers [i].AttachListener ();
		}
	}

	void OnDisable() {
		int total = _controllers.Length;
		for (int i = 0; i < total; i++) {
			_controllers [i].DetachListener ();
		}
	}

	void Start() {
		eventCtr.DispatchEvent(Events.UI_ADD, "Debug");
	}
}
