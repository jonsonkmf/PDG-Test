using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletBar : Bar
{
    [SerializeField] private Player _target;
	[SerializeField] protected TMP_Text Text2;
	private void OnEnable ( )
	{
		_target.BuletCountChange += OnValueChanged2;
		Text.text = _target.CountRedBullet.ToString();
		Text2.text = _target.CountGreenBullet.ToString();
	}

	private void OnDisable ( )
	{
		_target.BuletCountChange -= OnValueChanged2;
	}

	public void OnValueChanged2 ( int value, int value2 )
	{
		Text.text = value.ToString();
		Text2.text = value2.ToString();
	}
}
