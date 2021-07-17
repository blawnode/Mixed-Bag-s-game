using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("Game");
    }
    public void exitgame()
    {
        Debug.Log("exitgame");
        Application.Quit();
    }
}
