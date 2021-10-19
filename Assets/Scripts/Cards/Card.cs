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

    public List<CardEffectsMaker> cardEffects = new List<CardEffectsMaker>();
    public List<CardEffectsMaker> cardCorPass = new List<CardEffectsMaker>();
    public List<CardEffectsMaker> cardCorFail = new List<CardEffectsMaker>();

    public string Name { get { return cardName; } }
    public string Description { get { return cardDescription; } }
    public string Flavor { get { return cardFlavor; } }
    public Enums.Character Character { get { return cardCharacter; } }
    public Sprite FrontArt { get { return cardFront; } }
    public Sprite BackArt { get { return cardBack; } }

    private void SetList (List<CardEffect> effectsList, List<CardEffectsMaker> makerList) {
        for (int i = 0; i < makerList.Count; i++) {
            switch (makerList[i].effectType) {
                case Enums.CardEffects.Afflict:
                    effectsList.Add(makerList[i].afflictEffect);
                    break;
                case Enums.CardEffects.Attack:
                    effectsList.Add(makerList[i].attackEffect);
                    break;
                case Enums.CardEffects.Cleanse:
                    effectsList.Add(makerList[i].cleanseEffect);
                    break;
                case Enums.CardEffects.Draw:
                    effectsList.Add(makerList[i].drawEffect);
                    break;
                case Enums.CardEffects.Insert:
                    effectsList.Add(makerList[i].insertEffect);
                    break;
                case Enums.CardEffects.Modify:
                    effectsList.Add(makerList[i].modifyEffect);
                    break;
                case Enums.CardEffects.Reshuffle:
                    effectsList.Add(makerList[i].reshuffleEffect);
                    break;
                case Enums.CardEffects.Summon:
                    effectsList.Add(makerList[i].summonEffect);
                    break;
                case Enums.CardEffects.Vitality:
                    effectsList.Add(makerList[i].vitalityEffect);
                    break;
            }
        }
    }

    public void AddCardEffectMaker (int listNo) {
        if (listNo == 0) {
            cardEffects.Add(new CardEffectsMaker());
        } else if (listNo == 1)
            cardCorPass.Add(new CardEffectsMaker());
        else if (listNo == 2)
            cardCorFail.Add(new CardEffectsMaker());
    }

    public CardEffect GetEffect (int index, int listNo) {
        if (listNo == 0)
            return cardEffects[index].GetEffect();
        else if (listNo == 1)
            return cardCorPass[index].GetEffect();
        else if (listNo == 2)
            return cardCorFail[index].GetEffect();
        else
            return null;
    }

    public CardEffectsMaker GetEffectMaker (int index, int listNo) {
        if (listNo == 0)
            return cardEffects[index];
        else if (listNo == 1)
            return cardCorPass[index];
        else if (listNo == 2)
            return cardCorFail[index];
        else
            return null;
    }

    public IEnumerable Activate () {
        for (int i = 0; i < cardEffects.Count; i++) {
            yield return cardEffects[i].GetEffect().ApplyEffect();
        }
        yield return null;
    }
}
