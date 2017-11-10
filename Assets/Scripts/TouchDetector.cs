using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour {

	public SoundManager soundManager;

	private MapAnimator mapAnimator;
	private DataHandler dataHandler;

	void Start() {
		mapAnimator = GetComponent<MapAnimator> ();
		dataHandler = GetComponent<DataHandler> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Touched (Input.mousePosition);
		} else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
			Touched (Input.GetTouch (0).position);
		}
	}

	private void Touched(Vector3 position) {
		Ray ray = Camera.main.ScreenPointToRay (position);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 500, LayerMask.GetMask("map"))) {
			string tag = hit.transform.tag;
			switch (tag) {
			case "Gilgit":
				SelectProvince (0);
				break;
			case "KPK":
				SelectProvince (1);
				break;
			case "Punjab":
				SelectProvince (2);
				break;
			case "Balochistan":
				SelectProvince (3);
				break;
			case "Sindh":
				SelectProvince (4);
				break;
			}
		}
	}

	private void SelectProvince(int index) {
		if (mapAnimator.SelectProvince (index)) {
			soundManager.PlayMapClick ();
			dataHandler.ShowBoxes (index);
		}
	}

}
