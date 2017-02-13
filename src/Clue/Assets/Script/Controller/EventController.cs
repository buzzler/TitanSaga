using System.Collections.Generic;

public class EventController : Controller, IController {
	public	delegate void DelegateListener (params object[] list);
	private	List<DelegateListener> _events;

	public	EventController(Observer observer) : base (observer) {
		int total = System.Enum.GetValues (typeof(Events)).Length;
		_events = new List<DelegateListener> (total);
		for (int i = total -1 ; i>=0;i--) {
			_events.Add (null);
		}
	}

	public	void AttachListener() {
	}

	public	void DetachListener() {
	}

	public	void AddEventListener(Events type, DelegateListener action) {
		int index = (int)type;
		if (_events [index] != null) {
			_events [index] += action;
		} else {
			_events [index] = action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListener action) {
		int index = (int)type;
		if (_events [index] != null) {
			_events [index] -= action;
		}
	}

	public	bool HasEventListener(Events type) {
		DelegateListener rootAction = _events [(int)type];
		return (rootAction != null) && (rootAction.GetInvocationList ().Length > 0);
	}

	public	void DispatchEvent(Events type, params object[] list) {
		int index = (int)type;
		if (_events [index] != null) {
			_events [index].Invoke (list);
		}
	}
}
