using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject {
    //[Header("Card Info")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private string cardFlavor;
    [SerializeField] private Enums.Character cardCharacter;

    //[Header("Card Art")]
    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    [Header("Card Effects")]
    [SerializeField] private List<CardEffect> cardEffects = new List<CardEffect>();

    [Header("Corruption Effects")]
    [SerializeField] private List<CardEffect> corruptionPassEffects = new List<CardEffect>();
    [SerializeField] private List<CardEffect> corruptionFailEffects = new List<CardEffect>();

    public CardEffectsMaker effectBuffer = new CardEffectsMaker();

    public string Name { get { return cardName; } }
    public string Description { get { return cardDescription; } }
    public string Flavor { get { return cardFlavor; } }
    public Enums.Character Character { get { return cardCharacter; } }
    public Sprite FrontArt { get { return cardFront; } }
    public Sprite BackArt { get { return cardBack; } }
    public List<CardEffect> CardEffects { get { return cardEffects; } }
    public List<CardEffect> CorruptionPassEffects { get { return corruptionPassEffects; } }
    public List<CardEffect> CorruptionFailEffects { get { return corruptionFailEffects; } }

    public Card () {
        if (effectBuffer == null)
            effectBuffer = new CardEffectsMaker();
    }

    public void AddCardEffect (int listNo) {
        if (listNo == 0)
            cardEffects.Add(new CardEffect());
        else if (listNo == 1)
            corruptionPassEffects.Add(new CardEffect());
        else if (listNo == 2)
            corruptionFailEffects.Add(new CardEffect());
    }

    public CardEffect GetEffect (int index, int listNo) {
        if (listNo == 0)
            return cardEffects[index];
        else if (listNo == 1)
            return corruptionPassEffects[index];
        else if (listNo == 2)
            return corruptionFailEffects[index];
        else
            return null;
    }

    public void CheckCardEffect (int index, Enums.CardEffects effect, int listNo) {
        List<CardEffect> list;
        if (listNo == 0)
            list = cardEffects;
        else if (listNo == 1)
            list = corruptionPassEffects;
        else if (listNo == 2)
            list = corruptionFailEffects;
        else
            return;

        if (list[index].Effect == effect)
            return;
        switch(effect) {
            case Enums.CardEffects.None:
                list[index] = new CardEffect();
                break;
            case Enums.CardEffects.Afflict:
                list[index] = new AfflictEffect();
                break;
            case Enums.CardEffects.Attack:
                list[index] = new AttackEffect();
                break;
            case Enums.CardEffects.Cleanse:
                list[index] = new CleanseEffect();
                break;
            case Enums.CardEffects.Draw:
                list[index] = new DrawEffect();
                break;
            case Enums.CardEffects.Insert:
                list[index] = new InsertEffect();
                break;
            case Enums.CardEffects.Modify:
                list[index] = new ModifierEffect();
                break;
            case Enums.CardEffects.Reshuffle:
                list[index] = new ReshuffleEffect();
                break;
            case Enums.CardEffects.Summon:
                list[index] = new SummonEffect();
                break;
            case Enums.CardEffects.Vitality:
                list[index] = new VitalityEffect();
                break;
        }
    }
}
