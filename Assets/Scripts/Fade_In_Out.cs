using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fade_In_Out : BaseSingletonMono<Fade_In_Out> {

    bool alfaFrag;
    float a_color;
    float red, green, blue;    //RGBを操作するための変数
    [Header("透明化の速さ")]
    public float speed;  //透明化の速さ
    bool once = true;

    int phase=1;
    

    [Header("何秒暗転するか")]
    public float TimeCount = 15;
    // Use this for initialization
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;

        alfaFrag = false;
        a_color = 0;
    }

    public IEnumerator FadeIO(float fadeSpeed, float keepSeconds, System.Action corInKeep, System.Action corInEnd)
    {
		var cor = StartCoroutine(FadeOut(fadeSpeed));
		yield return cor;

		cor = StartCoroutine(FadeKeep(keepSeconds / 2));
		yield return cor;

		corInKeep();
		yield return new WaitForEndOfFrame();

		cor = StartCoroutine(FadeKeep(keepSeconds / 2));
		yield return cor;

		cor = StartCoroutine(FadeIn(fadeSpeed));
		yield return cor;

		corInEnd();
	}

    public void StartFade(float fadeSpeed, float keepSeconds, System.Action corInKeep, System.Action corInEnd)
    {
		StartCoroutine(FadeIO(fadeSpeed, keepSeconds, corInKeep, corInEnd));
    }

    

    public IEnumerator FadeOut(float _speed ) {
		a_color = 0;

		while (true)
        {
            GetComponent<Image>().color = new Color(red, green, blue, a_color);
            a_color += _speed * Time.deltaTime;
            //透明度が255になったら終了する。
            if (a_color >= 1)
            {
                a_color = 1;
                //Debug.Log("OUT");
                
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator FadeKeep(float keepTime)
    {
        while (true)
        {
            GetComponent<Image>().color = new Color(red, green, blue, 255);
            //Debug.Log("KEEP");
            keepTime -= 1 * Time.deltaTime;

            if (keepTime <= 0)
            {
                keepTime = 0;
                //phase=3;
                Debug.Log("KEEP END");
                break;
            }
            yield return null;
        }
        yield return null;
    }
    public IEnumerator FadeIn(float _speed)
    {
		a_color = 1;

        while (true)
        {
            GetComponent<Image>().color = new Color(red, green, blue, a_color);
            a_color -= _speed * Time.deltaTime;
            //透明度が255になったら終了する。
            if (a_color <= 0)
            {
                a_color = 0;
                //Debug.Log("IN");
                break;
            }
            yield return null;
        }
        yield return null;
    }
}
