using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Draggable))]
public class CardObject : MonoBehaviour
{
    public Card cardData;

    public void Play(){
        var character = GameManager.manager.characters[cardData.Character];
        character.cardToPlay = cardData;
        //cardData.DesignateTargets();
        Debug.Log($"card {cardData.Name} DesignateTarget");
    }

    public void Start(){
        GetComponent<Draggable>().onDragStop += (drag, drop) => {
            if(drop != null){
                Play();
            }
        };
    }
}
