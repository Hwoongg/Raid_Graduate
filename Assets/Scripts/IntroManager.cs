using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class IntroManager : MonoBehaviour
{
    [SerializeField] Image logo;
    [SerializeField] float fadeSpeed = 0.5f;

    bool isFadeIn;
    

    Color startColor;
    Color destColor;
    // Start is called before the first frame update
    void Start()
    {
        startColor = new Color(1, 1, 1, 0);
        destColor = new Color(1, 1, 1, 1);

        StartCoroutine(LogoAnimRoutine());
    }

    
    IEnumerator LogoAnimRoutine()
    {
        yield return StartCoroutine(LogoFade(startColor, destColor));

        yield return new WaitForSeconds(3.0f);

        yield return StartCoroutine(LogoFade(destColor, startColor));

        SceneManager.LoadScene("Title");
    }
    IEnumerator LogoFade(Color _start, Color _des)
    {
        float lerpVal = 0;
        while (true)
        {
            logo.color = Color.Lerp(_start, _des, lerpVal);
            lerpVal += Time.deltaTime * fadeSpeed;
            if (lerpVal > 1)
            {
                lerpVal = 1;
                break;
            }
            yield return null;
        }
        yield break;
    }
}
