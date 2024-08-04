using UnityEngine;
using TMPro;
using System;
using Scripts.Gameplay.GridSystem;
using System.Collections;

public class Unit : MonoBehaviour
{
    public event Action<int> OnHealthChanged;
    public event Action OnDie;

    [field: SerializeField] public int Level { get; private set;}
    [field: SerializeField] public Team Team { get; private set;}

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay;

    private SpriteRenderer _spriteRenderer;
    private UnitGrid _targetGrid;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    private int _maxHealth;

    public void Initialize(int level, Team team)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Level = level;
        Team = team;

        _health = 10;
        _maxHealth = _health;
        UpdateState();
    }

    public void IncreaseLevel()
    {
        Level++;
        UpdateState();
    }

    public void Attack(UnitGrid targetGrid)
    {
        _targetGrid = targetGrid;
        if (_targetGrid == null)
            return;

        StartCoroutine(Shoot());
    }

    public void GetDamage(int damage)
    {
        if (damage <= 0)
            throw new InvalidOperationException();

        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        StopCoroutine(Shoot());
        Destroy(gameObject);

        OnDie?.Invoke();
    }

    private void UpdateState()
    {
        _text.text = Level.ToString();
        _spriteRenderer.color = new Color(1, (float)(Level - 1) / 3, 0, 1);
    }

    private IEnumerator Shoot()
    {
        while (true) 
        {
            if (_targetGrid == null) break;

            if (_targetGrid.GetNearestUnit(this) == null) break;

            Vector2 direction = _targetGrid.GetNearestUnit(this).transform.position - transform.position;
            float zRotation = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Bullet instance = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.Euler(0, 0, zRotation));
            instance.Init(Team, _damage);

            yield return new WaitForSeconds(_shootDelay);
        }
    }
}
