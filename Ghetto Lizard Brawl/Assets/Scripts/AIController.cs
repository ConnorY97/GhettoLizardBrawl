using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Description:	Recieves input from AI to control the lizard
/// Requirements:	Lizard, Nav mesh agent
/// Author(s):		Connor Young, Reyhan Rishard
/// Date created:	04/05/20
/// Date modified:	04/05/20
/// </summary>


//Enums must be defined before anything else in the class 
enum State
{
	LOOKING,
	ATTACKING
}

[RequireComponent(typeof(Lizard))]
[RequireComponent(typeof(NavMeshAgent))]

public class AIController : MonoBehaviour
{
	private NavMeshAgent _ai = null;	
	private Lizard _src = null;
	private GameManager _gameManager = null;

	private float _targetTimer = 0.0f;
	private float _targetDistance; 
	
	//Inspector Variables-----------------------------
	public float targetRange;
	public float unexpectedRange; 







	public void Start()
	{
		_ai = GetComponent<NavMeshAgent>();
		_src = GetComponent<Lizard>();
		_gameManager = FindObjectOfType<GameManager>(); 
	}

	public void Update()
	{
	}
}