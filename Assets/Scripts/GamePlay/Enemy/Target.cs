using DefaultNamespace;
using UnityEngine;
namespace GamePlay.Enemy
{
    public class Target : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Bullet"))
            {
                GameManager.Instance.ChangeScore(1);
                Destroy(gameObject);
            }
        }
    }
}