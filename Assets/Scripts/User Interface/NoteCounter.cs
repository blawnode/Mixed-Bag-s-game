using UnityEngine;
using TMPro;

public class NoteCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;
    public int noteCount = 0;
    [SerializeField] private EscapePod escapePod;
    [SerializeField] private int neededNoteAmount = 8;

    void Start()
    {
        noteText.text = "Notes: " + noteCount.ToString() + "/8";
    }

    public void IncrementNoteCount()
    {
        noteText.text = "Notes: " + (++noteCount).ToString() + "/8";
        if(noteCount == neededNoteAmount)
        {
            escapePod.BeOpen();
            AudioManager.i.Play(AudioManager.AudioName.EscapePodOpen);
        }
    }
}
