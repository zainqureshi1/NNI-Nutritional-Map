using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitcher : MonoBehaviour {

	private MapAnimator mapAnimator;
	private DataHandler dataHandler;

	public GameObject screen1;
	public GameObject screen2;
	public GameObject[] screen3;
	public GameObject textBubbles;

	public CanvasGroup screen2ButtonsCanvas;
	public CanvasGroup[] screen3ButtonsCanvas;

	private bool screen2ButtonsAnimate = false;
	private float screen2ButtonsAnimTime = 1.0f;
	private float screen2ButtonsDeltaTime;

	private int screen3Index;

	private bool screen3ButtonsAnimate = false;
	private float screen3ButtonsAnimTime = 1.0f;
	private float screen3ButtonsDeltaTime;

	void Start() {
		mapAnimator = GetComponent<MapAnimator> ();
		dataHandler = GetComponent<DataHandler> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Home)) {
			Exit ();
		}
		if (screen2ButtonsAnimate) {
			screen2ButtonsDeltaTime += Time.deltaTime;
			if (screen2ButtonsDeltaTime >= screen2ButtonsAnimTime) {
				screen2ButtonsCanvas.alpha = 1.0f;
				screen2ButtonsAnimate = false;
			} else {
				screen2ButtonsCanvas.alpha = screen2ButtonsDeltaTime / screen2ButtonsAnimTime;
			}
		}
		if (screen3ButtonsAnimate) {
			screen3ButtonsDeltaTime += Time.deltaTime;
			if (screen3ButtonsDeltaTime >= screen3ButtonsAnimTime) {
				screen3ButtonsCanvas[screen3Index] .alpha = 1.0f;
				screen3ButtonsAnimate = false;
			} else {
				screen3ButtonsCanvas[screen3Index] .alpha = screen3ButtonsDeltaTime / screen3ButtonsAnimTime;
			}
		}
	}

	public void GoToScreen2() {
		Invoke("ShowScreen2", 1.0f);
	}

	public void GoToScreen3(int index) {
		screen2.SetActive (false);
		for (int i = 0; i < screen3.Length; i++) {
			screen3 [i].SetActive (i == index);
		}
		dataHandler.SetScreen (index);
		mapAnimator.StartInteraction ();
		screen3Index = index;

		screen3ButtonsAnimate = true;
		screen3ButtonsDeltaTime = 0.0f;
		screen3ButtonsCanvas [index].alpha = 0.0f;
	}

	public void BackToScreen2() {
		for (int i = 0; i < screen3.Length; i++) {
			screen3 [i].SetActive (false);
		}
		textBubbles.SetActive (false);
		screen2.SetActive (true);
		mapAnimator.CloseInteraction ();
	}

	public void ExitClicked() {
		Exit ();
	}

	private void ShowScreen2() {
		screen1.SetActive (false);
		screen2.SetActive (true);

		screen2ButtonsAnimate = true;
		screen2ButtonsDeltaTime = 0.0f;
		screen2ButtonsCanvas.alpha = 0.0f;
	}

	private void Exit() {
		Application.Quit ();
	}

}
