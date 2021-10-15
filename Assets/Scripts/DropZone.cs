using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Draggable.DragHandler onDrop;
    public bool hovering = false;

    public void OnPointerEnter(PointerEventData data){
        hovering = true;
    }
    public void OnPointerExit(PointerEventData data){
        hovering = true;
    }

    public void Update(){
        if(hovering && Input.GetMouseButtonUp(0)){
            if(GetComponent<RectTransform>().rect.Overlaps(Draggable.dragTarget.GetComponent<RectTransform>().rect)){
                Debug.Log($"{Draggable.dragTarget.name} dropped in {name}");
                if(onDrop != null){
                    onDrop(Draggable.dragTarget, this);
                }
                Draggable.dragTarget.Drop(this); //This could be changed so that only the onDrop handler decides if the dragTarget is valid
            }
            
        }
    }
}
