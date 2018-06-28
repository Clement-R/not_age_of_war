using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour {
	public int health = 100;
	public int cost;
	public int reward;
	public Side side;

	public bool isBase = false;
}

public enum Side
{
    RIGHT,
    LEFT
}