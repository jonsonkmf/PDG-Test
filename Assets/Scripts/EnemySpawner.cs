using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoint;
    [SerializeField] private GameObject _template;
    [SerializeField] private int _count;
    [SerializeField] private int _delaySpawn;

    private float _timeAfterLastSpawn;
    private int _spawned;

	private void Update ( )
    {
        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _delaySpawn && _count-1 >= _spawned)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }
    }

    private void InstantiateEnemy ( )
    {
        int number = Random.Range(0, _spawnPoint.Count);

		while (_spawnPoint[number].childCount !=0)
		{
            number = Random.Range(0, _spawnPoint.Count);
        }

        Enemy enemy = Instantiate(_template, _spawnPoint[number].position, _spawnPoint[number].rotation, _spawnPoint[number]).GetComponent<Enemy>();
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying ( Enemy enemy )
    {
        enemy.Dying -= OnEnemyDying;
        _spawned--;
        _timeAfterLastSpawn = 0;
    }
}
