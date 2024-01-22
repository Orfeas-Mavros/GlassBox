using UnityEngine;

public class Sizing : MonoBehaviour
{
    public float widthPercent;
    public float heightPercent;

    public float leftPercent;
    public float bottomPercent;

    private void Update()
    {
        float screenHeight = Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;

        Transform transformComponent = GetComponent<Transform>();

        float scaleWidth = screenWidth * (widthPercent / 100);
        float scaleHeight = screenHeight * (heightPercent / 100);

        transformComponent.localScale = new Vector3(2 * scaleWidth, 2 * scaleHeight, transformComponent.localScale.z);
        transformComponent.position = new Vector3(screenWidth * (leftPercent + widthPercent / 2 - 50) / 50, screenHeight * (bottomPercent + heightPercent / 2 - 50) / 50, transformComponent.position.z);
    }
}