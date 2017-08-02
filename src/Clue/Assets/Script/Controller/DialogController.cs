using System;
using System.Collections.Generic;

public class DialogController : Controller {
	private	DialogView		_view;
	private	Action			_callback;

	public	DialogController(Observer observer) : base (observer) {
		_view		= null;
		_callback	= null;
	}

	public	void SetCallback(Action callback) {
		_callback = callback;
	}

	public	void Show(string speaker, string comment, float speed = 0.03f) {
		if (_view == null) {
			var tr = observer.uiCtr.Add ("Dialog");
			_view = tr.GetComponent<DialogView> ();
			_view.SetCallback (OnClick);
			_view.Dialog (speaker, comment, speed);
		} else {
			_view.Dialog (speaker, comment, speed);
		}
	}

	public	void Hide() {
		if (_view != null) {
			_view.SetCallback (null);
			_view = null;
			observer.uiCtr.Remove ("Dialog");
		}
	}

	private	void OnClick() {
		if (_view == null)
			return;

		if (_view.IsPlaying ()) {
			_view.Skip ();
			return;
		}

		if (_callback != null) {
			_callback.Invoke ();
//			Hide ();
//		} else {
//			Hide ();
		}
	}
}
