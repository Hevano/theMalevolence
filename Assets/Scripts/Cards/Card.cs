using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject {
    //[Header("Card Info")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private string cardFlavor;
    [SerializeField] private Enums.Character cardCharacter;
    [SerializeField] private bool exiled;

    //[Header("Card Art")]
    [SerializeField] private Image cardFront;
    [SerializeField] private Image cardBack;

    public List<CardEffectsMaker> cardEffects = new List<CardEffectsMaker>();
    public List<CardEffectsMaker> cardCorPass = new List<CardEffectsMaker>();
    public List<CardEffectsMaker> cardCorFail = new List<CardEffectsMaker>();

    public string Name { get { return cardName; } }
    public string Description { get { return cardDescription; } }
    public string Flavor { get { return cardFlavor; } }
    public Enums.Character Character { get { return cardCharacter; } }
    public Image FrontArt { get { return cardFront; } }
    public Image BackArt { get { return cardBack; } }

    public Character AllyTarget { get; set; }
    public Character EnemyTarget { get; set; }

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
            cardEffects.Add(new CardEffectsMaker(this));
        } else if (listNo == 1)
            cardCorPass.Add(new CardEffectsMaker(this));
        else if (listNo == 2)
            cardCorFail.Add(new CardEffectsMaker(this));
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

    public IEnumerator Activate () {
        if (cardCorPass.Count > 0 || cardCorFail.Count > 0) {
            int corruptionCheck = Random.Range(0, 100);
            Character character;
            GameManager.manager.characters.TryGetValue(cardCharacter, out character);
            if (corruptionCheck >= character.Corruption)
                for (int i = 0; i < cardCorPass.Count; i++)
                    yield return cardCorPass[i].GetEffect().ApplyEffect();
            else
                for (int i = 0; i < cardCorFail.Count; i++)
                    yield return cardCorFail[i].GetEffect().ApplyEffect();
        }

        for (int i = 0; i < cardEffects.Count; i++)
            yield return cardEffects[i].GetEffect().ApplyEffect();
        yield return null;
    }

    public IEnumerator DesignateTargets() {
        AllyTarget = null;
        EnemyTarget = null;

        for (int i = 0; i < cardEffects.Count; i++) {
            yield return cardEffects[i].GetEffect().DesignateTarget();
        }

        for (int i = 0; i < cardCorPass.Count; i++) {
            yield return cardCorPass[i].GetEffect().DesignateTarget();
        }

        for (int i = 0; i < cardCorFail.Count; i++) {
            yield return cardCorFail[i].GetEffect().DesignateTarget();
        }
    }

    /*
        CardEffect Resolution and Targeting
    */
    // public IEnumerator ActivateEffect(){
    //     foreach(CardEffect effect in cardEffects){
    //         yield return effect.Activate();
    //     }
    // }

    // public IEnumerator AccquireTargets(){
    //     foreach(CardEffect effect in cardEffects){
    //         yield return effect.AccquireTarget();
    //     }
    // }
}
