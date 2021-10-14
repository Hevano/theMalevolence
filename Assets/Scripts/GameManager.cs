using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    public Dictionary<CharacterEnum, Deck> decks;
    public List<CardObject> hand;

    public List<Character> party;
    
    void Start()
    {
        if(manager != null){
            Destroy(this);
        }
        manager = this;
    }

}
