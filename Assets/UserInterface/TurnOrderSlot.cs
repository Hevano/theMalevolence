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
    
    //ISSUE: Dropping characterDisplaycontroller onto slot, the display in that slot consumes the raycast
    void Start()
    {
        turnOrder.Add(this);
        dropZone = GetComponent<DropZone>();
        dropZone.onDrop += (drag, drop) =>{
           CharacterDisplayController characterDisplay = null;
           if(drag.TryGetComponent<CharacterDisplayController>(out characterDisplay)){
                //Drop this slot's turn in the new turn's slot
                currentTurnDraggable.Drop(characterDisplay.currentTurnSlot.dropZone);
                currentTurnDraggable.transform.position = drop.transform.position;
                //Drop new turn into this slot
                drag.Drop(drop);
                drop.transform.position = transform.position;
           }
       };
    }
}
