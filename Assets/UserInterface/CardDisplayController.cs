using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Draggable))]
public class CardDisplayController : MonoBehaviour {
   
    public static GameObject cardDisplayPrefab;

    //The name of the displayed card. Must be changed to display the cards proper name (seperate field)
    [SerializeReference]
    private Text _name;
    public Text Name
    {
        get{return _name;}
    }

    //The description of the displayed card. Must be changed to display the cards proper description (seperate field)
    [SerializeReference]
    private Text _description;
    public Text Description
    {
        get { return _description; }
        set { _description = value; }
    }

    [SerializeReference]
    private Image _front;
    public Image Front
    {
        get { return _front; }
        set { _front = value; }
    }

    [SerializeReference]
    private Image _back;
    public Image Back
    {
        get { return _back; }
        set { _back = value; }
    }

    private Card _cardData;

    //The public facing card of this class. This is where all the cards data (from _cardData) is returned
    public Card CardData
    {
        get{return _cardData;}
        set
        {
            _cardData = value;
            //TODO: Set the text and image aspects of the cards using the card data

        }
    }

    public void Play()
    {
        var character = GameManager.manager.characters[CardData.Character];
        character.CardToPlay = CardData;

        //Activate the cards designate targets function. THIS DOESNT WORK??
        StartCoroutine(CardData.DesignateTargets());

        Debug.Log($"Card {CardData.Name} has targets");
        //Remove from hand display
        GameManager.manager.RemoveCardFromHand(this);
    }

    
    public void Start(){
        GetComponent<Draggable>().onDragStop += (drag, drop) => {
            if(drop != null && drop.name == "DropZone"){
                Play();
            }
        };
    }

    //Create a new display of the card selected
    public static CardDisplayController CreateCard(Card card){
        if(cardDisplayPrefab == null)
            cardDisplayPrefab = Resources.Load<GameObject>("UserInterface/CardDisplay");
        
        var cardGameObject = Instantiate<GameObject>(cardDisplayPrefab, Vector3.zero, Quaternion.identity);

        var cardDisplay = cardGameObject.GetComponent<CardDisplayController>();

        cardDisplay.CardData = card;
        cardDisplay._name.text = card.Name;
        cardDisplay._description.text = card.Description;
        cardDisplay._back.sprite = card.BackArt;
        cardDisplay._front.sprite = card.FrontArt;

        return cardDisplay;
    }
    
}
