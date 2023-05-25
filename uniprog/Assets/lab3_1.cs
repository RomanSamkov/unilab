using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class lab3_1 : MonoBehaviour
{
    //public List<int> li = new List<int> { 3, 2, 1, 4, 5, 6, 7, 8, 9 };
    //1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100
    // 1 2 3 4 5 6 7 8 9
    //___________________
    //________ 5 ________
    //_____3_______7_____
    //___2___4___6___8___
    //_1_______________9_
    //___________________

    public TextMeshProUGUI tmp1;
    public TextMeshProUGUI tmp2;
    public TMP_InputField InputField;
    public TMP_InputField SearchField;

    // Start is called before the first frame update
    void Start()
    {
        //List<int> l = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
        //Debug.Log(ListToString(l));
        //List<int> bl = new List<int>(l);
        //List<int> sl = new List<int>(l);
        //
        //l.Sort();
        //
        //int mid_i = (int)(l.Count / 2);
        //
        //bl.RemoveRange(0, mid_i+1);
        //
        //Debug.Log("Right wing: " + ListToString(bl));
        //
        //sl.Reverse();
        //
        //if (l.Count % 2 != 0)
        //{
        //    sl.RemoveRange(0, mid_i+1);
        //}
        //else
        //{
        //    sl.RemoveRange(0, mid_i);
        //}
        //
        //sl.Reverse();
        //
        //Debug.Log("Left wing: "+ListToString(sl));

        //BTree b = GenerateBTree(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 });
        //BTree b = GenerateBTree(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
        //Debug.Log(TreeToString(b, 0));



        tmp1.text = TreeToString(
            GenerateBalancedBTree(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }),
            0);


    }

    BTree CurrentTree = new BTree();


    public void TryGenerateTree()
    {
        string s = InputField.text;

        if (CheckNumString(s))
        {
            List<int> l = s?.Split(' ')?.Select(Int32.Parse)?.ToList();

            BTree bt = GenerateBTree(l);
            CurrentTree = bt;

            if (l.Count > 31)
            {
                tmp1.fontSize = 40 - l.Count/5;
            }

            tmp1.text = TreeToString(bt, 0);
            tmp2.text = "Конвертация дерева обратно в массив: \n" + ListToString(BTreeToIntList(bt), false);

        }
        else
        {
            tmp1.text = "Ожидание ввода...";
        }

        Searching = false;
    }

    bool CheckNumString(string str)
    {
        if (str.Length < 1)
        {
            return false;
        }

        if(str.Contains("- ") || str.Contains("  ") || (str[str.Length - 1] == '-') || (str[str.Length - 1] == ' ') || 
            str.Contains("0-") || str.Contains("1-") || str.Contains("2-") || str.Contains("3-") || str.Contains("4-") ||
            str.Contains("5-") || str.Contains("6-") || str.Contains("7-") || str.Contains("8-") || str.Contains("9-") ||
            str=="" || str=="-" || str==" " || str==null
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
    
    public void BalanceTree()
    {
        BTree bt = GenerateBalancedBTree(BTreeToIntList(CurrentTree));
        tmp1.text = TreeToString(bt, 0);

    }

    public BTree GenerateBalancedBTree(List<int> inputList)
    {
        //Debug.Log($"input = {ListToString(inputList, false)}");

        List<int> l = new List<int>(inputList);

        BTree bt = new BTree();

        if (l.Count > 3)
        {
            int mid_i = (int)(l.Count / 2);
            bt.Value = l[mid_i];

            List<int> bl = new List<int>(l);
            List<int> sl = new List<int>(l);

            bl.RemoveRange(0, mid_i + 1);

            sl.Reverse();
            if (l.Count % 2 != 0)
            {
                sl.RemoveRange(0, mid_i + 1);
            }
            else
            {
                sl.RemoveRange(0, mid_i);
            }
            sl.Reverse();

            bt.GreaterTree = GenerateBalancedBTree(bl);
            bt.LesserTree = GenerateBalancedBTree(sl);

            return bt;
        }
        else if (l.Count == 3)
        {
            bt.Value = l[1];
            bt.GreaterTree = new BTree(l[2]);
            bt.LesserTree = new BTree(l[0]);
            return bt;
        }
        else if (l.Count == 2)
        {
            bt.Value = l[1];
            bt.LesserTree = new BTree(l[0]);
            return bt;
        }
        else if (l.Count == 1)
        {
            bt.Value = l[0];

            return bt;
        }
        else
        {
            return bt;
        }        
    }

    public BTree GenerateBTree(List<int> inputList)
    {
        List<int> l = new List<int>(inputList);
        BTree bt = new BTree(inputList[0]);

        for(int i = 1; i < l.Count; i++)
        {
            bt.AddNumberToTree(l[i]);
        }

        return bt;
    }

    List<int> BTreeToIntList(BTree bt)
    {
        List<int> l = new List<int>();

        if (!(bt.GreaterTree != null) && !(bt.LesserTree != null))
        {
            l.Add(bt.Value);
        }
        else
        {
            if (bt.GreaterTree != null && bt.LesserTree != null)
            {
                //l.Add(bt.LesserTree.Value);
                l.AddRange(BTreeToIntList(bt.LesserTree));
                l.Add(bt.Value);
                l.AddRange(BTreeToIntList(bt.GreaterTree));
            }
            else if (!(bt.GreaterTree != null) && (bt.LesserTree != null))
            {
                l.AddRange(BTreeToIntList(bt.LesserTree));
                l.Add(bt.Value);
            }
            else if ((bt.GreaterTree != null) && !(bt.LesserTree != null))
            {
                l.Add(bt.Value);
                l.AddRange(BTreeToIntList(bt.GreaterTree));
            }
        }

        return l;
    }

    string ListToString(List<int> l, bool index)
    {
        string s = "";

        if(index)
            for(int i =0; i<l.Count; i++) 
            {
                s += $"[{i}]={l[i]}|";
            }
        else
        {
            for (int i = 0; i < l.Count; i++)
            {
                s += $"{l[i]} ";
            }
        }

        return s;
    }

    string TreeToString(BTree bt, int lvl)
    {
        string s = "";

        //string deb = $"bt.Value={bt.Value};";

        if (!(bt.GreaterTree!=null) && !(bt.LesserTree != null))
        {
            s = $"{Tabs(lvl)}{ColorTag(lvl)}{bt.Value}</color>";
        }
        else
        {
            if (bt.GreaterTree != null && bt.LesserTree != null)
            {
                //deb += $"bt.GreaterTree.Value={bt.GreaterTree.Value}; bt.LesserTree.Value={bt.LesserTree.Value}";
                s = $"{TreeToString(bt.LesserTree, lvl + 1)}</color>\n" +
                    $"{Tabs(lvl)}{ColorTag(lvl)}{bt.Value}</color>\n" +
                    $"{TreeToString(bt.GreaterTree, lvl+1)}</color>";
            }
            else if(!(bt.GreaterTree != null) && (bt.LesserTree != null))
            {
                s = $"{TreeToString(bt.LesserTree, lvl + 1)}</color>\n" +
                    $"{Tabs(lvl)}{ColorTag(lvl)}{bt.Value}</color>";
            }
            else if((bt.GreaterTree != null) && !(bt.LesserTree != null))
            {
                s = $"{Tabs(lvl)}{ColorTag(lvl)}{bt.Value}</color>\n" +
                    $"{TreeToString(bt.GreaterTree, lvl + 1)}</color>";
            }
        }

        //Debug.Log(deb);

        return s;
    }

    int S = 0;
    bool Searching = false;

    public void SearchForNum()
    {
        string s = SearchField.text;

        if (CheckNumString(s))
        {
            List<int> l = s?.Split(' ')?.Select(Int32.Parse)?.ToList();

            S = l[0];

            if(CheckIfContains(CurrentTree, S))
            {

                Searching = true;
                tmp1.text = SearchNum(GenerateBalancedBTree(BTreeToIntList(CurrentTree)), 0);
            }
            else
            {
                tmp1.text = SearchNum(GenerateBalancedBTree(BTreeToIntList(CurrentTree)), 0);
            }
        }
    }


    bool CheckIfContains(BTree bt, int num)
    {
        if (num == bt.Value)
        {
            return true;
        }
        else if (num > bt.Value && bt.GreaterTree != null)
        {
            return CheckIfContains(bt.GreaterTree, num);
        }
        else if (num < bt.Value && bt.LesserTree != null)
        {
            return CheckIfContains(bt.LesserTree, num);
        }

        return false;
    }

    string SearchNum(BTree bt, int lvl)
    {
        string s = "";

        //string deb = $"bt.Value={bt.Value};";

        if (!(bt.GreaterTree != null) && !(bt.LesserTree != null))
        {
            s = $"{Tabs(lvl)}{ColorTag(lvl)}{SearchColorTag(bt.Value)}{bt.Value}{SearchEndTag(bt.Value)}</color>";
        }
        else
        {
            if (bt.GreaterTree != null && bt.LesserTree != null)
            {
                //deb += $"bt.GreaterTree.Value={bt.GreaterTree.Value}; bt.LesserTree.Value={bt.LesserTree.Value}";
                s = $"{SearchNum(bt.LesserTree, lvl + 1)}</color>\n" +
                    $"{Tabs(lvl)}{ColorTag(lvl)}{SearchColorTag(bt.Value)}{bt.Value}{SearchEndTag(bt.Value)}</color>\n" +
                    $"{SearchNum(bt.GreaterTree, lvl + 1)}</color>";
            }
            else if (!(bt.GreaterTree != null) && (bt.LesserTree != null))
            {
                s = $"{SearchNum(bt.LesserTree, lvl + 1)}</color>\n" +
                    $"{Tabs(lvl)}{ColorTag(lvl)}{SearchColorTag(bt.Value)}{bt.Value}{SearchEndTag(bt.Value)}</color>";
            }
            else if ((bt.GreaterTree != null) && !(bt.LesserTree != null))
            {
                s = $"{Tabs(lvl)}{ColorTag(lvl)}{SearchColorTag(bt.Value)}{bt.Value}{SearchEndTag(bt.Value)}</color>\n" +
                    $"{SearchNum(bt.GreaterTree, lvl + 1)}</color>";
            }
        }

        return s;
    }

    string Tabs(int n)
    {
        return new string(' ', n*4);
    }

    string ColorTag(int i)
    {
        if (i == 0)
        {
            return "<color=#E32022>";
        }
        else if(i == 1)
        {
            return "<color=#FFFF00>";
        }
        else if (i == 2)
        {
            return "<color=#A0B21E>";
        }
        else if (i == 3)
        {
            return "<color=#4FAD3C> ";
        }
        else if (i == 4)
        {
            return "<color=#0EB1A6> ";
        }

        return "";
    }

    string SearchColorTag(int i)
    {
        //Debug.Log($"input:{i} ; Searching:{S}");
        if (i == S)
        {
            return "<color=#9F2B68><size=150%>";
        }

        return "";
    }

    string SearchEndTag(int i)
    {
        if (i == S)
        {
            return "</color><size=100%>";
        }

        return "";
    }
}



public class BTree
{
    public int Value;

    public BTree GreaterTree;
    public BTree LesserTree;

    public BTree()
    {
        
    }

    public BTree(int num)
    {
        Value = num;
    }



    public BTree(int value, BTree Greater, BTree Lesser)
    {
        Value = value;

        GreaterTree = Greater;
        LesserTree = Lesser;
    }

    public void AddNumberToTree(int num)
    {
        if (num > Value)
        {
            if (GreaterTree != null)
            {
                GreaterTree.AddNumberToTree(num);
            }
            else
            {
                GreaterTree = new BTree(num);
            }
        }
        else
        {
            if (LesserTree != null)
            {
                LesserTree.AddNumberToTree(num);
            }
            else
            {
                LesserTree = new BTree(num);
            }
        }
    }


}
