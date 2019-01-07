using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainDriver : MonoBehaviour {

	List<string> sequence = new List<string>();

	int num; // Random Number used to generate the next color
	float time; // Time interval between colors. Default is half second.
	public Text score, message;
	private bool playingBack; // Is the color pattern currently being played back?

	AudioSource green, red, yellow, blue, winsound, losesound; // Various sounds
	public Light green1, green2, green3, red1, red2, red3, yellow1, yellow2, yellow3, blue1, blue2, blue3; // All the lights.

	private List <KeyCode> def = new List<KeyCode>() {};
	private List <KeyCode> correct; // The sequence that correlates to what order the keys need to be pressed in.
	private int correctIndex; // The index counter for what order keys are pressed in.

	// Use this for initialization
	void Start () {
		correct = def;
		AudioSource[] audios = GetComponents<AudioSource> ();
		green = audios [0];
		red = audios [1];
		yellow = audios [2];
		blue = audios [3];
		winsound = audios [4];
		losesound = audios [5];
		message.text = "";
		message.enabled = false;
		TextUpdate ();

		//Game Start
		setTime ();
		Invoke ("OpeningSequence", 1);
	}
	
	// Update is called once per frame
	void Update () {
		setTime ();
		if (correct.Count != 0)
		if (Input.GetKeyDown (correct [correctIndex])) {
			if (++correctIndex == correct.Count) {
				correctIndex = 0;
				print ("Correct!");
				//Correct - do whatever is next
				Invoke ("Win", time);
			}
		} 
		else if (Input.GetKeyDown (KeyCode.P)) {
			if (!playingBack)
				StartCoroutine (PlayGame ());
		}

		else if (Input.GetKeyDown (KeyCode.Q)) {
			SceneManager.LoadScene ("Simon");
		}

		else if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();

		else if (Input.anyKeyDown) {
			correctIndex = 0;
			print ("Wrong!");
			//The user is wrong - game over.
			Invoke ("Lose", time);
		}


	}

	// Randomly assigns and adds a new color to the sequence.
	void GenerateColor() {
		num = Random.Range (1, 5);

		switch (num) {
		case 1:
			sequence.Add ("Red");
			correct.Add (KeyCode.RightArrow);
			break;
		case 2:
			sequence.Add ("Blue");
			correct.Add (KeyCode.DownArrow);
			break;
		case 3:
			sequence.Add ("Yellow");
			correct.Add (KeyCode.LeftArrow);
			break;
		case 4:
			sequence.Add ("Green");
			correct.Add (KeyCode.UpArrow);
			break;
		}
		StartCoroutine (PlayGame ());
	} 

	// What happens when you win
	void Win() {
		winsound.Play ();
		message.text = "Correct!";
		TextUpdate ();
		Invoke ("GenerateColor", time);
	}

	// What happens when you lose
	void Lose() {
		losesound.Play ();
		message.text = "Wrong!";
		TextUpdate ();
	}

	// Runs the game
	IEnumerator PlayGame() {

		playingBack = true;

		for (int i = 0; i < sequence.Count; i++) {
			print (sequence [i].ToString()); // Debug
			if (sequence [i].ToString () == "Red")
				Red ();
			if (sequence [i].ToString () == "Blue")
				Blue ();
			if (sequence [i].ToString () == "Yellow")
				Yellow ();
			if (sequence [i].ToString () == "Green")
				Green ();
			yield return new WaitForSeconds (1.5f * time);
		}

		playingBack = false;
	} 

	//Sets the Score Text as needed
	void TextUpdate () {
		score.text = "Score: " + (sequence.Count).ToString ();
		StartCoroutine (MessageDisplay());
	}

	IEnumerator MessageDisplay() {
		message.enabled = true;
		yield return new WaitForSeconds (2);
		message.enabled = false;
		if (sequence.Count == 0)
			message.text = "Game Start!";
	}

	//Checks the length of the sequence, and adjusts the difficulty accordingly.
	void setTime() {
		if (sequence.Count >= 1 && sequence.Count < 5)
			time = 0.5f;
		if (sequence.Count >= 5 && sequence.Count < 10)
			time = 0.4f;
		if (sequence.Count >= 10 && sequence.Count < 15 || sequence.Count == 0)
			time = 0.3f;
		if (sequence.Count >= 15)
			time = 0.2f;
		}
		
	//Turns on Green Lights
	void Green(){
		green.Play ();
		green1.intensity = 1;
		green2.intensity = 1;
		green3.intensity = 1;
		StartCoroutine (OffGreen ());
	}

	//Turns off Green Lights
	IEnumerator OffGreen(){
		yield return new WaitForSeconds (time);
		green1.intensity = 0;
		green2.intensity = 0;
		green3.intensity = 0;
	}

	//Turns on Red Lights
	void Red(){
		red.Play ();
		red1.intensity = 1;
		red2.intensity = 1;
		red3.intensity = 1;
		StartCoroutine (OffRed ());
	}

	//Turns off right lights
	IEnumerator OffRed(){
		yield return new WaitForSeconds (time);
		red1.intensity = 0;
		red2.intensity = 0;
		red3.intensity = 0;
	}

	//Turn on Yellow Lights
	void Yellow(){
		yellow.Play ();
		yellow1.intensity = 1;
		yellow2.intensity = 1;
		yellow3.intensity = 1;
		StartCoroutine (OffYellow ());
	}

	//Turns off yellow lights
	IEnumerator OffYellow(){
		yield return new WaitForSeconds (time);
		yellow1.intensity = 0;
		yellow2.intensity = 0;
		yellow3.intensity = 0;
	}

	//Turns on blue lights
	void Blue(){
		blue.Play ();
		blue1.intensity = 1;
		blue2.intensity = 1;
		blue3.intensity = 1;
		StartCoroutine (OffBlue ());
	}

	//Turns off blue lights
	IEnumerator OffBlue(){
		yield return new WaitForSeconds (time);
		blue1.intensity = 0;
		blue2.intensity = 0;
		blue3.intensity = 0;
	}
		
	//Prenumbered Sequence to start the game
	void OpeningSequence() {
		Green ();
		Invoke ("Red", time);
		Invoke ("Blue", 2*time);
		Invoke ("Yellow", 3*time);
		Invoke ("Green", 4*time);
		Invoke ("Red", 5*time);
		Invoke ("Blue", 6*time);
		Invoke ("Yellow", 7*time);
		Invoke ("Green", 9*time);
		Invoke ("Red", 9*time);
		Invoke ("Blue", 9*time);
		Invoke ("Yellow", 9*time);
		//Game Start
		Invoke ("GenerateColor", 12 * time);
		Invoke ("TextUpdate", 11.9f * time);
	}

	//A losing sequence before the next game starts
	void LoseSequence() {
		Yellow();
		Invoke ("Red", time);
		Invoke ("Yellow", 2*time);
		Invoke ("Red", 3*time);
		Invoke ("Green", 4*time);
		Invoke ("Blue", 5*time);
		Invoke ("Green", 6*time);
		Invoke ("Blue", 7*time);
		Invoke ("Green", 9*time);
		Invoke ("Red", 9*time);
		Invoke ("Blue", 9*time);
		Invoke ("Yellow", 9*time);
	}


}
