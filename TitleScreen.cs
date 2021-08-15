using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public SpriteRenderer title;

    void Start()
    {
        float orthoSize = title.bounds.size.x * Screen.height / Screen.width * .5f;

        Camera.main.orthographicSize = orthoSize;
    }
}
