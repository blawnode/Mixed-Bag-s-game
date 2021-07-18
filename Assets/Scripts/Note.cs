using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] protected string text;
    [SerializeField] protected TextAsset textAsset;
    [SerializeField] protected GameObject noteScreen;
    [SerializeField] protected TMPro.TextMeshProUGUI noteScreenText;
    [SerializeField] protected NoteReadingManager noteReadingManager;
    [SerializeField] protected Sprite bigNoteImage;

    public void Open()
    {
        Time.timeScale = 0;
        noteScreenText.text = textAsset.text;
        noteScreen.GetComponent<Animator>().Play("Enter");
        noteReadingManager.SetNote(this);
        noteReadingManager.SetImage(bigNoteImage);
    }

    public void Close()
    {
        Time.timeScale = 1;
        noteScreen.GetComponent<Animator>().Play("Leave");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Player"))
        {
            c2d.GetComponent<Player>().isUsingUI = true;
            Open();
        }
    }
}
