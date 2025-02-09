using DefaultNamespace;
using PurpleFlowerCore;
using UnityEngine;

namespace GamePlay
{
    public class GrenadeObj : MonoBehaviour
    {
        [SerializeField] private Transform fire;
        [SerializeField] private AudioClip explodeClip;

        private void Start()
        {
            Invoke("Explode", 5);
        }

        private void OnCollisionEnter(Collision other)
        {
            // if(other.gameObject.CompareTag("Target") || other.gameObject.CompareTag("Ground"))
                Explode();
        }
        
        private void Explode()
        {
            var colliders = Physics.OverlapSphere(transform.position, 10);
            foreach (var collider in colliders)
            {
                if(collider.gameObject.CompareTag("Target"))
                {
                    GameManager.Instance.ChangeScore(1);
                    Destroy(collider.gameObject);
                }
            }

            AudioSystem.PlayEffect(explodeClip, transform);
            fire.gameObject.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
}