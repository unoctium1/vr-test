using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeMessage : Floatie
{
    protected override void Update()
    {
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Destroy();
        }
        base.Update();
    }

    public override void OnAboutToBeDestroyed()
    {
        StartCoroutine(LerpSize());
    }

    private IEnumerator LerpSize()
    {
        float totalTime = waitBeforeDestroy - 0.1f;
        if(totalTime > 0f)
        {
            Vector3 initScale = transform.localScale;
            float elapsedTime = 0f;
            while (elapsedTime < totalTime)
            {
                transform.localScale = Vector3.Lerp(initScale, Vector3.zero, elapsedTime / totalTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localScale = Vector3.zero;
        }
        yield return null;
    }
}
