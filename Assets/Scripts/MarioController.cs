using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class MarioController : MonoBehaviour {

	// movement config
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float rotationSpeed = 720.0f;
	public string horizontalAxis;
	public string verticalAxis;
	public string idleAnimation;
	public string moveAnimation;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	private float normalizedVerticalSpeed = 0;
	private float heading;
	private Quaternion qTo;

	private CharacterController2D _controller;
	private Animator _animator;
	private Vector3 _velocity;

	void Awake()
	{
		_controller = GetComponent<CharacterController2D>();
		_animator = GetComponent<Animator>();
	}

	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		//		if( Input.GetKey( KeyCode.D) )
		//		{
		//			normalizedHorizontalSpeed = 1;
		//			transform.localRotation = Quaternion.Euler (0.0f, 0.0f, -90.0f);
		//		}
		//		else if( Input.GetKey( KeyCode.A ) )
		//		{
		//			normalizedHorizontalSpeed = -1;
		//			transform.localRotation = Quaternion.Euler (0.0f, 0.0f, 90.0f);
		//		}
		//		else
		//		{
		//			normalizedHorizontalSpeed = 0;
		//		}
		//
		//		if( Input.GetKey( KeyCode.W ) )
		//		{
		//			normalizedVerticalSpeed = 1;
		//			transform.localRotation = Quaternion.Euler (0.0f, 0.0f, 0.0f);
		//		}
		//		else if( Input.GetKey( KeyCode.S ) )
		//		{
		//			normalizedVerticalSpeed = -1;
		//			transform.localRotation = Quaternion.Euler (0.0f, 0.0f, 180.0f);
		//		}
		//		else
		//		{
		//			normalizedVerticalSpeed = 0;
		//		}

		normalizedHorizontalSpeed = Input.GetAxisRaw (horizontalAxis);
		normalizedVerticalSpeed = Input.GetAxisRaw (verticalAxis);

		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * groundDamping );

		_velocity.y = Mathf.Lerp( _velocity.y, normalizedVerticalSpeed * runSpeed, Time.deltaTime * groundDamping );

		if (normalizedHorizontalSpeed != 0 || normalizedVerticalSpeed != 0) {
//			heading = Mathf.Atan2 (normalizedVerticalSpeed, normalizedHorizontalSpeed);
//			qTo = Quaternion.Euler (0f, 0f, (heading * Mathf.Rad2Deg) + 90);
//			transform.localRotation = Quaternion.RotateTowards (transform.localRotation, qTo, rotationSpeed * Time.deltaTime);
			_animator.Play (Animator.StringToHash (moveAnimation));
		} else {
			_animator.Play (Animator.StringToHash (idleAnimation));
		}

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}
}
