using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We could keep card effects in their own namespace so they are accessed like CardEffects.[EffectName], i think it's a bit more readable
namespace CardEffects{
    public class Foresight : CardEffect
    {
        public override void Resolved(){
            DrawCards(GameManager.manager.decks[character.data.characterType], 2);
            character.Corruption += 10;
            if(!CorruptionCheck()){ //If we fail the corruption check
                DiscardCards(1,1); //Discard between 1 and 1 cards
            }
        }
    }
}

