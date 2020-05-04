using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Description: Recives input from the player 
///     Uses that input to control the Lizard class attached to the object 
/// Requirements: Lizard
/// </summary>

[RequireComponent(typeof(Lizard))]

public class PlayerInputController : MonoBehaviour
{
	// Recieve simple input from the player whether that is movement or attack information
	// Movement will be force based using rigidbodies

	[SerializeField] private string _forwardAxis;
	[SerializeField] private string _horizontalAxis;
	[SerializeField] private KeyCode _attackButton;

	private Vector3 bufferedMovementDirection;
	
	private Lizard _src; // Lizard being controlled by this controller.

	private void Awake()
	{
		_src = GetComponent<Lizard>();
	}

	private void Update()
	{
		Vector3 movementInput = new Vector3(Input.GetAxisRaw(_horizontalAxis), 0.0f, Input.GetAxisRaw(_forwardAxis));
		bufferedMovementDirection = movementInput.normalized;

		if (Input.GetKeyDown(_attackButton))
			_src.Attack();
	}

	private void FixedUpdate()
	{
		_src.Accelerate(bufferedMovementDirection);
	}
}
