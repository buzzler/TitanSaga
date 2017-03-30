using UnityEngine;

public class CameraController : Controller {
	private	Camera _actor;
	private	Camera _background;
	private	Transform _actorGroup;
	private Transform _backgroundGroup;

	public	CameraController(Observer observer) : base (observer) {
		_actor = GameObject.Find ("ActorCamera").GetComponent<Camera> ();
		_background = GameObject.Find ("BackgroundCamera").GetComponent<Camera> ();
		_actorGroup = null;
		_backgroundGroup = null;
	}

	public	void EnableActorCamera() {
		_actor.enabled = true;
	}

	public	void DisableActorCamera() {
		_actor.enabled = false;
	}

	public	Transform CreateActorGroup() {
		if (_actorGroup != null)
			return _actorGroup;
		else
			return _actorGroup = CreateGroup ("ActorGroup", _actor);
	}

	public	void EnableBackgroundCamera() {
		_background.enabled = true;
	}

	public	void DisableBackgroundCamera() {
		_background.enabled = false;
	}

	public	Transform CreateBackgroundGroup() {
		if (_backgroundGroup != null)
			return _backgroundGroup;
		else
			return _backgroundGroup = CreateGroup ("BackgroundGroup", _background);
	}

	private	Transform CreateGroup(string name, Camera camera) {
		var go = GameObject.Find (name);
		if (go == null)
			go = new GameObject (name);
		var tr = go.transform;
		var position = camera.transform.position;
		tr.position = new Vector3 (position.x, position.y, 0f);
		return tr;
	}
}