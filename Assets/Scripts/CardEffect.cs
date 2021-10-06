using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class CardEffect : ICard
{
    public Character character;
    private List<ITargetable> targets;

    /**  INTERFACE **/

    //Called when the card is played onto a character
    public void Played(Character c){

    }
    //Called when the card is executed on the character's turn
    public void Resolved(){}
    //Called when the card is permanently removed from the deck
    public void Destroyed(){}
    //Called when the card is drawn into the hand
    public void Drawn(){}
    //Called when the card is removed from play untill the end of the battle
    public void Exiled(){}
    //Called when the card is sent from the hand to the discard pile
    public void Discarded(){}
    //Called when the card was played onto a character but was cancelled for some reason (stun, etc.)
    public void Canceled(){
        character = null;
    }

    /** HELPER FUNCTIONS **/

    //Triggers the UI functions to select targets. Validator is a function that return true if the target is valid
    protected List<ITargetable> SelectTarget(int number, TargetSelector validator){
        return targets;
    }
    //Easier way to select targets without a validator, allowing all targets match the type
    protected List<ITargetable> SelectTarget(int number, TargetTypeEnum types){
        return targets;
    }

    //Removes the card from the deck permanently
    protected void DestroyCard(){

    }
    
    //Deals damage to target
    protected int DealDamage(ITargetable target, Damage damage){
        return damage.Value;
    }

    //Draws a number of cards into the hand, then returns the cards drawn
    protected List<CardObject> DrawCards(Deck deck, int num){
        return new List<CardObject>();
    }

    //Triggers UI to make player discard number of cards between min and max. Returns the cards that were discarded.
    protected List<CardObject> DiscardCards(int min, int max){
        return new List<CardObject>();
    }

    //Triggers UI to make player discard number of cards in the list. Returns the cards that were discarded.
    protected List<CardObject> DiscardCards(List<CardObject> cards){
        return new List<CardObject>();
    }

    //Makes a corruption check on a character, defaulting to the character that resolved the card
    protected bool CorruptionCheck(){
        return CorruptionCheck(character);
    }

    protected bool CorruptionCheck(Character c){
        if(c != null){
            return c.CorruptionCheck();
        } else{
            throw new System.Exception("Cannot make corruption check on null character");
        }
    }

    

}
