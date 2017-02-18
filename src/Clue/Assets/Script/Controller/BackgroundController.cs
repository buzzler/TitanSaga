using UnityEngine.SceneManagement;

public class BackgroundController : Controller {
	private	string currentBackground;

	public BackgroundController (Observer observer) : base (observer) {
		currentBackground = null;
	}

	public	void ChangeBackground(string name) {
		if (!string.IsNullOrEmpty (currentBackground)) {
			SceneManager.UnloadScene (currentBackground);
			currentBackground = null;
		}
		currentBackground = name;
		SceneManager.LoadScene (currentBackground, LoadSceneMode.Additive);
	}
}
