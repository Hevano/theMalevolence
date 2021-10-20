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

    public delegate void StatChangeHandler(string statName, int oldValue, int newValue);
    public event StatChangeHandler onStatChange;

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

        if(cardToPlay != null)
        {
            Debug.Log($"{name} playing card {cardToPlay.Name}");
            //Execute the selected card from the dropzone.
            yield return cardToPlay.Activate();
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
