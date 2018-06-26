using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveBehaviour : MonoBehaviour {
    public float speed = 100f;

    private Rigidbody2D _rb2D;
    private UnitData _data;
    private UnitStateBehaviour _state;
    private Animator _animator;
    
	void Start () {
        _data = GetComponent<UnitData>();
        _rb2D = GetComponent<Rigidbody2D>();
        _state = GetComponent<UnitStateBehaviour>();
        _animator = GetComponent<Animator>();

        switch (_data.side)
        {
            case Side.RIGHT:
                GetComponent<SpriteRenderer>().flipX = true;
                break;
            case Side.LEFT:
                break;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnitData unitData = collision.gameObject.GetComponent<UnitData>();

        if (unitData != null && unitData.side != _data.side)
        {
            if(_state.ActualState != UnitStateBehaviour.State.ATTACK)
            {
                _animator.SetBool("IsRunning", false);
                _state.ChangeState(UnitStateBehaviour.State.ATTACK);
                Debug.Log("Go to attack stance");
            }
        }
    }
}
