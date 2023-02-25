using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    private float _fadeSpeed = 3f;
    private float _alpha;

    void Awake()
    {
        _alpha = GetComponent<Text>().color.a;
    }

    public IEnumerator FadeOut()
    {
        while(GetComponent<Text>().color.a > 0)
        {
            Color colour = GetComponent<Text>().color;
            float fadeAmount = colour.a - (_fadeSpeed * Time.deltaTime);

            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
            GetComponent<Text>().color = colour;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        while (GetComponent<Text>().color.a < _alpha)
        {
            Color colour = GetComponent<Text>().color;
            float fadeAmount = colour.a + (_fadeSpeed * Time.deltaTime);
            
            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
            GetComponent<Text>().color = colour;
            yield return null;
        }
    }

    public void ChangeToFaded()
    {
        Color colour = GetComponent<Text>().color;
        colour = new Color(colour.r, colour.g, colour.b, 0);
        GetComponent<Text>().color = colour;
    }
}
