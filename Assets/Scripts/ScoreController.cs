using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public int score;
    public int totalScore;

    public GameObject[] scores;

    public Color32 color;
    // Start is called before the first frame update
    void Start()
    {
        if (score != 0) return;
        foreach (GameObject sprite in scores)
        {

            MeshRenderer rend = sprite.GetComponent<MeshRenderer>();

            rend.material.color = new Color32(0, 0, 0, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
