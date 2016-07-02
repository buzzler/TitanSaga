using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	private	Transform	_cameraTransform;
	private	Panning		_panning;
	private	PinchZoom	_pinch;
	private	Observer	_observer;

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
		_cameraTransform = Camera.main.transform;
		_panning = _cameraTransform.GetComponent<Panning> ();
		_pinch = _cameraTransform.GetComponent<PinchZoom> ();
		_observer = Observer.Instance;
	}

	void OnDestroy() {
		_cameraTransform = null;
		_observer = null;
	}

	public	Vector3 GetCurrentPosition() {
		return _cameraTransform.localPosition;
	}

	public	Vector3 GetCurrentTilePosition() {
		var pos = _cameraTransform.localPosition;
		return _observer.tileManager.GetTileCoord (pos.x, pos.y, true);
	}

	public	void Lock() {
		_panning.enabled = false;
		_pinch.enabled = false;
	}

	public	void Unlock() {
		_panning.enabled = true;
		_pinch.enabled = true;
	}
}
