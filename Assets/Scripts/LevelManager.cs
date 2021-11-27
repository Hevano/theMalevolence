using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance {
        get{
            return _instance;
        }
    }
    public List<string> levels;

    // Start is called before the first frame update
    void Start()
    {
        if(_instance != null){
            Destroy(gameObject);
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ToNextLevel(){
        levels.RemoveAt(0);

        //temp, we should create a victory scene
        if(levels.Count == 0){
            ToMainMenu();
        }
        SceneManager.LoadScene(levels[0]);
    }

    public void ToDeckBuilder(){
        SceneManager.LoadScene("DeckBuilder");
    }

    public void ToMainMenu(){
        SceneManager.LoadScene("Main Menu");
    }
}
