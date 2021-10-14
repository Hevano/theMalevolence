using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDisplayController : MonoBehaviour {
    // TO DO: Remove this once useless.
    [SerializeField]
    private List<CardDisplayController> CardsToDisplayImmediate = new List<CardDisplayController>();

    private List<CardDisplayController> DisplayedCards = new List<CardDisplayController>();

    public void Start() {
        foreach(CardDisplayController card in CardsToDisplayImmediate) {
            AddCard(card);
        }
    }

    public void AddCard(CardDisplayController card) {
        DisplayedCards.Add(card);
        DisplayCard(card);
    }

    public void DisplayCard(CardDisplayController card) {
        CardDisplayController NewCard = Instantiate(card, Vector3.zero, Quaternion.identity);
        RectTransform cardRectTransform = NewCard.GetComponent<RectTransform>();
        RectTransform UI = GameObject.FindGameObjectWithTag("HandDisplay").GetComponent<RectTransform>();
        cardRectTransform.SetParent(UI.transform);
        cardRectTransform.offsetMin = new Vector2(-UI.sizeDelta.y + DisplayedCards.Count * (+ Screen.width * 0.28f), UI.sizeDelta.y);
        cardRectTransform.offsetMax = new Vector2(0, 0);
        cardRectTransform.sizeDelta = new Vector2(card.GetComponent<RectTransform>().sizeDelta.x + Screen.width * 0.03f, card.GetComponent<RectTransform>().sizeDelta.y + Screen.height * 0.05f);
    }
}
