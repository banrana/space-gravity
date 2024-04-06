using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOver;
    public Canvas mainCanvas;
    public float menuOffset = 0.5f;
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1.0f;
        gameOver.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        isGameOver = true;
        //Time.timeScale = 0f;
        gameOver.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        if (isGameOver)
        {
            // Lấy vị trí và hướng quay của camera
            Vector3 cameraPosition = Camera.main.transform.position;
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Tính toán vị trí mới của Game Over Menu
            Vector3 menuPosition = cameraPosition + Camera.main.transform.forward * menuOffset;

            // Cập nhật vị trí và hướng quay của Main Canvas theo camera
            mainCanvas.transform.SetPositionAndRotation(menuPosition, cameraRotation);
        }
    }
}
