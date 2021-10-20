using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special Card effect: Afflicts the target with a status effect.</summary> */
[System.Serializable]
public class AfflictEffect : CardEffect {
    [SerializeField] private Enums.StatusEffects statusEffect;

    public override IEnumerator ApplyEffect () {
        switch (statusEffect) {
            case Enums.StatusEffects.CorruptionShield:
                break;
            case Enums.StatusEffects.Haste:
                break;
            case Enums.StatusEffects.Protected:
                break;
        }
        yield return null;
    }
}