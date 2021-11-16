using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioMgr;

    //GameObjects sound sources
    public GameObject sceneBGM;
    private AudioSource currentTrack, victoryTrack, failureTrack;
    public AudioSource sfxSrc;

    /*
    private List<AudioSource> sfx = new List<AudioSource>();

    public void Awake()
    {
        if (aCtrl == null)
        {

            levelMusic.Add(bgMusic1.GetComponent<AudioSource>());
            levelMusic.Add(bgMusic2.GetComponent<AudioSource>());

            currentTrack = levelMusic[ctrackIndex];

            currentTrack.loop = true;
            aCtrl = this;
        }

    }


    public void PlaySFX() { aCtrl.sfxSrc.Play(); }

    public void StopMusic() { currentTrack.Stop(); }

    public void PauseMusic() { currentTrack.Pause(); }

    public void PlayMusic() { currentTrack.Play(); }

    public void ChangeMusic()
    {
        ctrackIndex++;

        currentTrack.Stop();

        switch (ctrackIndex)
        {
            case 1:
                currentTrack = levelMusic[ctrackIndex];
                Debug.Log("Current track is now" + currentTrack);
                break;
            default:
                ctrackIndex = 0;
                currentTrack = levelMusic[ctrackIndex];
                break;

        }

        currentTrack.Play();



    }*/
}
