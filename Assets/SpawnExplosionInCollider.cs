using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosionInCollider : MonoBehaviour {

	public GameObject explosionPrefab;

	private BoxCollider2D _box;

	void Start () {
		_box = GetComponent<BoxCollider2D>();
		if (_box.Equals(null)) {
			Destroy(this);
		}

		pkm.EventManager.EventManager.StartListening("BaseDestroy", TriggerEvent);
	}

	public void TriggerEvent(dynamic o) {
		if (o.side == GetComponent<UnitData>().side) {
			StartCoroutine(Effect());
		}
	}

	private IEnumerator Effect() {
		int counter = 0;

		Vector2 center = new Vector2(transform.position.x, transform.position.y) + _box.offset;
		float minX = center.x + _box.size.x / 2f;
		float maxX = _box.size.x;
		float minY = _box.size.x;
		float maxY = _box.size.x;

		while (counter <= 10) {
			Vector2 randomPosition = Vector2.zero;

			// TODO : Get random point in collider


			Instantiate(explosionPrefab, randomPosition, Quaternion.identity);
			yield return new WaitForSeconds(Random.Range(0.15f, 0.35f));
			counter++;
		}
	}
}
