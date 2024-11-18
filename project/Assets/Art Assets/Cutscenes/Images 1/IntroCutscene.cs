using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public Image panel1;
    public Image panel2;
    public Image panel3;
    public float revealTime = 2.0f;
    public float delayBetweenPanels = 1.0f;

    void Start()
    {
        StartCoroutine(RevealPanels());
    }

    IEnumerator RevealPanels()
    {
        yield return StartCoroutine(FadeInPanel(panel1));
        yield return new WaitForSeconds(delayBetweenPanels);
        yield return StartCoroutine(FadeInPanel(panel2));
        yield return new WaitForSeconds(delayBetweenPanels);
        yield return StartCoroutine(FadeInPanel(panel3));
    }

    IEnumerator FadeInPanel(Image panel)
    {
        panel.gameObject.SetActive(true);
        Color color = panel.color;
        color.a = 0;
        panel.color = color;

        float elapsedTime = 0;
        while (elapsedTime < revealTime)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / revealTime);
            panel.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        panel.color = color;
    }
}
