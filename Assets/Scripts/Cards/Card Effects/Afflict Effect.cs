using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** <summary>Special Card effect: Afflicts the target with a status effect.</summary> */
[System.Serializable]
public class AfflictEffect : CardEffect {
    [SerializeField] private Enums.StatusEffects statusEffect;
    private StatusEffect effect;

    public override IEnumerator ApplyEffect () {
        switch (statusEffect) {
            case Enums.StatusEffects.CorruptionShield:
                effect = new StatChangePrevention(targets[0], "corruption", Enums.StatChangeEnum.Increase);
                break;
            case Enums.StatusEffects.ExtraCard:
                break;
            case Enums.StatusEffects.Protected:
                effect = new AttackSubstitution(GameManager.manager.characters[card.Character], targets[0]);
                break;
            case Enums.StatusEffects.BackTalk:
                effect = new BackTalk(GameManager.manager.characters[card.Character]);
                break;
        }
        yield return null;
    }
}
