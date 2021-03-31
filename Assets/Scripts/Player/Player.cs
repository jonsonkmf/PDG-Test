using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	[SerializeField] private WeaponSpawner _weaponSpawner;
	[SerializeField] private List<Weapon> _weapons;
	[SerializeField] private Transform _shootPoint;
	[SerializeField] private Transform _weaponConteiner;
	[SerializeField] private Weapon _currentWeapon;

	private int _currentWeaponNumber = 0;
	private int _countRedBullet = 1;
	private int _countGreenBullet = 1;

	public int CountRedBullet => _countRedBullet;
	public int CountGreenBullet => _countGreenBullet;
	public List<Weapon> Weapons => _weapons;
	public Weapon CurrrentWeapon => _currentWeapon;
	public Transform ShootPoint => _shootPoint;

	public event UnityAction IsWeaponTaked;
	public event UnityAction<int, int> BuletCountChange;

	private void Start ( )
	{
		foreach (var weapon in _weaponSpawner.Weapons)
		{
			_weapons.Add(weapon);
		}
		ChangeWeapon(_weapons[_currentWeaponNumber]);
	}

	private void OnTriggerEnter ( Collider other )
	{
		if(other.TryGetComponent<Weapon>(out Weapon takedweapon))
		{
			//Хотел реализовать через список оружия спаунера, но таr и не получилось сравнивать по типу
			//из-за этого не получилось реализовать так, чтобы при внесении новых типов оружия в спаунер пришлось редактировать тут
			if (takedweapon is GreenBomb)		
			{									
				_countGreenBullet++;
			}
			
			if (takedweapon is RedBomb)
			{
				_countRedBullet++;
			}

			AddWeapon(takedweapon);
			IsWeaponTaked?.Invoke();
			BuletCountChange?.Invoke(_countGreenBullet, _countRedBullet);
			takedweapon.transform.SetParent(_weaponConteiner);
			takedweapon.transform.position = new Vector3(_weaponConteiner.position.x, _weaponConteiner.position.y, _weaponConteiner.position.z);
		}
	}

	public void NextWeapon ( )
	{
		if (_currentWeaponNumber == _weapons.Count - 1)
			_currentWeaponNumber = 0;
		else
			_currentWeaponNumber++;

		ChangeWeapon(_weapons[_currentWeaponNumber]);
	}

	public void PreviousWeapon ( )
	{
		if (_currentWeaponNumber == 0)
			_currentWeaponNumber = _weapons.Count - 1;
		else
			_currentWeaponNumber--;

		ChangeWeapon(_weapons[_currentWeaponNumber]);
	}

	private void ChangeWeapon ( Weapon weapon )
	{
		_currentWeapon = weapon;
	}

	private void AddWeapon ( Weapon takedweapon )
	{
		_weapons.Add(takedweapon);
	}

	public void DeleteCurrentWeapon ( )
	{
		//Намудрил с этим скриптом и вообще со структурой игры. Извините за костыли)) всегда делая тестовое сильно волнуюсь
		if (_currentWeapon is GreenBomb)
		{
			_countGreenBullet--;
		}
		if (_currentWeapon is RedBomb)
		{
			_countRedBullet--;
		}

		BuletCountChange?.Invoke(_countGreenBullet, _countRedBullet);
		_weapons.Remove(_currentWeapon);
		Destroy(_currentWeapon.gameObject);

		if (_weapons.Count == 0)
		{
			_currentWeaponNumber = 0;
			_currentWeapon = null;
		}
		_currentWeaponNumber--;
		NextWeapon();
	}
}
