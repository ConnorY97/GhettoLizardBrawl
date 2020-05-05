using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; 

//Enums must be defined before anything else in the class 
public enum State
{
	MOVINGTOTARGET,
	CHOOSETARGET
}

/// <summary>
/// Description:	Recieves input from AI to control the lizard
/// Requirements:	Lizard, Nav mesh agent
/// Author(s):		Connor Young, Reyhan Rishard
/// Date created:	04/05/20
/// Date modified:	04/05/20
/// </summary>

[RequireComponent(typeof(Lizard))]
[RequireComponent(typeof(NavMeshAgent))]

public class AIController : MonoBehaviour
{
	//Components--------------------------------------
	private NavMeshAgent _ai = null;
	private Lizard _src = null;
	private GameManager _gameManager = null;

	//Target variables--------------------------------
	private float _searchTimer = 0.0f;
	private float _targetDistance = int.MaxValue;
	
	//Inspector Variables-----------------------------
	public float chaseRadius;
	[SerializeField]
	private GameObject _visualChaseRadius;
	[SerializeField]
	private GameObject _visualSurpriseRadius;
	[SerializeField]
	private State _currentState;
	[SerializeField]
	private Lizard _targetLizard;

	public void Start()
	{
		_ai = GetComponent<NavMeshAgent>();
		_src = GetComponent<Lizard>();
		_gameManager = FindObjectOfType<GameManager>();
		_currentState = State.CHOOSETARGET;

		_visualChaseRadius.transform.localScale = new Vector2(chaseRadius, chaseRadius);
		_visualSurpriseRadius.transform.localScale = new Vector2(chaseRadius * 0.5f, chaseRadius * 0.5f);
	}

	public void Update()
	{
		if (_targetDistance > chaseRadius)
		{
			_searchTimer += Time.deltaTime; 
		}

		switch (_currentState)
		{
			case State.MOVINGTOTARGET:				
				_ai.SetDestination(_targetLizard.transform.position);
				if (_searchTimer > 4.0f)
				{
					_currentState = State.CHOOSETARGET;
				}
				foreach (Lizard currentLizard in _gameManager.completeList)
				{
					if (_src != currentLizard && currentLizard != _targetLizard)
					{
						if (Vector3.Distance(this.transform.position, currentLizard.transform.position) < chaseRadius * 0.5f)
						{
							int randomNumber = Random.Range(1, 10);
							if (randomNumber <= 6)
							{
								_targetLizard = currentLizard;
								_searchTimer = 0.0f; 
							}

						}
					}
				}
				break;
			case State.CHOOSETARGET:				
				_targetLizard = FindRandomLizard();
				_searchTimer = 0.0f;
				_currentState = State.MOVINGTOTARGET;
				break;
			default:
				break;
		}
	}

	Lizard FindRandomLizard()
	{
		int randomIndex = Random.Range(0, 3);

		Lizard temp = _gameManager.completeList.Where(ai => ai != _src).ToList()[randomIndex];
		return temp; 
	}
}

