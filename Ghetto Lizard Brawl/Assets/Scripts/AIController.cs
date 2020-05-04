using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Description: Passes movement and attack information to the Liazrd class attached the object 
///     This class will control when the AI moves and attacks 
/// Requirements: Lizard
/// </summary>

[RequireComponent(typeof(Lizard))]
public class AIController : MonoBehaviour
{
	// This AI will use navmesh to find and move towards other controllers
	// Colliders will be used to determin if it is in range to attack or not 
	// Improvements that can me made are field of view for attack 
	public void Update()
	{
		// Compute AI logic.
	}
}