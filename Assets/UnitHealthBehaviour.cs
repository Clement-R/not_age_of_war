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
		StartCoroutine(Blink());

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

	private IEnumerator Blink() {
		var material = GetComponent<SpriteRenderer>().material;
		var counter = 0;
		while (counter <= 3) {
			material.SetFloat("_BlinkAmount", 1f);
			counter++;
			yield return new WaitForSeconds(0.15f);
			material.SetFloat("_BlinkAmount", 0f);
			yield return new WaitForSeconds(0.15f);
		}
	}
}
