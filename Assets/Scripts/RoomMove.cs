using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    private CameraMovement m_Camera;
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!other.isTrigger && other.CompareTag("Player"))
        {
             m_Camera.minPosition += cameraChange;
             m_Camera.maxPosition += cameraChange;
             other.transform.position += playerChange;
             if(needText)
             {
                StartCoroutine(PlaceNameCo());
             }
        }
    }

    private IEnumerator PlaceNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4);
        text.SetActive(false);
    }
}
