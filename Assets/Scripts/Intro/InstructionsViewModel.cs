using UnityEngine;
using System.Collections;

public class InstructionsViewModel : MonoBehaviour {

	public void ShowInstructionsPanel () {
		this.transform.SetAsLastSibling ();
	}

	public void HideInstructionsPanel () {
		this.transform.SetAsFirstSibling ();
	}
}
