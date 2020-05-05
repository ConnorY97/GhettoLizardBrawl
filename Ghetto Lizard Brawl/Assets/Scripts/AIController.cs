using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq; 

//Enums must be defined before anything else in the class 
public enum State
{
	IDLE,
	MOVINGTOTARGET,
	CHOOSETARGET, 
	ATTACKING
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

	//Private variables------------------------------
	Vector3 _facing = Vector3.forward; 

	//Inspector Variables-----------------------------
	public float chaseRadius;
	[SerializeField]
	private State _currentState;
	[SerializeField]
	private Lizard _targetLizard;
	public float attackRange = 0.05f; 

	[SerializeField] private KnockbackData _targetedKnockbackData;
	[SerializeField] private KnockbackData _targetlessKnockbackData;

	[SerializeField] private Hitbox _hitbox;

	public void Start()
	{
		//Assigning variables
		_src = GetComponent<Lizard>();
		_gameManager = FindObjectOfType<GameManager>();
		_currentState = State.CHOOSETARGET;
		_hitbox.Initialise(_src);
		_hitbox.ToggleTriggers(true);
	}

	private void OnEnable()
	{
		_hitbox.OnHitboxEnter += OnHitboxEnter;
	}

	private void OnDisable()
	{
		_hitbox.OnHitboxEnter -= OnHitboxEnter;
	}

	public void Update()
	{
		if (_targetLizard != null)
		{
			_facing = (_targetLizard.transform.position - _src.transform.position).normalized;
			_facing.y = _src.transform.position.y;
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

		if (_targetLizard == null)
		{
			_currentState = State.CHOOSETARGET;
		}

		//State machine for the AI 
		switch (_currentState)
		{
			case State.IDLE:
			_src.Stop();

			_targetLizard = FindRandomLizard();

			if (_targetLizard != null)
				_currentState = State.MOVINGTOTARGET;
			break;
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
				_targetDistance = Vector3.Distance(this.transform.position, _targetLizard.transform.position); 
				if (_targetDistance < attackRange)
					_currentState = State.ATTACKING;
				break;
			//Choosing a random target for the AI
			case State.CHOOSETARGET:
				_targetLizard = FindRandomLizard();
				_searchTimer = 0.0f;

				if (_targetLizard == null)
					_currentState = State.IDLE;
				else
					_currentState = State.MOVINGTOTARGET;
				break;
			case State.ATTACKING:
				_targetLizard.Knockback(_facing, _targetedKnockbackData);
				_currentState = State.CHOOSETARGET;
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
		if (_gameManager.completeList.Count - 1 <= 0)
			return null;

		int randomIndex = Random.Range(0, _gameManager.completeList.Count - 1);
		//Creates a list of lizards excluding the one this script is attached to 
		Lizard temp = _gameManager.completeList.Where(ai => ai != _src).ToList()[randomIndex];
		return temp; 
	}

	private void OnHitboxEnter(Lizard other)
	{
		Vector3 direction = (other.transform.position - transform.position).normalized;
		other.Knockback(direction, _targetlessKnockbackData);
	}
}

