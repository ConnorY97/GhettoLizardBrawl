﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Description:	Tracks all lizards in the arena and differentiates between AI and player controlled lizards 
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
	[SerializeField]
	private List<Lizard> _completeList;

	[Header("Spawn points")]
	public List<Transform> spawnPoints;

	[Header("Prefabs")]
	public Lizard aiControlledLizard;
	public Lizard playerControlledLizard; 

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i <= 3; i++)
		{
			Lizard aiTemp = Instantiate(aiControlledLizard, spawnPoints[i].position, Quaternion.identity);
			_aiList.Add(aiTemp); 
		}

		_player = Instantiate(playerControlledLizard, spawnPoints[4].position, Quaternion.identity);

		_completeList = new List<Lizard>(_aiList);
		_completeList.Add(_player); 
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
