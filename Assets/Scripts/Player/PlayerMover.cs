using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;
	[SerializeField] private float _rotateSpeed;

	public void MoveForward ( )
	{
		transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
	}

	public void MoveBack ( )
	{
		transform.Translate(-Vector3.forward * _moveSpeed * Time.deltaTime);
	}
	public void RotateLeft ( )
	{
		transform.Rotate(Vector3.up, -_rotateSpeed * Time.deltaTime);
	}
	public void RotateRight ( )
	{
		transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
	}
}
