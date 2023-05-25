using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lab2_1 : MonoBehaviour
{
    public List<string> MPoints = new List<string>
    {
        "point1",
        "point2",
        "point3",
        "point4",
        "point5"
    };

    public List<MARSH> Routes = new List<MARSH> 
    {
        new MARSH("point1", "point2", 0),
        new MARSH("point2", "point3", 1),
        new MARSH("point3", "point4", 2)
    };

    public TextMeshProUGUI tmp1;
    public TMP_InputField InputField;

    public void FindRoutes(string s)
    {
        s = InputField.text;
        string a = "";

        foreach (MARSH m in Routes)
        {
            if (m.Start == s || m.Finish == s)
            {
                a += $"\n{MarshToString(m)}";
            }
        }

        if (a != "")
        {
            tmp1.text = a;
            Debug.Log(a);
        }
        else
        {
            tmp1.text = "ничего не найдено";
        }
    }

    public string MarshToString(MARSH m)
    {
        string s;

        s = $"MARSH number {m.Number}: {m.Start} -> {m.Finish}";

        return s;
    }

}

public struct MARSH
{
    public string Start;
    public string Finish;
    public int Number;

    public MARSH(string St, string Fin, int Num)
    {
        Start = St;
        Finish = Fin;
        Number = Num;
    }
}
