using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioMgr;

    //BGM
    [SerializeField]
    private AudioClip sceneBGM, sceneIntro, victoryTrack, failureTrack;
    private AudioSource currentTrack;

    //SFX
    public GameObject SFXSource;
    private AudioSource SFXPlayer;
    public List<AudioClip> SoundEffects;
    

    public void Awake()
    {
        if (audioMgr == null)
        {

            //Play 'awake' SoundEffects for the scene (one time) CHANGE THIS IF WE GO WITH A 'STARTUP' SONG
            currentTrack = GetComponent<AudioSource>();
            currentTrack.volume = 0.2f;
            currentTrack.clip = sceneBGM;
            currentTrack.loop = true;


            //Configure audioSources
            SFXPlayer = SFXSource.GetComponent<AudioSource>();
            SFXPlayer.volume = 0.5f;
            currentTrack.playOnAwake = false;
            SFXPlayer.playOnAwake = false;


            audioMgr = this;

            currentTrack.Play();
        }

    }

    public void PlayCharacterSFX(GameObject SourceObject, string SFXName)
    {
        Transform[] ts = SourceObject.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in ts)
        { 
            if (t.gameObject.name == SFXName)
            { 
                PlayObjectSFX(t.gameObject);
                break;
            }

        }
    }

    public void PlayObjectSFX(GameObject SFXObject)
    {

        SFXObject.GetComponent<AudioSource>().Play();
        
    }

    public void PlayUISFX(string SFX)
    {

        switch (SFX)
        {
            case "PaperInteraction":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("CardInteraction"));
                break;
            case "PickupCard":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("PickupCard" + Random.Range(1, 3)));
                break;
            case "PlaceCard":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("PlaceCard" + Random.Range(1, 2)));
                break;
            case "Shuffle":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("Shuffle" + Random.Range(1, 2)));
                break;
            case "CorruptionFail":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("CorruptionFail"));
                break;
            case "CorruptionPass":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("CorruptionPass"));
                break;
            case "CorruptionGain":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("CorruptionGain"));
                break;
            case "CorruptionCleanse":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("CorruptionCleanse"));
                break;
            case "Heal":
                SFXPlayer.clip = SoundEffects.FindLast(sound => sound.name == ("Heal"));
                break;
            default:
                Debug.Log($"<color=red>AudioManager:</color> Sound effect of name {SFX} is not listed!");
                break;
        }

        SFXPlayer.Play();

    }

    public void StopMusic() { currentTrack.Stop(); }

    public void PauseMusic() { currentTrack.Pause(); }

    public void PlayMusic() { currentTrack.Play(); }

    public void ChangeMusic()
    {
        

    }
}
