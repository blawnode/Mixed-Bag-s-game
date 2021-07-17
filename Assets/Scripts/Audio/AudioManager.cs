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
        BatteryPickup,
        BatterySpawn,
        LowBatteryAlert,
        BreatheIn,
        BreatheOut,
        ButtonHover,
        ButtonRelease,
        GenericButtonPress,
        PlayButtonPress,
        Death,
        Ow,
        LargeCollision,
        MinorCollision,
        NoteClose,
        NoteRead,
        NotePickup,
        ScreenFadeIn,
        ScreenFadeOut,
    }

    private void Initialize()
    {
    }

    public void Play(AudioName name)
    {
        GameObject soundGameObject = new GameObject("Sound");
        DontDestroyOnLoad(soundGameObject);

        AudioSource source = soundGameObject.AddComponent<AudioSource>();
        source.clip = GetAudioClip(name);

        source.Play();
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

}
