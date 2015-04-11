using UnityEngine;
using System.Collections;
using System;

public class ScaleObject : MonoBehaviour {

    public float Delay;
    public Vector3 Target;
    public Vector3 TargetPosition;
    public bool DestroyAfterDelay;

    public Action OnScaleEnding;
    

    float startTime;
    Vector3 startScale;
    Vector3 startPosition;

    public void Scale (float delay, Vector3 target, bool destroyAfterDelay, Vector3 targetPositon, Action onScaleEnding)
    {
        startTime = Time.time;
        startScale = transform.localScale;
        startPosition = transform.position;

        OnScaleEnding = onScaleEnding;
        TargetPosition = targetPositon;
        Delay = delay;
        Target = target;
        DestroyAfterDelay = destroyAfterDelay;

        StartCoroutine("ScaleRoutine");
    }

    IEnumerator ScaleRoutine ()
    {
        float t = 0f;
        do
        {
            t = (Time.time - startTime) / Delay;
            Vector3 scale = Vector3.Lerp(startScale, Target, t);
            Vector3 position = Vector3.Lerp(startPosition, TargetPosition, t);
            transform.localScale = scale;
            transform.position = position;
            yield return new WaitForEndOfFrame();
        } while (t < 1f);

        OnScaleEnding();

        if(DestroyAfterDelay)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
