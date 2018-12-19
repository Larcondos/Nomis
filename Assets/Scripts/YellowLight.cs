using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLight : MonoBehaviour {

	public Light topLight;
	public Light midLight;
	public Light botLight;

	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			topLight.intensity = 1;
			midLight.intensity = 1;
			botLight.intensity = 1;
		} 
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			topLight.intensity = 0;
			midLight.intensity = 0;
			botLight.intensity = 0;
		} 
	}
}
