using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject exitButton;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject creditsMenu;

    private void Start()
    {
        if (!IsExitable())
        {
            print("The game is played on an unexitable platform!");
            exitButton.GetComponent<UnityEngine.UI.Button>().enabled = false;
            exitButton.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0, 0);
        }
    }

    public void OnMainMenuBtnPlay()
    {
        // TODO: Make sure the appropriate scene is named Game Scene. Then uncomment the code line.
        //SceneManager.LoadScene("Game Scene");
    }

    public void OnMainMenuBtnSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnMainMenuBtnMystery()
    {
        // TODO: idk lmao. Seems like a good opportunity for an easter egg.
    }

    public void OnMainMenuBtnCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void OnMainMenuBtnExit()
    {
        Application.Quit();
    }

    public void OnBtnBackToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    private bool IsExitable()
    {
        switch(Application.platform)
        {
            case RuntimePlatform.WebGLPlayer:
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                return false;
        }
        return true;
    }
}
