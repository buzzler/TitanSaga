using UnityEngine;
using UnityEngine.UI;

public	class EvidenceView : MonoBehaviour {
	public	void OnMouseUp() {
		SendMessageUpwards ("OnSelectEvidence", this, SendMessageOptions.RequireReceiver);
	}
}
