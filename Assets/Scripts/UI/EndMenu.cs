using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

namespace UI
{
    public class EndMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;

        private void OnEnable()
        {
            scoreText.text = $"Score: {GameManager.Instance.Score}";
        }
    }
}