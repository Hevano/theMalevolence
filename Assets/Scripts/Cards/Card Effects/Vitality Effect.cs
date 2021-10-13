using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: Restore HP to a target character or alter their Corruption.</summary> */
[System.Serializable]
public class VitalityEffect : CardEffect {

    public VitalityEffect () {
        Effect = Enums.CardEffects.Vitality;
    }
}