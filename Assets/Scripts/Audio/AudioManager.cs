using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static private AudioManager _i;

    public static AudioManager i
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("AudioManager")) as GameObject).GetComponent<AudioManager>();
                _i.Initialize();

                DontDestroyOnLoad(_i);
            }

            return _i;
        }
    }

    public enum AudioName
    {
        O2Pickup,
        LowO2Alert,
        BreatheIn, // DELETE ? // 
        BreatheOut, // DELETE ? // 
        ButtonHover,
        ButtonRelease,
        GenericButtonPress,
        PlayButtonPress,
        Death,
        Ow, // DELETE? //
        LargeCollision,
        MinorCollision,
        NoteClose,
        NoteRead,
        NotePickup,
        ScreenFadeIn,
        ScreenFadeOut,
        MediumCollision,
        RemovedO2Spawn,
        EscapePodLaunch
    }

    public enum MusicName
    {
        MainMenu,
        Game,
        Death,
        Finish,
    }

    private AudioSource currentMusicSource = null;

    private void Initialize()
    {
        GameObject soundGameObject = new GameObject("Sound");
        DontDestroyOnLoad(soundGameObject);

        currentMusicSource = soundGameObject.AddComponent<AudioSource>();
        currentMusicSource.clip = null;
    }

    public void Play(AudioName name)
    {
        GameObject soundGameObject = new GameObject("Sound");
        DontDestroyOnLoad(soundGameObject);

        AudioSource source = soundGameObject.AddComponent<AudioSource>();
        source.clip = GetAudioClip(name);

        source.Play();
    }

    public void PlayMusic(MusicName name)
    {
        currentMusicSource.clip = GetAudioClipMusic(name);
        if (name == MusicName.Death) currentMusicSource.loop = false;
        else currentMusicSource.loop = true;
        currentMusicSource.Play();
    }
    
    public void StopMusic()
    {
        currentMusicSource.Stop();
    }

    private AudioClip GetAudioClip(AudioName name)
    {
        foreach(GameAssets.AudioClipToken token in GameAssets.i.audioClips)
        {
            if (token.name == name)
                return token.clip;
        }

        Debug.LogError("Failed to find audio with name: " + name);
        return null;
    }

    private AudioClip GetAudioClipMusic(MusicName name)
    {
        foreach (GameAssets.AudioClipMusicToken token in GameAssets.i.audioClipsMusic)
        {
            if (token.name == name)
                return token.clip;
        }

        Debug.LogError("Failed to find audio with name: " + name);
        return null;
    }

}
