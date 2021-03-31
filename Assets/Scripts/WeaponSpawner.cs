using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoint;
    [SerializeField] private List<GameObject> _template;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _count;
    [SerializeField] private int _delaySpawn;
    [SerializeField] private Player _player;

    private float _timeAfterLastSpawn;
    private int _spawned;

    public List<Weapon> Weapons => _weapons;

    private void OnEnable ( )
	{
        _player.IsWeaponTaked += ChangeSpawn;

    }
    private void OnDisable ( )
    {
        _player.IsWeaponTaked -= ChangeSpawn;
    }

    private void Update ( )
    {
        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _delaySpawn && _count - 1 >= _spawned)
        {
            InstantiateWeapon();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }
    }

    private void InstantiateWeapon ( )
    {
        int numberPoint = Random.Range(0, _spawnPoint.Count);

        while (_spawnPoint[numberPoint].childCount != 0)
        {
            numberPoint = Random.Range(0, _spawnPoint.Count);
        }

        int numberWeapon = Random.Range(0, _template.Count);

        Enemy enemy = Instantiate(_template[numberWeapon], _spawnPoint[numberPoint].position, _spawnPoint[numberPoint].rotation, _spawnPoint[numberPoint]).GetComponent<Enemy>();
    }

    private void ChangeSpawn ( )
	{
        _spawned--;
        _timeAfterLastSpawn = 0;
    }
}
