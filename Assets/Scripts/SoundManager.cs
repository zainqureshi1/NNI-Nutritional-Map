using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip buttonClick;
	public AudioClip mapClick;

	private AudioSource source;

	void Start() {
		source = GetComponent<AudioSource> ();
	}

	public void PlayButtonClick() {
		source.clip = buttonClick;
		source.Play ();
	}

	public void PlayMapClick() {
		source.clip = mapClick;
		source.Play ();
	}

}
