using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class TitleUI : MonoBehaviour
    {
        [SerializeField] private string loadScene = "PlayeScene";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ResetGameDate();
                Debug.Log("리셋");
            }
        }
        public void Play()
        {
            SceneManager.LoadScene(loadScene);
        }
        void ResetGameDate()
        {
            PlayerPrefs.DeleteAll();
        }
    }
    
}
