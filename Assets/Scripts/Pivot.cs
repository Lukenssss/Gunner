using UnityEngine;

public class Pivot : MonoBehaviour
{
    private GameObject playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = playerTransform.transform.position + new Vector3(0f, 0f, 0f);

        if (Controller.isMoving == false)
        {
            transform.RotateAround(playerTransform.transform.position, new Vector3(0f,1f, 0f), 2f * Time.deltaTime);
        }
    }
}
