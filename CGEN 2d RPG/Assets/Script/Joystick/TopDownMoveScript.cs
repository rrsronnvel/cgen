using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TopDownMoveScript : MonoBehaviour {

	// Variables to hold Input directions in X and Y axses
	float dirX, dirY;

	// Move speed variable can be set in Inspector with slider
	[Range(1f, 20f)]
	public float moveSpeed = 5f;
		
	// Update is called once per frame
	void Update () {

		// Getting move direction according to button pressed
		// multiplied by move speed and time passed
		dirX = CrossPlatformInputManager.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
		dirY = CrossPlatformInputManager.GetAxis ("Vertical") * moveSpeed * Time.deltaTime;

		// Setting new transform position of game object
		transform.position = new Vector2 (transform.position.x + dirX, transform.position.y + dirY);
	}
}
