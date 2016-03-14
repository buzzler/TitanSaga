using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PinchZoom : MonoBehaviour
{
	public float perspectiveZoomSpeed = 0.5f;
	public float orthoZoomSpeed = 0.5f;
	private	Camera _camera;

	void Awake() {
		_camera = GetComponent<Camera>();
	}

	void Update()
	{
		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			_camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
			_camera.orthographicSize = Mathf.Max(_camera.orthographicSize, 0.1f);
		}
	}
}