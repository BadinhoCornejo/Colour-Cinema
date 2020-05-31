using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound : MonoBehaviour
{
    public string name;

    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume;
    
    [Range(.1f, 3)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
