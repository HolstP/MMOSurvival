using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public float interactionRange = 50f;

	public virtual void Interact() {
		Debug.Log ("Interacting with base class.");
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position, interactionRange);
	}
}
