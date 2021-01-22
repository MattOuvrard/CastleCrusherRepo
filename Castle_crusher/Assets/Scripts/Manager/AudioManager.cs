using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance { get; set; }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            instance = _instance;
        }
        else
        {
            Destroy(this);
        }
    }




    AudioSource m_audio;
    public List<AudioClip> SFXAudio;


    private void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }


    public void jouer(string audioname)
    {
        AudioClip temp = FindClip(audioname);
        if (temp != null)
        {
            m_audio.clip = temp;
            m_audio.Play();
        }
    }


    AudioClip FindClip(string s)
    {
        foreach(AudioClip ac in SFXAudio)
        {
            if(s == ac.name)
            {
                return ac;
            }
        }
        return null;
    }
}
