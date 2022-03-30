using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Core.Scripts._Utilities
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            try
            {
                Debug.LogFormat("Loading scene '{0}'", sceneName);
                SceneManager.LoadScene(sceneName);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}