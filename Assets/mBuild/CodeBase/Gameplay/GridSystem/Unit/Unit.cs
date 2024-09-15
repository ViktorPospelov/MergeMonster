using Scripts.Infrastructure;
using Gameplay.Data;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Serialization;

namespace Gameplay.GridSystem
{
    public class Unit : MonoBehaviour
    {
        public event Action OnStateChanged;
        public event Action OnDie;
        
        [field: SerializeField] public UnitState State { get; private set;}
        [field: SerializeField] public int Level { get; private set;}
        [field: SerializeField] public Team Team { get; private set;}
        public int CurrentHealth { get; private set; }
        public int MaxHealth{ get; private set; }

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private UnitView _unitView;
        private CircleCollider2D _collider;
        private UnitGrid _targetGrid;
        private int _damage;

        private void Start()
        {
            _collider = GetComponent<CircleCollider2D>();
        }

        public void Initialize(int level, Team team)
        {
            Team = team;
            Level = level;
            _unitView.OnShoot += Shoot;
            _unitView.Initialize(this);

            UpdateState();
        }

        public void IncreaseLevel()
        {
            Level++;
            UpdateState();
        }
        
        public void StartFight(UnitGrid targetGrid)
        {
            if (Level == 0)
            {
                _collider.enabled = false;
                return;
            }
            
            _targetGrid = targetGrid;
            State = UnitState.Attacking;
            OnStateChanged?.Invoke();
        }
        
        public void GetDamage(int damage)
        {
            if (damage <= 0)
                throw new InvalidOperationException();

            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
            OnStateChanged?.Invoke();
            
            if (CurrentHealth <= 0)
                Die();
        }
        
        private void Shoot()
        {
            if (_targetGrid == null)
                throw new InvalidOperationException();

            if (_targetGrid.GetNearestUnit(this) == null)
                StopFight();
                
            Vector2 direction = _targetGrid.GetNearestUnit(this).transform.position - transform.position;
            float zRotation = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Bullet instance = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.Euler(0, 0, zRotation));
            instance.Init(Team, _damage);
        }
        
        private void UpdateState()
        {
            UnitData data = ServiceLocator.GetService<DataProvider>().UnitDataContainer.unitsData[Level];
            MaxHealth = data.MaxHealth;
            CurrentHealth = MaxHealth;
            _damage = data.Damage;
            
            if (Level == 0)
                State = UnitState.Disabled;
            else
                State = UnitState.Idle;
            
            
            OnStateChanged?.Invoke();
        }
        
        private void StopFight()
        {
            _collider.enabled = true;
            _targetGrid = null;
            UpdateState();
        }
        
        private void Die()
        {
            State = UnitState.Disabled;
            OnStateChanged?.Invoke();
            
            OnDie?.Invoke();
        }
    }

    public enum UnitState
    {
        Idle,
        Disabled,
        Attacking
    }
}
