using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public float triggerRadius; // Радиус, в пределах которого музыка будет включаться/выключаться
    [SerializeField] private AudioSource audioSource;
    private Transform player;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        // Включаем музыку, если игрок достаточно близко
        if (distance <= triggerRadius && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        // Выключаем музыку, если игрок достаточно далеко
        else if (distance > triggerRadius && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
