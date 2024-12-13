using UnityEngine;
using Invector.vCharacterController;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public PanelManager _panelManager;

    public Transform[] cheackpointTransforms;
    //private vThirdPersonController controller;
    private FPSInputs fpsController;
    private int currentCheckpointIndex = 0;

    private float lastInputTime = 0.000f;
    public float idleTimeLimit = 60.0f; // 60 секунд

    private Transform platformsParent;

    public float updateFlightSpeed = 10f;


    private void Start()
    {
        _panelManager = GameObject.Find("Canvas").GetComponent<PanelManager>();
        _panelManager.HideCursor();

        
        fpsController = GetComponent<FPSInputs>();
        if (cheackpointTransforms.Length == 0)
        {
            // Установите стартовую точку как чекпоинт, если они не заданы
            cheackpointTransforms = new[] { transform };

            // Найти родителя "Platforms"
            platformsParent = GameObject.Find("Platforms").transform;

            // Инициализировать массив checkpointTransforms
            InitializeCheckpointTransforms();
        }

    }

    private void Update()
    {
        CheckPlayerIdle();

        /*if (fpsController.playerGrounded || (!fpsController.playerGrounded && Input.GetMouseButtonUp(0)))
        {
            controller.airSpeed = 5;
        }
        // Ускорение в воздухе при зажатой ЛКМ
        if (!fpsController.playerGrounded && Input.GetMouseButtonDown(0))
        {
            controller.airSpeed = updateFlightSpeed;
        }*/

        // Проверяем, достиг ли персонаж нового чекпоинта
        if (fpsController.playerGrounded && IsOnCheckpoint())
        {
            SetCheckpoint(currentCheckpointIndex);

            // Если достигнут последний чекпоинт, переходим на следующую сцену
            if (currentCheckpointIndex == cheackpointTransforms.Length - 1)
            {
                LoadNextScene();
            }
        }

        // Возрождаем персонажа на чекпоинте, если он упал
        if (!fpsController.playerGrounded && transform.position.y < cheackpointTransforms[currentCheckpointIndex].position.y - 40f)
        {
            Respawn();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!_panelManager._panel.activeSelf)
            {
                _panelManager.ShowCursor();
                _panelManager.ShowPanel();
            }
        }
    }

    private void InitializeCheckpointTransforms()
    {
        // Получить все дочерние объекты платформ
        Transform[] platformTransforms = platformsParent.GetComponentsInChildren<Transform>();

        // Найти все объекты чекпоинтов и добавить их в массив
        int checkpointCount = 0;
        foreach (Transform child in platformTransforms)
        {
            if (child.Find("Cheackpoint") != null)
            {
                checkpointCount++;
            }
        }
        cheackpointTransforms = new Transform[checkpointCount];
        checkpointCount = 0;
        foreach (Transform child in platformTransforms)
        {
            if (child.Find("Cheackpoint") != null)
            {
                cheackpointTransforms[checkpointCount++] = child.Find("Cheackpoint").transform;
            }
        }
    }

    private bool IsOnCheckpoint()
    {
        for (int i = 0; i < cheackpointTransforms.Length; i++)
        {
            if (Vector3.Distance(transform.position, cheackpointTransforms[i].position) < 3f && currentCheckpointIndex < i)
            {
                currentCheckpointIndex = i;

                return true;
            }
        }
        return false;
    }

    private void CheckPlayerIdle()
    {
        // Обновляем время последнего действия игрока
        if (Input.anyKey || _panelManager._panelInfo.activeSelf)
        {
            lastInputTime = 0f;
        } 
        else 
        {
            lastInputTime += Time.deltaTime;
            Debug.Log(lastInputTime);
        }

        // Если игрок бездействует больше 60 секунд, переходим на "MenuScene"
        if (lastInputTime >= idleTimeLimit)
        {
            Debug.Log($"{lastInputTime} , {idleTimeLimit}");
            _panelManager.InMenu();
        }
    }

    private void SetCheckpoint(int index)
    {
        Debug.Log($"New checkpoint set at index {index}!");
    }

    private void Respawn()
    {
        transform.position = cheackpointTransforms[currentCheckpointIndex].position + new Vector3(0, 2, 0);
        transform.rotation = cheackpointTransforms[currentCheckpointIndex].rotation;
        
        Debug.Log($"Respawned at checkpoint {currentCheckpointIndex}!");
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 3)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


}