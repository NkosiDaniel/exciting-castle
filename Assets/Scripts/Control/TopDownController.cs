using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public Rigidbody2D rg2d;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;
    [SerializeField] float walkSpeed;
    [SerializeField] float frameRate;
    [SerializeField] GameObject footSteps;
    private float idleTime;
    private Vector2 direction;
    
    public float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }
    public float FrameRate { get { return frameRate;} set { frameRate = value; } }

    private void Start() {
        
    }
    // Update is called once per frame
    void Update()
    {   
        //get direction of input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //set walk based on direction
        rg2d.velocity = direction * walkSpeed;

        HandleSpriteFlip();
        SetSprite();

        if(rg2d.velocity.x != 0 || rg2d.velocity.y != 0) 
        {
            footSteps.SetActive(true);
        }
        else
        {
            footSteps.SetActive(false);
        }
        
    }

    void SetSprite() 
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if(directionSprites != null) 
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        else 
        {
            idleTime = Time.time;
        }
    }

    void HandleSpriteFlip() 
    {
        if(!spriteRenderer.flipX && direction.x < 0) 
            spriteRenderer.flipX = true;

        else if(spriteRenderer.flipX && direction.x > 0)
            spriteRenderer.flipX = false;
    }

    List<Sprite> GetSpriteDirection() 
    {

        List<Sprite> selectedSprites = null;

        if(direction.y > 0) 
        {
            if(Mathf.Abs(direction.x) > 0)
                selectedSprites = neSprites;
            else
                selectedSprites = nSprites;
        }
        else if(direction.y < 0) 
        {
            if(Mathf.Abs(direction.x) > 0)
                selectedSprites = seSprites;
            else
                selectedSprites = sSprites;
        }
        else 
        {
            if(Mathf.Abs(direction.x) > 0)
                selectedSprites = eSprites;
        }
        return selectedSprites;
    }
}
