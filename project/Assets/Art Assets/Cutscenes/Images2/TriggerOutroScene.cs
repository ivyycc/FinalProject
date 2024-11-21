using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerOutroScene : MonoBehaviour
{
    public GameObject dialoguePanel;
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;
    public string nextSceneName = "CutscenesScene2";
    public bool canLoadOutro = false;

    private bool isFading = false;

    void Start()
    {
        fadePanel.alpha = 0f;  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (canLoadOutro && !isFading)
            {
                StartCoroutine(FadeOutAndLoadScene());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         
            Time.timeScale = 0;
            Debug.Log("In Trigger");
            canLoadOutro = true;
            dialoguePanel.SetActive(true);
        }
    }


    private IEnumerator FadeOutAndLoadScene()
    {
        isFading = true;  
        dialoguePanel.SetActive(false);  

       
        yield return StartCoroutine(FadeToBlack());

      
        yield return new WaitForSecondsRealtime(1f);  


        Time.timeScale = 1;

   
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        
        SceneManager.LoadScene(nextSceneName);
    }

   
    private IEnumerator FadeToBlack()
    {
        float timeElapsed = 0f;

      
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime; 
            fadePanel.alpha = Mathf.Clamp01(timeElapsed / fadeDuration);  
            yield return null;
        }

        fadePanel.alpha = 1f;  
    }
}
