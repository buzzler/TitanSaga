using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PinchZoom : MonoBehaviour
{
	public	float orthoSize = 2.5f;

	private	Camera _camera;
	private	bool	_zoom;
	private	float	_ortho;
	private	float	_magnitude;

	void Awake() {
		_camera = GetComponent<Camera>();
		_zoom = false;
		_ortho = orthoSize;
		_magnitude = 0f;
	}

	void Update()
	{
		#if UNITY_EDITOR
		if ((!_zoom) && Input.GetKey(KeyCode.LeftShift)) {
			BeginZoom ();
		} else if (_zoom && (!Input.GetKey(KeyCode.LeftShift))) {
			EndZoom ();
		} else if (_zoom && Input.GetKey(KeyCode.LeftShift)) {
			if (Input.GetKey(KeyCode.Equals)) {
				_camera.orthographicSize = Mathf.Clamp (_ortho + 1.5f, 1f, 4.5f);
			} else if (Input.GetKey(KeyCode.Minus)) {
				_camera.orthographicSize = Mathf.Clamp (_ortho - 1.5f, 1f, 4.5f);
			} else {
				_camera.orthographicSize = Mathf.Clamp (_ortho, 1f, 4.5f);
			}
		} else if ((!_zoom) && (!Input.GetKey(KeyCode.LeftShift)) && _camera.orthographicSize != orthoSize) {
			ReleaseZoom ();
		}
		#else
		int tc = Input.touchCount;
		if ((!_zoom) && tc == 2) {
			BeginZoom ();
		} else if (_zoom && tc != 2) {
			EndZoom ();
		} else if (_zoom && tc == 2) {
			HoldZoom ();
		} else if ((!_zoom) && tc != 2 && _camera.orthographicSize != orthoSize) {
			ReleaseZoom ();
		}
		#endif
	}

	private	void BeginZoom() {
		if (!_zoom) {
			_zoom = true;
			_ortho = _camera.orthographicSize;
			_magnitude = GetMagnitude ();
		}
	}

	private	void EndZoom() {
		if (_zoom) {
			_zoom = false;
			_magnitude = 0f;
		}
	}

	private	void HoldZoom() {
		var current = GetMagnitude ();
		var inch = (_magnitude - current) / Screen.dpi;
		var rate = inch / 2f;
		var delta = rate * 1.5f;
		_camera.orthographicSize = Mathf.Clamp (_ortho + delta, 1f, 4.5f);
	}

	private	void ReleaseZoom() {
		var molph = (orthoSize - _camera.orthographicSize) * 0.005f;
		if (molph < 0.00101) {
			_camera.orthographicSize += molph;
		} else {
			_camera.orthographicSize = orthoSize;
		}
	}

	private	float GetMagnitude() {
		if (Input.touchCount >= 2) {
			Touch t1 = Input.touches [0];
			Touch t2 = Input.touches [1];
			return (t1.position - t2.position).magnitude;
		} else {
			return 0f;
		}
	}
}
