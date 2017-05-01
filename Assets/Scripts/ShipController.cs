using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public float speed;
	public GameObject buttons;
	public GameObject bottomBoosterFire;
	public GameObject topBoosterFire;
	public GameObject leftBoosterFire;
	public GameObject rightBoosterFire;

	private Rigidbody2D rb;
	public bool playerOneCanDrive = false;
	public bool playerTwoCanDrive = false;
	private Color darkColor;
	private SpriteRenderer bottomBoosterFireRenderer;
	private SpriteRenderer topBoosterFireRenderer;
	private SpriteRenderer leftBoosterFireRenderer;
	private SpriteRenderer rightBoosterFireRenderer;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		darkColor = buttons.GetComponent<SpriteRenderer> ().material.color;
		bottomBoosterFireRenderer = bottomBoosterFire.GetComponent<SpriteRenderer> ();
		bottomBoosterFireRenderer.enabled = false;
		topBoosterFireRenderer = topBoosterFire.GetComponent<SpriteRenderer> ();
		topBoosterFireRenderer.enabled = false;
		leftBoosterFireRenderer = leftBoosterFire.GetComponent<SpriteRenderer> ();
		leftBoosterFireRenderer.enabled = false;
		rightBoosterFireRenderer = rightBoosterFire.GetComponent<SpriteRenderer> ();
		rightBoosterFireRenderer.enabled = false;
	}

	void FixedUpdate () {
		if (playerOneCanDrive || playerTwoCanDrive) {
			float moveHorizontal = 0f;
			float moveVertical = 0f;

			if (playerOneCanDrive) {
				moveHorizontal = Input.GetAxis ("Ship_Horizontal_P1");
				moveVertical = Input.GetAxis ("Ship_Vertical_P1");
			}

			if (playerTwoCanDrive) {
				moveHorizontal = Input.GetAxis ("Ship_Horizontal_P2");
				moveVertical = Input.GetAxis ("Ship_Vertical_P2");
			}

			// Need to decide how to handle two drivers, currently just adding both of their input together so they either
			// cancel each other out or double the normal range to 2.0

			// Also if the two drivers are cancelling each other out, neither booster lights up. I think both should light up.
//			if (playerOneCanDrive && playerTwoCanDrive) {
//				moveHorizontal = Input.GetAxis ("Ship_Horizontal_P1");
//				moveHorizontal += Input.GetAxis ("Ship_Horizontal_P2");
//				moveVertical = Input.GetAxis ("Ship_Vertical_P1");
//				moveVertical += Input.GetAxis ("Ship_Vertical_P2");
//			}

			if (moveVertical > 0.5f) {
				bottomBoosterFireRenderer.enabled = true;
			} else {
				bottomBoosterFireRenderer.enabled = false;
			}
			if (moveVertical < -0.5f) {
				topBoosterFireRenderer.enabled = true;
			} else {
				topBoosterFireRenderer.enabled = false;
			}
			if (moveHorizontal > 0.5f) {
				leftBoosterFireRenderer.enabled = true;
			} else {
				leftBoosterFireRenderer.enabled = false;
			}
			if (moveHorizontal < -0.5f) {
				rightBoosterFireRenderer.enabled = true;
			} else {
				rightBoosterFireRenderer.enabled = false;
			}

			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

			rb.AddForce (movement * speed);
		} else {
			bottomBoosterFireRenderer.enabled = false;
			topBoosterFireRenderer.enabled = false;
			leftBoosterFireRenderer.enabled = false;
			rightBoosterFireRenderer.enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
//		if (other.name == "PlayerOne" && !playerTwoCanDrive) {
//			playerOneCanDrive = true;
//		}
//		if (other.name == "PlayerTwo" && !playerOneCanDrive) {
//			playerTwoCanDrive = true;
//		}
//		buttons.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.name == "PlayerOne") {
			playerOneCanDrive = false;
		}
		if (other.name == "PlayerTwo") {
			playerTwoCanDrive = false;
		}
		if (!playerOneCanDrive && !playerTwoCanDrive) {
			buttons.GetComponent<SpriteRenderer> ().material.color = darkColor;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.name == "PlayerOne" && !playerOneCanDrive) {
			// if press L1 and !playerTwoCanDrive
			if (Input.GetButtonDown ("Interact_P1") && !playerTwoCanDrive) {
				playerOneCanDrive = true;
				buttons.GetComponent<SpriteRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f);
			}
		}
		if (other.name == "PlayerTwo" && !playerTwoCanDrive) {
			// if press F or L1 and !playerOneCanDrive
			if (Input.GetButtonDown ("Interact_P2") && !playerOneCanDrive) {
				playerTwoCanDrive = true;
				buttons.GetComponent<SpriteRenderer> ().material.color = new Color (1.0f, 1.0f, 1.0f);
			}
		}
	}
}
