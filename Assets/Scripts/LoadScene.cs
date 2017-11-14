using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public Slider progressSlider;
	public Text progressText;

	private float cummulativeProgress;
	private float opProgressWeight;

	void Start () {
		#if UNITY_STANDALONE_WIN || UNITY_EDITOR
		opProgressWeight = 0.5f;
		#else
		opProgressWeight = 1.0f;
		#endif

		StartCoroutine(LoadMainScene());
	}

	private IEnumerator LoadMainScene() {
		yield return null;
		AsyncOperation operation = SceneManager.LoadSceneAsync(1);
		operation.allowSceneActivation = false;
		while (!operation.isDone && operation.progress < 0.9f) {
			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			cummulativeProgress = progress * opProgressWeight;
			updateProgress ();
			yield return null;
		}
		operation.allowSceneActivation = true;
		WaitForSeconds wait1 = new WaitForSeconds (0.2f);
		WaitForSeconds wait2 = new WaitForSeconds (0.4f);
		while (!operation.isDone && cummulativeProgress < 1) {
			cummulativeProgress += 0.003f * Random.value;
			updateProgress ();
			yield return (Random.value * 100) % 2 == 0 ? wait1 : wait2;
		}
		cummulativeProgress = 1;
		updateProgress ();
	}

	private void updateProgress() {
		if (cummulativeProgress >= 1) {
			cummulativeProgress = 1;
		}
		progressSlider.value = cummulativeProgress;
		progressText.text = (cummulativeProgress * 100f).ToString("F1") + "%";
	}

}
