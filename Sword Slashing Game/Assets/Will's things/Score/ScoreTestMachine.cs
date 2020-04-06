using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTestMachine : MonoBehaviour
{
    // Start is called before the first frame update
    ScoreSystem scoreSystem;
    void Start()
    {
        scoreSystem = GameObject.FindObjectOfType<ScoreSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            scoreSystem.AddToScore(1);
        }else if (Input.GetKeyDown(KeyCode.O))
        {
            scoreSystem.AddToScore(-1);
        }
    }
}
