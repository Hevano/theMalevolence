using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDisplayController : MonoBehaviour {
    [SerializeField]
    private List<CardDisplayController> DisplayedCards = new List<CardDisplayController>();

    public void Start() {
        foreach(CardDisplayController card in DisplayedCards) {
            DisplayCard(card);
        }
    }

    public void AddCard(CardDisplayController card) {
        DisplayedCards.Add(card);
        DisplayCard(card);
    }

    public void DisplayCard(CardDisplayController card) {
        Instantiate<CardDisplayController>(card, new Vector3(transform.position.x + (DisplayedCards.Count * 120), transform.position.y, 0), transform.rotation, transform);
    }
}
