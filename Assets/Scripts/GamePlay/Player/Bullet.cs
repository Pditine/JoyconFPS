using UnityEngine;

namespace GamePlay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float lifeTime = 3;
        private Vector3 _direction;
        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }
        private void Update()
        {
            transform.position += _direction * (speed * Time.deltaTime);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }

        public void Init(Vector3 direction)
        {
            _direction = direction;
            transform.forward = direction;
        }
    }
}