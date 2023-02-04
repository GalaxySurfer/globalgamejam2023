using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject SystemElementPrefab;
	public GameObject IconParent;

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
		GameObject folderButton = Instantiate(SystemElementPrefab, IconParent.transform);
		GameObject textButton = Instantiate(SystemElementPrefab, IconParent.transform);
		GameObject imageButton = Instantiate(SystemElementPrefab, IconParent.transform);
		folderButton.GetComponent<SystemElement>().InitFolderButton("folder", 0);
		textButton.GetComponent<SystemElement>().InitTextFileButton("textfile.text", 0);
		imageButton.GetComponent<SystemElement>().InitImageFileButton("birthday.pic", 0);
	}

	public void OpenFolder(int id)
	{

	}

	public void OpenTextFile(int id)
	{

	}

	public void OpenImageFile(int id)
	{

	}
}
