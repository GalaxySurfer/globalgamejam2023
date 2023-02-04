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
	public int EndWorldState;
	public GameObject IconParent;
	public GameObject GameScreen;
	public Cutty CuttyScreen;
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
	private DialogueChain _currentDialogueChain = null;
	private int _currentDialogueIndex = -1;

	// The current state of the world.
	public int WorldState { get; private set; }

	private List<View> _viewList;
	private List<TextFile> _textFileList;
	private List<ImageFile> _imageFileList;
	private List<BinFile> _binFileList;
	private List<DialogueChain> _dialogueList;

	private PasswordLookup _passwords;
	private HideTable _hideTable;

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
		_dialogueList = Resources.LoadAll<DialogueChain>("DialogueChains").ToList();

		_passwords = Resources.LoadAll<PasswordLookup>("")[0];
		_hideTable = Resources.LoadAll<HideTable>("")[0];

		CuttyScreen.ToggleCutty(false);

		View rootFolder = _viewList.First(x => x.ViewId == 1);
		RenderView(rootFolder);
	}

	public void SetWorldState(int newState)
	{
		if (newState < 0) return;
		if (newState != WorldState)
		{
			WorldState = newState;
			EvaluateWorldState();
		}
	}

	private void EvaluateWorldState()
	{
		if (WorldState >= EndWorldState)
		{
			Debug.Log("GAME OVER");
			Debug.Break();
		}
		else
		{
			CuttyScreen.EvaluateWorldState();
		}
	}

	#region UI
	// Re-Render the existing view.
	public void RefreshView()
	{
		View currentView = _viewList.First(x => x.ViewId == _currentViewId);
		RenderView(currentView);
	}

	private void RenderView(View view)
	{
		// Clear parent first
		for (int index = IconParent.transform.childCount - 1; index >= 0; index--)
		{
			Destroy(IconParent.transform.GetChild(index).gameObject);
		}
		PathField.text = view.Path;
		_currentViewId = view.ViewId;
		foreach (ViewElement element in view.Elements)
		{
			// If this is null, then we have to render it, not hide it.
			if (_hideTable.HideList.FirstOrDefault(x => x.WorldState <= WorldState && x.SystemElementId == element.Id && x.Type == element.Type) == null)
			{
				GameObject systemElement = Instantiate(SystemElementPrefab, IconParent.transform);
				SystemElement elementComp = systemElement.GetComponent<SystemElement>();
				switch (element.Type)
				{
					case ElementType.Folder:
						elementComp.InitFolderButton(element.Name, element.Id, element.HasPassword);
						break;
					case ElementType.Text:
						elementComp.InitTextFileButton(element.Name, element.Id, element.HasPassword);
						break;
					case ElementType.Image:
						elementComp.InitImageFileButton(element.Name, element.Id, element.HasPassword);
						break;
					case ElementType.Bin:
						elementComp.InitBinFileButton(element.Name, element.Id);
						break;
				}
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
			View view = _viewList.First(x => x.Elements.FirstOrDefault(y => y.Id == id && y.Type == ElementType.Text) != null);
			view.Elements.First(x => x.Id == id).OnOpen?.Invoke();
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
	public void NextDialogueText()
	{
		_currentDialogueIndex += 1;
		if (_currentDialogueIndex >= _currentDialogueChain.Chain.Count)
		{
			// end dialogue
			CuttyScreen.SetSpeechBubbleText("");
			CuttyScreen.ToggleSpeechBubble(false);
			CuttyScreen.ToggleBlockScreen(false);
		}
		else
		{
			string text = _currentDialogueChain.Chain[_currentDialogueIndex].Text;
			List<DialogueText.Choice> choices = _currentDialogueChain.Chain[_currentDialogueIndex].Choices;
			Sprite cuttySprite = _currentDialogueChain.Chain[_currentDialogueIndex].CuttyImage;
			CuttyScreen.SetSpeechBubbleText(text);
			CuttyScreen.SetCuttyImage(cuttySprite);
			CuttyScreen.SetChoices(choices);
		}
	}

	public void StartNewDialogueChain(int dialogueChainId)
	{
		DialogueChain chain = _dialogueList.First(x => x.Id == dialogueChainId);
		_currentDialogueChain = chain;
		_currentDialogueIndex = -1;
	}
	#endregion

	#region Utility
	public PasswordPair GetPasswordPair(int id, ElementType type)
	{
		PasswordPair pair = _passwords.PasswordList.FirstOrDefault(x => x.Id == id && x.Type == type);
		return pair;
	}

	public bool CheckPassword(int id, string password, ElementType type)
		=> _passwords.PasswordList.FirstOrDefault(x => x.Id == id && x.Password == password && x.Type == type) != null;
	#endregion
}
