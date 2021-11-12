using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraAction : StatusEffect
{
    //On combat phase start, designate the extra action, (will need to edit the cardDisplayController to handle playing cards in the resolve phase)
    //On character turn start, execute the extra action 

    Enums.Action action;
    Card card;
    Character watchedCharacter;
    public ExtraAction(Character targetCharacter, Enums.Action action){
        GameManager.manager.onPhaseChange += DesignateAction;
        watchedCharacter = targetCharacter;
    }

    public void DesignateAction(Enums.GameplayPhase phase){
        if(phase == Enums.GameplayPhase.Resolve){
            
            var index = GameManager.manager.turns.IndexOf(watchedCharacter);
            if(action == Enums.Action.Card){
                GameManager.manager.turns.Insert(index + 1, new ExtraCardTurn(watchedCharacter));
            } else {
                GameManager.manager.turns.Insert(index + 1, watchedCharacter);
                watchedCharacter.onTurnEnd += ResolveAttack;
            }
            
            
            GameManager.manager.onPhaseChange -= DesignateAction;
        }
    }

    public void ResolveAttack(){
        watchedCharacter.Action = action;
        watchedCharacter.CardToPlay = null;
        watchedCharacter.onTurnEnd -= ResolveAttack;
    }

    //Wrapper class that allows an extra card to be played and resolved in order
    private class ExtraCardTurn : ITurnExecutable{
        public Character character;
        public ExtraCardTurn(Character c){
            character = c;
        }
        public IEnumerator GetTurn(){
            character.Action = Enums.Action.Card;
            bool anyValidCards = false;
            foreach(CardDisplayController cardDisplay in GameManager.manager.hand.DisplayedCards){
                if(cardDisplay.CardData.Character == character.data.characterType){
                    cardDisplay.GetComponent<Draggable>().planningPhaseOnly = false;
                    anyValidCards = true;
                }
            }
            if(anyValidCards){
                yield return CombatUIManager.Instance.DisplayMessage($"Giving Pointers: Play an extra {character.data.name} card", 3);
                while(character.CardToPlay == null){
                    yield return new WaitForEndOfFrame();
                }

                foreach(CardDisplayController cardDisplay in GameManager.manager.hand.DisplayedCards){
                    if(cardDisplay.CardData.Character == character.data.characterType){
                        cardDisplay.GetComponent<Draggable>().planningPhaseOnly = true;
                    }
                }
                yield return character.CardToPlay.Activate();
            }
            yield return null;
        }
    }
}
