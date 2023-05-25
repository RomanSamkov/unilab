using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class lab4 : MonoBehaviour
{
    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    public TMP_InputField InputField;

    // Start is called before the first frame update
    void Start()
    {
        List<int> li = new List<int> { 1, 2, 3, 1, 6, 8, 91, 3 };
        //Debug.Log(((int)Mathf.Pow(2, li.Count)) - 1);

        CalculateMinK(li);

        //CalculateK(new int[] {1, 2, 3, 4, 3, 5, 1, 2, 3, 4, 7, 3 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Classic example
    //1 2 3 4 5 _3_ 6 7
    //Alt 
    //1 2 3 4 5 _8_ 6 7

    public void Input()
    {
        string s = InputField.text;

        if (CheckNumString(s))
        {
            List<int> l = s?.Split(' ')?.Select(Int32.Parse)?.ToList();

            CalculateMinK(l);
        }
        else
        {
            tmp1.text = $"Ожидание ввода...";
            tmp2.text = $"Ожидание ввода...";
        }
    }

    bool CheckNumString(string str)
    {
        if (str.Length < 2)
        {
            return false;
        }

        if (str.Contains("- ") || str.Contains("  ") || (str[str.Length - 1] == '-') || (str[str.Length - 1] == ' ') ||
            str.Contains("0-") || str.Contains("1-") || str.Contains("2-") || str.Contains("3-") || str.Contains("4-") ||
            str.Contains("5-") || str.Contains("6-") || str.Contains("7-") || str.Contains("8-") || str.Contains("9-") ||
            str == "" || str == "-" || str == " " || str == null
            )
        {
            return false;
        }

        foreach (char c in str)
        {
            if ((c < '0' || c > '9') && (c != ' ') && (c != '-'))
                return false;
        }

        return true;
    }

    void CalculateMinK(List<int> l)
    {
        if (CheckList(l))
        {
            //Debug.Log($"This list is already non-decreasing");
            tmp2.text = $"Массив неубывающий";
        }
        else
        {
            //Debug.Log($"l.Count={l.Count}");
            int BestResult = l.Count-1;

            int Variations = ((int)Mathf.Pow(2, l.Count))-1;
            int length = System.Convert.ToString(Variations, 2).Length;

            //Debug.Log($"Variations={Variations}");

            for(int i = 1; i< Variations; i++)
            {
                List<bool> lb = ConvertToBoolArray(i, length);

                if (CountFalse(lb) < BestResult)
                {
                    var res = CheckSolution(l, lb);

                    if (res.Item1)
                    {

                        int k = CountFalse(lb);

                        //Debug.Log($"variation_{i} is successful! K={k}");

                        if (k < BestResult)
                        {
                            BestResult = k;
                            tmp1.text = $"Минимальное K = {k}";
                            tmp2.text = $"Полученный массив: {IntListToString(res.Item2)}";
                        }
                    }
                }
            }

            //Debug.Log($"Minimal K={BestResult}");
        }

    }

    (bool, List<int>) CheckSolution(List<int> l, List<bool> b)
    {
        List<string> ls = IntListToStringList(l);

        for (int i = 0; i<l.Count; i++)
        {
            if (!b[i])
            {
                ls[i] = "r";
            }
        }

        //Debug.Log(StringListToString(ls));

        for (int i = 0; i < ls.Count; i++)
        {
            //Debug.Log($"i={i}; ls[i]={ls[i]}");

            while (ls[i]=="r")
            {
                if (i == ls.Count - 1)
                {
                    ls.RemoveAt(i);
                    break;
                }
                else
                {
                    ls.RemoveAt(i);
                }
                //Debug.Log($"WHILE i={i}; ls[i]={ls[i]}");
            }
        }

        //Debug.Log(StringListToString(ls));

        List<int> nli = StringListToIntList(ls);

        return (CheckList(nli), nli);
    }

    List<string> IntListToStringList(List<int> l)
    {
        List<string> ls = new List<string>();

        foreach(int i in l)
        {
            ls.Add($"{i}");
        }

        return ls;
    }

    List<int> StringListToIntList(List<string> l)
    {
        List<int> li = new List<int>();

        foreach (string s in l)
        {
            li.Add(System.Int32.Parse(s));
        }

        return li;
    }

    List<bool> ConvertToBoolArray(int num, int length)
    {
        List<bool> b = new List<bool>();

        string binary = System.Convert.ToString(num, 2);
        

        if (binary.Length < length)
        {
            binary = new string('0', length - binary.Length) + binary;
        }

        //Debug.Log($"{num}->{binary}");

        foreach (char c in binary)
        {
            if (c == '1')
            {
                b.Add(true);
            }
            else
            {
                b.Add(false);
            }
        }

        

        return b;
    }

    bool CheckList(List<int> l)
    {
        for (int i = 1; i < l.Count; i++)
        {
            if (l[i] < l[i - 1])
            {
                return false;
            }
        }

        return true;
    }

    bool CheckArray(int[] ar)
    {
        for (int i = 1; i < ar.Length; i++)
        {
            if (ar[i] < ar[i - 1])
            {
                return false;
            }
        }

        return true;
    }

    int CountFalse(List<bool> lb)
    {
        int k = 0;
        foreach(bool b in lb)
        {
            if (!b) k++;
        }
        return k;
    }

    string IntListToString(List<int> l)
    {
        string s = "";

        for (int i = 0; i < l.Count; i++)
        {
            s += $"{l[i]} ";
        }

        return s;
    }

    string ArToString(int[] ar)
    {
        string s = "";

        for (int i = 0; i < ar.Length; i++)
        {
            s += $"{ar[i]} ";
        }

        return s;
    }

    string StringListToString(List<string> ls)
    {
        string s = "";

        foreach(string s1 in ls)
        {
            s += s1;
        }

        return s;
    }
}
