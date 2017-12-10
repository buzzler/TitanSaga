using UnityEngine;
using System.Collections;

public class BackgroundView : MonoBehaviour {
	public	float minRotationY;
	public	float maxRotationY;

	private Observer _observer;
	private	Transform _camera;
	private	Vector3 _defaultRotation;
	private	bool _interact;
	private	Material _material;
	private	int _tweenId;
	private	bool _moving;
	private	float _center;
	private	float _originM;
	private	Vector3 _originC;
	private	float _fov;

	void Start() {
		_observer = GameObject.FindObjectOfType<Observer> ();
		_camera = _observer.cameraCtr.background.transform;
		_defaultRotation = _camera.eulerAngles;
		_fov = maxRotationY - minRotationY;
		_interact = false;
		_material = GetComponentInChildren<MeshRenderer> ().material;

		_observer.cameraCtr.ResetBackgroundCamera ();
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
			_observer.mansionCtr.CheckEvidence (selected.alias);
		}
	}

	void Update()
	{
		if (!_interact)
			return;
		
		if (Input.GetMouseButtonDown (0)) {
			_moving = true;
			_originM = Input.mousePosition.x;
			_originC = _camera.eulerAngles;
			_center = Screen.width / 2f;
			return;
		} else if (Input.GetMouseButtonUp (0)) {
			_moving = false;
			return;
		}

		if (_moving) {
			var x = (Input.mousePosition.x - _originM) / _center;
			var rotY = Mathf.Clamp (_originC.y - (_fov * x) / 2f, minRotationY, maxRotationY);
			_camera.eulerAngles = new Vector3 (_originC.x, rotY, _originC.z);
		}
	}
}
