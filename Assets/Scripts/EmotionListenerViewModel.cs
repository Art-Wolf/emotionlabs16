using UnityEngine;
using Affdex;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmotionListenerViewModel : ImageResultsListener {

	public Text strongestEmo;
	public float currentSmile;
	public float currentSadness;
	public float currentSuprise;
	public float currentAnger;
	public FeaturePoint[] featurePointsList;
	delegate void ControlDelegate();
	ControlDelegate ctrlDelegate;

	public EmotionListenerViewModel() {
		//EventController.Instance.Subscribe ();
		//subscribe to events in constructor or in awake functions
	}

	public override void onFaceFound(float timestamp, int faceId) {
		Debug.Log("Found the face");
	}

	public override void onFaceLost(float timestamp, int faceId) {
		Debug.Log("Lost the face");
	}

	public override void onImageResults(Dictionary<int, Face> faces) {
		Debug.Log("Got face results, faces: "+ faces.Count);


		if(faces.Count > 0) {
			

			string currStrongestEmoStr = "Nothing";
			float currStrongestEmo = 0;
			faces[0].Emotions.TryGetValue (Emotions.Joy, out currentSmile);
			faces[0].Emotions.TryGetValue (Emotions.Sadness, out currentSadness);
			faces[0].Emotions.TryGetValue (Emotions.Surprise, out currentSuprise);
			faces[0].Emotions.TryGetValue (Emotions.Anger, out currentAnger);

			if (currentSmile > currStrongestEmo) {
				currStrongestEmo = currentSmile;
				currStrongestEmoStr = "Joy";
				ctrlDelegate = OnNorthEmo;
			}
			if (currentAnger > currStrongestEmo) {
				currStrongestEmo = currentAnger;
				currStrongestEmoStr = "Anger";
				ctrlDelegate = OnEastEmo;
			}
			if (currentSadness > currStrongestEmo) {
				currStrongestEmo = currentSadness;
				currStrongestEmoStr = "Sadness";
				ctrlDelegate = OnSouthEmo;
			}
			if (currentSuprise > currStrongestEmo) {
				currStrongestEmo = currentSuprise;
				currStrongestEmoStr = "Suprise";
				ctrlDelegate = OnWestEmo;
			}

			this.strongestEmo.text = "Custom Strongest Emotion: " + currStrongestEmoStr + "/" + currStrongestEmo;
			// invoke direction event
			ctrlDelegate();
		}
	}

	public void OnNorthEmo() {
		Debug.Log("North Emo");
	}

	public void OnWestEmo() {
		Debug.Log("West Emo");
		EventController.Instance.Publish (new GoWestEvent("random"));
	}

	public void OnEastEmo() {
		Debug.Log("East Emo");
	}

	public void OnSouthEmo() {
		Debug.Log("South Emo");
	}
}
