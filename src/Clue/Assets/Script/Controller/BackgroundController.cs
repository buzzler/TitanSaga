using UnityEngine.SceneManagement;

public class BackgroundController : Controller, IController {
	private	string currentBackground;
	public BackgroundController (Observer observer) : base (observer) {
		currentBackground = null;
	}

	public	void AttachListener() {
		observer.AddEventListener (Events.BACKGROUND_CHANGE, OnChangeBackground);
	}

	public	void DetachListener() {
		observer.RemoveEventListener (Events.BACKGROUND_CHANGE, OnChangeBackground);
	}

	private	void OnChangeBackground(string name) {
		if (!string.IsNullOrEmpty (currentBackground)) {
			SceneManager.UnloadScene (currentBackground);
			currentBackground = null;
		}
		currentBackground = name;
		SceneManager.LoadScene (currentBackground, LoadSceneMode.Additive);
	}
}
