using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Reshuffles a number of cards back into the deck.</summary> */
[System.Serializable]
public class ReshuffleEffect : CardEffect {

    public virtual IEnumerator ApplyEffect () {
        foreach (Character c in targets) {
            Enums.Character charType = c.data.characterType;
            Deck deck;
            GameManager.manager.decks.TryGetValue(charType, out deck);
            //Get all cards from discard with same chartype
            Deck discardedCards = new Deck();
            //Add cards back to deck
            deck.AddDeck(discardedCards);
        }
        yield return null;
    }
}
