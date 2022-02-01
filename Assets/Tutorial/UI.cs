using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public int count = 4;



    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                count--;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                count--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                count--;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                count--;
            }

            if (count == 0)
            {
                popUpIndex++;
            }

        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;

                GameObject.Find("Player").GetComponent<PlayerHp>().LE_DamageAction(1);
            }
        }
        else if (popUpIndex == 2)
        {
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetHealPack)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetItem1)
            {
                popUpIndex++;
            }
        }
    }
}
