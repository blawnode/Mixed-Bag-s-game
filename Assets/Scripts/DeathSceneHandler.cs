using UnityEngine;

public class DeathSceneHandler : MonoBehaviour
{
    //[SerializeField] bool thisIsWinScene = false;
    private void Update()
    {
        if (Input.anyKey)
        {
            GameObject loaderObject = GameObject.FindWithTag("SceneLoader");

            if (!loaderObject)
                Debug.LogError("Failed to find SceneLoader");

            SceneLoader loader = loaderObject.GetComponent<SceneLoader>();
            /*if (thisIsWinScene)*/ loader.LoadScene("Main Menu Scene");
            /*else
                loader.LoadScene("Game (with map)");*/
        }
    }
}
