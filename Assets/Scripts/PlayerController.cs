using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class PlayerController : MonoBehaviour
{
    public float Speed = 7;

    private float screenHalfWidthInWorldUnits;

    private Health health;

    private ScoreController score;

    private List<Color32> _colours;

    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        score = GetComponent<ScoreController>();
        currentScene = SceneManager.GetActiveScene();

        // Obtener la peli actual y su respectiva paleta de colores
        GameObject imageGameObject = GameObject.FindWithTag("Film");

        Image image = imageGameObject.GetComponent<Image>();

        string imageName = image.sprite.name;

        _colours = GetActiveColors(imageName);

        //Dimensiones de la pantalla
        float halfPlayerWith = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWith;

        // Audio

        Scene scene = SceneManager.GetActiveScene();

        FindObjectOfType<AudioManager>().Stop("Theme_01");
        if(scene.name.Equals("Nivel1"))
        {
            FindObjectOfType<AudioManager>().Play("Theme_02");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * Speed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        //Colisión a la izquierda
        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }

        //Colisión a la derecha
        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (!triggerCollider.CompareTag("Falling Block")) return;

        // Verifica si el bloque con el que hace colisión pertenece a la paleta de colores
        GameObject fallingBlockGameObject = GameObject.FindGameObjectWithTag("Falling Block");

        FallingBlock fallingBlock = fallingBlockGameObject.GetComponent<FallingBlock>();

        Color32 color = fallingBlock.color;

        if (_colours.Contains(color))
        {
            // Verifica que el color no esté en el score
            if (score.scores.Select(i => 
                    i.GetComponent<MeshRenderer>().material.color)
                    .Any(_color => ((Color32) _color).Equals(color)))
            {
                return;
            }

            FindObjectOfType<AudioManager>().Play("ScoreAudio");

            score.score++;

            if (health.health < 5)
            {
                health.health++;
            }

            MeshRenderer rend = score.scores[score.score -1].GetComponent<MeshRenderer>();

            rend.material.color = new Color32(color.r, color.g, color.b, 255);

            Debug.Log(score.score);

            if (score.score == score.totalScore)
            {
                Loader.NextScene();
            }

            return;
        }


        FindObjectOfType<AudioManager>().Play("DamageAudio");

        health.health--;

        if (health.health != 0) return;
        health.hearts[0].sprite = health.emptyHeart;

        FindObjectOfType<AudioManager>().Stop("Theme_02");
        FindObjectOfType<AudioManager>().Play("Theme_01");

        Destroy(gameObject);

        Loader.Load(Loader.Scene.GameOver);
    }

    List<Color32> GetActiveColors(string imageName)
    {
        List<int> coloursCode;

        switch (imageName)
        {
            case "Interstellar":
                coloursCode = Colours.ColourPalette.GetRange(30,9);
                break;
            case "Blade_Runner_1982":
                coloursCode = Colours.ColourPalette.GetRange(20, 9);
                break;
            case "Call_Me_By_Your_Name":
                coloursCode = Colours.ColourPalette.GetRange(10, 9);
                break;
            case "Moonrise_Kingdom":
                coloursCode = Colours.ColourPalette.GetRange(0, 9);
                break;
            case "Suspiria_1977":
                coloursCode = Colours.ColourPalette.GetRange(40, 9);
                break;
            default: return null;
        }

        return coloursCode.Select(Color.FromArgb).
                        Select(colorArgb => new Color32(colorArgb.R, colorArgb.G, colorArgb.B, 255)).
                        ToList();
    }

}
