using UnityEngine;
using TMPro;

public class NoteCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI noteText;
    public static int noteCount = 0;


    void Update()
    {

            noteText.text = "Notes: " + noteCount.ToString() +"/8";  

    }
}
