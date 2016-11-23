using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;

	private Vector3 relativeCameraPosition = new Vector3(0,16,-2);
	public PlayerController player;

	private float rotationX = -90.0f;
	private float rotationY = 0.0f;
 
	void Start ()
	{
		//relativeCameraPosition = new Vector3(0, 10, 0);
		//Screen.lockCursor = true;
	}
 
	void Update ()
	{
		if (player == null)
		{
			player = FindObjectOfType<PlayerController>();
		}



		//rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
		//rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
		//rotationY = Mathf.Clamp (rotationY, -90, 90);
 
		//transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		//transform.localRotation *= Quaternion.AngleAxis(targetFollow.transform.rotation.y, Vector3.left);
		if (player.focusCharacter != null)
		{
			transform.position = player.focusCharacter.transform.position + relativeCameraPosition;
			//Quaternion targetRotation = Quaternion.LookRotation(player.focusCharacter.transform.position - transform.position);
			//transform.rotation = targetRotation;
		}
		

		//if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		//{
		//	transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
		//	transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		//}
		//else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
		//{
		//	transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
		//	transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
		//}
		//else
		//{
		//	transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
		//	transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		//}
 
 
		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			relativeCameraPosition.y++;
		}
		if (Input.GetKey (KeyCode.DownArrow))
		{
			relativeCameraPosition.y--;
		}

 
		if (Input.GetKeyDown (KeyCode.End))
		{
			Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
		}
	}
}
