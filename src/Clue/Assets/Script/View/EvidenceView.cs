using UnityEngine;
using UnityEngine.UI;

public	class EvidenceView : MonoBehaviour {
	public	string alias;

	public	void Awake() {
		if (string.IsNullOrEmpty (alias))
			alias = string.Empty;
	}

	public	void OnMouseUp() {
		SendMessageUpwards ("OnSelectEvidence", this, SendMessageOptions.RequireReceiver);
	}
}
