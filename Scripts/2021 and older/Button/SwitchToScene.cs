using UnityEngine;
using UnityEngine.SceneManegment;
using System.Collections;

public class SwitchToScene : Monobehaviour {

	public string sceneName;
	
	public void ChangeScene()
	{
		SceneManager.changeScene(sceneName);
	}

}
