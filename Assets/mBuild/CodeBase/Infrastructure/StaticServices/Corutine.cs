using UnityEngine;

namespace Scripts.Infrastructure
{
    public class Corutine : MonoBehaviour
    {
        public static Corutine Instance { get; private set; }

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
    }
}
