using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBoard_TextManager: MonoBehaviour {

    public Text airtap;
    public Text welcome;
    public GameObject orange;
    public GameObject coffee;

    //private Coroutine fadeIn_instance = null;
    //private Coroutine fadeOut_instance = null;

    // to count the number of taps
    private int count = 0;

    private void Start()
    {
        foreach (Text child in orange.transform.GetComponentsInChildren<Text>())
        {
            StartCoroutine(FadeOut(child));
        }

        foreach (Text child in coffee.transform.GetComponentsInChildren<Text>())
        {
            StartCoroutine(FadeOut(child));
        }

        StartCoroutine(FadeOut(welcome));
    }

    public void FadeText(string fromWho)
    {
        // if the animation is still in progress, ignore
        switch (fromWho)
        {
            case "fromPWC":
                StartCoroutine(FadeOut(airtap));
                StartCoroutine(FadeIn(welcome));
                break;

            case "fromGoBack":
                StartCoroutine(FadeOut(welcome));
                StartCoroutine(FadeIn(airtap));

                foreach (Text child in coffee.transform.GetComponentsInChildren<Text>())
                {
                    StartCoroutine(FadeOut(child));
                }

                foreach (Text child in orange.transform.GetComponentsInChildren<Text>())
                {
                    StartCoroutine(FadeOut(child));
                }

                break;

            case "fromOrange":
                if (gameObject.GetComponent<DataBoard_Fade>().positionFixed)
                {
                    foreach (Text child in orange.transform.GetComponentsInChildren<Text>())
                    {
                        StartCoroutine(FadeIn(child));
                    }

                    foreach (Text child in coffee.transform.GetComponentsInChildren<Text>())
                    {
                        StartCoroutine(FadeOut(child));
                    }

                    StartCoroutine(FadeOut(welcome));
                }
                break;

            case "fromCoffee":
                if (gameObject.GetComponent<DataBoard_Fade>().positionFixed)
                {
                    foreach (Text child in coffee.transform.GetComponentsInChildren<Text>())
                    {
                        StartCoroutine(FadeIn(child));
                    }

                    foreach (Text child in orange.transform.GetComponentsInChildren<Text>())
                    {
                        StartCoroutine(FadeOut(child));
                    }

                    StartCoroutine(FadeOut(welcome));
                }
                break;
        }
        count += 1;

    }

    IEnumerator FadeIn(Text text)
    {
        while(text.color.a < 1)
        {
            text.color += new Color(0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    IEnumerator FadeOut(Text text)
    {
        while (text.color.a > 0)
        {
            text.color -= new Color(0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
