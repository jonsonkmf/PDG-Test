using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBomb : Weapon
{
    public override void Shoot ( Transform shootPoint)
    {
        Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
    }
}