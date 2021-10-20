using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDisplayController : MonoBehaviour {
    [SerializeField]
    private List<CardDisplayController> DisplayedCards = new List<CardDisplayController>();

    public void AddCard(CardDisplayController card) {
        DisplayedCards.Add(card);
        card.GetComponent<Draggable>().zone = this.GetComponent<DropZone>();
        DisplayCard(card);
    }

    public void RemoveCard(CardDisplayController card){
        DisplayedCards.Remove(card);
        Destroy(card.gameObject);
    }

    public void DisplayCard(CardDisplayController card) {

        CardDisplayController NewCard = card;
        RectTransform cardRectTransform = NewCard.GetComponent<RectTransform>();
        RectTransform UI = GameObject.FindGameObjectWithTag("HandDisplay").GetComponent<RectTransform>();
        cardRectTransform.SetParent(UI.transform);
        cardRectTransform.offsetMin = new Vector2(-((DisplayedCards.Count - 0.5f) * (UI.sizeDelta.x * 1f)), UI.sizeDelta.y);
        cardRectTransform.offsetMax = new Vector2(0, 0);
        cardRectTransform.sizeDelta = new Vector2(60, 90); //new Vector2(card.GetComponent<RectTransform>().sizeDelta.x + Screen.width * 0.03f, card.GetComponent<RectTransform>().sizeDelta.y + Screen.height * 0.05f);

    }
}
