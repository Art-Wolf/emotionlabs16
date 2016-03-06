using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{	
	public float speed;
	//public GUIText countText;
	//public GUIText winText;
	//private int count;
	private int numberOfGameObjects;


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
		if(other.gameObject.tag == "PickUp")
		{
			GetComponent<Rigidbody> ().mass = 0.1f;
			other.gameObject.SetActive(false);
			//count = count + 1;
			//SetCountText();
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
