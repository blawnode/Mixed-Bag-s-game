using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSceneHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            GameObject loaderObject = GameObject.FindWithTag("SceneLoader");

            if (!loaderObject)
                Debug.LogError("Failed to find SceneLoader");

            SceneLoader loader = loaderObject.GetComponent<SceneLoader>();
            loader.LoadScene("Game (with map)");
        }
    }
}
