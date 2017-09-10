public class StateController : Controller {
	
	public	StateController(Observer observer) : base (observer) {
		observer.OnInited += OnInited;
	}

	private	void OnInited() {
	}

	public	void Backup() {
		observer.actorCtr.Backup ();
		observer.backgroundCtr.Backup ();
		observer.uiCtr.Backup ();
		observer.backgroundCtr.SetInteractivity (false);
	}

	public	void Restore() {
		observer.actorCtr.Restore ();
		observer.backgroundCtr.Restore ();
		observer.uiCtr.Restore ();
	}
}
