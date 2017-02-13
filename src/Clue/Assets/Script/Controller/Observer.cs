using UnityEngine;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour {
	public	AudioController		ac;
	public	UIController		uc;
	public	CameraController	cc;
	public	EventController		ec;

	void Awake() {
		ac = new AudioController ();
		uc = new UIController ();
		cc = new CameraController ();
		ec = new EventController ();

		ac.OnInit ();
		uc.OnInit ();
		cc.OnInit ();
		ec.OnInit ();
	}

	void Start() {
		uc.AddUI ("Debug");
	}

	void Update() {
		ac.OnUpdate ();
		uc.OnUpdate ();
		cc.OnUpdate ();
		ec.OnUpdate ();
	}
}
