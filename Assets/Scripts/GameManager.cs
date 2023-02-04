using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static PasswordLookup;
using static View;

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
	private int _currentViewId = 0;

	// The current state of the world.
	public int WorldState { get; private set; }

	private List<View> _viewList;
	private List<TextFile> _textFileList;
	private List<ImageFile> _imageFileList;
	private List<BinFile> _binFileList;
	private PasswordLookup _passwords;

	private readonly Dictionary<int, Action> _worldStateActions = new Dictionary<int, Action>()
	{
		{ 2, () => {} }
	};

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
		_textFileList = Resources.LoadAll<TextFile>("TextFiles").ToList();
		_imageFileList = Resources.LoadAll<ImageFile>("ImageFiles").ToList();
		_binFileList = Resources.LoadAll<BinFile>("BinFiles").ToList();
		_passwords = Resources.LoadAll<PasswordLookup>("")[0];

		View rootFolder = _viewList.First(x => x.ViewId == 1);
		RenderView(rootFolder);
	}

	public void SetWorldState(int newState)
	{
		WorldState = newState;
		EvaluateWorldState();
	}

	public void SetWorldStateFromBin(int binFileId)
	{
		WorldState = _binFileList.First(x => x.Id == binFileId).NewWorldState;
		EvaluateWorldState();
	}

	private void EvaluateWorldState()
	{
		if(_worldStateActions.ContainsKey(WorldState))
		{
			_worldStateActions[WorldState]?.Invoke();
		}
	}

	#region UI
	private void RenderView(View view)
	{
		// Clear parent first
		for (int index = IconParent.transform.childCount - 1; index >= 0; index--)
		{
			Destroy(IconParent.transform.GetChild(index).gameObject);
		}
		PathField.text = view.Path;
		_currentViewId = view.ViewId;
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
				case View.ElementType.Bin:
					systemElement.GetComponent<SystemElement>().InitBinFileButton(element.Name, element.Id);
					break;
			}
		}
	}
	
	public void GoBack()
	{
		if (_currentViewId <= 0) return;
		int previousViewId = _viewList.First(x => x.ViewId == _currentViewId).PreviousViewId;
		if (previousViewId == 0) return;
		View previousView = _viewList.First(x => x.ViewId == previousViewId);
		RenderView(previousView);
	}

	public void OpenFolder(int id, bool hasPassword)
	{
		if (hasPassword)
		{
			PasswordPair pair = GetPasswordPair(id, ElementType.Folder);
			if (pair == null)
			{
				Debug.LogError($"No password pair found for id '{id}', type '{Enum.GetName(typeof(ElementType), ElementType.Folder)}'");
			}
			else
			{
				GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
				window.GetComponent<PasswordWindow>().InitPasswordFolder(pair.Hint, id);
			}
		}
		else
		{
			View view = _viewList.First(x => x.ViewId == id);
			RenderView(view);
		}
	}

	public void OpenTextFile(int id, bool hasPassword)
	{
		//TODO: Make a setup that lets us find a text asset file given an id.
		if (hasPassword)
		{
			PasswordPair pair = GetPasswordPair(id, ElementType.Text);
			if (pair == null)
			{
				Debug.LogError($"No password pair found for id '{id}', type '{Enum.GetName(typeof(ElementType), ElementType.Text)}'");
			}
			else
			{
				GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
				window.GetComponent<PasswordWindow>().InitPasswordText(pair.Hint, id);
			}
		}
		else
		{
			GameObject window = Instantiate(TextWindowPrefab, GameScreen.transform);
			TextFile file = _textFileList.First(x => x.Id == id);
			window.GetComponent<TextWindow>().Init(file.FileName, file.Text);
		}
	}

	public void OpenImageFile(int id, bool hasPassword)
	{
		//TODO: Make a setup that lets us find an image asset file given an id.
		if (hasPassword)
		{
			PasswordPair pair = GetPasswordPair(id, ElementType.Image);
			if (pair == null)
			{
				Debug.LogError($"No password pair found for id '{id}', type '{Enum.GetName(typeof(ElementType), ElementType.Image)}'");
			}
			else
			{
				GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
				window.GetComponent<PasswordWindow>().InitPasswordImage(pair.Hint, id);
			}
		}
		else
		{
			GameObject window = Instantiate(ImageWindowPrefab, GameScreen.transform);
			ImageFile file = _imageFileList.First(x => x.Id == id);
			window.GetComponent<ImageWindow>().Init(file.FileName, file.Image);
		}
	}

	public void OpenBinFile(int id)
	{
		PasswordPair pair = GetPasswordPair(id, ElementType.Bin);
		if (pair == null)
		{
			Debug.LogError($"No password pair found for id '{id}', type '{Enum.GetName(typeof(ElementType), ElementType.Bin)}'");
		}
		else
		{
			GameObject window = Instantiate(PasswordWindowPrefab, GameScreen.transform);
			window.GetComponent<PasswordWindow>().InitPasswordBin(pair.Hint, id);
		}
	}
	#endregion

	#region Cutty
	public void StartDialogue(int id)
	{

	}
	#endregion

	#region Utility
	private PasswordPair GetPasswordPair(int id, ElementType type)
	{
		PasswordPair pair = _passwords.PasswordList.FirstOrDefault(x => x.Id == id && x.Type == type);
		return pair;
	}

	public bool CheckPassword(int id, string password, ElementType type)
		=> _passwords.PasswordList.FirstOrDefault(x => x.Id == id && x.Password == password && x.Type == type) != null;
	#endregion
}
