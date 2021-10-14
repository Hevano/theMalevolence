using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplayController : MonoBehaviour {
    [SerializeReference]
    private Text _hptxt;
    [SerializeReference]
    private Text _cptxt;
    [SerializeReference]
    private Text _nametxt;

    public Text HealthDisplay { get { return _hptxt;} set { _hptxt = value; } } 
    public Text CorruptionDisplay { get { return _cptxt;} set { _cptxt = value; } } 
    public Text NameDisplay { get { return _nametxt; } set { _nametxt = value; } } 
}
