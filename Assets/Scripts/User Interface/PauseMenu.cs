using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] Player player;
    private float lastTimeScale = 1;
    bool isFocused = true;
    [SerializeField] private Animator loaderScreen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!(isFocused && focus))
            OnPause();

        isFocused = focus;
    }

    public void OnPause()
    {
        if(!pauseScreen.activeInHierarchy)
        {
            lastTimeScale = Time.timeScale;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            AudioManager.i.PauseMusic();
            player.isUsingUI = true;
        }
    }

    private void TogglePause()
    {
        if (pauseScreen.activeInHierarchy) OnPressBack();
        else OnPause();
    }

    public void OnPressBack()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = lastTimeScale;
        AudioManager.i.UnPauseMusic();
        player.isUsingUI = false;
    }

    public void OnPressExit()
    {
        AudioManager.i.UnPauseMusic();
        loaderScreen.updateMode = AnimatorUpdateMode.UnscaledTime;
        sceneLoader.LoadScene("Main Menu Scene");
    }
}
