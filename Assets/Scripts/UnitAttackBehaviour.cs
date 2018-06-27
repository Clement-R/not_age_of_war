using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackBehaviour : MonoBehaviour {
	public float attackCooldown = 0.5f;
    [HideInInspector]
    public UnitHealthBehaviour target;

    private UnitStateBehaviour _state;
	private UnitHealthBehaviour _healthSystem;
	private Animator _animator;
	private float _lastAttackTime = 0f;

	void Start () {
        _state = GetComponent<UnitStateBehaviour>();
		_animator = GetComponent<Animator>();
		_healthSystem = GetComponent<UnitHealthBehaviour>();
	}
	
	void Update () {
		if (_state.ActualState == UnitStateBehaviour.State.ATTACK) {
			if (_lastAttackTime + attackCooldown <= Time.time && target != null) {
                _animator.SetBool("CanAttack", true);
            } else if(target == null) {
		        _animator.SetBool("CanAttack", false);
		        _animator.SetBool("TargetSet", false);
		        _animator.SetBool("IsRunning", true);
				_state.ChangeState(UnitStateBehaviour.State.RUN);
			}
		}
    }

    /// <summary>
    /// Called in attack animation
    /// </summary>
	private void Attack() {
		target.GetHit(25);
        _lastAttackTime = Time.time;
        _animator.SetBool("CanAttack", false);
    }
}
