using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScreenShake : MonoBehaviour
{

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = new Vector3(0, 0, -1);
        float elapsedTime = 0f;

        while(elapsedTime < duration) {
            float xOffset = Random.Range(0.15f, -0.15f) * magnitude;
            float yOffset = Random.Range(0.15f, -0.15f) * magnitude;
            transform.position = new Vector3(xOffset, yOffset, -1);
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        transform.position = originalPosition;

    }

}
