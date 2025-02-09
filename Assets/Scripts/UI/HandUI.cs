using System;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HandUI : MonoBehaviour
    {
        [SerializeField] private Hand hand;
        [SerializeField] private AmmoUI ammoUI;
        [SerializeField] private Text text;

        private void Start()
        {
            hand.OnChangeWeapon += ChangeWeapon;
            ChangeWeapon(true);
        }

        private void ChangeWeapon(bool isGun)
        {
            text.text = isGun ? "当前武器:枪" : "当前武器:手雷";
            ammoUI.gameObject.SetActive(isGun);
        }
    }
}