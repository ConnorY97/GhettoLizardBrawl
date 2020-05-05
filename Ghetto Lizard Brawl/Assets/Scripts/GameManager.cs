using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

/// <summary>
/// Description:	Tracks all lizards in the arena and differentiates between AI and player controlled lizards.
/// Requirements:	Lizard, Nav mesh agent
/// Author(s):		Connor Young, Reyhan Rishard colour 
/// Date created:	04/05/20
/// Date modified:	04/05/20
/// </summary>

public class GameManager : MonoBehaviour
{ 
	private List<Lizard> _aiList = new List<Lizard>();
	private Lizard _player;

	//Inspector Variables--------------------------------------
	[Header("Lizard list")]
	public List<Lizard> completeList;
	[Header("Spawn points")]
	public List<Transform> aiSpawnPoints;
	public Transform playerSpawnPoint;
	[Header("Prefabs")]
	public Lizard[] aiControlledLizards;
	public Lizard playerControlledLizard; 

	private Lizard[] teams;

	private int _aiKnockouts = 0;
	[SerializeField] private int _playerLives = 3;

	[SerializeField] private GameObject _gameoverPanel;
	[SerializeField] private TMP_Text _gameoverTextMeshPro;
	[SerializeField] private string _gameoverSuffix;
	[SerializeField] private string _gameoverPrefix;


	// Start is called before the first frame update
	void Start()
	{
		_gameoverPanel.SetActive(false);
		teams = new Lizard[aiControlledLizards.Length];
		//Adding AI lizards to their own list 
		for (int i = 0; i < aiControlledLizards.Length; i++)
		{
			//Lizard aiTemp = Instantiate(aiControlledLizards[i], spawnPoints[i].position, Quaternion.identity);
			//_aiList.Add(aiTemp);
			//completeList.Add(aiTemp); 

			teams[i] = SpawnLizard(aiControlledLizards[i], aiSpawnPoints[i].position, true);
		}
		
		//_player = Instantiate(playerControlledLizard, spawnPoints[4].position + Vector3.up * (playerControlledLizard.transform.localScale.y), Quaternion.identity);
		_player = SpawnLizard(playerControlledLizard, playerSpawnPoint.position, false);
		//completeList.Add(_player); 
	}

	void OnEnable()
	{
		Lizard.OnLizardKnockout += OnKnockout;
	}

	void OnDisable()
	{
		Lizard.OnLizardKnockout -= OnKnockout;
	}

	Lizard SpawnLizard(Lizard lizard, Vector3 position, bool ai)
	{
		Lizard temp = Instantiate(lizard, position, Quaternion.identity);
		
		if (ai)
			_aiList.Add(temp);
		else
			_player = temp;

		completeList.Add(temp);

		return temp;
	}

	void OnKnockout(Lizard lizard)
	{
		int team = GetTeam(lizard);
		bool isPlayer = lizard == _player;

		completeList.Remove(lizard);

		if (isPlayer)
		{
			_playerLives--;

			if (_playerLives == 0)
			{
				// CONNOR SHOW GAMEOVER SCREEN HERE AND DISPLAY _aiKnockouts on it.
				Debug.Log("GAME OVERR!");
				Gameover();
			}
		}
		else
		{
			_aiKnockouts++;
			_aiList.Remove(lizard);
		}

		Destroy(lizard.gameObject);

		if (!isPlayer)
			StartCoroutine(SpawnCo(aiControlledLizards[team], aiSpawnPoints[team].position, true, team));
		else
			StartCoroutine(SpawnCo(playerControlledLizard, playerSpawnPoint.position, false, -1));	
	}

	int GetTeam(Lizard lizard)
	{
		for (int i = 0; i < teams.Length; i++)
		{
			if (teams[i] == lizard)
				return i;
		}

		Debug.Log(lizard.name);
		return -1;
	}

	IEnumerator SpawnCo(Lizard lizard, Vector3 position, bool isAI, int team)
	{
		yield return new WaitForSecondsRealtime(1.0f);

		if (!isAI)
		{
			_player = SpawnLizard(lizard, position, false);
		}
		else
		{
			Lizard temp = SpawnLizard(lizard, position, true);
			teams[team] = temp;
		}
	}

	void Gameover()
	{
		Time.timeScale = 0f;
		_gameoverTextMeshPro.text = $"{_gameoverSuffix} {_aiKnockouts} {_gameoverPrefix}";
		_gameoverPanel.SetActive(true);
	}
}