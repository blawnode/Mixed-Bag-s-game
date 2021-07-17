using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] protected string text;
    [SerializeField] protected GameObject noteScreen;
    [SerializeField] protected TMPro.TextMeshProUGUI noteScreenText;
    [SerializeField] protected NoteReadingManager noteReadingManager;

    public void Open()
    {
        Time.timeScale = 0;
        noteScreenText.text = text;
        noteScreen.GetComponent<Animator>().Play("Enter");
        noteReadingManager.SetNote(this);
    }

    public void Close()
    {
        // TODO: Be able to read this again later? (Might be too complex for our time limit)
        Time.timeScale = 1;
        noteScreen.GetComponent<Animator>().Play("Leave");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
            Open();
    }
}
