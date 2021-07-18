using UnityEngine;

public class EscapePod : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;

    public void BeOpen()
    {
        GetComponent<Animator>().Play("Open");
    }

    public void StartFlying()
    {
        GetComponent<Animator>().Play("Motion");
    }

    // Animator event
    public void Victory()
    {
        sceneLoader.LoadScene("Finish");
    }
}
