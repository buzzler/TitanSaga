using UnityEngine;

public	class MansionController : Controller {
	private	string		_id;
	private	MansionData	_current;

	public	MansionController(Observer observer) : base (observer) {
	}

	public	void Clear() {
		_id = null;
		_current = null;
	}

	public	bool Load(string id) {
		try {
			if (_id == id)
				return false;

			var asset = observer.bundleCtr.Load<TextAsset> (id + "/Mansion");
			if (asset == null)
				return false;
			var data = JsonUtility.FromJson<MansionData> (asset.text);
			if (data == null)
				return false;

			_current = data;
			_id = id;
			return true;
		} catch (System.Exception e) {
			if (Debug.isDebugBuild)
				Debug.LogError (e.Message);
			return false;
		}
	}

	public	void ShowPrologue() {
		if (_current == null)
			return;
		ShowScenario (_current.prologueScenario);
	}

	public	void ShowEpilogue() {
		if (_current == null)
			return;
		ShowScenario (_current.epilogueScenario);
	}

	public	void ShowScenario(string filename) {
		if (string.IsNullOrEmpty (_id))
			return;
		if (string.IsNullOrEmpty (filename))
			return;
		
		observer.scenarioCtr.Play (string.Format("{0}/{1}", _id, filename));
	}
}