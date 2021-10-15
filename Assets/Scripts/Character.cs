using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITurnExecutable
{
    public int Health {get; set;}
    public int Corruption{get;set;}
    private bool _defeated = false;
    public bool Defeated {
        get{
            return _defeated;
        }
        set{
            _defeated = value;
            GameManager.manager.CheckGameOver();
        }
    }
    public CharacterData data;

    public Card cardToPlay = null;
    public bool CorruptionCheck(){
        return false;
    }

    void Start(){
        Health = 10;
    }

    //Temporary implementation of character's turn
    public IEnumerator GetTurn(){
        Debug.Log($"{name}'s turn");
        if(cardToPlay != null){
            Debug.Log($"{name} playing card {cardToPlay.Name}");
            //cardToPlay.ActivateCard();
        }
        yield return new WaitForSeconds(0.5f);
    }
}
