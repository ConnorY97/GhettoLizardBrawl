using System.Collections;
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
	[SerializeField]
	private List<Lizard> _allLizards;

	public List<Transform> spawnPoints;

	public Object aiControlledLizard;
	public Object playerControlledLizard; 

	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i <= 3; i++)
		{
			Instantiate(aiControlledLizard, spawnPoints[i]); 
		}
		Instantiate(playerControlledLizard, spawnPoints[4]); 
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
