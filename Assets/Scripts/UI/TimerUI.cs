using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private Text text;

        private void Update()
        {
            text.text = $"剩余时间:{(int)GameManager.Instance.currentTime}";
        }
    }
}