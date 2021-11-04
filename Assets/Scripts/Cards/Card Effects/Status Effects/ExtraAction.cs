using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAction : StatusEffect
{
    //On combat phase start, designate the extra action, (will need to edit the cardDisplayController to handle playing cards in the resolve phase)
    //On character turn start, execute the extra action 

    Enums.Actions action;
    Card card;
    Character watchedCharacter;
    public ExtraAction(Character character, Enums.Action action){
        GameManager.manager.onPhaseChange += 
    }

    public DesignateAction(Enums.GameplayPhase phase){
        if(phase == Enums.GameplayPhase.Resolve){
            watchedCharacter.onTurnEnd += ResolveAction;
            if(action == Enums.Action.Card){
                //Get player to play card
                //card = 
            }
        }
    }

    public void ResolveAction(){
        var index = GameManager.manager.turns.IndexOf(watchedCharacter);
        GameManager.manager.turns.Insert(index + 1, watchedCharacter);
        watchedCharacter.action = action;
        watchedCharacter.CardToPlay = null; //Have to clear out any existing card to make sure
        watchedCharacter.CardToPlay = card;
    }
}
