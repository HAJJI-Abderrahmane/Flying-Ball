using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalebdScript : MonoBehaviour
{
    public GameObject backGroundImage;
    public Camera mainCam;
    void Start()
    {
        Scalebd();
    }

    private void Scalebd()
    {
        Vector2 devScrRes = new Vector2(Screen.width, Screen.height);
        float srcHeight = Screen.height;
        float srcWidth = Screen.width;
        float AspectRatio = srcWidth / srcHeight;

        mainCam.aspect = AspectRatio;

        float camHeight = 100.0f * mainCam.orthographicSize * 2.0f;
        float camWidth = camHeight * AspectRatio;

        SpriteRenderer backgroundimgsr = backGroundImage.GetComponent<SpriteRenderer>();
        float bgImH = backgroundimgsr.sprite.rect.height;
        float bgImW = backgroundimgsr.sprite.rect.width;

        float bgImg_scaleH = camHeight / bgImH;
        float bgImg_scaleW = camWidth / bgImW;

        backGroundImage.transform.localScale = new Vector3(bgImg_scaleW*0.002f, bgImg_scaleH*0.002f, 1);

    }


}
