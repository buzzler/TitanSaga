using UnityEngine;

public class BundleController : Controller {
	public	BundleController(Observer observer) : base (observer) {
	}

	public	Object Load (string bundlePath) {
		return Resources.Load (bundlePath);
	}

	public	T Load<T> (string bundlePath) where T : Object {
		return Resources.Load<T> (bundlePath);
	}

	public	T Instantiate<T> (string bundlePath) where T : Object {
		var r = Load<T> (bundlePath);
		if (r != null)
			return GameObject.Instantiate<T> (r);
		return null;
	}

	public	Object Instantiate (string bundlePath) {
		var r = Load (bundlePath);
		if (r != null)
			return GameObject.Instantiate (r);
		return null;
	}

	public	Object Instantiate (string bundlePath, Transform parent) {
		var r = Load (bundlePath);
		if (r != null)
			return GameObject.Instantiate (r, parent);
		return null;
	}

	public	Object Instantiate (string bundlePath, Vector3 position, Quaternion rotation) {
		var r = Load (bundlePath);
		if (r != null)
			return GameObject.Instantiate (r, position, rotation);
		return null;
	}

	public	Object Instantiate (string bundlePath, Transform parent, bool worldPositionStays) {
		var r = Load (bundlePath);
		if (r != null)
			return GameObject.Instantiate (r, parent, worldPositionStays);
		return null;
	}

	public	Object Instantiate (string bundlePath, Vector3 position, Quaternion rotation, Transform parent) {
		var r = Load (bundlePath);
		if (r != null)
			return GameObject.Instantiate (r, position, rotation, parent);
		return null;
	}
}