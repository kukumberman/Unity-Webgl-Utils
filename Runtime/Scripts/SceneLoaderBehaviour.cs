using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kukumberman.WebglUtils
{
    public sealed class SceneLoaderBehaviour : MonoBehaviour
    {
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
