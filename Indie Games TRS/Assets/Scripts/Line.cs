using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

public class Line : MonoBehaviour
{
    [SerializeField] private float animationDuration = 3f;

    public UnityEngine.Rendering.Universal.Light2D light2D;
    private LineRenderer lineRenderer;
    private Vector3[] linePoints;
    private int pointsCount;
    

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

        pointsCount = lineRenderer.positionCount;
        linePoints = new Vector3[pointsCount];
        lineRenderer.GetPositions(linePoints);

        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        light2D.intensity = 1;
    }

    public IEnumerator LineAnimation()
    {
        lineRenderer.enabled = true;

        float segmentDuration = animationDuration / (pointsCount - 1);

        for (int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;

            Vector3 startPosition = linePoints[i];
            Vector3 endPosition = linePoints[i + 1];

            while (Time.time - startTime < segmentDuration)
            {
                float t = (Time.time - startTime) / segmentDuration;
                Vector3 pos = Vector3.Lerp(startPosition, endPosition, t);

                for (int j = i + 1; j < pointsCount; j++)
                    lineRenderer.SetPosition(j, pos);

                yield return null;
            }

            lineRenderer.SetPosition(i + 1, endPosition);
        }
        
        StartCoroutine(EnableLight());
    }

    private IEnumerator EnableLight()
    {
        while (light2D.intensity < 2)
        {
            light2D.intensity += 1f * Time.deltaTime;
            yield return null;
        }
    }
}
