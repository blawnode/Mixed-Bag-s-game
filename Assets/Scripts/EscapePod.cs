using UnityEngine;

public class EscapePod : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    private bool isFlying = false;

    public void BeOpen()
    {
        GetComponent<Animator>().Play("Open");
    }

    public void StartFlying()
    {
        if (!isFlying)
        {
            GetComponent<Animator>().Play("Motion");
            AudioManager.i.Play(AudioManager.AudioName.EscapePodLaunch);
            isFlying = true;
        }
    }

    // Animator event
    public void Victory()
    {
        sceneLoader.LoadScene("Finish");
    }
}
