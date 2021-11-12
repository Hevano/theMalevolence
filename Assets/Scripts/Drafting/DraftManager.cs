using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraftManager : MonoBehaviour {
    public Card[] cardsToBeAdded = new Card[0];
    public HandDisplayController draftCards;
    public HandDisplayController chosenCards;

    public void Awake() {
        PlaceCardInDraft();
    }

    public void PlaceCardInDraft() {
        foreach (Card c in cardsToBeAdded)  {
            draftCards.AddCard(CardDisplayController.CreateCard(c));
        }
    }
}
