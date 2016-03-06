using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	//public GUIText countText;
	//public GUIText winText;
	//private int count;
	private int numberOfGameObjects;
	public AudioClip buzzAudio;


	void Awake()
	{
		EventController.Instance.Subscribe<GoWestEvent> (GoWest);
		EventController.Instance.Subscribe<GoEastEvent> (GoEast);
		EventController.Instance.Subscribe<GoNorthEvent> (GoNorth);
		EventController.Instance.Subscribe<GoSouthEvent> (GoSouth);
		EventController.Instance.Subscribe<GameOverEvent> (GameOver);
	}

	void GameOver(GameOverEvent eventTest) {
		EventController.Instance.UnSubscribe<GoWestEvent>(GoWest);
		EventController.Instance.UnSubscribe<GoEastEvent>(GoEast);
		EventController.Instance.UnSubscribe<GoNorthEvent>(GoNorth);
		EventController.Instance.UnSubscribe<GoSouthEvent>(GoSouth);
	}

	void GoWest(GoWestEvent eventTest) {
		Vector3 movement = new Vector3(-1.0f, 0.0f, 0.0f);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}

	void GoEast(GoEastEvent eventTest) {
		Vector3 movement = new Vector3(1.0f, 0.0f, 0.0f);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}

	void GoNorth(GoNorthEvent eventTest) {
		Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}

	void GoSouth(GoSouthEvent eventTest) {
		Vector3 movement = new Vector3(0.0f, 0.0f, -1.0f);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}

	void Start()
	{
		//count = 0;
		//SetCountText();
		//winText.text = "";
		numberOfGameObjects = GameObject.FindGameObjectsWithTag("PickUp").Length;
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		
		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.tag == "PickUpGood")
		{
			GetComponent<Rigidbody> ().mass = 0.3f;
			other.gameObject.SetActive(false);
			//count = count + 1;
			//SetCountText();
		} 
		if (other.gameObject.tag == "PickUpBad") {
			//Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);

			other.gameObject.SetActive(false);

			TextAsset zapperConfig = Resources.Load("config") as TextAsset;
			string url = zapperConfig.text;
			WWWForm form = new WWWForm ();
			form.AddField ("fake", "xy");

			WWW www = new WWW (url, form);
			EventController.Instance.Publish (new ZappedEvent());

			if (www.error == null) {
				AudioSource audio = other.gameObject.GetComponent<AudioSource>();
				audio.Play ();
			} else {
				Debug.Log("IFTTT error: " + www.error);
			}
		
		}
	}
	
	/*void SetCountText ()
	{
		countText.text = "Count: " + count.ToString();
		if(count >= numberOfGameObjects)
		{
			winText.text = "YOU WIN!";
		}
	}*/
}
