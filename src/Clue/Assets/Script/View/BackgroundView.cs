using UnityEngine;
using System.Collections;

public class BackgroundView : MonoBehaviour {
	private	bool _interact;
	private	Material _material;
	private	int _tweenId;
	private	bool _moving;
	private	float _center;
	private	float _origin;

	void Start() {
		_material = GetComponentInChildren<MeshRenderer> ().material;
		OnShowFirst ();
	}

	void OnDestroy() {
		LeanTween.cancel (_tweenId);
	}

	public	void UpdateTexture(float blend) {
		_material.SetFloat ("_Blend", blend);
	}

	private	void OnShowFirst() {
		_tweenId = LeanTween.delayedCall (0.2f, () => {
			_tweenId = LeanTween.value(gameObject, UpdateTexture, 0f, 1f, 0.2f).setOnComplete(OnShowSecond).id;
		}).id;
	}

	private	void OnShowSecond() {
		_tweenId = LeanTween.delayedCall (0.2f, () => {
			_tweenId = LeanTween.value(gameObject, UpdateTexture, 1f, 0f, 0.2f).setOnComplete(OnShowFirst).id;
		}).id;
	}

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

	void Update()
	{
		if (!_interact)
			return;
		
		if (Input.GetMouseButtonDown (0)) {
			_moving = true;
			_origin = Input.mousePosition.x;
			_center = Screen.width / 2f;
			return;
		} else if (Input.GetMouseButtonUp (0)) {
			_moving = false;
			return;
		}

		if (_moving) {
			var x = (Input.mousePosition.x - _center) / _center;
			Debug.Log (x);
		}
	}
}
