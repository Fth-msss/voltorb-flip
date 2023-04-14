using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    AudioListener listen;
    [SerializeField]
    AudioClip amogus;



    float mastervolume;
    float musicvolume;
    float soundvolume;

    public void ChangeMasterVolume(float value)
    {

        Mastervolume = value;
        audioMixer.SetFloat("MasterVolume", value);
    }

    public void ChangeMusicVolume(float value)
    {
        
        Musicvolume = value;
        audioMixer.SetFloat("MusicVolume", value);
    }

    public void ChangeSoundVolume(float value)
    {
        
        Soundvolume = value;
        audioMixer.SetFloat("SoundVolume", value);
    }


    public void test()
    {

        PlaySound(amogus, audioMixer.FindMatchingGroups("Sound")[0]);
    }


    public void PlaySound(AudioClip soundclip)
    {
        AudioSource newsound;

        newsound = gameObject.AddComponent<AudioSource>();

        newsound.clip = soundclip;
        newsound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Sound")[0];
        newsound.Play();
        playingsounds.Add(newsound);

    }

    public void PlaySound(AudioClip soundclip, AudioMixerGroup channel) 
    {
        //this is either 10 iq or 300 there is no inbetween
        AudioSource newsound;

         newsound = gameObject.AddComponent<AudioSource>();

        newsound.clip = soundclip;
        newsound.outputAudioMixerGroup = channel;
        newsound.Play();
        playingsounds.Add(newsound);

    }

    List<AudioSource> playingsounds= new List<AudioSource>();



    float destroytimer = 0;
    float timerlength=1;

    public float Mastervolume { get => mastervolume; set => mastervolume = value; }
    public float Musicvolume { get => musicvolume; set => musicvolume = value; }
    public float Soundvolume { get => soundvolume; set => soundvolume = value; }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");



        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }



    //the only update in the whole game and it makes it work with 12 fps
    //this is truly the best work of my life yet


    void Update()
    {
        if (Time.time > destroytimer) 
        {
            destroytimer = Time.time+timerlength;

           
            foreach (AudioSource sound in playingsounds.ToArray())
            {
                if (!sound.isPlaying) { playingsounds.Remove(sound); Destroy(sound); }
            }

        }

    }


}
