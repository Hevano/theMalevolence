using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Inserts a card into the target's deck.</summary> */
[System.Serializable]
public class InsertEffect : CardEffect {
    [SerializeField] private Card toInsert;

    public override IEnumerator ApplyEffect () {
        foreach (Character c in targets) {
            Deck deck;
            GameManager.manager.decks.TryGetValue(c.data.characterType, out deck);
            deck.AddCard(toInsert);
            yield return CombatUIManager.Instance.DisplayMessage($"Added {toInsert.Name} to {c.data.name}'s deck");
        }
        yield return new WaitForSeconds(1f);
    }
}
