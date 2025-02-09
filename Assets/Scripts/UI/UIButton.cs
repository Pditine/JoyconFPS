using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class UIButton : MonoBehaviour
    {
        public UnityEvent onClick;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Bullet"))
            {
                PFCLog.Debug("button clicked");
                onClick.Invoke();
            }
        }
    }
}