using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public CheckpointManager _player; 
    public GameObject _panel;


    public AudioSource _music;
    private bool musicFlag;

    private void Start()
    {

        _music = GetComponent<AudioSource>();
        musicFlag = true;
        if (SceneManager.GetActiveScene().name != "MenuScene")
        {
            _player = GameObject.Find("Player").GetComponent<CheckpointManager>();
            _panel = GameObject.Find("EscapePanel");
        }
        else
        {
            _panel = GameObject.Find("AuthorsPanel");
        }
        _panel.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Scene_1");
    }
    public void ExitPanel()
    {
        _panel.SetActive(false);
        if (SceneManager.GetActiveScene().name != "MenuScene")
        {
            HideCursor();
        }
    }
    public void ShowPanel()
    {
        _panel.SetActive(true);
    }
    public void InMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HideCursor()
    {
        // Выключаем курсор
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TurnOnMusic()
    {
        if (musicFlag)
        {
            _music.Pause();
            musicFlag = !musicFlag;

        }
        else
        {
            _music.Play();
            musicFlag = !musicFlag;
        }
    }
}
