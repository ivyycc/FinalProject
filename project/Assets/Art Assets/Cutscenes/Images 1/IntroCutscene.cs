using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class IntroCutscene: MonoBehaviour
{
    public Image[] images; 
    public Button nextButton; 
    public CanvasGroup fadeCanvasGroup;

    private int currentImageIndex = 0; 
    private float fadeDuration = 1.0f; 

  
    void Start()
    {
        AudioManager.instance.InitializeWind(FMODEvents.instance.CutScenesMusic, this.transform.position);
        foreach (var image in images)
        {
            image.gameObject.SetActive(false);
        }

       
        ShowImage(currentImageIndex);
        nextButton.onClick.AddListener(OnNextButtonPressed); 
    }


    private void ShowImage(int index)
    {
        if (index < 0 || index >= images.Length) return; 


        images[index].gameObject.SetActive(true);
        StartCoroutine(FadeImageIn(images[index]));
    }

  
    private IEnumerator FadeImageIn(Image image)
    {
        CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = image.gameObject.AddComponent<CanvasGroup>(); 
        }

        float elapsedTime = 0f;

        
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f; 
    }

  
    private void OnNextButtonPressed()
    {
        currentImageIndex++;

       
        if (currentImageIndex >= images.Length)
        {
            StartCoroutine(FadeOutToBlackAndLoadScene());
            nextButton.interactable = false; 
        }
        else
        {
          
            ShowImage(currentImageIndex);
        }
    }

   
    private IEnumerator FadeOutToBlackAndLoadScene()
    {
        
        foreach (var image in images)
        {
            CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = image.gameObject.AddComponent<CanvasGroup>();
            }

            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 0f; 
        }

       
        yield return FadeToBlack();

        AudioManager.instance.StopSound2();
        SceneManager.LoadScene("BeginningScene");
    }

   
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f; 
    }
}
