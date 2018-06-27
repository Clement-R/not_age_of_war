using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBehaviour : MonoBehaviour {

	public int Health { get; private set; }
	public int maxHealth;

	private GameObject _healthbar;

	void Start () {
		Health = GetComponent<UnitData>().health;
		_healthbar = transform.GetChild(1).gameObject;
		maxHealth = Health;
	}

	public void LoseHealth(int amount) {
		Health -= amount;
	}

	public void GetHit(int amount) {
		// Update health
		LoseHealth(amount);
		// Update UI
		float value = Health * (1.0f / maxHealth);
		value = Mathf.Clamp(value, 0f, 1f);
		Debug.Log(value);

		_healthbar.transform.localScale = new Vector3(value, 1, 0);

		// Detech death
		if (Health <= 0) {
			// TODO : Death
			Destroy(gameObject);
		}
	}
}
