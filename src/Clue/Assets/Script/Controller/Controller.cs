using System;

public class Controller {
	protected	Observer		observer;

	public	Controller(Observer observer) {
		System.Console.WriteLine (this.ToString ());
		this.observer = observer;
	}
}