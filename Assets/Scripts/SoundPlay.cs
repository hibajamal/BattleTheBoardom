using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public AudioSource p1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Awake()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("music");
        if (obj.Length>1)
        {
            Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
