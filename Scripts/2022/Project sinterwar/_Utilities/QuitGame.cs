using UnityEngine;

namespace Assets._Core.Scripts._Utilities
{
    public class QuitGame : MonoBehaviour
    {
        public void Quit()
        {
            Debug.Log("Game quit");
            Application.Quit();
        }
    }
}
