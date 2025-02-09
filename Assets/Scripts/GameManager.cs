using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : SingletonMono<GameManager>
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject gamePlay;
        [SerializeField] private GameObject endMenu;
        public bool rumbleOn = true;
        public bool BGMMute = false;
        public bool EffectMute = false;
        public void StartGame()
        {
            PFCLog.Debug("start game");
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

        public void Quit()
        {
            Application.Quit();
        }
    }
}