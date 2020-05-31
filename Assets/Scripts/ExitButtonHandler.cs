using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonHandler : MonoBehaviour
{
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = button.GetComponent<Button>();

        btn.onClick.AddListener(ExitGame);
    }

    public void ExitGame()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
