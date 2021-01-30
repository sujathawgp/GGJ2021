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

    private GameObject playingClipAtPointGameObject = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClipAtPoint(Vector3 position)
    {
        playingClipAtPointGameObject = new GameObject("OneShotAudio:" + name);
        playingClipAtPointGameObject.transform.position = position;
        AudioSource audio = playingClipAtPointGameObject.AddComponent<AudioSource>();
        audio.clip = source.clip;
        audio.Play();
        UnityEngine.Object.Destroy(playingClipAtPointGameObject, audio.clip.length);
    }

    public bool IsPlaying()
    {
        if (playingClipAtPointGameObject != null)
            return true;
        else if (source.isPlaying)
            return true;

        return false;

    }

    public void Stop()
    {
        if (playingClipAtPointGameObject != null)
        {
            playingClipAtPointGameObject.GetComponent<AudioSource>().Stop();
            UnityEngine.Object.Destroy(playingClipAtPointGameObject);
        }
        else
        {
            source.Stop();
        }
    }
}
