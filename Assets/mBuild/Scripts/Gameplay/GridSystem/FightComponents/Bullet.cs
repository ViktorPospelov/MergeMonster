using Scripts.Gameplay.GridSystem;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    [SerializeField] private int _damage;
    private Team _team;

    public void Init(Team team, int damage)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * _speed;
        _rigidbody.isKinematic = true;

        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;

        _damage = damage;
        _team = team;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Unit unit))
        {
            if (unit.Team != _team)
            {
                unit.GetDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
