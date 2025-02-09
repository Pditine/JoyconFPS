using System;
using DefaultNamespace;
using PurpleFlowerCore;
using UnityEngine;
namespace GamePlay.Enemy
{
    public class Target : MonoBehaviour
    {
        private void OnEnable()
        {
            EventSystem.AddEventListener("GameOver", DestroyTarget);
        }
        
        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameOver", DestroyTarget);
        }

        private void DestroyTarget()
        {
            Destroy(gameObject);
        }
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