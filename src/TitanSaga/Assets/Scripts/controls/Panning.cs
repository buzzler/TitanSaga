using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour {
	private	Transform _transform;
	private Vector3 _dest = Vector3.zero;
	private	Vector3 _prev = Vector3.zero;
	private	float _mult = 0.01f;
	private	Rect _bound;

	void Start() {
		_transform = transform;
		_dest = _transform.localPosition;
	}
		
	void Update() {
		var lp = _transform.localPosition;
		var dist = (_dest - lp) / 2;
		_transform.localPosition += dist;

		#if UNITY_EDITOR
		if (Input.GetButton("Fire1")) {
			if (_prev == Vector3.zero) {
				_prev = Input.mousePosition;
			}
			var cp = Input.mousePosition;
			_bound = Observer.Instance.tileManager.bound;
			_dest += (_prev - cp) * _mult;

			_dest.x = Mathf.Clamp(_dest.x, _bound.xMin, _bound.xMax);
			_dest.y = Mathf.Clamp(_dest.y, _bound.yMin, _bound.yMax);
			_prev = cp;
		} else {
			_prev = Vector3.zero;
		}
		#else
		if (Input.touchCount > 0) {
			var touch = Input.touches [0];
			if (_prev == Vector3.zero) {
				_prev = touch.position;
			}
			var cp = touch.position;
			_bound = Observer.Instance.tileManager.bound;

			_dest.x += (_prev.x - cp.x) * _mult;
			_dest.y += (_prev.y - cp.y) * _mult;
			_dest.x = Mathf.Clamp(_dest.x, _bound.xMin, _bound.xMax);
			_dest.y = Mathf.Clamp(_dest.y, _bound.yMin, _bound.yMax);
			_prev = cp;
		} else {
			_prev = Vector3.zero;
		}
		#endif
	}
}