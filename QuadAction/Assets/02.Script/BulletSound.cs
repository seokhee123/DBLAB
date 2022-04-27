using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSound : MonoBehaviour
{
    AudioSource audio;
    private void Awake()
    {
        this.audio = GetComponent<AudioSource>();
    }
}
