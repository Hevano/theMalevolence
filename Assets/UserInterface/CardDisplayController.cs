using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Draggable))]
public class CardDisplayController : MonoBehaviour {
    [SerializeReference]
    private Text _name;

    public Text Name { get { return _name;  } }

    public static GameObject cardDisplayPrefab;

    private Card _cardData;
    public Card CardData{
        get{
            return _cardData;
        }
        set{
            _cardData = value;
            //TODO: Set the text and image aspects of the cards using the card data
        }
    }

    public void Play(){
        var character = GameManager.manager.characters[CardData.Character];
        character.cardToPlay = CardData;
        //CardData.DesignateTargets();
        Debug.Log($"card {CardData.Name} DesignateTarget");
        //Remove from hand display
    }

    /// HERE Dingus
    public void Start(){
        GetComponent<Draggable>().onDragStop += (drag, drop) => {
            if(drop != null && drop.name == "DropZone"){
                Play();
            }
        };
    }

    public static CardDisplayController CreateCard(Card card){
        if(cardDisplayPrefab == null)
            cardDisplayPrefab = Resources.Load<GameObject>("UserInterface/CardDisplay");
        
        var cardGameObject = Instantiate<GameObject>(cardDisplayPrefab, Vector3.zero, Quaternion.identity);
        var cardDisplay = cardGameObject.GetComponent<CardDisplayController>();
        cardDisplay.CardData = card;
        return cardDisplay;
    }
    
}
