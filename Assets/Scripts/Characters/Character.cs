using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ITurnExecutable, ITargetable
{
    public int Health
    {
        get;
        set;
    }

    public int Corruption
    {
        get;
        set;
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

    public bool CorruptionCheck(){
        return false;
    }

    void Start(){
        Health = 10;
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
