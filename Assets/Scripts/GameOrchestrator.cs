using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOrchestrator : MonoBehaviour {

	void Start () {
		pkm.EventManager.EventManager.StartListening("GameEnd", OnGameEnd);
	}

	private void OnGameEnd(dynamic obj) {
		if (obj.side == Side.LEFT) {
			Debug.Log("Game win");
		} else {
			Debug.Log("Game lose");
		}
	}
}
