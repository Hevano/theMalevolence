using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    [SerializeField] private List<Card> cardList;

    public List<Card> CardList { get { return cardList; } }

    public Card Draw() {
        Card ret = cardList[0];
        cardList.RemoveAt(0);
        return ret;
    }

    public void Shuffle() {
        List<Card> tempList = cardList;
        cardList.Clear();
        int drawnCard;
        while (tempList.Count > 0) {
            drawnCard = Random.Range(0, tempList.Count);
            cardList.Add(tempList[drawnCard]);
            tempList.RemoveAt(drawnCard);
        }
    }

    public void AddCard(Card card) {
        cardList.Add(card);
        Shuffle();
    }

    public void AddDeck(Deck deck) {
        foreach (Card c in deck.CardList)
            cardList.Add(c);
        Shuffle();
    }
}
