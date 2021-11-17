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
    private List<AudioSource> sfx = new List<AudioSource>();
    public List<AudioClip> SoundEffects;

    public void Awake()
    {
        if (audioMgr == null)
        {
            //Play 'awake' sfx for the scene (one time)
            //currentTrack = sceneIntro.GetComponent<AudioSource>();
            //currentTrack.loop = false;

            //Foreach SoundEffect stored in the unity editor's gameobject audioManager is attached to, add them to th sfx AudioSource list.
            foreach (AudioClip sound in SoundEffects)
            {

                AudioSource selSFX = new AudioSource();
                selSFX.clip = sound;

                sfx.Add(selSFX);
                    
            }


            audioMgr = this;
        }

    }


    public void PlaySFX(GameObject SFXprefab)
    {
        //audioMgr.sfxSrc.Play();
    }

    public void StopMusic() { currentTrack.Stop(); }

    public void PauseMusic() { currentTrack.Pause(); }

    public void PlayMusic() { currentTrack.Play(); }

    public void ChangeMusic()
    {
        

    }
}
