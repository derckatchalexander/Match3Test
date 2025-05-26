using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform gridParent;
    public List<GameObject> elementPrefabs;
    public int gridWidth = 8, gridHeight = 8;
    public ActionBar actionBar;

    private List<ElementInstance> activeElements = new();

    void Awake() => Instance = this;

    void Start() => GenerateField();

    public void GenerateField()
    {
        ClearField();
        List<GameObject> pool = GenerateElementPool();
        StartCoroutine(SpawnElementsGradually(pool));
    }

    private IEnumerator SpawnElementsGradually(List<GameObject> pool)
    {
        int total = gridWidth * gridHeight;
        int batchSize = 1;

        for (int i = 0; i < total; i++)
        {
            GameObject prefab = pool[i];
            GameObject obj = Instantiate(prefab, new Vector3(Random.Range(-1f, 1f), 6f, 0), Quaternion.identity, gridParent);
            var instance = obj.GetComponent<ElementInstance>();
            activeElements.Add(instance);

            if ((i + 1) % batchSize == 0)
                yield return new WaitForSeconds(0.5f);
        }
    }

    List<GameObject> GenerateElementPool()
    {
        int count = gridWidth * gridHeight;
        int typesNeeded = count / 3;
        List<GameObject> chosen = new();

        for (int i = 0; i < typesNeeded; i++)
        {
            var prefab = elementPrefabs[Random.Range(0, elementPrefabs.Count)];
            chosen.Add(prefab); chosen.Add(prefab); chosen.Add(prefab);
        }

        Shuffle(chosen);
        return chosen;
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }

    void ClearField()
    {
        foreach (var e in activeElements)
            Destroy(e.gameObject);
        activeElements.Clear();
    }

    public void OnElementClicked(ElementInstance element)
    {
        activeElements.Remove(element);

        var rb = element.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Static;

        Destroy(element.GetComponent<Collider2D>());
        actionBar.AddToBar(element);
    }
    public void ReshuffleField()
{
    Shuffle(activeElements);
    int index = 0;
    for (int y = 0; y < gridHeight; y++)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if (index >= activeElements.Count)
                return;

            var element = activeElements[index];
            element.transform.SetParent(gridParent);
            element.transform.position = new Vector3(Random.Range(-1f, 1f), 6f, 0);
            index++;
        }
    }
}
}