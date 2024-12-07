using UnityEngine;
using Invector.vCharacterController;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public Transform[] checkpointTransforms;
    private vThirdPersonController controller;
    private int currentCheckpointIndex = 0;

    void Start()
    {
        controller = GetComponent<vThirdPersonController>();
        if (checkpointTransforms.Length == 0)
        {
            // Установите стартовую точку как чекпоинт, если они не заданы
            checkpointTransforms = new[] { transform };
        }
    }

    void Update()
    {
        if (controller.isGrounded || (!controller.isGrounded && Input.GetMouseButtonUp(0)))
        {
            controller.airSpeed = 5;
        }
        // Ускорение в воздухе при зажатой ЛКМ
        if (!controller.isGrounded && Input.GetMouseButtonDown(0))
        {
            controller.airSpeed = 10;
        }
        // Проверяем, достиг ли персонаж нового чекпоинта
        if (controller.isGrounded && IsOnCheckpoint())
        {
            SetCheckpoint(currentCheckpointIndex);

            // Если достигнут последний чекпоинт, переходим на следующую сцену
            if (currentCheckpointIndex == checkpointTransforms.Length - 1)
            {
                LoadNextScene();
            }
        }

        // Возрождаем персонажа на чекпоинте, если он упал
        if (!controller.isGrounded && transform.position.y < checkpointTransforms[currentCheckpointIndex].position.y - 5f)
        {
            Respawn();
        }
    }

    bool IsOnCheckpoint()
    {
        for (int i = 0; i < checkpointTransforms.Length; i++)
        {
            if (Vector3.Distance(transform.position, checkpointTransforms[i].position) < 5f && currentCheckpointIndex < i)
            {
                currentCheckpointIndex = i;

                return true;
            }
        }
        return false;
    }

    void SetCheckpoint(int index)
    {
        Debug.Log($"New checkpoint set at index {index}!");
    }

    void Respawn()
    {
        transform.position = checkpointTransforms[currentCheckpointIndex].position + new Vector3(0,1,0);
        transform.rotation = checkpointTransforms[currentCheckpointIndex].rotation;
        Debug.Log($"Respawned at checkpoint {currentCheckpointIndex}!");
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 10)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}