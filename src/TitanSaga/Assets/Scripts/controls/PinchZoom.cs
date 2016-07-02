using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PinchZoom : MonoBehaviour
{
	public	float orthoZoomSpeed = 0.01f;
	public	float orthoSize = 3f;
	private	Camera _camera;

	void Awake() {
		_camera = GetComponent<Camera>();
	}

	void Update()
	{
		if (Input.touchCount == 2) {
			Touch touchZero = Input.touches [0];
			Touch touchOne = Input.touches [1];

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			_camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
			_camera.orthographicSize = Mathf.Clamp (_camera.orthographicSize, 1f, 5f);
		} else if (_camera.orthographicSize != orthoSize) {
			var delta = (orthoSize - _camera.orthographicSize) * orthoZoomSpeed;
			if (delta < 0.0001) {
				_camera.orthographicSize = orthoSize;
			} else {
				_camera.orthographicSize += delta;
			}
		}
	}
}