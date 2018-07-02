using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealthBehaviour : MonoBehaviour {

	public int Health { get; private set; }
    [HideInInspector]
    public int maxHealth;

	private GameObject _healthbar;
	private UnitData _data;
	void Start () {
		_data = GetComponent<UnitData>();
		Health = _data.health;
		
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

		_healthbar.transform.localScale = new Vector3(value, 1, 0);

		// Detech death
		if (Health <= 0) {
			Die();
		}
	}

	private void Die() {
		// Drop gold
		if (_data.side != Side.LEFT && !_data.isBase)
		{
			int reward = _data.reward / 10;
			for (int i = 0; i < 10; i++)
			{
				Instantiate(GameData.Instance.goldPrefab, transform.position, Quaternion.identity).GetComponent<GoldDropEffect>().reward = reward;
			}	
		}
		
		// TODO : Play particle system and animation + disable box collider and fade out after delay

		if (_data.isBase) {
			pkm.EventManager.EventManager.TriggerEvent("BaseDestroy", new { side = _data.side });
			// Time.timeScale = 0f;
			Debug.Log("Game end");
		} else {
			Destroy(gameObject);
		}
	}

	private IEnumerator Blink() {
		var material = GetComponent<SpriteRenderer>().material;
		var counter = 0;
        float amount = 1f;
		while (counter <= 2) {
            amount = amount == 1f ? 0f : 1f;
            material.SetFloat("_BlinkAmount", amount);
			counter++;
            yield return new WaitForSeconds(0.15f);
		}
	}
}
