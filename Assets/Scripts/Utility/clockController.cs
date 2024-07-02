using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using UnityEngine.Rendering.Universal;
using UnityEditor;
using UnityEngine.Rendering;

public class clockController : MonoBehaviour
{
    [Header("Clock UI")]
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private TMP_Text clockText;
    private Image spriteRenderer;
    private float elapsedTime;
    private bool isTimed;
    private float funcTimer = 0;
    private float funcDuration = 2;
    private AudioManager audioManager;

    [Header("Player Controller")]
    [SerializeField] TopDownController playerController;

    [Header("Time in a Day")]
    [SerializeField] private float timeInADay = 86400f;

    [Header("Elapse Speed")]
    [SerializeField] private float timeScale = 0.5f;

    [Header("Alarm")]
    [SerializeField] private float playSoundAtTime;
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip soundClip;
    
    [Header("Additional UI")]
    [SerializeField] GameObject gravityClockPanal;
    [Header("Shader Info")]
    [SerializeField] private Material voronoi;

    private void Start()
    {
        spriteRenderer = GetComponent<Image>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime * timeScale;
        elapsedTime %= timeInADay;

        funcTimer -= Time.deltaTime;

        if(funcTimer < 0 && isTimed) 
        {
            ResetTimeFunc();
            isTimed = false;
        }

        UpdateClockUI();
        ActivateTimeFunc();
    }

    private void LateUpdate()
    {
        CheckTime();
    }

    void UpdateClockUI()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 3600f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 3600f) / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime - hours * 3600f - (minutes * 60f));

        string clockString = string.Format("{0:00}:{1:00}", hours, minutes);
        clockText.text = clockString;
    }

    void CheckTime() 
    {
        if(elapsedTime >= 21600f && elapsedTime < 43200f)
            spriteRenderer.sprite = sprites[1];
        else if(elapsedTime >= 43200f && elapsedTime < 64800f)
            spriteRenderer.sprite = sprites[2];
        else if(elapsedTime >= 64800f && elapsedTime < 86400)
            spriteRenderer.sprite = sprites[3];
        else
            spriteRenderer.sprite = sprites[0];
    }

    public void ActivateTimeFunc() 
    {
        if(Input.GetKey(KeyCode.Z))
        {
            gravityClockPanal.SetActive(true);
        }

        else
            gravityClockPanal.SetActive(false);
    }
    
    public void ResetTimeFunc() 
    {
        voronoi.SetFloat("_FullscreenIntensity", 0f);
        audioManager.StopSound("SlowTime");
        playerController.WalkSpeed = 1;
        playerController.FrameRate = 3;
        Time.timeScale = 1f;
    }

    public void SlowTimeFunc() 
    {
        voronoi.SetFloat("_FullscreenIntensity", 0.52f);
        audioManager.PlaySound("SlowTime");
        funcTimer = funcDuration;

        if(funcTimer > 0) {
            playerController.WalkSpeed = 4;
            playerController.FrameRate = 12;
            Time.timeScale = 0.25f;
            isTimed = true;
        }
    }

    public void SpeedTimeFunc() {}

    public void StopTimeFunc() {}
}
