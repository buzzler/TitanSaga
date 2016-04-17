using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {
	public	TextAsset	asset;
	private	UILibrary	_config;
	private	UIState		_opened;
	private	object[]	_data;

	public	UILibrary	config	{get{return _config;}}

	void Awake() {
		GameObject.DontDestroyOnLoad(gameObject);
		_config = TitanUtility.LoadFromText<UILibrary>(asset.text);
		_config.Hashing();
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
}
