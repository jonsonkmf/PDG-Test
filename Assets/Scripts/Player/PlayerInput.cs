using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
	[SerializeField] private TrajectoryRenderer Trajectory;

	private PlayerMover _mover;
	private Player _player;

	private void Start ( )
	{
		_mover = GetComponent<PlayerMover>();
		_player = GetComponent<Player>();
	}

	private void Update ( )
	{
		float enter;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		new Plane(-transform.forward, transform.position + Camera.main.transform.forward*10).Raycast(ray, out enter);
		Vector3 mouseInWorld = ray.GetPoint(enter);
		Vector3 speed = (mouseInWorld - _player.ShootPoint.position).normalized*20;
		_player.ShootPoint.rotation = Quaternion.LookRotation(speed);

		if (Input.GetMouseButton(0))
			Trajectory.ShowTrajectory(_player.ShootPoint.position, speed);
		else
		{
			if (Input.GetMouseButtonUp(0))
			{
				Trajectory.UnShowTrajectory();

				if (_player.Weapons.Count != 0)
				{
					_player.CurrrentWeapon.Shoot(_player.ShootPoint);
					_player.DeleteCurrentWeapon();
				}

			}

			if (Input.GetKey(KeyCode.W))
			{
				_mover.MoveForward();
			}

			if (Input.GetKey(KeyCode.S))
			{
				_mover.MoveBack();
			}

			if (Input.GetKey(KeyCode.A))
			{
				_mover.RotateLeft();
			}

			if (Input.GetKey(KeyCode.D))
			{
				_mover.RotateRight();
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				_player.NextWeapon();
			}

			if (Input.GetKeyDown(KeyCode.Q))
			{
				_player.PreviousWeapon();
			}
		}		
	}
}
