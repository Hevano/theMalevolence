using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectsMaker : MonoBehaviour {
    public Enums.CardEffects effectType;

    public CardEffect effect;

    public CardEffect cardEffect = new CardEffect();
    public AttackEffect attackEffect = new AttackEffect();
    public CleanseEffect cleanseEffect = new CleanseEffect();
    public DrawEffect drawEffect = new DrawEffect();
    public ModifierEffect modifyEffect = new ModifierEffect();
    public ReshuffleEffect reshuffleEffect = new ReshuffleEffect();

    public void SetEffect () {
        switch(effectType) {
            case Enums.CardEffects.Attack:
                effect = attackEffect;
                break;
            case Enums.CardEffects.Cleanse:
                effect = cleanseEffect;
                break;
        }
    }
}
