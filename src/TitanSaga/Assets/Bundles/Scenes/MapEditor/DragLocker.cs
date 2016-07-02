using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DragLocker : MonoBehaviour {
	public	void OnBeginDrag() {
		Observer.Instance.cameraManager.Lock ();
	}

	public	void OnEndDrag() {
		Observer.Instance.cameraManager.Unlock ();
	}
}
