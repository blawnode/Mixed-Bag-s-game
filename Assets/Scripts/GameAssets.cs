using UnityEngine;

public class GameAssets : MonoBehaviour
{
    static private GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
                DontDestroyOnLoad(_i);
            }

            return _i;
        }
    }

    [System.Serializable]
    public struct AudioClipToken
    {
        public AudioManager.AudioName name;
        public AudioClip clip;
    }

    [System.Serializable]
    public struct AudioClipMusicToken
    {
        public AudioManager.MusicName name;
        public AudioClip clip;
    }

    public AudioClipToken[] audioClips;
    public AudioClipMusicToken[] audioClipsMusic;
}
