using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    public Canvas mainCanvas;
    public float menuOffset = 0.5f; // Khoảng cách Pause Menu cách camera

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        HideAndLockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Play();
            }
            else
            {
                Stop();
                pauseMenu.SetActive(true);
                ShowAndUnlockCursor();
            }
        }
    }

    void Stop()
    {
        Time.timeScale = 0f;
        isPaused = true;
        ShowAndUnlockCursor();
    }

    public void Play()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        HideAndLockCursor();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void LateUpdate()
    {
        // Lấy vị trí và hướng quay của camera
        Vector3 cameraPosition = Camera.main.transform.position;
        Quaternion cameraRotation = Camera.main.transform.rotation;

        // Tính toán vị trí mới của Pause Menu
        Vector3 menuPosition = cameraPosition + Camera.main.transform.forward * menuOffset;

        // Cập nhật vị trí và hướng quay của Main Canvas theo camera
        mainCanvas.transform.SetPositionAndRotation(menuPosition, cameraRotation);
    }

    void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void ShowAndUnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
