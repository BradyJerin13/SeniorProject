using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {
	
	float scrollSpeed = 0;
	float scrollSpeed2 = 0;

	void Start () {
		scrollSpeed = Random.Range (0.15f, 0.15f);
		scrollSpeed2 = Random.Range (-0.10f, 0.10f);
	}
	
	void FixedUpdate() {
		float offset = Time.time * scrollSpeed;
		float offset2 = Time.time * scrollSpeed2;
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (-offset2,offset);
	}
}
