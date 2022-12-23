using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] List<Transform> innerTiles = new List<Transform>();
    private void Start()
    {
        int numHoles = Random.Range(1, 5);
        for (int i = 0; i < numHoles; i++)
        {
            int rand = Random.Range(0, innerTiles.Count);
            Destroy(innerTiles[rand].gameObject);
            innerTiles.RemoveAt(rand);
        }
    }
    public void FallApart()
    {
        LeanTween.moveLocalY(gameObject, transform.position.y + 100, 1f).setEaseInSine();
        StartCoroutine(DestroyPlatform());
    }
    IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
