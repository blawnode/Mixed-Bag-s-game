using UnityEngine;

public class EscapePod : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;

    public void StartFlying()
    {
        GetComponent<Animator>().Play("Motion");
    }

    public void Victory()
    {
        // TODO: sceneLoader.LoadScene("Victory Scene");
    }
}
