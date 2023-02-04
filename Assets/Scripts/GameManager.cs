using System.Collections.Generic;
using System.Linq;
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

	private List<View> _viewList;

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
		_viewList = Resources.LoadAll<View>("Views").ToList();
		View rootFolder = _viewList.First(x => x.ViewId == 0);
		RenderView(rootFolder);
	}

	public void RenderView(View view)
	{
		// Clear parent first
		for (int index = IconParent.transform.childCount - 1; index >= 0; index--)
		{
			Destroy(IconParent.transform.GetChild(index).gameObject);
		}

		foreach (View.ViewElement element in view.Elements)
		{
			GameObject systemElement = Instantiate(SystemElementPrefab, IconParent.transform);
			switch (element.Type)
			{
				case View.ElementType.Folder:
					systemElement.GetComponent<SystemElement>().InitFolderButton(element.Name, element.Id, element.HasPassword);
					break;
				case View.ElementType.Text:
					systemElement.GetComponent<SystemElement>().InitTextFileButton(element.Name, element.Id, element.HasPassword);
					break;
				case View.ElementType.Image:
					systemElement.GetComponent<SystemElement>().InitImageFileButton(element.Name, element.Id, element.HasPassword);
					break;
			}
		}
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
