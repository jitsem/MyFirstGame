using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPositionStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void Awake() {
        if(fadeInPanel != null){
            var panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity);
            Destroy(panel, 1);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerPositionStorage.startValue = playerPosition;
            StartCoroutine(FadeCo());
        }   
    }

    public IEnumerator FadeCo()
    {
        if(fadeOutPanel != null){
            var panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);            
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOp.isDone)
        {
            yield return null;
        }
    }
}
