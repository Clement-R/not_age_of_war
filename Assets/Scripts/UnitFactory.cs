using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour {

    public GameObject loadingBar;

    public Transform spawnPosition;
    public List<UnitsToPrefab> units = new List<UnitsToPrefab>();

    private bool _isBusy = false;

    private void Start()
    {
        if(loadingBar != null)
            loadingBar.transform.localScale = new Vector2(0f, loadingBar.transform.localScale.y);
    }

    public void SpawnUnit(Units unit)
    {
        GameObject unitToSpawn = units.Find(e => e.unit == unit).prefab;
        if (!_isBusy)
            StartCoroutine(Spawn(unitToSpawn));
    }

    public void SpawnUnit(int index)
    {
        GameObject unitToSpawn = units[0].prefab;
        if(!_isBusy)
            StartCoroutine(Spawn(unitToSpawn));
    }

    private IEnumerator Spawn(GameObject unitToSpawn)
    {
        _isBusy = true;
        float t = 0f;
        while(t < 2f)
        {
            loadingBar.transform.localScale = new Vector2(t * (1f / 2f), loadingBar.transform.localScale.y);
            t += Time.deltaTime;
            yield return null;
        }

        Instantiate(unitToSpawn, spawnPosition.position, Quaternion.identity);
        loadingBar.transform.localScale = new Vector2(0f, loadingBar.transform.localScale.y);
        _isBusy = false;
    }
}

[System.Serializable]
public class UnitsToPrefab
{
    public Units unit;
    public GameObject prefab;
}

public enum Units
{
    LIGHT_PUNCH,
}