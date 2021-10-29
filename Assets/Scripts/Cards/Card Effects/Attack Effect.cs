using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: The character makes an attack action.</summary> */
[System.Serializable]
public class AttackEffect : CardEffect {
    [SerializeField] private int dieNumber;
    [SerializeField] private int dieSize;
    [SerializeField] private int dieBonus;

    public override IEnumerator ApplyEffect () {
        Character self;
        GameManager.manager.characters.TryGetValue(card.Character, out self);

        //Create the damage object
        int[] damVals = new int[3];

        if (dieNumber > 0) damVals[0] = dieNumber;
        else damVals[0] = self.data.basicAttack.DieNumber;

        if (dieSize > 0) damVals[1] = dieSize;
        else damVals[1] = self.data.basicAttack.DieSize;

        if (dieBonus != 0) damVals[2] = dieBonus;
        else damVals[2] = self.data.basicAttack.DieBonus;

        Damage damage = new Damage(damVals[0], damVals[1], damVals[2]);

        foreach (Character c in targets) {
            int value = damage.Value;
            c.Health -= value;
            CombatUIManager.Instance.SetDamageText(value, c.transform);
        }
        yield return new WaitForSeconds(1f);
    }

    public override void ApplyModification () {
        if (modifyingValue != 0) {
            switch (modification) {
                case Enums.Modifier.Add:
                    dieBonus += modifyingValue;
                    break;
                case Enums.Modifier.Subtract:
                    dieBonus -= modifyingValue;
                    break;
                case Enums.Modifier.Multiply:
                    dieBonus *= modifyingValue;
                    break;
                case Enums.Modifier.Divide:
                    dieBonus /= modifyingValue;
                    break;
            }
        }
    }
}
