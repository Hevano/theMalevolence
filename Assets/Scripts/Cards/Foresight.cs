using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We could keep card effects in their own namespace so they are accessed like CardEffects.[EffectName], i think it's a bit more readable
namespace CardEffects{
    public class Foresight : CardEffect
    {
        public override void Resolved(){
            DrawCards(new Deck(), 2); //Assume we have a static global reference to the right deck, or their is a UI method to select a specific deck
            if(!CorruptionCheck()){ //If we fail the corruption check
                DiscardCards(1,1); //Discard between 1 and 1 cards
            }
        }
    }
}

