using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionBar : MonoBehaviour
{
    public Transform[] slots;
    private List<ElementInstance> barElements = new();

    public GameObject winScreen;
    public GameObject loseScreen;
    

    public void AddToBar(ElementInstance element)
    {
        if (barElements.Count >= 7)
        {
            loseScreen.SetActive(true);
            return;
        }

        barElements.Add(element);
        element.transform.SetParent(slots[barElements.Count - 1]);
        element.transform.localPosition = Vector3.zero;
        CheckMatches();

        if (GameManager.Instance.transform.childCount == 0)
            winScreen.SetActive(true);
    }

    void CheckMatches()
    {
        for (int i = 0; i < barElements.Count; i++)
        {
            var baseElement = barElements[i].data;
            var matchGroup = barElements.FindAll(e =>
                e.data.animal == baseElement.animal &&
                e.data.shape == baseElement.shape &&
                e.data.borderColor == baseElement.borderColor);

            if (matchGroup.Count >= 3)
            {
                foreach (var e in matchGroup)
                {
                    Destroy(e.gameObject);
                    barElements.Remove(e);
                }
                Rearrange();
                break;
            }
        }
    }

    void Rearrange()
    {
        for (int i = 0; i < barElements.Count; i++)
        {
            barElements[i].transform.SetParent(slots[i]);
            barElements[i].transform.localPosition = Vector3.zero;
        }
    }
}
