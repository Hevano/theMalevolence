using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Allows the character to draw an additional card.</summary> */
[System.Serializable]
public class DrawEffect : CardEffect {
    
    /** <summary>The number of cards to draw.</summary> */
    [SerializeField] private int cardsToDraw;
    [SerializeField] private bool fromDiscard;
    [SerializeField] private bool toDiscard;

    public override IEnumerator ApplyEffect () {
        ApplyModification();

        if (toDiscard) {
            for (int i = 0; i < cardsToDraw; i++) {
                //Tell game manager to discard a card
            }
        } else if (fromDiscard) {
            for (int i = 0; i < cardsToDraw; i++) {
                //Tell game manager to draw a card from the discard
            }
        } else {
            for (int i = 0; i < cardsToDraw; i++) {
                yield return GameManager.manager.ExecuteDrawPhase();
                GameManager.manager.phase = Enums.GameplayPhase.Resolve;
            }
        }
        yield return new WaitForSeconds(1f);
    }

    public override void ApplyModification () {
        if (modifyingValue != 0) {
            switch (modification) {
                case Enums.Modifier.Add:
                    cardsToDraw += modifyingValue;
                    break;
                case Enums.Modifier.Subtract:
                    cardsToDraw -= modifyingValue;
                    break;
                case Enums.Modifier.Multiply:
                    cardsToDraw *= modifyingValue;
                    break;
                case Enums.Modifier.Divide:
                    cardsToDraw /= modifyingValue;
                    break;
            }
        }
    }
}
