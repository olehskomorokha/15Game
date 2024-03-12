using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Random_Manager : MonoBehaviour
{
    private List<int> random_number_list = new List<int>();

    public TextMeshProUGUI random_numbers_holder;

    public int total_numbers_in_list = 15;

    public void random_number_generator()
    {
        random_numbers_holder.text = "";
        for (int i = 0; i < total_numbers_in_list; i++)
        {
            int rand = Random.Range(1, 15);
            random_number_list.Add(rand);
        }
        foreach(var num in random_number_list)
        {
            random_numbers_holder.text += num.ToString() + ", ";
        }
    }

}
