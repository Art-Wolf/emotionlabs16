using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {

	private double timer;
	private float seconds = 20f;
	private bool timesUp = false;

	public GUIText clock;

	public void Start () {
		clock.text = seconds.ToString();
		InvokeRepeating ("CountdownTimer", 1.0f, 1.0f); 
		//StartCoroutine (timeNearlyUpCoroutine ());
	}

	void Update() {
		if (timesUp) {
			clock.text = string.Format ("GAME OVER");
			return;
		}
		if (seconds < 10)
			StartCoroutine (timeNearlyUpCoroutine ());
	}

	public void CountdownTimer () {
		if (--seconds == 0) {
			timesUp = true;
			CancelInvoke ("CountdownTimer");
		}
		clock.text = seconds.ToString();
	}

	private IEnumerator timeNearlyUpCoroutine(){
		while (!timesUp) {
			if (seconds < 10f) {
				clock.color = Color.red;
				clock.text = "";
				yield return 0;
				clock.text = seconds.ToString();
				yield return 0;
			}
		}
	}
		
}