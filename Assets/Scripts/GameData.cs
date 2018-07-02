using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData Instance
    {
        get {
            return _instance;
        }
    }
    
    public GameObject goldPrefab;
    public GameObject moneyText;
    public Side playerSide = Side.LEFT;
    
    private static GameData _instance = null;

    private void Start()
    {
        _instance = this;
    }
}
