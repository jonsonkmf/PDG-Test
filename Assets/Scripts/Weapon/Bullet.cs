using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _timeToDestruct = 10;
    [SerializeField] private int _damage = 20;
    [SerializeField] private int _speed = 20;
    [SerializeField] private GameObject _particleHit;
    [SerializeField] private int _radius = 5;


    private Rigidbody _rb;

    void Awake ( )
    {
        Invoke("DestroyNow", _timeToDestruct);
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.TransformDirection(Vector3.forward * _speed);  //надо передавать сюда вектор направления выстрела
    }

	private void OnCollisionEnter ( Collision collision )
	{
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(_particleHit, pos, rot);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider nearbyObject in colliders)
        {
            Enemy enemy = nearbyObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }

        DestroyNow();
    }

    void DestroyNow ( )
    {
        Destroy(gameObject);
    }
}
