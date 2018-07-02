using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveBehaviour : MonoBehaviour {

    public Transform min;
    public Transform max;

    public float scrollSpeed = 10f;

    private float _cameraHalfWidth;
	private float _minX;
	private float _maxX;

	private void Start()
    {
        _cameraHalfWidth = Camera.main.orthographicSize * ((float)Screen.width / Screen.height);
	    _minX = min.position.x + _cameraHalfWidth;
	    _maxX = max.position.x - _cameraHalfWidth;
	}

    void Update () {
		if(Input.GetKey(KeyCode.LeftArrow))
        {
            Move(-1f);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(1f);
        }
    }

    private void Move(float direction)
    {
        transform.Translate(new Vector3(scrollSpeed * direction, 0f, 0f));
	    transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), 0f, -10f);
    }
}
