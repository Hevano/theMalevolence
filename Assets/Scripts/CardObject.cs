using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Note: CardObject could technically implement the ICard interface. Could we use this somehow?
public class CardObject : MonoBehaviour
{
    public CardData data;
    public Character character;
    //Called when the card is played onto a character
    public void Played(Character c){
        data.CardEffect.Played(character);
	}
    //Called when the card is executed on the character's turn
    public void Resolved(){
        data.CardEffect.Resolved();
	}
    //Called when the card is permanently removed from the deck
    public void Destroyed(){
        data.CardEffect.Destroyed();
	}
    //Called when the card is drawn into the hand
    public void Drawn(){
        data.CardEffect.Drawn();
	}
    //Called when the card is removed from play untill the end of the battle
    public void Exiled(){
        data.CardEffect.Exiled();
	}
    //Called when the card is sent from the hand to the discard pile
    public void Discarded(){
        data.CardEffect.Discarded();
	}
    //Called when the card was played onto a character but was cancelled for some reason (stun, etc.)
    public void Canceled(){
        data.CardEffect.Canceled();
	}
}
