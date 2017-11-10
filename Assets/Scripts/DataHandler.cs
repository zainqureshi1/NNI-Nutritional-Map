using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour {

	private class KnowledgeOfMicroNutrient  {
		public static float[] zinc = { 4.5f, 4.2f, 6.6f, 2.8f, 7.5f, 6.1f };
		public static float[] iron = { 22.7f, 13.9f, 29.8f, 8.6f, 23.6f, 24.8f };
		public static float[] vitaminA = { 28.8f, 12.5f, 28.6f, 10.6f, 23.5f, 24f };
		public static float[] vitaminD = { 16.7f, 9.9f, 24.3f, 6.4f, 22.5f, 20.8f };
	}

	private class BreastFeedingRate {
		public static float[] oneHour = { 61.8f, 74.3f, 28.1f, 63.4f, 50.5f, 40.5f };
		public static float[] exclusive = { 0f, 0f, 0f, 0f, 0f, 38f };
		public static float[] semiSolid = { 51.3f, 35.3f, 49.2f, 48.6f, 62.6f, 51.3f };
	}

	private class NutritionalStatus {
		public static float[] stunting = { 35.9f, 41.9f, 39.8f, 0f, 56.7f, 45f };
		public static float[] wasting = { 8.1f, 12f, 9.5f, 0f, 13.6f, 11};
		public static float[] underweight = { 12.6f, 26.1f, 26.1f, 0f, 42.3f, 30f };
		public static float[] obeisty = { 0f, 0f, 0f, 0f, 0f, 10f };
	}

	private class MicroNutrientDeficincies  {
		public static float[] zinc = { 32.6f, 45.4f, 38.4f, 39.5f, 38.6f, 39.2f };
		public static float[] iron = { 36.2f, 26.4f, 48.6f, 32.5f, 40.6f, 43.8f };
		public static float[] vitaminA = { 71.8f, 68.5f, 50f, 73.5f, 53.3f, 54f };
		public static float[] anemia = { 37f, 28.9f, 40.3f, 43.4f, 43.3f, 40f };
	}

	private MapAnimator mapAnimator;

	public GameObject[] optionButtonsContainers;
	public Sprite spriteButtonNormal;
	public Sprite spriteButtonPressed;
	private Image[][] optionButtonImages;
	private Text[][] optionButtonTexts;

	public GameObject textBubbleContainer;
	public GameObject[] textBubbles;
	private Text[] bubblesTexts;

	public GameObject[] textBoxesContainers;
	private GameObject[][] textBoxes;
	private Text[][] boxesTexts;

	private int screen;
	private int option;
	private int province;
	private float[] values;

	void Start () {
		mapAnimator = GetComponent<MapAnimator> ();

		optionButtonImages = new Image[optionButtonsContainers.Length][];
		optionButtonTexts = new Text[optionButtonsContainers.Length][];
		for (int i = 0; i < optionButtonsContainers.Length; i++) {
			optionButtonImages [i] = optionButtonsContainers [i].GetComponentsInChildren<Image> ();
			optionButtonTexts [i] = optionButtonsContainers [i].GetComponentsInChildren<Text> ();
		}

		bubblesTexts = new Text[textBubbles.Length];
		for (int i = 0; i < textBubbles.Length; i++) {
			bubblesTexts [i] = textBubbles [i].GetComponentInChildren<Text> ();
		}

		int boxContainers = textBoxesContainers.Length;
		textBoxes = new GameObject[boxContainers][];
		boxesTexts = new Text[boxContainers][];
		for (int i = 0; i < boxContainers; i++) {
			Transform container = textBoxesContainers [i].transform;
			int childBoxes = container.childCount;
			textBoxes[i] = new GameObject[childBoxes];
			boxesTexts [i] = new Text[childBoxes];
			for (int j = 0; j < childBoxes; j++) {
				textBoxes [i] [j] = container.GetChild(j).gameObject;
				boxesTexts [i] [j] = textBoxes [i] [j].GetComponentInChildren<Text> ();
			}
		}
	}

	public void SetScreen(int screen) {
		this.screen = screen;
		textBubbleContainer.SetActive (false);
		textBoxesContainers [screen].SetActive (false);
		for (int i = 0; i < optionButtonImages [screen].Length; i++) {
			optionButtonImages [screen] [i].sprite = spriteButtonNormal;
			optionButtonTexts [screen] [i].color = Color.white;
		}
	}

	public void ShowBubbles(int option) {
		if (!mapAnimator.SelectProvince (-1)) {
			return;
		}

		this.option = option;
		this.province = -1;

		for (int i = 0; i < optionButtonImages [screen].Length; i++) {
			optionButtonImages [screen] [i].sprite = i == option ? spriteButtonPressed : spriteButtonNormal;
			optionButtonTexts [screen] [i].color = i == option ? Color.black : Color.white;
		}

		textBoxesContainers [screen].SetActive (false);
		textBubbleContainer.SetActive (true);
		for (int i = 0; i < textBubbles.Length; i++) {
			textBubbles [i].SetActive (false);
		}
		for (int i = 0; i < bubblesTexts.Length; i++) {
			bubblesTexts [i].text = "";
		}

		values = GetBubbleValues();
		if (values == null) {
			return;
		}

		StartCoroutine (ShowAnimateBubbles());
	}

	public void ShowBoxes(int province) {
		this.province = province;
		this.option = -1;

		for (int i = 0; i < optionButtonImages [screen].Length; i++) {
			optionButtonImages [screen] [i].sprite = spriteButtonNormal;
			optionButtonTexts [screen] [i].color = Color.white;
		}

		textBubbleContainer.SetActive (false);
		for (int i = 0; i < boxesTexts[screen].Length; i++) {
			boxesTexts [screen] [i].text = "";
		}

		values = GetBoxValues();
		if (values == null) {
			return;
		}

		textBoxesContainers [screen].SetActive (true);
		for (int i = 0; i < textBoxes [screen].Length; i++) {
			textBoxes [screen] [i].SetActive (values[i] != 0);
		}

		Invoke ("ShowBoxesText", 1.0f);
	}

	private float[] GetBubbleValues() {
		switch (screen) {
		case 0:
			switch (option) {
			case 0:
				return KnowledgeOfMicroNutrient.zinc;
			case 1:
				return KnowledgeOfMicroNutrient.iron;
			case 2:
				return KnowledgeOfMicroNutrient.vitaminA;
			case 3:
				return KnowledgeOfMicroNutrient.vitaminD;
			}
			break;
		case 1:
			switch (option) {
			case 0:
				return BreastFeedingRate.oneHour;
			case 1:
				return BreastFeedingRate.exclusive;
			case 2:
				return BreastFeedingRate.semiSolid;
			}
			break;
		case 2:
			switch (option) {
			case 0:
				return NutritionalStatus.stunting;
			case 1:
				return NutritionalStatus.wasting;
			case 2:
				return NutritionalStatus.underweight;
			case 3:
				return NutritionalStatus.obeisty;
			}
			break;
		case 3:
			switch (option) {
			case 0:
				return MicroNutrientDeficincies.zinc;
			case 1:
				return MicroNutrientDeficincies.iron;
			case 2:
				return MicroNutrientDeficincies.vitaminA;
			case 3:
				return MicroNutrientDeficincies.anemia;
			}
			break;
		}
		return null;
	}

	private float[] GetBoxValues() {
		float[] vals = null;
		switch (screen) {
		case 0:
			vals = new float[4];
			vals [0] = KnowledgeOfMicroNutrient.zinc [province];
			vals [1] = KnowledgeOfMicroNutrient.iron [province];
			vals [2] = KnowledgeOfMicroNutrient.vitaminA [province];
			vals [3] = KnowledgeOfMicroNutrient.vitaminD [province];
			break;
		case 1:
			vals = new float[3];
			vals [0] = BreastFeedingRate.oneHour [province];
			vals [1] = BreastFeedingRate.exclusive [province];
			vals [2] = BreastFeedingRate.semiSolid [province];
			break;
		case 2:
			vals = new float[4];
			vals [0] = NutritionalStatus.stunting [province];
			vals [1] = NutritionalStatus.wasting [province];
			vals [2] = NutritionalStatus.underweight [province];
			vals [3] = NutritionalStatus.obeisty [province];
			break;
		case 3:
			vals = new float[4];
			vals [0] = MicroNutrientDeficincies.zinc [province];
			vals [1] = MicroNutrientDeficincies.iron [province];
			vals [2] = MicroNutrientDeficincies.vitaminA [province];
			vals [3] = MicroNutrientDeficincies.anemia [province];
			break;
		}
		return vals;
	}

	private IEnumerator ShowAnimateBubbles() {
		WaitForSeconds wait = new WaitForSeconds (0.35f);
		for (int i = 0; i < textBubbles.Length; i++) {
			if (values [i] == 0) {
				continue;
			}
			textBubbles [i].SetActive (true);
			yield return wait;
			bubblesTexts[i].text = values[i].ToString() + "%";
		}
	}

	private void ShowBoxesText() {
		for (int i = 0; i < boxesTexts [screen].Length; i++) {
			if (values [i] != 0) {
				boxesTexts [screen] [i].text = values[i].ToString() + "%";
			}
		}
	}

}
