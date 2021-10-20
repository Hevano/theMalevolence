using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropZone))]
public class TurnOrderSlot : MonoBehaviour
{
    public static List<TurnOrderSlot> turnOrder = new List<TurnOrderSlot>();
    public DropZone dropZone;
    public Draggable currentTurnDraggable;
    public ITurnExecutable Turn{
        get{
            return currentTurnDraggable.GetComponent<CharacterDisplayController>().Character;
        }
    }

    public void PlaceTurn(CharacterDisplayController display){
        currentTurnDraggable = display.GetComponent<Draggable>();
        display.currentTurnSlot = this;
        display.transform.position = transform.position;
    }
    
    //ISSUE: Dropping characterDisplaycontroller onto slot, the display in that slot consumes the raycast
    void Start()
    {
        turnOrder.Add(this);
        dropZone = GetComponent<DropZone>();
        dropZone.onDrop += (drag, drop) =>{
            if(drag == currentTurnDraggable) return;
            CharacterDisplayController characterDisplay = null;
            if(drag.TryGetComponent(out characterDisplay)){
                drag.Drop(drop);
                //Drop this slot's turn in the new turn's slot
                characterDisplay.currentTurnSlot.PlaceTurn(currentTurnDraggable.GetComponent<CharacterDisplayController>());
                //Drop new turn into this slot
                PlaceTurn(characterDisplay);
            }
       };
    }
}
