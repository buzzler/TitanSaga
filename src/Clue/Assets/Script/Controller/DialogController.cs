public class DialogController : Controller {
	public	DialogController(Observer observer) : base (observer) {
	}

	public	void Show(string speaker, string comment, float speed = 0.03f) {
		var tr = observer.uiCtr.Add ("Dialog");
		var view = tr.GetComponent<DialogView> ();
		view.SetDialog (speaker, comment, speed);
	}

	public	void Hide() {
		observer.uiCtr.Remove ("Dialog");
	}
}
	