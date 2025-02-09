using System;
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
        [SerializeField] private GameObject warningUI;
        public bool rumbleOn = true;
        public bool BGMMute = false;
        public bool EffectMute = false;
        private int score = 0;
        public event Action<int> OnScoreChange; 

        private void Start()
        {
            if(JoyconManager.Instance.j.Count == 0)
            {
                warningUI.SetActive(true);
            }
        }

        public void StartGame()
        {
            PFCLog.Debug("start game");
            mainMenu.SetActive(false);
            endMenu.SetActive(false);
            gamePlay.SetActive(true);
            UISystem.ShowUI("ScoreUI");
            score = 0;
            ChangeScore(0);
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

        public void ChangeScore(int delta)
        {
            score += delta;
            OnScoreChange?.Invoke(score);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}