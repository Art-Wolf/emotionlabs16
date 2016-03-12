using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Countdown : MonoBehaviour {

	private double timer;
	private float startingSeconds = 120f;
	private float seconds = 120f;
	private bool timesUp = false;
	//GameObject[] endObjects;
	public GameObject winPanel;
	public Text winPanelText;

	public Text clock;

	public void Start () {
		//clock.text = seconds.ToString();
		//endObjects = GameObject.FindGameObjectsWithTag("ShowOnEnd");
		hideEnd();
		InvokeRepeating ("CountdownTimer", 1.0f, 1.0f); 
		//StartCoroutine (timeNearlyUpCoroutine ());
	}

	void Update() {
		if (timesUp) {
			
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
		float minutesText = Mathf.Floor (seconds / 60);
		float secondsText = seconds % 60;

		if (secondsText > 59f)
			secondsText = 59f;
		
		clock.text = "0" + minutesText + ":" +secondsText ;

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
		/*foreach(GameObject g in endObjects){
			g.SetActive(false);
		}*/
		winPanel.SetActive(false);
		winPanel.transform.SetAsFirstSibling ();
	}

	public void showEnd(){
		/*foreach(GameObject g in endObjects){
			g.SetActive(true);
		}*/
		winPanelText.text = "Failed :(";
		winPanel.SetActive(true);
		winPanel.transform.SetAsLastSibling ();
	}
		
}