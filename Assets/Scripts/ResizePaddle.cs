using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePaddle : MonoBehaviour
{
    //this has nothing to do with voltorb flip 
    //i was just testing 9 sliced sprites and made pongs paddle


    float minSize = 5;
    float maxSize = 10;
    float resizeSpeed = 3;
    float positionY;
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.size = new Vector2(7.5f,sprite.size.y);
        positionY = -Camera.main.orthographicSize + sprite.size.y * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float sizeX = sprite.size.x;
        sizeX += Input.GetAxisRaw("Mouse ScrollWheel") * resizeSpeed;
        sizeX = Mathf.Clamp(sizeX, minSize, maxSize);
        sprite.size = new Vector2(sizeX,sprite.size.y);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, positionY);
    }
}
