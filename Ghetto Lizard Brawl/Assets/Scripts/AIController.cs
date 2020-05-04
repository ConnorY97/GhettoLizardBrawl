using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Description:	Recieves input from AI to control the lizard
/// Requirements:	Lizard, Nav mesh agent
/// Author(s):		Connor Young, Reyhan Rishard
/// Date created:	04/05/20
/// Date modifited:	04/05/20
/// </summary>

[RequireComponent(typeof(Lizard))]
[RequireComponent(typeof(NavMeshAgent))]

public class AIController : MonoBehaviour
{
	private NavMeshAgent _ai = null;
	private Lizard _src = null;
	public Transform target = null; 


	



	public void Start()
	{
		_ai = GetComponent<NavMeshAgent>();
		_src = GetComponent<Lizard>(); 
	}

	public void Update()
	{
		_ai.SetDestination(target.position);
	}
}