using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISystem : MonoBehaviour
{
    // These variables are used when adding new notes to note screen
    float ButtonStartYPos = 225f;
    float ButtonOffset = 55f;
    int NoteNumb;
    public Button ButtonNotePrefab;

    [SerializeField] GameObject NotesAndObjectives;
    [SerializeField] GameObject NotesList;
    [SerializeField] GameObject NoteRead;

    [SerializeField] public Text UpdateText;
    [SerializeField] public Text NoteTitleText;
    [SerializeField] public Text NoteReadText;

    [SerializeField] Button NotesButton;

    bool IsPauseActive;

    private void Start()
    {
        NotesAndObjectives.SetActive(false);
        NotesButton.onClick.AddListener(OpenNoteList);
        UpdateText.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !NoteRead.activeSelf)
        {
            StartCoroutine(UpdatePause());
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && NoteRead.activeSelf)
        {
            StartCoroutine(NoteList());
        }
    }

    IEnumerator UpdatePause()
    {
        IsPauseActive = !IsPauseActive;
        if (IsPauseActive)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator NoteList()
    {
        OpenNoteList();
        yield return new WaitForSeconds(1);
    }

    void OpenNoteList()
    {
        NotesList.SetActive(true);
        NoteRead.SetActive(false);
        NotesAndObjectives.SetActive(false);
    }

    public void CloseNoteList()
    {
        NotesList.SetActive(false);
        NoteRead.SetActive(false);
        NotesAndObjectives.SetActive(true);
    }

    void OpenPauseMenu()
    {
        NotesList.SetActive(false);
        NoteRead.SetActive(false);
        NotesAndObjectives.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ClosePauseMenu()
    {
        NotesList.SetActive(false);
        NoteRead.SetActive(false);
        NotesAndObjectives.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenNoteRead()
    {
        CloseNoteList();
        NoteRead.SetActive(true);
    }
    
    void CloseNoteRead()
    {
        OpenNoteList();
        NoteRead.SetActive(false);
    }

    public void AddNewNote(string NoteName, string NoteText)
    {
        var NewNoteButton = Instantiate(ButtonNotePrefab, Vector3.zero, Quaternion.identity);
        var RectTransform = NewNoteButton.GetComponent<RectTransform>();
        NewNoteButton.GetComponentInChildren<Text>().text = NoteName;
        RectTransform.SetParent(NotesList.transform, false);
        RectTransform.anchoredPosition = new Vector3(0, ButtonStartYPos, 0);
        NewNoteButton.gameObject.AddComponent<NoteReadingSystem>();
        NewNoteButton.GetComponent<NoteReadingSystem>().NoteName = NoteName;
        NewNoteButton.GetComponent<NoteReadingSystem>().NoteText = NoteText;
        ButtonStartYPos -= ButtonOffset;
    }
}