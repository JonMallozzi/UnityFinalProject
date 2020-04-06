using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a class for interacting with objects
public class Interactable : MonoBehaviour
{
	public float radius = 3f;
	
	bool isFocus = false;
	Transform player;

	bool hasInteracted = false;

	public virtual void Interact () {}

	void Update () { 

		if (isFocus && !hasInteracted) {
			float distance = Vector3.Distance(player.position, transform.position);
			
			if (distance <= radius){
				Interact();
				hasInteracted = true;
			}
		}
	}

	public void onFocused (Transform playerTransform){
		isFocus = true;
		player = playerTransform;
	}

	public void onDefocused (){
		isFocus = false;
		player = null;
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
