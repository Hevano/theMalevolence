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
    [Tooltip("Will return to draggable object to it's previous position if it did not land on a valid drop zone")]
    public bool returnIfNotDropped = true;
    private Vector3 returnPos;
    [Tooltip("If True, the object will follow the mouse while being dragged. If false, some other code must handle the movement using the draghandler events")]
    public bool followMouse = true;
    private bool letGo = false;
    public DropZone zone;
    public void OnPointerDown(PointerEventData data){
        dragTarget = this;
        dragging = true;
        if(onDragStart != null){
            onDragStart(this, null);
        }
        returnPos = this.transform.position;
        GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void OnPointerUp(PointerEventData data){
        dragging = false;
        letGo = true;
        GetComponent<GraphicRaycaster>().enabled = true;

    }

    public void Drop(DropZone zoneWhereDropped){
        letGo = false;
        dragTarget = null;
        if(zoneWhereDropped != null){
            zone = zoneWhereDropped;
        } else if(returnIfNotDropped) {
            transform.position = returnPos;
        }
        if(onDragStop != null){
            onDragStop(this, zone);
        }
    }

    public void LateUpdate(){
        if(dragging){
            if(followMouse) transform.position = Input.mousePosition;
            if(onDrag != null){
                onDrag(this, zone);
            }
        } else if(letGo) {
            Drop(null);
        }
    }
}
