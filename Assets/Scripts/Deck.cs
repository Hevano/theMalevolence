using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    public List<CardObject> cards;
    public List<CardObject> discard;
    public List<CardData> decklist;

    //Fills list 'cards' with cardObjects based on card data
    public void ConstructCardObjects(){

    }
    
    //Randomizes the cards currently in the deck
    public void Shuffle(){

    }

    //Adds the cards in the discard back into the deck then shuffles
    public void ReShuffle(){
        cards.AddRange(discard);
        Shuffle();
    }

    //Draws a number of cards from the front of the list 'cards', and adds them to the hand. Returns the cards that were drawn.
    public List<CardObject> Draw(int num){
        return new List<CardObject>();
    }

    //Draws a specific set of cards from the deck into the hand, then returns the cards that were drawn. Throws an exception if any of the cardsToDraw are not present in the deck
    public List<CardObject> Draw(List<CardObject> cardsToDraw){
        return new List<CardObject>();
    }

    //Takes a specific card from the discard pile and returns it to the hand. Returns the card specified
    public List<CardObject> Recover(List<CardObject> cardsToRecover){
        return new List<CardObject>();
    }

    //Dumps a number of cards off the top of the deck into the discard
    public List<CardObject> Mill(int num){
        return new List<CardObject>();
    }
}
