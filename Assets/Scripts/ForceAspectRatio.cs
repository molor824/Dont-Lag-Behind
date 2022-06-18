using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    void Update()
    {
        var res = Screen.currentResolution;

        // height and width that are updated by aspect
        var height = res.height;
        // width is modified depending on height 
        var width = height / 9 * 16;

        if (width == res.width) return;

        // if actual width is bigger than the new width, it means screen is more taller than 16:9
        // so modify the height instead of width
        if (width > res.width)
        {
            width = res.width;
            height = width / 16 * 9;
        }

        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}
