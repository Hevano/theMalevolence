using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyCharacter : Character
{
    //overriden to update character display, and to return cards to then hand if the player plays multiple
    public override Card CardToPlay
    {
        get
        {
            return _cardToPlay;
        }
        set {
            var newCard = value;
            //If the player plays a card while another card has yet to be played, return the first to the hand
            if (_cardToPlay != null && newCard != null){
                GameManager.manager.PlaceCardInHand(CardToPlay);
            }
            _cardToPlay = newCard;
            //Update the Character's action
            if(_cardToPlay != null){
                Action = Enums.Action.Card;
            } else {
                Action = Enums.Action.Attack;
            }
        }
    }

    //Pull current characters basic attack (can create new one and save to the data object for specific chars)
    public IEnumerator BasicAttack(Damage damage = null){
        yield return Targetable.GetTargetable(Enums.TargetType.Foes, this, $"{data.name} attacks: Select a foe!", 1);
        Character target = (Character)Targetable.currentTargets[0];
        damage = damage == null ? data.basicAttack : damage;
        InvokeAttackHandler(target, ref damage);
        
        target.Health -= damage.Value;
    }

    //Executes the character's turn, where they either play their card or attack
    public override IEnumerator GetTurn(){
        Debug.Log($"{data.name}'s turn");
        InvokeTurnStartHandler();
        if(Defeated)
        {
            Debug.Log($"{data.name} has been defeated and cannot continue the fight");
        }
        else if(Action == Enums.Action.Card && CardToPlay != null)
        {
            Debug.Log($"{data.name} playing card {CardToPlay.Name}");
            //CombatUIManager.Instance.DisplayMessage($"{name} plays {CardToPlay.Name}");
            yield return CombatUIManager.Instance.RevealCard(CardToPlay);
            //Execute the selected card from the dropzone.
            yield return CardToPlay.Activate();
        }
        else if(Action == Enums.Action.Attack || Action == Enums.Action.Silenced)
        {
            yield return BasicAttack();
        }
        InvokeTurnEndHandler();
        yield return new WaitForSeconds(1f);
    }
}
