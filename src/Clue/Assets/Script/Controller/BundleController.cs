using System;

public class BundleController : Controller, IController{
	public	BundleController(Observer observer) : base (observer) {
	}

	public	void AttachListener() {
	}

	public	void DetachListener() {
	}
}