using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Allows the character to draw an additional card.</summary> */
[System.Serializable]
public class DrawEffect : CardEffect {
    
    /** <summary>The number of cards to draw.</summary> */
    [SerializeField] private int cardsToDraw;
    /** <summary>Draw cards from the discard pile.</summary> */
    [SerializeField] private bool fromDiscard;
    /** <summary>Discard cards in hand to discard pile. Overrides fromDiscard.</summary> */
    [SerializeField] private bool toDiscard;

    private int cards;

    /** <summary>Applies the effect onto the relevant targets</summary> */
    public override IEnumerator ApplyEffect () {
        ApplyModification();

        //Sends a number of numbers from the player's hand into the discard pile
        if (toDiscard) {
            for (int i = 0; i < cards; i++) {
                //Tell game manager to discard a card
            }
        }
        //Draws a number of cards from the discard pile into the player's hand
        else if (fromDiscard) {
        for (int i = 0; i < cards; i++) {
                //Tell game manager to draw a card from the discard
            }
        } 
        //Draw a number of cards from your decks
        else {
            for (int i = 0; i < cards; i++) {
                //Tell the game manager to enter the draw phase, then return to resolve phase
                yield return GameManager.manager.ExecuteDrawPhase();
                GameManager.manager.phase = Enums.GameplayPhase.Resolve;
            }
        }
        yield return new WaitForSeconds(1f);
    }

    /** <summary>Takes the modification from the MODIFY effect and increases "cardsToDraw" value</summary> */
    public override void ApplyModification () {
        if (modifyingValue != 0) {
            switch (modification) {
                case Enums.Modifier.Add:
                    cards = cardsToDraw + modifyingValue;
                    break;
                case Enums.Modifier.Subtract:
                    cards = cardsToDraw - modifyingValue;
                    break;
                case Enums.Modifier.Multiply:
                    cards = cardsToDraw * modifyingValue;
                    break;
                case Enums.Modifier.Divide:
                    cards = cardsToDraw / modifyingValue;
                    break;
            }
        }
    }
}
