using UnityEngine;

public class CameraController : Controller, IController {
	private	Camera actor;
	private	Camera background;

	public	CameraController(Observer observer) : base (observer) {
		actor = GameObject.Find ("ActorCamera").GetComponent<Camera> ();
		background = GameObject.Find ("BackgroundCamera").GetComponent<Camera> ();
	}

	public	void AttachListener() {
	}

	public	void DetachListener() {
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