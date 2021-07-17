using UnityEngine;

public class NoteReadingManager : MonoBehaviour
{
    Note currentNote;

    public void SetNote(Note note)
    {
        currentNote = note;
    }

    public void CloseNote()
    {
        currentNote.Close();
    }
}
