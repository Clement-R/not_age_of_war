using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour {
	public int health = 100;
	public Side side;
}

public enum Side
{
    RIGHT,
    LEFT
}