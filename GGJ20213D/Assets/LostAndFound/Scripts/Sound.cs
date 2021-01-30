using Unity.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    // Do not change this class structure. This class allows us to have all parameters in same place in the project under AudioManager.
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0.1f, 3.0f)]
    public float pitch;

    public string name;

    [HideInInspector]
    public AudioSource source;

    public bool loop = false;

    [Range(0f, 1f)]
    public float spatialBlend = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
