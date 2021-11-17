using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioMgr;
    

    //GameObjects sound sources
    public GameObject sceneBGM, sceneIntro;

    //BGM
    private AudioSource currentTrack, victoryTrack, failureTrack;

    //SFX
    public AudioSource SFXSource;
    private List<AudioSource> sfx = new List<AudioSource>();
    public List<AudioClip> SoundEffects;
    

    public void Awake()
    {
        if (audioMgr == null)
        {
            //Play 'awake' sfx for the scene (one time)
            currentTrack = sceneIntro.GetComponent<AudioSource>();
            currentTrack.loop = false;


            currentTrack.playOnAwake = false;

            SFXSource.playOnAwake = false;

            //Foreach SoundEffect stored in the unity editor's gameobject audioManager is attached to, add them to th sfx AudioSource list.
            foreach (AudioClip sound in SoundEffects)
            {

                SFXSource.clip = sound;

                sfx.Add(SFXSource);
               
            }


            audioMgr = this;
        }

    }


    public void PlayObjectSFX(GameObject SFX)
    {

        AudioSource selectSFX = new AudioSource();
        selectSFX = sceneIntro.GetComponent<AudioSource>();

        selectSFX.Play();
    }

    public void PlayUISFX(string SFX)
    {


        switch(SFX)
        {
            case "CardInteraction":
                break;
            case "PickupCard":
                Random.Range(1, 3);
                break;
            case "PlaceCard":
                Random.Range(1, 2);
                break;
            case "Shuffle":
                Random.Range(1, 2);
                break;
            case "CorruptionFail":
                break;
            case "CorruptionPass":
                break;
            case "CorruptionGain":
                break;
            case "CorruptionCleanse":
                break;
            case "Heal":
                break;

            default:
                Debug.Log($"<color=red>AudioManager:</color> Sound effect of name {SFX} is not listed!");
                break;
        }

    }

    public void StopMusic() { currentTrack.Stop(); }

    public void PauseMusic() { currentTrack.Pause(); }

    public void PlayMusic() { currentTrack.Play(); }

    public void ChangeMusic()
    {
        

    }
}
