using System;
using UnityEngine;

namespace GamePlay
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private GameObject grenade;
        [SerializeField] private GameObject gun;
        [SerializeField] private float cd = 0.5f;
        private float _currentCd;
        private Joycon Joycon => JoyconManager.Instance.j.Count > 0 ? JoyconManager.Instance.j[0] : null;
        private void Update()
        {
            _currentCd += Time.deltaTime;
            if(Joycon != null && Joycon.GetButtonDown(Joycon.Button.DPAD_UP) && _currentCd >= cd)
            {
                _currentCd = 0;
                if(grenade.activeSelf)
                {
                    grenade.SetActive(false);
                    gun.SetActive(true);
                }
                else
                {
                    grenade.SetActive(true);
                    gun.SetActive(false);
                }
            }
        }
    }
}