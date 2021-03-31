using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Enemy _target;

    private void OnEnable()
    {
        _target.HealthChanged += OnValueChanged;
        Text.text = _target.Health.ToString();
    }

    private void OnDisable()
    {
        _target.HealthChanged -= OnValueChanged;
    }
}