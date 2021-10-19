using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: The character makes an attack action.</summary> */
[System.Serializable]
public class AttackEffect : CardEffect {
    [SerializeField] private int damageBonus;

    public override IEnumerable ApplyEffect () {
        Character self;
        GameManager.manager.characters.TryGetValue(card.Character, out self);
        foreach (Character c in targets) {
            //Tell game manager to attack the chosen target
        }
        yield return null;
    }

    public override void ApplyModification () {
        if (modifyingValue != 0) {
            switch (modification) {
                case Enums.Modifier.Add:
                    damageBonus += modifyingValue;
                    break;
                case Enums.Modifier.Subtract:
                    damageBonus -= modifyingValue;
                    break;
                case Enums.Modifier.Multiply:
                    damageBonus *= modifyingValue;
                    break;
                case Enums.Modifier.Divide:
                    damageBonus /= modifyingValue;
                    break;
            }
        }
    }
}
