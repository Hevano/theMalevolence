using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public new string name;
    public string cardText;
    public CharacterEnum characterType;

    [SerializeField]
    private CardEnum cardEffectType;
    private CardEffect _cardEffect;
    public CardEffect CardEffect {
        get{
            if(_cardEffect == null){
                _cardEffect = CardEffect.cardMap[cardEffectType];
            }
            return _cardEffect;
        }
    }


}
