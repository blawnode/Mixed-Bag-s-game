using UnityEngine;

public class NoteReadingManager : MonoBehaviour
{
    Note currentNote;
    [SerializeField] Player player;

    public void SetNote(Note note)
    {
        currentNote = note;
    }

    public void CloseNote()
    {
        currentNote.Close();
        currentNote = null;
        player.isUsingUI = false;
    }
}
