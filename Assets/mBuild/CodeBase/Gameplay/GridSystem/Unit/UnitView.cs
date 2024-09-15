using Event = Spine.Event;
using UnityEngine;
using Spine.Unity;
using System;
using Spine;

namespace Gameplay.GridSystem
{
    public class UnitView : MonoBehaviour
    {
        private const string SKIN_NAME = "lvl";
        private const string IDLE_ = "idle";
        private const string SHOOT_ = "Atack";
        private const string _ALLY = "Player";
        private const string _ENEMY = "Enemi";

        public event Action OnShoot;
        
        [SerializeField] private string _eventName;
        private bool _shooted;
        
        private SkeletonAnimation _skeletonAnimation;
        
        private MeshRenderer _mesh;

        private Unit _parentUnit;
        
        public void Initialize(Unit parentUnit)
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _mesh = GetComponent<MeshRenderer>();
            
            _parentUnit = parentUnit;
            _parentUnit.OnStateChanged += UpdateState;
        }
        
        private void UpdateState()
        {
            if (_parentUnit.State == UnitState.Idle)
                StartIdleAnim();
            else if(_parentUnit.State == UnitState.Attacking) 
                StartShootAnim();
            else 
                DisableView();
        }

        private void StartIdleAnim()
        {
            EnableView();
            
            //set skin 
            Skeleton skeleton = _skeletonAnimation.Skeleton;
            skeleton.SetSkin(skeleton.Data.FindSkin(_parentUnit.Level + SKIN_NAME));
                
            //set anim
            if (_parentUnit.Team == Team.Ally)
                _skeletonAnimation.AnimationState.SetAnimation(0, IDLE_ + _ALLY, true);
            else
                _skeletonAnimation.AnimationState.SetAnimation(0, IDLE_ + _ENEMY, true);
            
            skeleton.SetToSetupPose();
        }
        
        private void StartShootAnim()
        {
            EnableView();
            _skeletonAnimation.AnimationState.Event += HandleShootEvent;
            
            if (_parentUnit.Team == Team.Enemy)
                _skeletonAnimation.AnimationState.SetAnimation(0, SHOOT_ + _ENEMY, true);
            else
                _skeletonAnimation.AnimationState.SetAnimation(0, SHOOT_ + _ALLY, true);
            
            _skeletonAnimation.AnimationState.Event -= HandleShootEvent;
        }

        private void EnableView()
        {
            _mesh.enabled = true;
        }
        
        private void DisableView()
        {
            _skeletonAnimation.ClearState();
            _mesh.enabled = false;
        }

        private void HandleShootEvent(TrackEntry trackentry, Event e)
        {
            if (_skeletonAnimation.Skeleton.Data.FindEvent(_eventName) == e.Data)
                OnShoot?.Invoke();
        }
    }
}