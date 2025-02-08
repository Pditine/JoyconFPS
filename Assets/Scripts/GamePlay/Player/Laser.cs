using UnityEngine;

namespace GamePlay
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LineRenderer theLine;
        [SerializeField] private Vector3 offset;
        private void Update()
        {
            theLine.SetPosition(0, transform.position + offset);
            theLine.SetPosition(1, -transform.up * 1000);
        }
    }
}
