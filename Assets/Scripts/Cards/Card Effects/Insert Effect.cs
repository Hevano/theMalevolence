using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Inserts a card into the target's deck.</summary> */
[System.Serializable]
public class InsertEffect : CardEffect {
    [SerializeField] private Card toInsert;

    public virtual IEnumerator ApplyEffect () {
        foreach (Character c in targets) {
            Deck deck;
            GameManager.manager.decks.TryGetValue(c.data.characterType, out deck);
            deck.AddCard(toInsert);
        }
        yield return null;
    }
}
