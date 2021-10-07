using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffectsMaker : MonoBehaviour {
    public Enums.CardEffects effectType;

    public List<CardEffect> effectList;

    public CardEffect cardEffect = new CardEffect();
    public AttackEffect attackEffect = new AttackEffect();
    public CleanseEffect cleanseEffect = new CleanseEffect();
    public CorruptEffect corruptEffect = new CorruptEffect();
    public DrawEffect drawEffect = new DrawEffect();
    public HasteEffect hasteEffect = new HasteEffect();
    public HealEffect healEffect = new HealEffect();
    public ModifierEffect modifyEffect = new ModifierEffect();
    public ProtectEffect protectEffect = new ProtectEffect();
    public RemoveEffect removeEffect = new RemoveEffect();
    public ReplaceValueEffect replaceValueEffect = new ReplaceValueEffect();
    public ReshuffleEffect reshuffleEffect = new ReshuffleEffect();

    public void SetEffect () {

    }
}
