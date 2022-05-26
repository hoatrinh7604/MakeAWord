using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentController : MonoBehaviour
{
    [SerializeField] GameObject charPreFab;
    [SerializeField] int maxRow = 3;
    [SerializeField] int maxCol1, maxCol2, maxCol3;
    [SerializeField] Transform row1, row2, row3;

    private List<char> listChar;
    private List<int> listIndex;

    private void Start()
    {
        listChar = new List<char>();
        listIndex = new List<int>();
        SetAlphabet();
        SpawRandomChar();
    }

    public void SpawRandomChar()
    {
        // Row 1
        for(int i = 0; i < maxCol1; i++)
        {
            GameObject item = Instantiate(charPreFab, Vector3.zero, Quaternion.identity, row1);
            item.transform.localPosition = Vector3.zero;
            int index = GetRandomIndex();
            item.GetComponent<CharController>().UpdateText(listChar[index]);
            listChar[index] = '0';
        }

        // Row 2
        for (int i = 0; i < maxCol2; i++)
        {
            GameObject item = Instantiate(charPreFab, Vector3.zero, Quaternion.identity, row2);
            item.transform.localPosition = Vector3.zero;
            int index = GetRandomIndex();
            item.GetComponent<CharController>().UpdateText(listChar[index]);
            listChar[index] = '0';
        }

        // Row 3
        for (int i = 0; i < maxCol3; i++)
        {
            GameObject item = Instantiate(charPreFab, Vector3.zero, Quaternion.identity, row3);
            item.transform.localPosition = Vector3.zero;
            int index = GetRandomIndex();
            item.GetComponent<CharController>().UpdateText(listChar[index]);
            listChar[index] = '0';
        }
    }

    public int GetRandomIndex()
    {
        listIndex.Clear();

        for (int i = 0; i < listChar.Count; i++)
        {
            if (listChar[i] != '0')
            {
                listIndex.Add(i);
            }
        }

        int index = Random.Range(0, listIndex.Count);
        return listIndex[index];
    }

    public void SetAlphabet()
    {
        listChar.Clear();
        char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        for(int i =0; i< alpha.Length; i++)
        {
            listChar.Add(alpha[i]);
        }
    }
}
