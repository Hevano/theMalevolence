using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void DragHandler(Draggable drag, DropZone drop);
    public event DragHandler onDragStart;
    public event DragHandler onDrag;
    public event DragHandler onDragStop;
    public static Draggable dragTarget;
    public bool dragging = false;
    public bool returnToZone = true;
    private bool letGo = false;
    public DropZone zone;
    public void OnPointerDown(PointerEventData data){
        dragTarget = this;
        dragging = true;
        if(onDragStart != null){
            onDragStart(this, null);
        }
    }

    public void OnPointerUp(PointerEventData data){
        dragging = false;
        letGo = true;

    }

    public void Drop(DropZone zoneWhereDropped){
        letGo = false;
        dragTarget = null;
        if(zoneWhereDropped != null){
            zone = zoneWhereDropped;
            if(onDragStop != null){
                onDragStop(this, zone);
            }
            if(zone != null){
                transform.position = zone.transform.position;
            }
        }
        
    }

    public void LateUpdate(){
        if(dragging){
            transform.position = Input.mousePosition;
            if(onDrag != null){
                onDrag(this, zone);
            }
        } else if(letGo) {
            Drop(null);
        }
    }
}
