using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Lamya.CrossyRoad
{
    public class GameOver : MonoBehaviour
    {
        public static GameOver Instance;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject endMenu;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        /// <summary>
        /// This method is called every time there is a lose condition and it disables the player and enables the game over screen.
        /// </summary>
        public void GameEnded()
        {
            player.SetActive(false);
            endMenu.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void MainMenuLoad()
        {
            SceneManager.LoadScene(0);
        }

    }

}
