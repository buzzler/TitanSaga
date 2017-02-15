using System.Collections.Generic;
/*
public class EventController : Controller, IController {
	public	delegate void DelegateListener ();
	public	delegate void DelegateListenerBool (bool value);
	public	delegate void DelegateListenerInt (int value);
	public	delegate void DelegateListenerFloat (float value);
	public	delegate void DelegateListenerObject (object value);
	public	delegate void DelegateListenerParams (params object[] values);
	public	delegate void DelegateListenerString (string value);

	private Dictionary<Events, DelegateListener>		_dic;
	private Dictionary<Events, DelegateListenerBool>	_dicBool;
	private Dictionary<Events, DelegateListenerInt>		_dicInt;
	private Dictionary<Events, DelegateListenerFloat>	_dicFloat;
	private Dictionary<Events, DelegateListenerObject>	_dicObject;
	private	Dictionary<Events, DelegateListenerParams>	_dicParams;
	private Dictionary<Events, DelegateListenerString>	_dicString;

	public	EventController(Observer observer) : base (observer) {
		_dic = new Dictionary<Events, DelegateListener> ();
		_dicBool = new Dictionary<Events, DelegateListenerBool> ();
		_dicFloat = new Dictionary<Events, DelegateListenerFloat> ();
		_dicInt = new Dictionary<Events, DelegateListenerInt> ();
		_dicObject = new Dictionary<Events, DelegateListenerObject> ();
		_dicParams = new Dictionary<Events, DelegateListenerParams> ();
		_dicString = new Dictionary<Events, DelegateListenerString> ();
	}

	public	void AttachListener() {
	}

	public	void DetachListener() {
	}

	public	void AddEventListener(Events type, DelegateListener action) {
		if (_dic.ContainsKey (type)) {
			_dic [type] += action;
		} else {
			_dic.Add (type, action);
		}
	}

	public	void AddEventListener(Events type, DelegateListenerBool action) {
		if (_dicBool.ContainsKey (type)) {
			_dicBool [type] += action;
		} else {
			_dicBool.Add (type, action);
		}
	}

	public	void AddEventListener(Events type, DelegateListenerFloat action) {
		if (_dicFloat.ContainsKey (type)) {
			_dicFloat [type] += action;
		} else {
			_dicFloat.Add (type, action);
		}
	}
	
	public	void AddEventListener(Events type, DelegateListenerInt action) {
		if (_dicInt.ContainsKey (type)) {
			_dicInt [type] += action;
		} else {
			_dicInt.Add (type, action);
		}
	}

	public	void AddEventListener(Events type, DelegateListenerObject action) {
		if (_dicObject.ContainsKey (type)) {
			_dicObject [type] += action;
		} else {
			_dicObject.Add (type, action);
		}
	}

	public	void AddEventListener(Events type, DelegateListenerParams action) {
		if (_dicParams.ContainsKey (type)) {
			_dicParams [type] += action;
		} else {
			_dicParams.Add (type, action);
		}
	}

	public	void AddEventListener(Events type, DelegateListenerString action) {
		if (_dicString.ContainsKey (type)) {
			_dicString [type] += action;
		} else {
			_dicString.Add (type, action);
		}
	}

	public	void RemoveEventListener(Events type, DelegateListener action) {
		if (_dic.ContainsKey (type)) {
			_dic [type] -= action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListenerBool action) {
		if (_dicBool.ContainsKey (type)) {
			_dicBool [type] -= action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListenerFloat action) {
		if (_dicFloat.ContainsKey (type)) {
			_dicFloat [type] -= action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListenerInt action) {
		if (_dicInt.ContainsKey (type)) {
			_dicInt [type] -= action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListenerObject action) {
		if (_dicObject.ContainsKey (type)) {
			_dicObject [type] -= action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListenerParams action) {
		if (_dicParams.ContainsKey (type)) {
			_dicParams [type] -= action;
		}
	}

	public	void RemoveEventListener(Events type, DelegateListenerString action) {
		if (_dicString.ContainsKey (type)) {
			_dicString [type] -= action;
		}
	}

	public	void DispatchEvent(Events type) {
		if (_dic.ContainsKey (type)) {
			_dic [type].Invoke ();
		}
	}

	public	void DispatchEvent(Events type, bool value) {
		if (_dicBool.ContainsKey (type)) {
			_dicBool [type].Invoke (value);
		}
	}

	public	void DispatchEvent(Events type, float value) {
		if (_dicFloat.ContainsKey (type)) {
			_dicFloat [type].Invoke (value);
		}
	}

	public	void DispatchEvent(Events type, int value) {
		if (_dicInt.ContainsKey (type)) {
			_dicInt [type].Invoke (value);
		}
	}

	public	void DispatchEvent(Events type, object value) {
		if (_dicObject.ContainsKey (type)) {
			_dicObject [type].Invoke (value);
		}
	}

	public	void DispatchEvent(Events type, params object[] values) {
		if (_dicParams.ContainsKey (type)) {
			_dicParams [type].Invoke (values);
		}
	}

	public	void DispatchEvent(Events type, string value) {
		if (_dicString.ContainsKey (type)) {
			_dicString [type].Invoke (value);
		}
	}
}
*/