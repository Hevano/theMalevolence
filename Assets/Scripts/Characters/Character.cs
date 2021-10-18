using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITurnExecutable, ITargetable
{
    public int Health {get; set;}
    public int Corruption{get;set;}
    private bool _defeated = false;

    public bool Defeated
    {
        get
        {
            return _defeated;
        }
        set
        {
            _defeated = value;
            GameManager.manager.CheckGameOver();
        }
    }

    public CharacterData data;

    public Card cardToPlay = null;

    public Card attackCard = null;

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
            //yield return cardToPlay.ActivateEffect();
        }
        yield return new WaitForSeconds(0.5f);
    }
}
