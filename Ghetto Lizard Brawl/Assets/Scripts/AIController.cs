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
/// Requirements:	Lizard
/// Author(s):		Connor Young, Reyhan Rishard
/// Date created:	04/05/20
/// Date modified:	04/05/20
/// </summary>

[RequireComponent(typeof(Lizard))]

public class AIController : MonoBehaviour
{
	//Components--------------------------------------
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
		//Assigning variables
		_src = GetComponent<Lizard>();
		_gameManager = FindObjectOfType<GameManager>();
		_currentState = State.CHOOSETARGET;
		_visualChaseRadius.transform.localScale = new Vector2(chaseRadius, chaseRadius);
		_visualSurpriseRadius.transform.localScale = new Vector2(chaseRadius * 0.5f, chaseRadius * 0.5f);
	}

	public void Update()
	{

		if (_targetLizard != null)
		{
			Vector3 _facing = (_targetLizard.transform.position - _src.transform.position).normalized;
			_src.Orientate(_facing);
		}
		else
		{
			_src.Orientate(Vector3.forward);
		}

		//If the target it outside the chase radius then the timer will increase 
		if (_targetDistance > chaseRadius)
		{
			_searchTimer += Time.deltaTime; 
		}

		//State machine for the AI 
		switch (_currentState)
		{
			//Moving towards the AI target 
			case State.MOVINGTOTARGET:
				Vector3 direction = (_targetLizard.transform.position - this.transform.position).normalized;
				_src.Accelerate(direction); 
				//If the timer exceeds 4 seconds the AI will change target
				if (_searchTimer > 4.0f)
				{
					_currentState = State.CHOOSETARGET;
				}
				//When a lizard passes by close enough 60% of the time it will become the new target 
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
			//Choosing a random target for the AI
			case State.CHOOSETARGET:
				_targetLizard = FindRandomLizard();
				_searchTimer = 0.0f;
				_currentState = State.MOVINGTOTARGET;
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// Returns a random lizard in the scene excluding itself 
	/// </summary>
	/// <returns></returns>
	Lizard FindRandomLizard()
	{		
		int randomIndex = Random.Range(0, 3);
		//Creates a list of lizards excluding the one this script is attached to 
		Lizard temp = _gameManager.completeList.Where(ai => ai != _src).ToList()[randomIndex];
		return temp; 
	}
}

