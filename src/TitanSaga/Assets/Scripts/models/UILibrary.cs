using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Config")]
public class UILibrary {
	[XmlAttribute("start")]
	public	string		startState;
	[XmlElement("Event")]
	public	UIEvent[]	events;
	[XmlElement("State")]
	public	UIState[]	states;

	private	Dictionary<string, UIEvent> _dictionaryEvent;
	private	Dictionary<string, UIState> _dictionaryState;

	public	UILibrary () {
		events = new UIEvent[0];
		states = new UIState[0];
		_dictionaryEvent = new Dictionary<string, UIEvent>();
		_dictionaryState = new Dictionary<string, UIState>();
	}

	public	void Hashing() {
		for (int i = events.Length-1 ; i >= 0 ; i--) {
			var e = events[i];
			_dictionaryEvent.Add(e.id, e);
		}
		for (int i = states.Length-1 ; i >= 0 ; i--) {
			var s = states[i];
			_dictionaryState.Add(s.id, s);
		}
	}

	public	UIEvent FindEvent(string id) {
		if (_dictionaryEvent.ContainsKey(id)) {
			return _dictionaryEvent[id];
		}
		return null;
	}

	public	UIState FindState(string id) {
		if (_dictionaryState.ContainsKey(id)) {
			return _dictionaryState[id];
		}
		return null;
	}
}

public	class UIEvent {
	[XmlAttribute("id")]
	public	string	id;
}

public	class UIState {
	[XmlAttribute("id")]
	public	string	id;
	[XmlAttribute("scene")]
	public	string	sceneName;
}

public	class UIStateParam {
	private	Dictionary<string, string>	_dicString;
	private	Dictionary<string, bool>	_dicBool;
	private	Dictionary<string, int>		_dicInt;
	private	Dictionary<string, float>	_dicFloat;

	public	UIStateParam() {
		_dicBool = new Dictionary<string, bool> ();
		_dicFloat = new Dictionary<string, float> ();
		_dicInt = new Dictionary<string, int> ();
		_dicString = new Dictionary<string, string> ();
	}

	public	bool HasKey(string key) {
		return _dicBool.ContainsKey (key) || _dicFloat.ContainsKey (key) || _dicInt.ContainsKey (key) || _dicString.ContainsKey (key);
	}

	public	void SetString(string key, string value) {
		_dicString [key] = value;
	}

	public	void SetBool(string key, bool value) {
		_dicBool [key] = value;
	}

	public	void SetInt(string key, int value) {
		_dicInt [key] = value;
	}

	public	void SetFloat(string key, float value) {
		_dicFloat [key] = value;
	}

	public	string GetString(string key) {
		string result;
		_dicString.TryGetValue (key, out result);
		return result;
	}

	public	bool GetBool(string key) {
		bool result;
		_dicBool.TryGetValue (key, out result);
		return result;
	}

	public	int GetInt(string key) {
		int result;
		_dicInt.TryGetValue (key, out result);
		return result;
	}

	public	float GetFloat(string key) {
		float result;
		_dicFloat.TryGetValue(key, out result);
		return result;
	}
}
