using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [SerializeField] Canvas pauseMenu;
    [SerializeField] Sprite[] images;

    private bool _isPaused;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _isPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.gameObject.SetActive(true);
            int rand = Random.Range(0, images.Length);
            pauseMenu.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = images[rand];
            Time.timeScale = 0;
            _isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            _isPaused = false;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        _isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
