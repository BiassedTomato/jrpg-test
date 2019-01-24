using UnityEngine;
using System.Collections;
using UnityEditor;

public class CameraShaker : MonoBehaviour
{
    public void Shake()
    {
        StartCoroutine(ScreenShake(1f, 0.2f));
    }

   public IEnumerator ScreenShake(float delay, float magnitude)
    {
        Vector3 defaultPosition = transform.position;

        for(int i=0; i<5; i++)
        {
            transform.position = new Vector3(defaultPosition.x + Random.Range(-1f,1f)*magnitude, defaultPosition.y + Random.Range(-1f, 1f) * magnitude,-10);
            yield return new WaitForSeconds(0.03f);
        }

        transform.position = defaultPosition;

    }
}