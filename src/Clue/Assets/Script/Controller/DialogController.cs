using System.Collections.Generic;

public class DialogController : Controller {
	private	DialogView		_view;
	private Queue<string>	_speakers;
	private Queue<string>	_comments;
	private Queue<float>	_speeds;

	public	DialogController(Observer observer) : base (observer) {
		_view		= null;
		_speakers	= new Queue<string> ();
		_comments	= new Queue<string> ();
		_speeds		= new Queue<float> ();
	}

	public	void Show(string speaker, string comment, float speed = 0.03f) {
		if (_view == null) {
			var tr = observer.uiCtr.Add ("Dialog");
			_view = tr.GetComponent<DialogView> ();
			_view.SetCallback (OnClick);
			_view.Dialog (speaker, comment, speed);
		} else {
			_speakers.Enqueue (speaker);
			_comments.Enqueue (comment);
			_speeds.Enqueue (speed);
		}
	}

	public	void Hide() {
		if (_view != null) {
			_view.SetCallback (null);
			_view = null;
			_speakers.Clear ();
			_comments.Clear ();
			_speeds.Clear ();
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

		if (_comments.Count > 0) {
			_view.Dialog (_speakers.Dequeue (), _comments.Dequeue (), _speeds.Dequeue ());
		} else {
			Hide ();
		}
	}
}
	