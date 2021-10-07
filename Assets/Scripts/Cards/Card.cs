using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject {
    [Header("Card Info")]
    [SerializeField] private string cardName;
    [SerializeField] private string cardDescription;
    [SerializeField] private string cardFlavor;
    [SerializeField] private Enums.Character cardCharacter;

    [Header("Card Art")]
    [SerializeField] private Sprite cardFront;
    [SerializeField] private Sprite cardBack;

    [SerializeField] private CardEffectsMaker cardEffects = new CardEffectsMaker();

    public string Name { get { return cardName; } }
    public string Description { get { return cardDescription; } }
    public string Flavor { get { return cardFlavor; } }
    public Enums.Character Character { get { return cardCharacter; } }
    public Sprite FrontArt { get { return cardFront; } }
    public Sprite BackArt { get { return cardBack; } }
}
