using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveBehaviour : MonoBehaviour {
    public float speed = 100f;

    private Rigidbody2D _rb2D;
    private UnitData _data;
    private UnitStateBehaviour _state;
    private Animator _animator;
	private UnitAttackBehaviour _attackBehaviour;
    private BoxCollider2D _box2D;
    private Vector2 _direction;
    private float _rayDistance;

    void Start () {
        _data = GetComponent<UnitData>();
        _rb2D = GetComponent<Rigidbody2D>();
        _state = GetComponent<UnitStateBehaviour>();
        _animator = GetComponent<Animator>();
		_attackBehaviour = GetComponent<UnitAttackBehaviour>();
        _box2D = GetComponent<BoxCollider2D>();
        _direction = transform.right;
        _rayDistance = Mathf.Abs(_box2D.offset.x) + (_box2D.size.x) + 20f;
        
        switch (_data.side)
        {
            case Side.RIGHT:
                GetComponent<SpriteRenderer>().flipX = true;
                _direction = -transform.right;
                _box2D.offset = new Vector2(-_box2D.offset.x, _box2D.offset.y);
                break;

            case Side.LEFT:
                _direction = transform.right;
                break;
        }
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(_direction.x, _direction.y, 0f) * _rayDistance, Color.red);

        foreach (var hit in Physics2D.RaycastAll(transform.position, _direction, _rayDistance))
        {
            if(hit.transform.gameObject != gameObject)
            {
                UnitData unitData = hit.transform.gameObject.GetComponent<UnitData>();

                if (unitData != null && unitData.side != _data.side)
                {
	                if (hit.transform.gameObject.GetComponent<UnitHealthBehaviour>().Health > 0) {
	                    if (_state.ActualState != UnitStateBehaviour.State.ATTACK)
	                    {
	                        _animator.SetBool("IsRunning", false);
	                        _animator.SetBool("TargetSet", true);
	                        _state.ChangeState(UnitStateBehaviour.State.ATTACK);

	                        _attackBehaviour.target = hit.transform.gameObject.GetComponent<UnitHealthBehaviour>();

	                        Debug.Log("Go to attack stance");
	                    }
	                } else {
		                if (unitData.isBase) {
			                _state.ChangeState(UnitStateBehaviour.State.IDLE);
			                _animator.SetBool("IsBlocked", true);
						}
	                }
                }
                else if (unitData != null && unitData.side == _data.side)
                {
                    _state.ChangeState(UnitStateBehaviour.State.IDLE);
                    _animator.SetBool("IsBlocked", true);
                }
                break;
            }
            else
            {
                if(_state.ActualState == UnitStateBehaviour.State.IDLE)
                {
                    _state.ChangeState(UnitStateBehaviour.State.RUN);
                    _animator.SetBool("IsBlocked", false);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(_state.ActualState == UnitStateBehaviour.State.RUN)
        {
            switch (_data.side)
            {
                case Side.RIGHT:
                    _rb2D.velocity = -transform.right * speed;
                    break;
                case Side.LEFT:
                    _rb2D.velocity = transform.right * speed;
                    break;
            }
        }
        else
        {
            _rb2D.velocity = Vector2.zero;
        }
    }
}
