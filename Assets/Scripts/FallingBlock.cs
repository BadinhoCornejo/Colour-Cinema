using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FallingBlock : MonoBehaviour
{
    public float Speed = 7;

    public Vector2 spawnSizeMinMax;

    public Color32 color;

    private Vector2 screenHalfSizeWorldUnits;

    private AudioSource audioSource;
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        // Asignando color aleatoriamente
        List<int> colourPalette = Colours.ColourPalette;

        int colourIndex = Random.Range(0, colourPalette.Count);

        System.Drawing.Color _color = System.Drawing.Color.FromArgb(colourPalette[colourIndex]);

        MeshRenderer rend = GetComponent<MeshRenderer>();

        rend.material.color = new Color32(_color.R, _color.G, _color.B,255);

        color = rend.material.color;

        // Audio
        audioSource = GetComponent<AudioSource>();

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.Equals("MainMenu") || scene.name.Equals("GameOver") || scene.name.Equals("Terminado"))
        {
            audioSource.mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime, Space.World);

        if (!(transform.position.y < 0)) return;
        if (transform.position.y < -screenHalfSizeWorldUnits.y)
        {
            Destroy(gameObject);
        }
    }
}
