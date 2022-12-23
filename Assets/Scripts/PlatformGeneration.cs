using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    public static float platformDist = 7;
    int platformsGenerated;
    List<Platform> platforms = new List<Platform>();
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            GeneratePlatform();
        }
        GameEvents.instance.onPlatformGenerate += GeneratePlatform;
    }
    void GeneratePlatform()
    {
        if (platformsGenerated >4)
        StartCoroutine(DestroyPlatform());
        float yPos = -platformDist * platformsGenerated;
        Platform platform = Instantiate(platformPrefab, new Vector3(0, yPos, 0), Quaternion.identity).GetComponent<Platform>();
        platforms.Add(platform);
        platformsGenerated += 1;
    }
    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(.05f);
        platforms[0].FallApart();
        platforms.RemoveAt(0);
    }
}
