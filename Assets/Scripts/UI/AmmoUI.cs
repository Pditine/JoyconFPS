using System;
using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AmmoUI : MonoBehaviour
    {
        [SerializeField] private Gun gun;
        [SerializeField] private Text text;

        private void Start()
        {
            gun.OnAmmoChange += ChangeAmmo;
            gun.OnReload += OnReload;
        }

        private void ChangeAmmo(int current,int max)
        {
            text.text = $"子弹:{current}/{max}";
        }
        
        private void OnReload()
        {
            text.text = "装弹中...";
        }
    }
}