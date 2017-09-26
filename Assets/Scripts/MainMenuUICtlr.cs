using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUICtlr : MonoBehaviour {

	public void LoadScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
	
}
