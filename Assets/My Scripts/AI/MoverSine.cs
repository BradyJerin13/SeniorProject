using UnityEngine;
using System.Collections;

public class MoverSine : MonoBehaviour {

	float index = 0;
	float startz = 0;
	float startx = 0;

	void Start () {
		startz = transform.position.z;
		startx = transform.position.x;
	}
	
	public void Update(){
		index += Time.deltaTime;
		startz += 0.16f;
		float x = startx + 1 * Mathf.Sin (10 * index);
		transform.position = new Vector3(x, 0, startz);
	}
}