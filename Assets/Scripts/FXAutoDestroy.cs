using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXAutoDestroy : MonoBehaviour {
	public void DestroyGameObject() {
		Destroy(gameObject);
	}
}
