using UnityEngine;
using TMPro;

public class NoteCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;
    public int noteCount = 0;
    [SerializeField] private EscapePod escapePod;

    void Start()
    {
        noteText.text = "Notes: " + noteCount.ToString() + "/8";
    }

    public void IncrementNoteCount()
    {
        noteText.text = "Notes: " + (++noteCount).ToString() + "/8";
        if(noteCount == 1)
        {
            escapePod.BeOpen();
        }
    }
}
