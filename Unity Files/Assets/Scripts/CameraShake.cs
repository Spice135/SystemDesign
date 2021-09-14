using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    float shakeAmount;
    float shakeDuration;
    float shakePercentage;
    float startAmount;
    float startDuration;
    float smoothAmount = 5f;
    bool isRunning = false;

    public void ShakeThisCamera()
    {
        shakeAmount = 25f;
        shakeDuration = 1.5f;
        ShakeCamera();
    }

    void ShakeCamera()
    {
        startAmount = shakeAmount;
        startDuration = shakeDuration; 

        if (!isRunning)
            StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        isRunning = true;
        while (shakeDuration > 0.01f)
        {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeAmount;
            rotationAmount.z = 0;
            shakePercentage = shakeDuration / startDuration;
            shakeAmount = startAmount * shakePercentage;
            shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isRunning = false;
    }

}