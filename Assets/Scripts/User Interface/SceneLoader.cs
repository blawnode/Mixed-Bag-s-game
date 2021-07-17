using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private Animator deathTransition;

    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneCoroutine(index));
    }

    public void LoadScene(string name)
    {
        StartCoroutine(LoadSceneCoroutine(name));
    }

    public void LoadDeathScene()
    {
        deathTransition.SetTrigger("Start");
    }

    public void LoadSceneFromDeath()
    {
        StartCoroutine(LoadSceneFromDeathCoroutine());
    }

    private IEnumerator LoadSceneFromDeathCoroutine()
    {
        deathTransition.SetTrigger("End");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Game");
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

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(name);
    }

}