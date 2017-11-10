using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAnimator : MonoBehaviour {

	// 0 = Gilgit, 1 = KPK, 2 = Punjab, 3 = Balochistan, 4 = Sindh
	public Transform[] provinces;

	public float maxScale = 5.0f;

	private bool interactive = false;
	private int visibleProvince = -1;

	private bool animating = false;
	private int showIndex = -1;
	private int hideIndex = -1;
	private float animTime = 0.75f;
	private float deltaTime = 0.0f;

	void Update () {
		if (animating) {
			deltaTime += Time.deltaTime;
			if (deltaTime >= animTime) {
				animating = false;
				deltaTime = animTime;
			}
			if (showIndex >= 0 && showIndex < provinces.Length) {
				provinces [showIndex].localScale = new Vector3(1, (deltaTime / animTime * (maxScale - 1)) + 1, 1);
			}
			if (hideIndex >= 0 && hideIndex < provinces.Length) {
				provinces [hideIndex].localScale = new Vector3(1, ( (animTime - deltaTime) / animTime * (maxScale - 1)) + 1, 1);
			}
		}
	}

	public bool SelectProvince(int index) {
		if (!interactive || animating) {
			return false;
		}
		hideIndex = visibleProvince;
		visibleProvince = index;
		showIndex = visibleProvince;
		if (hideIndex >= 0 || showIndex >= 0) {
			animating = true;
			deltaTime = 0.0f;
		}
		return true;
	}

	public void StartInteraction() {
		interactive = true;
	}

	public void CloseInteraction() {
		interactive = false;
		SelectProvince (-1);
	}

}
