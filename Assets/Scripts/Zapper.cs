using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Zapper : MonoBehaviour {

	public GUIText zapped;

	void Start () {

		TextAsset zapperConfig = Resources.Load("config") as TextAsset;
		string url = zapperConfig.text;
		zapped.text = url;
		WWWForm form = new WWWForm ();
		form.AddField ("fake", "xy");

		WWW www = new WWW (url, form);

		StartCoroutine (WaitForRequest (www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		if (www.error == null) {
			zapped.text = "GOTCHA";
		} else {
			zapped.text = www.error;
		}
	}
}
