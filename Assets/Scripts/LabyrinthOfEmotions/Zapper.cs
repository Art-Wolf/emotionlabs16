using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Zapper : MonoBehaviour {

	public AudioClip buzzAudio;

	void OnCollisionEnter(Collision collisionInfo)
	{
		Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);

		TextAsset zapperConfig = Resources.Load("config") as TextAsset;
		string url = zapperConfig.text;
		WWWForm form = new WWWForm ();
		form.AddField ("fake", "xy");

		WWW www = new WWW (url, form);
		EventController.Instance.Publish (new ZappedEvent());

		if (www.error == null) {
			AudioSource audio = GetComponent<AudioSource>();
			audio.clip = buzzAudio;
			audio.Play ();
		} else {
			Debug.Log("IFTTT error: " + www.error);
		}
	}
}
