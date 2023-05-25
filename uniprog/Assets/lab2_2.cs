using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lab2_2 : MonoBehaviour
{
    Dictionary<string, List<string>> dict =
        new Dictionary<string, List<string>>
        {
            { "Информатика", new List<string>{"Тьюринг", "Гёдель", "Линус"}},
            { "Математика",   new List<string>{ "Гаусс", "Архимед", "Вестяк"}},
            { "Физика",    new List<string>{"Эйнштейн", "Тесла", "Ньютон", "Максвелл", "Браун"}} 
        };

    public TextMeshProUGUI tmp1;
    public TMP_InputField InputField;

    public void FindAuthors()
    {
        string s = InputField.text;
        List<string> ls;
        if (dict.TryGetValue(s, out ls))
        {
            ls = dict[s];
            string a = "";

            for(int i = 0; i<ls.Count; i++)
            {
                if (i + 2 == ls.Count)
                {
                    a += ls[i] + " и ";
                }
                else if (i + 1 == ls.Count)
                {
                    a += ls[i] + ".";
                }
                else
                {
                    a += ls[i] + ", ";
                }
            }

            tmp1.text = a;
        }
        else
        {
            tmp1.text = "неизвестный предмет";
        }
    }

}
