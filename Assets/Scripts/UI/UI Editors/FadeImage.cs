using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    private float _fadeSpeed = 3f;
    private float _alpha;

    void Awake()
    {
        _alpha = GetComponent<Image>().color.a;
    }

    public IEnumerator FadeOut()
    {
        while(GetComponent<Image>().color.a > 0)
        {
            Color colour = GetComponent<Image>().color;
            float fadeAmount = colour.a - (_fadeSpeed * Time.deltaTime);

            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
            GetComponent<Image>().color = colour;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        while (GetComponent<Image>().color.a < _alpha)
        {
            Color colour = GetComponent<Image>().color;
            float fadeAmount = colour.a + (_fadeSpeed * Time.deltaTime);
            
            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
            GetComponent<Image>().color = colour;
            yield return null;
        }
    }

    public void ChangeToFaded()
    {
        Color colour = GetComponent<Image>().color;       
        colour = new Color(colour.r, colour.g, colour.b, 0);
        GetComponent<Image>().color = colour;
    }
}
