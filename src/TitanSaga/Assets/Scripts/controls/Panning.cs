using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour {
	public	float speed = 0.1F;
	public	Rect bound;

	private Vector3 dest = Vector3.zero;
	private	Vector3 prev = Vector3.zero;
	private	float mult = 0.01f;

	void Start() {
		dest = transform.localPosition;
	}
		
	void Update() {
		var lp = transform.localPosition;
		var dist = (dest - lp) / 2;
		transform.localPosition += dist;

		#if UNITY_EDITOR
		if (Input.GetButton("Fire1")) {
			if (prev == Vector3.zero) {
				prev = Input.mousePosition;
			}
			var cp = Input.mousePosition;
			bound = Observer.Instance.tileManager.bound;
			dest += (prev - cp) * mult;

			dest.x = Mathf.Clamp(dest.x, bound.xMin, bound.xMax);
			dest.y = Mathf.Clamp(dest.y, bound.yMin, bound.yMax);
			prev = cp;
		} else {
			prev = Vector3.zero;
		}
		#else
		if (Input.touchCount > 0) {
			var touch = Input.touches [0];
			if (prev == Vector3.zero) {
				prev = touch.position;
			}
			var cp = touch.position;
			bound = Observer.Instance.tileManager.bound;

			dest.x += (prev.x - cp.x) * mult;
			dest.y += (prev.y - cp.y) * mult;
			dest.x = Mathf.Clamp(dest.x, bound.xMin, bound.xMax);
			dest.y = Mathf.Clamp(dest.y, bound.yMin, bound.yMax);
			prev = cp;
		} else {
			prev = Vector3.zero;
		}
		#endif
	}
}