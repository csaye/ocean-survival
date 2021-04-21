using UnityEngine;
using UnityEngine.SceneManagement;

namespace OceanSurvival.UI
{
    public class MenuButton : MonoBehaviour
    {
        public void LoadScene(string scene) => SceneManager.LoadScene(scene);
        public void Quit() => Application.Quit();
    }
}
