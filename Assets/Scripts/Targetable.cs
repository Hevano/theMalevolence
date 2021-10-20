using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Character))]
public class Targetable : MonoBehaviour, IPointerClickHandler
{
    private static bool targetting = false;
    private static Enums.TargetType targetType;
    public static List<ITargetable> currentTargets = new List<ITargetable>();

    [Tooltip("List of target types that apply to this gameobject")]
    public List<Enums.TargetType> targetTypes;
    public ITargetable target; //For now, we can assume characters are the only targetable entities

    public void Start(){
        target = GetComponent<Character>();
    }

    public void OnPointerClick(PointerEventData data){ //Camera needs to have the PhysicsRaycast Component
        Debug.Log("Clicked");

        if(targetting && targetTypes.Contains(targetType)){
            currentTargets.Add(target);
            Debug.Log($"{name} has been targeted");
        }

    }

    
    public static IEnumerator GetTargetable(Enums.TargetType type, string msg, int count = 1){

        //send msg to some Text object in the screen to inform the player what they are targetting
        Debug.Log(msg);

        GameManager.manager.cardDropZone.GetComponent<UnityEngine.UI.Image>().raycastTarget = false;

        currentTargets = new List<ITargetable>();
        targetting = true;

        //loop while target is being found. Checks each frame if the number of targets is returned.
        while (currentTargets.Count < count){
            yield return new WaitForEndOfFrame();
        }

        GameManager.manager.cardDropZone.GetComponent<UnityEngine.UI.Image>().raycastTarget = true;
        targetting = false;
    }
}

