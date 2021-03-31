using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected TMP_Text Text;

    public void OnValueChanged (int value)
    {
        Text.text = value.ToString();
    }
}