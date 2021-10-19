using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special card effect: Changes another effects value based on a set of factors.</summary> */
[System.Serializable]
public class ModifierEffect : CardEffect {

    /** <summary>The desired effect on a card effect's value.</summary> */
    [SerializeField] private Enums.Modifier modifierEffect;
    /** <summary>The desired factor to influence a card effect's value.</summary> */
    [SerializeField] private Enums.ModifierFactors modifierFactor;
    /** <summary>The magnitude of the modifier based on the factor.</summary> */
    [SerializeField] private int modifierPerFactor;
    /** <summary>The required value of a factor to produce the desired effect.</summary> */
    [SerializeField] private int perFactorValue;
    /** <summary>The index number of the card effect to modify.</summary> */
    [SerializeField] private int effectIndex;
    
    public virtual IEnumerable ApplyEffect () {
        int value = 0;

        switch (modifierFactor) {
            case Enums.ModifierFactors.Cards_Played:
                break;
            case Enums.ModifierFactors.Corruption:
                break;
            case Enums.ModifierFactors.Hand_Size:
                break;
            case Enums.ModifierFactors.Health:
                break;
        }

        CardEffect effect = card.cardEffects[effectIndex].GetEffect();
        effect.SetModification(value, modifierEffect);
        yield return null;
    }
}
