using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform groundCheckTransform;
	private bool jumpKeyWasPressed;
	private float horizntalInput;
	private Rigidbody rigidbodyComponent;
	private int superJumpRemaining;
	private int score;

	// Use this for initialization
	void Start () {
		rigidbodyComponent = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		//check if space key is pressed down
		if (Input.GetKeyDown (KeyCode.Space)) {
			jumpKeyWasPressed = true;
		}
		horizntalInput = Input.GetAxis("Horizontal");
	}


	// fixed update is caleed once every physics update
	private void FixedUpdate(){
		rigidbodyComponent.velocity = new Vector3 (horizntalInput, rigidbodyComponent.velocity.y, 0);
		if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1) {
			return;
		}
			
		if (jumpKeyWasPressed) {
			Debug.Log("Space key was pressed Down");

			float jumpPower = 5;
			if (superJumpRemaining > 0) {
				jumpPower *= 2;
				superJumpRemaining--;
			}
			rigidbodyComponent.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
			jumpKeyWasPressed = false;
		}

	}

	private void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == 8) {
			Destroy(other.gameObject);
			superJumpRemaining++;
			score++;
		}
	}


}
