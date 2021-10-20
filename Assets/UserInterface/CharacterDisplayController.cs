using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Draggable))]
public class CharacterDisplayController : MonoBehaviour {
    [SerializeReference]
    private Text _hptxt;
    [SerializeReference]
    private Text _cptxt;
    [SerializeReference]
    private Text _nametxt;
    [SerializeField]
    private int maxHP;

    public Text HealthDisplay { get { return _hptxt;} set { _hptxt = value; } } 
    public Text CorruptionDisplay { get { return _cptxt;} set { _cptxt = value; } } 
    public Text NameDisplay { get { return _nametxt; } set { _nametxt = value; } } 

    [SerializeField]
    private Character _character;
    public Character Character {
        get{
            return _character;
        }
        set{
            _character = value;
            //Set the fields based on the character data
            //Subscribe to character events to continually update
        }
    }

    public void ChangeHealth(int currentHealth) {
        HealthDisplay.text = currentHealth + "/" + maxHP;
    }

    public TurnOrderSlot currentTurnSlot;
    public void Start(){
        var d = GetComponent<Draggable>();
        d.followMouse = false;
        d.onDrag += (drag, drop) =>{
            Vector3 pos = d.transform.position;
            pos.y = Input.mousePosition.y;
            transform.position = pos;
        };
        d.onDragStart += (drag, drop) =>{
            ToggleRayCastOnOthers(false);
        };

        d.onDragStop += (drag, drop) =>{
            ToggleRayCastOnOthers(true);
        };
    }

    //Toggles the graphic raycast component on all other (Slightly jank, a better method probably exists)
    private void ToggleRayCastOnOthers(bool enabled){
        var gr = GetComponent<GraphicRaycaster>();
        bool myToggleState = gr.enabled;
        foreach(TurnOrderSlot slot in TurnOrderSlot.turnOrder){
            slot.currentTurnDraggable.GetComponent<GraphicRaycaster>().enabled = enabled;
        }
        gr.enabled = myToggleState;
    }
}
