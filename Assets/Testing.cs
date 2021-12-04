using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Testing : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        GameManager.manager.testing = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Active scene is {SceneManager.GetActiveScene().name}");

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            
            Application.Quit();
        }
        else
        { 
            if (GameManager.manager.phase == Enums.GameplayPhase.Planning)
            {
                GameManager.manager.EndPlanning();
            }
            else if (GameManager.manager.phase == Enums.GameplayPhase.Resolve)
            {

            }
        }
    }
}
