using UnityEngine;
using System.Collections;

public class BackgroundView : MonoBehaviour {
	private	bool interact;

	public	void SetIntectivity(bool value) {
		interact = value;
	}

	public	void OnSelectEvidence(EvidenceView selected) {
		if (interact) {
			Debug.Log (selected.gameObject.name);
		}
	}
}

