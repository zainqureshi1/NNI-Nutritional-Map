using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoAnimation : MonoBehaviour {

	private Image image;

	private Vector3 defaultScale;
	private Color defaultColor;

	private float upTime = 2.25f;
	private float downTime = 2.0f;
	private float waitTime = 0.75f;

	private float deltaTime;
	private float maxTime;
	private int state;

	private float deltaAnim;

	void Start () {
		image = GetComponent<Image> ();
		defaultScale = transform.localScale;
		defaultColor = image.color;
		deltaTime = 0.0f;
		maxTime = upTime;
		state = 0;
		deltaAnim = 0.0f;
	}

	void Update () {
		deltaTime += Time.deltaTime; 
		if (deltaTime >= maxTime) {
			deltaTime = 0.0f;
			switch (state) {
			case 0:
				state = 1;
				maxTime = downTime;
				break;
			case 1:
				state = 2;
				maxTime = waitTime;
				break;
			case 2:
				state = 0;
				maxTime = upTime;
				break;
			}
		}

		if (state == 2) {
			return;
		}

		deltaAnim = deltaTime / maxTime;
		if (state == 1) {
			deltaAnim = 1 - deltaAnim;
		}

		transform.localScale = defaultScale * deltaAnim;
		image.color = new Color (defaultColor.r, defaultColor.g, defaultColor.b, deltaAnim);
	}

}
