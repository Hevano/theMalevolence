using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Card effect: The character makes an attack action.</summary> */
[System.Serializable]
public class AttackEffect : CardEffect {
    /** <summary>The number of die to roll</summary> */
    [SerializeField] private int dieNumber;
    /** <summary>The number of sides on each die</summary> */
    [SerializeField] private int dieSize;
    /** <summary>An attack bonus to add after die have been rolled</summary> */
    [SerializeField] private int dieBonus;

    /** <summary>Applies the effect onto the relevant targets</summary> */
    public override IEnumerator ApplyEffect () {
        Character self;
        GameManager.manager.characters.TryGetValue(card.Character, out self);

        //Create the damage object based on inputs. If value is invalid/0, use character's value
        int[] damVals = new int[3];
    
        if (dieNumber > 0) damVals[0] = dieNumber;
        else damVals[0] = self.data.basicAttack.DieNumber;
        if (dieSize > 0) damVals[1] = dieSize;
        else damVals[1] = self.data.basicAttack.DieSize;
        if (dieBonus != 0) damVals[2] = dieBonus;
        else damVals[2] = self.data.basicAttack.DieBonus;
        Damage damage = new Damage(damVals[0], damVals[1], damVals[2]);

        ApplyModification();

        //Apply effect on each target
        foreach (Character c in targets) {
            int value = damage.Value;
            c.Health -= value;
        }
        yield return new WaitForSeconds(1f);
    }

    /** <summary>Takes the modification from the MODIFY effect and increases "dieBonus" value</summary> */
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
