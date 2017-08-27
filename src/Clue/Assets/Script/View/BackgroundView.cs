using UnityEngine;
using System.Collections;

public class BackgroundView : MonoBehaviour {
	private	bool interact;

	public	void SetIntectivity(bool value) {
		interact = value;
	}

	public	void OnSelectEvidence(EvidenceView selected) {
		if (interact) {
			var ob = GameObject.FindObjectOfType<Observer> ();
			ob.mansionCtr.CheckEvidence (selected.alias);
		}
	}
}
