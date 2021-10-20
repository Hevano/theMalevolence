using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITurnExecutable, ITargetable
{
    //For scrolling health bars, healthbar should be it's own class that dictates how scrolling health works
    [SerializeField]
    private int _health;
    public int Health
    {
        get{
            return _health;
        }
        set{
            if(onStatChange != null){
                onStatChange("health", _health, value);
            }
            _health = value;
        }
    }

    [SerializeField]
    private int _corruption;
    public int Corruption
    {
        get{
            return _corruption;
        }
        set{
            if(onStatChange != null){
                onStatChange("corruption", _corruption, value);
            }
            _corruption = value;
        }
    }
    public bool enemy = false; //ONCE WE MAKE A CHILD CLASS FOR CHARS AND BOSSES, REMOVE THIS 

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

    [SerializeField]
    private Card _cardToPlay = null;
    public Card CardToPlay{
        get{
            return _cardToPlay;
        }
        set {
            var newCard = value;
            if (enemy == true)
            {
                newCard = value;
            }
            else if (CardToPlay != null){
                GameManager.manager.PlaceCardInHand(CardToPlay);
            }
            if((onActionChange != null) && (enemy == false)){
                onActionChange(_cardToPlay, newCard);
                _cardToPlay = value;
            }
        }
    }

    public delegate void StatChangeHandler(string statName, int oldValue, int newValue);
    public event StatChangeHandler onStatChange;

    public delegate void ActionChangeHandler(Card prev, Card newCard);
    public ActionChangeHandler onActionChange;

    public bool CorruptionCheck(){
        return false;
    }

    void Start(){
        Health = data.health;
        Corruption = data.corruption;
    }

    //Temporary implementation of character's turn
    public IEnumerator GetTurn(){

        Debug.Log($"{name}'s turn");

        if(CardToPlay != null)
        {
            Debug.Log($"{name} playing card {CardToPlay.Name}");
            //Execute the selected card from the dropzone.
            yield return CardToPlay.Activate();
        }
        else
        {
            //Do a damage attack

            //Pull current characters basic attack (can create new one and save to the data object for specific chars)
            yield return Targetable.GetTargetable(Enums.TargetType.Foes, "Select the boss", 1);
            Character target = (Character)Targetable.currentTargets[0];
            target.Health -= data.basicAttack.Value;

        }
        yield return new WaitForSeconds(0.5f);
    }
}
