using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	private	Camera 		_camera;
	private	Transform	_cameraTransform;

	void Awake() {
		GameObject.DontDestroyOnLoad (gameObject);
		_camera = Camera.main;
		_cameraTransform = _camera.transform;
	}

	public	Vector3 GetCurrentPosition() {
		return _cameraTransform.localPosition;
	}

	public	Vector3 GetCurrentTilePosition() {
		var pos = _cameraTransform.localPosition;
		return Observer.Instance.tileManager.GetVoxelCoord (pos.x, pos.y, true);
	}
}
