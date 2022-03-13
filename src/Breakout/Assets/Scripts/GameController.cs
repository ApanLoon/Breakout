using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Debug.LogError($"GameController: Detected multiple instances in the scene, disabling \"{gameObject.name}\".");
                    gameObject.SetActive(false);
                }
            }
        }

        public void BallDied(Ball ball)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
