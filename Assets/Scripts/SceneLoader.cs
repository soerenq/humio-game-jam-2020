using Humio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public void LoadScene(string sceneName)
	{
		Console.Instance.ReplaceText($"You enter {sceneName}");
	   	SceneManager.LoadScene(sceneName); // Additive? hm.
	}
}
