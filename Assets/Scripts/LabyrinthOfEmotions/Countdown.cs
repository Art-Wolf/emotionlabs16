using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Countdown : MonoBehaviour {

	private double timer;
	private float startingSeconds = 120f;
	private float seconds = 120f;
	private bool timesUp = false;
	GameObject[] endObjects;

	public Text clock;

	public void Start () {
		//clock.text = seconds.ToString();
		endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
		hideEnd();
		InvokeRepeating ("CountdownTimer", 1.0f, 1.0f); 
		//StartCoroutine (timeNearlyUpCoroutine ());
	}

	void Update() {
		if (timesUp) {
			if (seconds > 10f || seconds > 70f) {
				clock.text = "0" +  ((startingSeconds % 60f) - (seconds % 60f))+ ":" + (startingSeconds - seconds);
			} else {
				clock.text = "00:0" + seconds.ToString ();
			}
			//clock.text = string.Format ("GAME OVER");
			//SceneManager.LoadScene ("GameOver");
			return;
		}
		if (seconds < 10)
			StartCoroutine (timeNearlyUpCoroutine ());
	}

	public void CountdownTimer () {
		if (--seconds == 0) {
			timesUp = true;
			showEnd();
			Time.timeScale = 0;
			CancelInvoke ("CountdownTimer");
		}
		if (seconds > 10f || seconds > 70f) {
			clock.text = "0" +  (seconds % 60f)+ ":" + seconds.ToString ();
		} else {
			clock.text = "00:0" + seconds.ToString ();
		}
	}

	private IEnumerator timeNearlyUpCoroutine(){
		while (!timesUp) {
			if (seconds < 10f) {
				clock.color = Color.red;
				yield return 0;
				clock.color = Color.clear;
				yield return 0;
			}
		}
	}

	public void hideEnd(){
		foreach(GameObject g in endObjects){
			g.SetActive(false);
		}
	}

	public void showEnd(){
		foreach(GameObject g in endObjects){
			g.SetActive(true);
		}
	}
		
}