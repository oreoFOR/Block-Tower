using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] GameObject platformPrefab;
    [SerializeField] GameObject finishPrefab;
    public static float platformDist = 7;
    int platformsGenerated;
    List<Platform> platforms = new List<Platform>();
    int randLevelLength;
    [SerializeField] Vector2Int levelLength;
    private void Start()
    {
        randLevelLength = Random.Range(levelLength.x,levelLength.y);
        for (int i = 0; i < 5; i++)
        {
            GeneratePlatform(platformPrefab);
        }
        GameEvents.instance.onPlatformGenerate += SelectPlatform;
    }
    void GeneratePlatform(GameObject prefab){
        print("passed");
        if (platformsGenerated >4)
        StartCoroutine(DestroyPlatform());
        float yPos = -platformDist * platformsGenerated;
        Platform platform = Instantiate(prefab, new Vector3(0, yPos, 0), Quaternion.identity).GetComponent<Platform>();
        platforms.Add(platform);
        platformsGenerated += 1;
    }
    void SelectPlatform()
    {
        if(platformsGenerated<randLevelLength){
            GeneratePlatform(platformPrefab);
        }
        else if(platformsGenerated == randLevelLength){
            GeneratePlatform(finishPrefab);
        }
        else{
            StartCoroutine(DestroyPlatform());
        }
    }
    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(.05f);
        platforms[0].FallApart();
        platforms.RemoveAt(0);
    }
}
