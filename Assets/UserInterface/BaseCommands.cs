using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trying to use this class to integrate basic commands using cards. These cards would be displayed all the time, and would
// be draggable like regular cards. They would be displayed next to each character, in the 'base commands' area
public class BaseCommands : MonoBehaviour
{

    [SerializeField]
    private List<CardDisplayController> DisplayedAttacks = new List<CardDisplayController>();

    public GameManager currentManager;

    public void Start()
    {
        foreach (Character character in currentManager.party)
        {
            CardDisplayController attackCard = CardDisplayController.CreateCard(character.attackCard);
            AddCard(attackCard);
        }
    }

    public void AddCard(CardDisplayController card)
    {
        DisplayedAttacks.Add(card);
        DisplayCard(card);
    }

    public void DisplayCard(CardDisplayController card)
    {
        CardDisplayController NewCard = card;
        RectTransform cardRectTransform = NewCard.GetComponent<RectTransform>();
        RectTransform UI = GameObject.FindGameObjectWithTag("HandDisplay").GetComponent<RectTransform>();
        cardRectTransform.SetParent(UI.transform);
        cardRectTransform.offsetMin = new Vector2(-((DisplayedAttacks.Count - 4) * (UI.sizeDelta.x * 1f)), UI.sizeDelta.y);
        cardRectTransform.offsetMax = new Vector2(0, 0);
        cardRectTransform.sizeDelta = new Vector2(80, 120); //new Vector2(card.GetComponent<RectTransform>().sizeDelta.x + Screen.width * 0.03f, card.GetComponent<RectTransform>().sizeDelta.y + Screen.height * 0.05f);
    }
}
