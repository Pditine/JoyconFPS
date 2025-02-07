using PurpleFlowerCore.Utility;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : SingletonMono<GameManager>
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject gamePlay;
        [SerializeField] private GameObject endMenu;
        
        public void StartGame()
        {
            mainMenu.SetActive(false);
            endMenu.SetActive(false);
            gamePlay.SetActive(true);
        }
        
        public void EndGame()
        {
            endMenu.SetActive(true);
            gamePlay.SetActive(false);
            mainMenu.SetActive(true);
        }
        
        public void BackToMainMenu()
        {
            endMenu.SetActive(false);
            gamePlay.SetActive(false);
            mainMenu.SetActive(true);
        }
    }
}