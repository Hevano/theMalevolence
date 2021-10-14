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
        RectTransform cardRectTransform = card.GetComponent<RectTransform>();
        RectTransform UI = GameObject.FindGameObjectWithTag("HandDisplay").GetComponent<RectTransform>();
        UI.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 1f, Screen.height * 1f);
        cardRectTransform.SetParent(UI.transform);
        cardRectTransform.offsetMin = new Vector2(-(Screen.width + Screen.height * 0.18f) + DisplayedCards.Count * (Screen.height * 0.4f), 0);
        cardRectTransform.offsetMax = new Vector2(0, 0);
        cardRectTransform.sizeDelta = new Vector2(Screen.height * 0.09f, Screen.height * 0.09f);
    }
}
