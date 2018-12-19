using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	AudioSource green, red, yellow, blue;

	void Start() {
		AudioSource[] audios = GetComponents<AudioSource> ();
		green = audios [0];
		red = audios [1];
		yellow = audios [2];
		blue = audios [3];
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.UpArrow))
			green.Play ();
		if (Input.GetKeyDown (KeyCode.RightArrow))
			red.Play ();
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			yellow.Play ();
		if (Input.GetKeyDown (KeyCode.DownArrow))
			blue.Play ();
		
	}
}
