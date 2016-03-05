using UnityEngine;
using Affdex;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmotionListenerViewModel : ImageResultsListener {

	public Text strongestEmo;
	public Text NorthEmoText;
	public Text EastEmoText;
	public Text SouthEmoText;
	public Text WestEmoText;

	private Text strongestText;

	public float currentSmile;
	public float currentSadness;
	public float currentSuprise;
	public float currentAnger;
	public FeaturePoint[] featurePointsList;
	delegate void ControlDelegate();
	ControlDelegate ctrlDelegate;
	public int[] nextNavArray = new int[4];
	public int[] currNavArray = new int[4];

	//private enum emotionEnum {Joy, Sadness, Anger, Suprise};
	//Dictionary<string, float> emotionDict = new Dictionary<string, float>();

	public EmotionListenerViewModel() {
		//EventController.Instance.Subscribe ();
		//subscribe to events in constructor or in awake functions
	//	emotionDict.Add(emotionEnum.Joy, currentSmile);
	//	emotionDict.Add(emotionEnum.Sadness, currentSadness);
	//	emotionDict.Add(emotionEnum.Anger, currentAnger);
	//	emotionDict.Add(emotionEnum.Suprise, currentSuprise);

		//RandomizeEmotions();
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
			if (strongestText != null) {
				strongestText.color = Color.black;
			}

			string currStrongestEmoStr = "Nothing";
			float currStrongestEmo = 0;

			faces[0].Emotions.TryGetValue (Emotions.Joy, out currentSmile);
			faces[0].Emotions.TryGetValue (Emotions.Sadness, out currentSadness);
			faces[0].Emotions.TryGetValue (Emotions.Surprise, out currentSuprise);
			faces[0].Emotions.TryGetValue (Emotions.Disgust, out currentAnger);

			if (currentSmile > currStrongestEmo) {
				currStrongestEmo = currentSmile;
				currStrongestEmoStr = "Joy";
				ctrlDelegate = OnNorthEmo;
				strongestText = NorthEmoText;
			}
			if (currentAnger > currStrongestEmo) {
				currStrongestEmo = currentAnger;
				currStrongestEmoStr = "Disgust";
				ctrlDelegate = OnEastEmo;
				strongestText = EastEmoText;
			}
			if (currentSadness > currStrongestEmo) {
				currStrongestEmo = currentSadness;
				currStrongestEmoStr = "Sadness";
				ctrlDelegate = OnSouthEmo;
				strongestText = SouthEmoText;
			}
			if (currentSuprise > currStrongestEmo) {
				currStrongestEmo = currentSuprise;
				currStrongestEmoStr = "Suprise";
				ctrlDelegate = OnWestEmo;
				strongestText = WestEmoText;
			}

			this.strongestEmo.text = "Custom Strongest Emotion: " + currStrongestEmoStr + "/" + currStrongestEmo;
			//strongestText.color = Color.green;
			// invoke direction event
			ctrlDelegate();

		}
	}

//	private void RandomizeEmotions() {
//		nextNavArray [0] = 0;
//		nextNavArray [1] = 1;
//		nextNavArray [2] = 2;
//		nextNavArray [3] = 3;
//		for(int i = 0; i < nextNavArray.Length; i++) {
//			string tmp = nextNavArray[i];
//			int r = Random.Range(i, nextNavArray.Length);
//			nextNavArray[i] = nextNavArray[r];
//			nextNavArray[r] = tmp;
//		}
//	}

	public void OnNorthEmo() {
		Debug.Log("North Emo");
		EventController.Instance.Publish (new GoNorthEvent("random"));
	}

	public void OnWestEmo() {
		Debug.Log("West Emo");
		EventController.Instance.Publish (new GoWestEvent("random"));
	}

	public void OnEastEmo() {
		Debug.Log("East Emo");
		EventController.Instance.Publish (new GoEastEvent("random"));
	}

	public void OnSouthEmo() {
		Debug.Log("South Emo");
		EventController.Instance.Publish (new GoSouthEvent("random"));
	}
}
