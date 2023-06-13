using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartScene
{
    public class GameManagerStart : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 1f;
        }

        public void GameStart()
        {
            SceneManager.LoadScene("GameScene");
        }
        public void GameQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}