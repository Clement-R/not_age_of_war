using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateBehaviour : MonoBehaviour {
    public enum State
    {
        IDLE,
        RUN,
        ATTACK
    }

    public State ActualState
    {
        get { return _actualState ;}
    }

    private State _actualState = State.RUN;

    void Start () {
		
	}

    public void ChangeState(State newState)
    {
        _actualState = newState;
    }
}
