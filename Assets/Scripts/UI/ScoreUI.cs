using System;
using DefaultNamespace;
using PurpleFlowerCore;

namespace UI
{
    public partial class ScoreUI : UINode
    {
        private void Start()
        {
            GameManager.Instance.OnScoreChange += ChangeScore;
        }

        private void ChangeScore(int score)
        {
            ScoreUIText.text = $"当前分数：{score}";
        }
    }
}