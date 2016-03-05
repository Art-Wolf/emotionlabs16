using UnityEngine;
using Affdex;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class EmotionListenerViewModel : ImageResultsListener {

	public Text strongestEmo;
	public Text NorthEmoText;
	public Text EastEmoText;
	public Text SouthEmoText;
	public Text WestEmoText;

	public Text ChangeEmoCountText;

	public FeaturePoint[] featurePointsList;
	public int[] nextNavArray = new int[4];
	public int[] currNavArray = new int[4];

	public EmoNav strongestEmoNav;
	private int emoChangeCount = 0;
	private int emoChangeInterval = 15;

	private enum emotionEnum {Joy, Sadness, Disgust, Suprise};
	Dictionary<int, EmoNav> emotionDict = new Dictionary<int, EmoNav>();

	public EmotionListenerViewModel() {
		//EventController.Instance.Subscribe ();
		//subscribe to events in constructor or in awake functions
	}

	public void Start() {
		Debug.Log("Starting");
		emotionDict.Add((int)emotionEnum.Joy, new EmoNav ("Joy", 0, "North"));
		emotionDict.Add((int)emotionEnum.Sadness, new EmoNav ("Sadness", 0, "South"));
		emotionDict.Add((int)emotionEnum.Disgust, new EmoNav ("Disgust", 0, "East"));
		emotionDict.Add((int)emotionEnum.Suprise, new EmoNav ("Suprise", 0, "West"));
		//InvokeRepeating ("UpdateEmoNav", 0f, 15f);
		InvokeRepeating ("UpdateEmoChangeCount", 0f, 1f);
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
			strongestEmoNav = new EmoNav ("Nothing", 0, "Nowhere");


			faces[0].Emotions.TryGetValue (Emotions.Joy, out emotionDict [(int)emotionEnum.Joy].valence);
			faces[0].Emotions.TryGetValue (Emotions.Sadness, out emotionDict [(int)emotionEnum.Sadness].valence);
			faces[0].Emotions.TryGetValue (Emotions.Disgust, out emotionDict [(int)emotionEnum.Disgust].valence);
			faces[0].Emotions.TryGetValue (Emotions.Surprise, out emotionDict [(int)emotionEnum.Suprise].valence);


			if (emotionDict [(int)emotionEnum.Joy].valence > strongestEmoNav.valence) {
				strongestEmoNav = emotionDict [(int)emotionEnum.Joy];
			}

			if (emotionDict[(int)emotionEnum.Disgust].valence > strongestEmoNav.valence) {
				strongestEmoNav = emotionDict [(int)emotionEnum.Disgust];
			}
			if (emotionDict [(int)emotionEnum.Sadness].valence > strongestEmoNav.valence) {
				strongestEmoNav = emotionDict [(int)emotionEnum.Sadness];
			}
			if (emotionDict [(int)emotionEnum.Suprise].valence > strongestEmoNav.valence) {
				strongestEmoNav = emotionDict [(int)emotionEnum.Suprise];
			}

			this.strongestEmo.text = "Custom Strongest Emotion: " + strongestEmoNav.name + "/" + strongestEmoNav.valence;
			HighlightAndEvent (strongestEmoNav.direction);
		}
	}

	public void HighlightAndEvent(string direction) {
		NorthEmoText.color = Color.black;
		EastEmoText.color = Color.black;
		SouthEmoText.color = Color.black;
		WestEmoText.color = Color.black;

		switch(direction) {
		case "North":
			NorthEmoText.color = Color.green;
			OnNorthEmo();
			break;
		case "East":
			EastEmoText.color = Color.green;
			OnEastEmo();
			break;
		case "South":
			SouthEmoText.color = Color.green;
			OnSouthEmo();
			break;
		case "West":
			WestEmoText.color = Color.green;
			OnWestEmo();
			break;
		default:
			break;
		}
	}

	private void UpdateEmoChangeCount() {
		if (emoChangeCount > 0) {
			ChangeEmoCountText.text = "Change Emo In: " + emoChangeCount;
			emoChangeCount--;
		} else {
			ChangeEmoCountText.text = "Change Emo In: " + emoChangeCount;
			UpdateEmoNav ();
			emoChangeCount = 15;
		}
	}

	private void UpdateEmoNav() {
		Debug.Log ("Doing Randomize!");
		RandomizeEmotions();

		emotionDict [nextNavArray [0]].direction = "North";
		NorthEmoText.text = emotionDict [nextNavArray [0]].name;

		emotionDict [nextNavArray [1]].direction = "East";
		EastEmoText.text  = emotionDict [nextNavArray [1]].name;

		emotionDict [nextNavArray [2]].direction = "South";
		SouthEmoText.text  = emotionDict [nextNavArray [2]].name;

		emotionDict [nextNavArray [3]].direction = "West";
		WestEmoText.text  = emotionDict [nextNavArray [3]].name;
	}

	private void RandomizeEmotions() {
		nextNavArray [0] = 0;
		nextNavArray [1] = 1;
		nextNavArray [2] = 2;
		nextNavArray [3] = 3;
		for(int i = 0; i < nextNavArray.Length; i++) {
			int tmp = nextNavArray[i];
			int r = UnityEngine.Random.Range(i, nextNavArray.Length);
			nextNavArray[i] = nextNavArray[r];
			nextNavArray[r] = tmp;
		}

		Debug.Log(nextNavArray [0] + "/" + nextNavArray [1] + "/" + nextNavArray [2] + "/" + nextNavArray [3]);
	}

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

	public class EmoNav {
		public string name;
		public float valence;
		public string direction;

		public EmoNav(string name, float valence, string direction){
			this.name = name;
			this.valence = valence;
			this.direction = direction;
		}
	}

}
