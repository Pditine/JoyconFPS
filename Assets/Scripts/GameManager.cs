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
        [SerializeField] private AudioClip bgm;
        public bool rumbleOn = true;
        public bool BGMMute = false;
        public bool EffectMute = false;
        private int _score = 0;
        public int Score => _score;
        [SerializeField]private float gameTime = 60;
        public float currentTime = 0;
        private bool _gameStarted = false;
        public event Action<int> OnScoreChange; 
        public event Action<float> OnTimeChange;

        private void Start()
        {
            if(JoyconManager.Instance.j.Count == 0)
            {
                warningUI.SetActive(true);
            }
            AudioSystem.PlayBGM(bgm);
            AudioSystem.BGMVolume = 0.2f;
        }

        public void StartGame()
        {
            PFCLog.Debug("start game");
            mainMenu.SetActive(false);
            endMenu.SetActive(false);
            gamePlay.SetActive(true);
            _score = 0;
            currentTime = gameTime;
            _gameStarted = true;
            ChangeScore(0);
        }

        private void Update()
        {
            if (_gameStarted)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    EndGame();
                }
                OnTimeChange?.Invoke(currentTime);
            }
        }


        private void EndGame()
        {
            _gameStarted = false;
            EventSystem.EventTrigger("GameOver");
            endMenu.SetActive(true);
            gamePlay.SetActive(false);
            mainMenu.SetActive(false);
        }
        
        public void BackToMainMenu()
        {
            endMenu.SetActive(false);
            gamePlay.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void ChangeScore(int delta)
        {
            _score += delta;
            OnScoreChange?.Invoke(_score);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}