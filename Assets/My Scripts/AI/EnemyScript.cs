using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	static public int health = 100;
	public GameObject shot;
	private float nextFire;
	public Transform shotSpawn;

	public int scoreValue;
	private GameController gameController;

	void Start () {

		transform.rotation = Quaternion.Euler(0, 180, 0);

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update ()
	{
		if (Time.time > nextFire) {
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
						nextFire = Time.time + 1.0f;
						GetComponent<AudioSource>().Play ();
				}
	}
}
