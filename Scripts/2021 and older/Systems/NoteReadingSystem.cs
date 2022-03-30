using UnityEngine;
using UnityEngine.UI;
public class NoteReadingSystem : MonoBehaviour {
    [SerializeField] Button ThisButton;
    UISystem _UISystem;
    public string NoteName;
    public string NoteText;
    private void Start()
    {
        _UISystem = FindObjectOfType<UISystem>();
        ThisButton = gameObject.GetComponent<Button>();
        ThisButton.onClick.AddListener(WriteText);
    }

    void WriteText()
    {
        _UISystem.NoteTitleText.text = NoteName;
        _UISystem.NoteReadText.text = NoteText;
        _UISystem.OpenNoteRead();
    }
}
