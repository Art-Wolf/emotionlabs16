using UnityEngine;
using System.Collections;

public class WallColorController : MonoBehaviour 
{	
	private GameObject[] wallTops;
	private Component[] topRender;

	public Material sadMaterial;
	public Material angryMaterial;
	public Material happyMaterial;
	public Material surpriseMaterial;


	public AudioClip happyAudio;
	public AudioClip sadAudio;
	public AudioClip angryAudio;
	public AudioClip surpriseAudio;
	public AudioClip applauseAudio;

	private float startingMetalic = 10f;
	private float endingMetalic = 5f;
	private float currentMetalic = 0f;

	private bool decreasing = true;

	private float currentMat = 1;

	private string currentEmotion = "";

	void Start()
	{
		Debug.Log ("WallColorController: Start");
		if (wallTops == null) {
			wallTops = GameObject.FindGameObjectsWithTag ("top");
		}

		//InvokeRepeating("changeWallColors", 1f, 1f);
	}

	void OnLevelWasLoaded(int level) {
		Debug.Log ("WallColorController: OnLevelWasLoaded");
		if (wallTops == null) {
			wallTops = GameObject.FindGameObjectsWithTag ("top");
		}
	}

	/** Don't call.. **/
	void changeWallColors() 
	{
		if (decreasing) {
			if (currentMetalic > endingMetalic)
				currentMetalic -= 1f;
			else {
				decreasing = false;
				currentMetalic += 1f;
			}
		} else {
			if (currentMetalic < startingMetalic)
				currentMetalic += 1f;
			else {
				decreasing = true;
				currentMetalic -= 1f;
			}
		}

		sadMaterial.SetVector("_Color", new Vector4(0f, 0f, 1f , 1f ));
		angryMaterial.SetVector("_Color", new Vector4(1f, 0f, 0f, 1f));
		happyMaterial.SetVector("_Color", new Vector4(0f, 1f, 0f, 1f));
		surpriseMaterial.SetVector("_Color", new Vector4(1f, 0.92f, 1f, 1f));

		foreach (GameObject top in wallTops) {
			topRender = top.GetComponentsInChildren<Renderer>();
			foreach (Renderer rend in topRender) 
			{
				if (currentMat == 1) {
					rend.material.Lerp (rend.material, surpriseMaterial, 0.5f);
				} else if (currentMat == 2) {
					rend.material.Lerp (rend.material, angryMaterial, 0.5f);
				} else if (currentMat == 3) {
					rend.material.Lerp (rend.material, happyMaterial, 0.5f);
				} else {
					rend.material.Lerp (rend.material, sadMaterial, 0.5f);
				}
					
			}
		}

	}

	void Awake()
	{
		EventController.Instance.Subscribe<GoWestEvent> (GoWest);
		EventController.Instance.Subscribe<GoEastEvent> (GoEast);
		EventController.Instance.Subscribe<GoNorthEvent> (GoNorth);
		EventController.Instance.Subscribe<GoSouthEvent> (GoSouth);
		EventController.Instance.Subscribe<GameOverEvent> (GameOver);
	}

	void GameOver(GameOverEvent eventTest) {
		/*EventController.Instance.UnSubscribe<GoWestEvent>(GoWest);
		EventController.Instance.UnSubscribe<GoEastEvent>(GoEast);
		EventController.Instance.UnSubscribe<GoNorthEvent>(GoNorth);
		EventController.Instance.UnSubscribe<GoSouthEvent>(GoSouth);*/
		setMusic ("GameOver");
	}

	void setMusic(string emotion) {
		if (!currentEmotion.Equals(emotion)) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.Stop();

			if (emotion.Equals ("Joy")) {
				audio.clip = happyAudio;
			} else if (emotion.Equals ("Sadness")) {
				audio.clip = sadAudio;
			} else if (emotion.Equals ("Disgust")) {
				audio.clip = angryAudio;
			} else if (emotion.Equals ("Suprise")) {
				audio.clip = surpriseAudio;
			} else if (emotion.Equals ("GameOver")) {
				audio.clip = applauseAudio;
			}

			audio.Play();
			currentEmotion = emotion;
		}
	}

	Material getColor(string emotion) {
		setMusic (emotion);
		if (emotion.Equals ("Joy")) {
			return happyMaterial;
		} else if (emotion.Equals ("Sadness")) {
			return sadMaterial;
		} else if (emotion.Equals ("Disgust")) {
			return angryMaterial;
		}
		else {
			return surpriseMaterial;
		}
	}

	void GoWest(GoWestEvent eventTest) {
		currentMat = 1;
		foreach (GameObject top in wallTops) {
			if (top != null) {
				topRender = top.GetComponentsInChildren<Renderer> ();
				foreach (Renderer rend in topRender) {
					rend.material.Lerp (rend.material, getColor (eventTest.emotion), 0.5f);
				}
			}
		}
	}

	void GoEast(GoEastEvent eventTest) {
		currentMat = 2;
		foreach (GameObject top in wallTops) {
			if (top != null) {
				topRender = top.GetComponentsInChildren<Renderer> ();
				foreach (Renderer rend in topRender) {
					rend.material.Lerp (rend.material, getColor (eventTest.emotion), 0.5f);
				}
			}
		}
	}

	void GoNorth(GoNorthEvent eventTest) {
		currentMat = 3;
		foreach (GameObject top in wallTops) {
			if (top != null) {
				topRender = top.GetComponentsInChildren<Renderer> ();
				foreach (Renderer rend in topRender) {
					rend.material.Lerp (rend.material, getColor (eventTest.emotion), 0.5f);
				}
			}
		}
	}

	void GoSouth(GoSouthEvent eventTest) {
		currentMat = 4;
		foreach (GameObject top in wallTops) {
			if (top != null) {
				topRender = top.GetComponentsInChildren<Renderer> ();
				foreach (Renderer rend in topRender) {
					rend.material.Lerp (rend.material, getColor (eventTest.emotion), 0.5f);
				}
			}
		}
	}
}