using UnityEngine;

namespace Assets.mBuild.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void Update()
        {
            Vector2 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}
