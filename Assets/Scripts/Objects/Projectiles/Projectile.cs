using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public float lifeTime;
    private float lifeTimeInSeconds;
    public Rigidbody2D m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeInSeconds = lifeTime;
    }
    
    // Update is called once per frame
    void Update()
    {
        lifeTimeInSeconds -= Time.deltaTime;
        if(lifeTimeInSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void Launch(Vector2 destination)
    {
        m_RigidBody.velocity = destination.normalized * moveSpeed;
    }

}
