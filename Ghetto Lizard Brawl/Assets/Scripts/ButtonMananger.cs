using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

/// <summary>
/// Description:	Manages button behaviour
/// Requirements:	Lizard
/// Author(s):		Connor Young, Reyhan Rishard
/// Date created:	04/05/20
/// Date modified:	04/05/20
/// </summary>

public class ButtonMananger : MonoBehaviour
{
   // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit(); 
    }

    public void Replay()
    {
        SceneManager.LoadScene(1); 
    }

    public void Play()
    {
        SceneManager.LoadScene(1); 
    }
}
