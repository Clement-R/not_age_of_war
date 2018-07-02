using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GoldDropEffect : MonoBehaviour
{
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	public int reward;
	
	public GameObject moneyText;
	
	private Rigidbody2D _rb2d;
	private TrailRenderer _trail;

	private bool goTo = false;
	
	void Start ()
	{
		_rb2d = GetComponent<Rigidbody2D>();
		_trail = GetComponent<TrailRenderer>();
		_trail.enabled = false;
			
		// Apply random force to throw in air
		_rb2d.AddForce(new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), ForceMode2D.Impulse);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		moneyText = GameData.Instance.moneyText;

		_rb2d.gravityScale = 0f;
		_trail.enabled = true;
		goTo = true;
	}

	private void FixedUpdate() {
		if (goTo) {
			_rb2d.velocity = Vector2.zero;
			_rb2d.AddForce((moneyText.transform.position - transform.position).normalized * 40000f);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		// Give money to enemy
		pkm.EventManager.EventManager.TriggerEvent("UnitDie", new { side = Side.RIGHT, reward = reward });
		Destroy(this.gameObject);
	}
}
