using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionController : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    // Start is called before the first frame update
    public bool showCollision = true;
    void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapRenderer.enabled = showCollision;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
