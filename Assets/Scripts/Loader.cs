using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GenericSceneManagement = UnityEngine.SceneManagement;
public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour { }
    public enum Scene
    {
        Nivel1,
        Nivel2,
        Nivel3,
        Nivel4,
        Nivel5,
        MainMenu,
        Terminado,
        Loading,
        GameOver,
    }

    private static Action onLoaderCallback;

    private static AsyncOperation loadingAsyncOperation;

    private static Stack<Scene> loadedScenes = new Stack<Scene>();
    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void Restart()
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");

            Debug.Log(loadedScenes.Count);

            if (loadedScenes.Count > 0)
            {
                loadedScenes.Pop();
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(loadedScenes.Pop()));
            }
            else
            {
                loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(Scene.Nivel1));
            }
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadedScenes.Push(scene);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        return loadingAsyncOperation?.progress ?? 1f;
    }
    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    public static void NextScene()
    {
        GenericSceneManagement.Scene currentScene = SceneManager.GetActiveScene();

        switch (currentScene.name)
        {
            case "Nivel1":
                Load(Scene.Nivel2);
                break;
            case "Nivel2":
                Load(Scene.Nivel3);
                break;
            case "Nivel3":
                Load(Scene.Nivel4);
                break;
            case "Nivel4":
                Load(Scene.Nivel5);
                break;
            case "Nivel5":
                Load(Scene.Terminado);
                break;
        }
    }
}
