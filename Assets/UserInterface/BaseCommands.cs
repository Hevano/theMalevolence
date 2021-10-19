using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trying to use this class to integrate basic commands using cards. These cards would be displayed all the time, and would
// be draggable like regular cards. They would be displayed next to each character, in the 'base commands' area
public class BaseCommands : MonoBehaviour
{
    //New DisplayAttacks CardDisplayController list. This would contain all the basic attacks for each party member. 
    [SerializeField]
    private List<CardDisplayController> DisplayedAttacks = new List<CardDisplayController>();

    //Establish the current gamemanager object. Would be utilized in the inspector
    public GameManager currentManager;

    //Start this script by getting each character in the current game manager, and find their assigned attackCard, and addCard to this DisplayedAttacks list.
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

        //Get the UI rect that will display these cards based on the 'tag' assigned to the game object.
        RectTransform UI = GameObject.FindGameObjectWithTag("BaseCommands").GetComponent<RectTransform>();
        
        cardRectTransform.SetParent(UI.transform);

        cardRectTransform.offsetMin = new Vector2(-((DisplayedAttacks.Count - 4) * (UI.sizeDelta.x * 1f)), UI.sizeDelta.y);
        cardRectTransform.offsetMax = new Vector2(0, 0);
        cardRectTransform.sizeDelta = new Vector2(80, 120); //new Vector2(card.GetComponent<RectTransform>().sizeDelta.x + Screen.width * 0.03f, card.GetComponent<RectTransform>().sizeDelta.y + Screen.height * 0.05f);
    }
}
