using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

/// <summary>
/// Creates wandering behaviour for a CharacterController.
/// </summary>
[RequireComponent(typeof(MobController))]
public class Wander : MonoBehaviour
{
	public float speed = 3;
	public float directionChangeInterval = 1;

	public float xRange;
	public float zRange;

	MobController controller;
	private Vector3 _targetPosition;

	private Vector3 _originalPosition;

	void Awake()
	{
		controller = GetComponent<MobController>();

		_originalPosition = transform.position;

		// Set random initial rotation
		StartCoroutine(NewHeading());
	}

	void Update()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_targetPosition - transform.position), directionChangeInterval * Time.deltaTime);

		//transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetPosition, Time.deltaTime * directionChangeInterval);
		var forward = transform.TransformDirection(Vector3.forward);
		controller.Move();
	}

	/// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
	IEnumerator NewHeading()
	{
		while (true)
		{
			xRange = Random.Range(-3, 3);
			zRange = Random.Range(-3, 3);

			_targetPosition = _originalPosition + new Vector3(xRange, 0, zRange);
			yield return new WaitForSeconds(directionChangeInterval);
		}
	}

}
