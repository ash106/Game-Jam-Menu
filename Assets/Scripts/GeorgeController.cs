using UnityEngine;
using System.Collections;
using Prime31;


public class GeorgeController : MonoBehaviour
{
	// movement config
//	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public string horizontalAxis;
	public string verticalAxis;
	public string idleAnimation;
	public string moveAnimation;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;
	private float normalizedVerticalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;


	void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		normalizedHorizontalSpeed = Input.GetAxisRaw (horizontalAxis);
		normalizedVerticalSpeed = Input.GetAxisRaw (verticalAxis);

		if( Input.GetKey( KeyCode.LeftArrow ) )
		{
//			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

//			_animator.Play( Animator.StringToHash( moveAnimation ) );
		}
		else if( Input.GetKey( KeyCode.RightArrow ) )
		{
//			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

//			_animator.Play( Animator.StringToHash( moveAnimation ) );
		}
		else
		{
//			normalizedHorizontalSpeed = 0;

//			_animator.Play( Animator.StringToHash( idleAnimation ) );
		}


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
