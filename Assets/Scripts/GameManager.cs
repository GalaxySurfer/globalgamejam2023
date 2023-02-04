using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject IconParent;
    public GameObject GameScreen;
    public TMP_Text PathField;
    [Header("Prefabs"), Space(5)]
    public GameObject SystemElementPrefab;
    public GameObject TextWindowPrefab;
    public GameObject ImageWindowPrefab;
    public GameObject PasswordWindowPrefab;

    [Header("Testing")]
    [SerializeField]
    private Sprite testSprite;
    [SerializeField, TextArea]
    private string testText;

    // The id of the current view. Used with the GoBack() function.
    private int _currentViewId;
    // The current state of the world.
    private int _worldState;

    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Only one object of type {GetType()} may exist. Destroying {gameObject}.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    #endregion

    private void Start()
    {
        //TODO: Remove this as it's only for testing purposes that it is here.
        GameObject folderButton = Instantiate(SystemElementPrefab, IconParent.transform);
        GameObject textButton = Instantiate(SystemElementPrefab, IconParent.transform);
        GameObject imageButton = Instantiate(SystemElementPrefab, IconParent.transform);
        folderButton.GetComponent<SystemElement>().InitFolderButton("folder", 0, false);
        textButton.GetComponent<SystemElement>().InitTextFileButton("text_file.text", 1, false);
        imageButton.GetComponent<SystemElement>().InitImageFileButton("birthday.pic", 2, false);

        GameObject passwordFolderButton = Instantiate(SystemElementPrefab, IconParent.transform);
        GameObject passwordTextButton = Instantiate(SystemElementPrefab, IconParent.transform);
        GameObject passwordImageButton = Instantiate(SystemElementPrefab, IconParent.transform);
        passwordFolderButton.GetComponent<SystemElement>().InitFolderButton("passwordFolder", 3, true);
        passwordTextButton.GetComponent<SystemElement>().InitTextFileButton("passwordText", 4, true);
        passwordImageButton.GetComponent<SystemElement>().InitImageFileButton("passwordImage", 5, true);
    }

    public void GoBack()
    {
        //TODO: Use a lookup table to figure out what view should be shown given the _currentViewId
        Debug.Log("GoBack");
    }

    public void OpenFolder(int id, bool hasPassword)
    {
        //TODO: Make a setup that can switch to a different view.
        if (hasPassword)
        {
            GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
            window.GetComponent<PasswordWindow>().InitPasswordFolder("My birthday", id);
        }
        else
        {
            //TODO: Write the path of the new view to the main windows path field.
            Debug.Log($"Show View with id: {id}");
        }
    }

    public void OpenTextFile(int id, bool hasPassword)
    {
        //TODO: Make a setup that lets us find a text asset file given an id.
        if (hasPassword)
        {
            GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
            window.GetComponent<PasswordWindow>().InitPasswordText("Best Friend", id);
        }
        else
        {
            GameObject window = Instantiate(TextWindowPrefab, GameScreen.transform);
            window.GetComponent<TextWindow>().Init("text_file.text", testText);
        }
    }

    public void OpenImageFile(int id, bool hasPassword)
    {
        //TODO: Make a setup that lets us find an image asset file given an id.
        if (hasPassword)
        {
            GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
            window.GetComponent<PasswordWindow>().InitPasswordImage("Pet's Name", id);
        }
        else
        {
            GameObject window = Instantiate(ImageWindowPrefab, GameScreen.transform);
            window.GetComponent<ImageWindow>().Init("birthday.pic", testSprite);
        }
    }

    public bool CheckPassword(string password, int id)
    {
        //TODO: Make a setup that includes a lookup table for password and id pairs.
        return false;
    }
}
