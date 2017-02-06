using UnityEngine;

public class CameraController : IController {
	private	Camera actor;
	private	Camera background;

	public	void OnInit() {
		System.Console.WriteLine ("CameraController.OnInit");
		actor = GameObject.Find ("ActorCamera").GetComponent<Camera> ();
		background = GameObject.Find ("BackgroundCamera").GetComponent<Camera> ();
	}

	public	void OnUpdate() {
	}

	public	void EnableActorCamera() {
		actor.enabled = true;
	}

	public	void DisableActorCamera() {
		actor.enabled = false;
	}

	public	void EnableBackgroundCamera() {
		background.enabled = true;
	}

	public	void DisableBackgroundCamera() {
		background.enabled = false;
	}
}
