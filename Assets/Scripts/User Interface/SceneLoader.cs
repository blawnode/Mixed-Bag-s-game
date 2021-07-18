using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main Menu Scene":
                AudioManager.i.PlayMusic(AudioManager.MusicName.MainMenu);
                break;
            case "Game (with map)":
                AudioManager.i.PlayMusic(AudioManager.MusicName.Game);
                break;
            case "DeathByO2": case "DeathByHp": case "DeathByLock":
                AudioManager.i.PlayMusic(AudioManager.MusicName.Death);
                break;
            case "Finish":
                AudioManager.i.PlayMusic(AudioManager.MusicName.Finish);
                break;
        }
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadSceneCoroutine(name));
    }

    private IEnumerator LoadSceneCoroutine(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(index);
    }

    private IEnumerator LoadSceneCoroutine(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

}