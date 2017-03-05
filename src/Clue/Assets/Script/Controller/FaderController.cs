using UnityEngine;
using System;

public class FaderController : Controller
{
	private	Transform _tr;
	private	Action _callback;

	public	FaderController(Observer observer) : base (observer) {
		_tr = null;
		_callback = null;
		observer.OnInited += OnInited;
	}

	private	void OnInited() {
		_tr = observer.uiCtr.Add ("Fader");
	}

	public	void FadeOut(Action callback = null) {
		_callback = callback;
		LeanTween.color (_tr as RectTransform, Color.black, 0.5f).setEaseInOutCubic ().setOnComplete (OnFade);
	}

	public	void FadeIn(Action callback = null) {
		_callback = callback;
		LeanTween.color (_tr as RectTransform, Color.clear, 1f).setEaseInOutCubic ().setOnComplete (OnFade);
	}

	private	void OnFade() {
		if (_callback != null) {
			Action cb = _callback;
			_callback = null;
			cb.Invoke ();
		}
	}
}

