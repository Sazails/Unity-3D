using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts._Utilities
{
    public class SceneUtility : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void ReloadLoadedScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
