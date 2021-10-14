using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: The character makes an attack action.</summary> */
[System.Serializable]
public class AttackEffect : CardEffect {

    public AttackEffect () {
        Effect = Enums.CardEffects.Attack;
    }
}
