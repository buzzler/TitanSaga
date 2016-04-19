using UnityEngine;
using System.Collections;

public class Panning : MonoBehaviour {
	public	float speed = 0.1F;
	public	Rect bound;

	private	float mult = 0.01f;
	private Vector3 dest = Vector3.zero;
	private	Vector3 prev = Vector3.zero;

	void Start() {
		dest = transform.localPosition;
	}

	void Update() {
		// for touch
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
		}

		// for mouse
		var lp = transform.localPosition;
		var dist = (dest - lp) / 2;
		transform.localPosition += dist;
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
	}
}