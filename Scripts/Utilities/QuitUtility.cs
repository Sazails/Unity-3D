using UnityEngine;

namespace _Project.Scripts._Utilities
{
    public class QuitUtility : MonoBehaviour
    {
        public void Quit()
        {
            Debug.Log("Quitting game!");
            Application.Quit();
        }
    }
}
