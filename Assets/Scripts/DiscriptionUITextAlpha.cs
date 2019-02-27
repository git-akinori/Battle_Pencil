﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscriptionUITextAlpha :MonoBehaviour
{
    Text Mytext;
    float alpha;
    float red, green, blue;    //RGBを操作するための変数

	static bool isCanvasChanged = false;
   
    // Use this for initialization
    void Start ()
    {
        Mytext = GetComponent<Text>();
        alpha = GetComponent<Text>().color.a; 
    }
   

    public void Appear()
    {
		if(!isCanvasChanged) {
			BlackBoardManager.Instance.ChangeCanvas(BlackBoardManager.ECanvasType.Select);
		}

        Mytext.color =new Color(1,1,1,1);
        
        Debug.Log("ok");
    }
    public void Disappear()
    {
        Mytext.color = new Color(1, 1, 1, 0);
        
        Debug.Log("banish");
    }
    
}
