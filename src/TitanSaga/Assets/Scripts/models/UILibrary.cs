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
