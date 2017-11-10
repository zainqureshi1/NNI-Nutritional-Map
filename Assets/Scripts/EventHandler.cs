using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour {

	public ScreenSwitcher screenSwitcher;

	public void Screen1Animated() {
		screenSwitcher.GoToScreen2 ();
	}

}
