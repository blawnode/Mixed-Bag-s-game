using UnityEngine;

public class NoteReadingManager : MonoBehaviour
{
    Note currentNote;
    [SerializeField] UnityEngine.UI.Image noteImage;
    [SerializeField] Player player;
    [SerializeField] NoteCounter noteCounter;

    public void SetNote(Note note)
    {
        currentNote = note;
    }

    public void SetImage(Sprite image)
    {
        noteImage.sprite = image;
    }

    public void CloseNote()
    {
        currentNote.Close();
        currentNote = null;
        player.isUsingUI = false;
        player.canOpenPod = true;
        noteCounter.IncrementNoteCount();
    }
}
