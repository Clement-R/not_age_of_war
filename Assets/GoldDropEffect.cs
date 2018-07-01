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

	public GameObject moneyText;
	
	private Rigidbody2D _rb2d;
	private TrailRenderer _trail;
	
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
		Debug.Log("Collision with floor");

		moneyText = GameData.Instance.moneyText;

		// TODO : Got to money transform
		// Vector3 target = Camera.main.ScreenToWorldPoint(moneyText.rectTransform.position);
		
		Debug.Log((moneyText.transform.position - transform.position).normalized);

		_rb2d.gravityScale = 0f;
		_trail.enabled = true;
		_rb2d.AddForce((moneyText.transform.position - transform.position).normalized * 1000f, ForceMode2D.Impulse);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(this.gameObject);
	}
}
