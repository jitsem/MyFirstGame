using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableProjectile : Projectile
{
    public override void Launch(Vector2 destination)
    {
        base.Launch(destination);
        RotateSprite(destination);
    }

    private void RotateSprite(Vector2 destination)
    {
        if (destination != Vector2.zero)
        {
            float angle = Mathf.Atan2(destination.y, destination.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
