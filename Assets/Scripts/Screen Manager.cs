using UnityEngine;

public class ScreenManager : MonoBehaviour
{

        [SerializeField] private GameObject startScreen;
        private bool gameStarted = false;
        private void Start()
        {
            Time.timeScale = 0f;
        }
        void Update()
        {
            if (!gameStarted && Input.anyKeyDown)
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            gameStarted = true;
            startScreen.SetActive(false);
            Time.timeScale = 1f;
        }


}
