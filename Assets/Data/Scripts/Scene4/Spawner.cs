using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Cube4 cube;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateCube();
        }
    }
    private void CreateCube()
    {
        // Инициализация куба
        Instantiate(cube);
    }
}
