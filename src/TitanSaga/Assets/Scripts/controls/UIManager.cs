using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
	public	TextAsset							asset;
	private	UILibrary							_config;
	private	UIState								_opened;
	private	object[]							_data;
	private	Dictionary<string, UIStateParam>	_params;

	public	UILibrary	config	{get{return _config;}}

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		_config = TitanUtility.LoadFromText<UILibrary>(asset.text);
		_config.Hashing();
		_params = new Dictionary<string, UIStateParam> ();
	}

	void Start() {
		var startState = _config.FindState(_config.startState);
		OpenState(startState);
	}

	public	void OpenState(string stateId, params object[] data) {
		KeepData (data);
		var state = _config.FindState (stateId);
		if (state != null) {
			OpenState (state);
		}
	}

	public	object[] GetKeptData() {
		var d = _data;
		_data = null;
		return d;
	}

	private	void KeepData(params object[] data) {
		_data = data;
	}

	private	void OpenState(UIState state) {
		if (_opened == state) {
			return;
		}
		if (_opened != null) {
			SceneManager.UnloadScene(_opened.sceneName);
			_opened = null;
		}
		if (state != null) {
			_opened = state;
			SceneManager.LoadSceneAsync(state.sceneName, LoadSceneMode.Additive);
		} else {
			_opened = null;
		}
	}

	public	void CloseAllState() {
		if (_opened != null) {
			SceneManager.UnloadScene(_opened.sceneName);
			_opened = null;
		}
	}

	public	UIStateParam GetStateParam(string stateId) {
		if (_config.FindState (stateId) != null) {
			if (!_params.ContainsKey (stateId)) {
				_params [stateId] = new UIStateParam ();
			}
			return _params [stateId];
		}
		return null;
	}

	public	void ClearStateParam(string stateId) {
		if (_params.ContainsKey (stateId)) {
			_params.Remove (stateId);
		}
	}

	public	void ClearAllStateParam() {
		_params.Clear ();
		_params = new Dictionary<string, UIStateParam> ();
	}
}
