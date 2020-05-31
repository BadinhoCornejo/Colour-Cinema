using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();

        btn.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Loader.Load(Loader.Scene.Nivel1);
    }
}
