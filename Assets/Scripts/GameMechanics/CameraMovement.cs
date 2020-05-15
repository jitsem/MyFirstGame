using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Positioning Values")]
    public Transform target;
    public float smoothing;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    [Header("Position Reset")]
    public VectorValue camMin;
    public VectorValue camMax;
    private Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        maxPosition = camMax.startValue;
        minPosition = camMin.startValue;
        MoveToTarget();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        var targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    public void KickScreen()
    {
        m_Animator.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }
    private IEnumerator KickCo()
    {
        yield return null;
        m_Animator.SetBool("kickActive", false);
    }

}
