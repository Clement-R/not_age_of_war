using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosionInCollider : MonoBehaviour {

	public GameObject explosionPrefab;
    public bool startOnAwake = false;

	private BoxCollider2D _box;

	void Start () {
		_box = GetComponent<BoxCollider2D>();
		if (_box.Equals(null)) {
			Destroy(this);
		}

		if (startOnAwake)
		{
			StartCoroutine(Effect());
		}
		else
		{
			pkm.EventManager.EventManager.StartListening("BaseDestroy", TriggerEvent);	
		}
	}

	public void TriggerEvent(dynamic o) {
		if (o.side == GetComponent<UnitData>().side) {
			StartCoroutine(Effect());
		}
	}

	private IEnumerator Effect() {
		int counter = 0;

		Vector2 center = new Vector2(transform.position.x, transform.position.y) + _box.offset * transform.localScale;

		// Debug.Log(center);
		// Debug.DrawLine(center + new Vector2(40f, 40f), center - new Vector2(40f, 40f), Color.red, 15f);
		// Debug.DrawLine(center + new Vector2(40f, -40f), center - new Vector2(40f, -40f), Color.red, 15f);

		float minX = center.x - (((_box.size.x / 2f) * transform.localScale.x) - 25f);
		float maxX = center.x + (((_box.size.x / 2f) * transform.localScale.x) - 25f);
		float minY = center.y - (((_box.size.y / 2f) * transform.localScale.y) - 25f);
		float maxY = center.y + (((_box.size.y / 2f) * transform.localScale.y) - 25f);

		while (counter <= 50) {
			Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

			// Debug.DrawLine(randomPosition + new Vector2(20f, 20f), randomPosition - new Vector2(20f, 20f), Color.blue, 15f);
			// Debug.DrawLine(randomPosition + new Vector2(20f, -20f), randomPosition - new Vector2(20f, -20f), Color.blue, 15f);

			Instantiate(explosionPrefab, randomPosition, Quaternion.identity);
			yield return new WaitForSecondsRealtime(Random.Range(0.15f, 0.35f));
			counter++;
		}
	}
}
