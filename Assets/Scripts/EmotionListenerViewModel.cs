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

	public Image NorthEmoImg;
	public Image EastEmoImg;
	public Image SouthEmoImg;
	public Image WestEmoImg;

	public Image NorthArrowImg;
	public Image EastArrowImg;
	public Image SouthArrowImg;
	public Image WestArrowImg;

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

		emotionDict.Add((int)emotionEnum.Joy, new EmoNav ("Joy", 0, "North", "Sprites/joyIcon", Color.green));
		emotionDict.Add((int)emotionEnum.Sadness, new EmoNav ("Sadness", 0, "South", "Sprites/sadIcon", Color.blue));
		emotionDict.Add((int)emotionEnum.Disgust, new EmoNav ("Disgust", 0, "East", "Sprites/disgustIcon", Color.red));
		emotionDict.Add((int)emotionEnum.Suprise, new EmoNav ("Suprise", 0, "West", "Sprites/supriseIcon", Color.yellow));
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
			strongestEmoNav = new EmoNav ("Nothing", 0, "Nowhere", "Sprites/angryIcon", Color.gray);

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
			HighlightAndEvent (strongestEmoNav.direction, strongestEmoNav.name);
		}
	}

	public void HighlightAndEvent(string direction, string emotion) {
		NorthEmoText.color = Color.black;
		EastEmoText.color = Color.black;
		SouthEmoText.color = Color.black;
		WestEmoText.color = Color.black;

		NorthArrowImg.color = Color.gray;
		EastArrowImg.color = Color.gray;
		SouthArrowImg.color = Color.gray;
		WestArrowImg.color = Color.gray;

		switch(direction) {
		case "North":
			NorthEmoText.color = Color.green;
			OnNorthEmo(emotion);
			break;
		case "East":
			EastEmoText.color = Color.green;
			OnEastEmo(emotion);
			break;
		case "South":
			SouthEmoText.color = Color.green;
			OnSouthEmo(emotion);
			break;
		case "West":
			WestEmoText.color = Color.green;
			OnWestEmo(emotion);
			break;
		default:
			break;
		}
	}

	private void UpdateEmoChangeCount() {
		if (emoChangeCount > 0) {
			ChangeEmoCountText.text = "Emotions Change: " + emoChangeCount;
			emoChangeCount--;
		} else {
			ChangeEmoCountText.text = "Emotions Change: " + emoChangeCount;
			UpdateEmoNav ();
			emoChangeCount = 10;
		}
	}

	private void UpdateEmoNav() {
		Debug.Log ("Doing Randomize!");
		RandomizeEmotions();

		emotionDict [nextNavArray [0]].direction = "North";
		NorthEmoText.text = emotionDict [nextNavArray [0]].name;
		NorthEmoImg.sprite = Resources.Load<Sprite> (emotionDict [nextNavArray [0]].sprite);
		NorthArrowImg.color = emotionDict [nextNavArray [0]].emoColor;

		emotionDict [nextNavArray [1]].direction = "East";
		EastEmoText.text  = emotionDict [nextNavArray [1]].name;
		EastEmoImg.sprite = Resources.Load<Sprite> (emotionDict [nextNavArray [1]].sprite);
		EastArrowImg.color = emotionDict [nextNavArray [1]].emoColor;

		emotionDict [nextNavArray [2]].direction = "South";
		SouthEmoText.text  = emotionDict [nextNavArray [2]].name;
		SouthEmoImg.sprite = Resources.Load<Sprite> (emotionDict [nextNavArray [2]].sprite);
		SouthArrowImg.color = emotionDict [nextNavArray [2]].emoColor;

		emotionDict [nextNavArray [3]].direction = "West";
		WestEmoText.text  = emotionDict [nextNavArray [3]].name;
		WestEmoImg.sprite = Resources.Load<Sprite> (emotionDict [nextNavArray [3]].sprite);
		WestArrowImg.color = emotionDict [nextNavArray [3]].emoColor;
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

	public void OnNorthEmo(string emotion) {
		Debug.Log("North Emo");
		EventController.Instance.Publish (new GoNorthEvent(emotion));
	}

	public void OnWestEmo(string emotion) {
		Debug.Log("West Emo");
		EventController.Instance.Publish (new GoWestEvent(emotion));
	}

	public void OnEastEmo(string emotion) {
		Debug.Log("East Emo");
		EventController.Instance.Publish (new GoEastEvent(emotion));
	}

	public void OnSouthEmo(string emotion) {
		Debug.Log("South Emo");
		EventController.Instance.Publish (new GoSouthEvent(emotion));
	}

	public class EmoNav {
		public string name;
		public float valence;
		public string direction;
		public string sprite;
		public Color emoColor;

		public EmoNav(string name, float valence, string direction, string sprite, Color emoColor){
			this.name = name;
			this.valence = valence;
			this.direction = direction;
			this.sprite = sprite;
			this.emoColor = emoColor;
		}
	}

}
