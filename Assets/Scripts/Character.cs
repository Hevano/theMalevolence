using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITurnExecutable
{
    public int Health {get; set;}
    public int Corruption{get;set;}
    public bool defeated = false;
    public CharacterData data;
    public bool CorruptionCheck(){
        return false;
    }

    void Start(){
        Health = 10;
    }

    //Temporary implementation of character's turn
    public IEnumerator GetTurn(){
        Debug.Log($"{name}'s turn");
        Health--;
        if(Health == 0){
            defeated = true;
        }
        GameManager.manager.CheckGameOver();
        yield return new WaitForSeconds(0.5f);
    }
}
