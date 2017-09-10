using UnityEngine;
using System.Collections;

public class BackgroundView : MonoBehaviour {
	private	bool _interact;

	public	void SetIntectivity(bool value) {
		_interact = value;
	}

	public	bool GetIntectivity() {
		return _interact;
	}

	public	void OnSelectEvidence(EvidenceView selected) {
		if (_interact) {
			var ob = GameObject.FindObjectOfType<Observer> ();
			ob.mansionCtr.CheckEvidence (selected.alias);
		}
	}
}
