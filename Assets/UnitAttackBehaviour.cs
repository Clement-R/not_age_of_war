using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackBehaviour : MonoBehaviour {

    private UnitStateBehaviour _state;

    void Start () {
        _state = GetComponent<UnitStateBehaviour>();
    }
	
	void Update () {
        if (_state.ActualState == UnitStateBehaviour.State.ATTACK)
        {
            Debug.Log("ATTACK");
        }
    }
}
