using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Do not change this class structure. This class allows us to have all parameters in same place in the project under AudioManager.

    [SerializeField]
    private Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    public void Play(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound with name: " + name + " was not found.");
        }
        else
        {
            if (position != Vector3.zero)
            {
                AudioSource.PlayClipAtPoint(s.source.clip, position, s.source.volume);
            }
            else
            {
                s.source.Play();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Play("startgame", Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
