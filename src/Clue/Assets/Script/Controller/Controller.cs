using System;

public class Controller {
	protected	Observer		observer;
	protected	EventController	dispatcher;

	public	Controller(Observer observer) {
		System.Console.WriteLine (this.ToString ());
		this.observer = observer;
		this.dispatcher = observer.eventCtr;
	}
}