using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBomb : Weapon
{
    public override void Shoot ( Transform shootPoint )
    {
        Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
    }
}