using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lab1_2 : MonoBehaviour
{
    public int[,] F = new int[10, 10];
    public int[,] B = new int[10, 10];

    public int K;
    public int L;

    public TextMeshProUGUI txt1;
    public TextMeshProUGUI txt2;
    public TextMeshProUGUI numTMP;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
    }

    public void Restart()
    {
        F = GenerateArray(-10, 10);
        txt1.text = ArrayToString(F, "F");

        numTMP.text = $"ѕеремножение положительных чисел массива на четных строчках = {CalculateN2(F, true)}";
        txt2.text = $"ѕеремножение положительных чисел массива на нечетных строчках = {CalculateN2(F, false)}";
    }

    

    long CalculateN2(int[,] ar, bool even)
    {
        long ans = 1;

        List<int> nums = new List<int>();

        for (int i1 = 0; i1 < 5; i1++)
        {
            if((i1%2==0)==even)
            for (int i2 = 0; i2 < 6; i2++)
            {
                    if (ar[i1, i2] > 0)
                        nums.Add(ar[i1, i2]);
            }
        }

        foreach(int i in nums)
        {
            ans *= i;
        }

        return ans;
    }

    int[,] GenerateArray(int min, int max)
    {
        int[,] ans = new int[5, 6];

        for (int i1 = 0; i1 < 5; i1++)
        {
            for (int i2 = 0; i2 < 6; i2++)
            {
                ans[i1, i2] = Random.Range(min, max);
            }
        }

        return ans;
    }

    string ArrayToString(int[,] ar, string name)
    {
        string s = $"ћассив {name}:\n";

        for (int i1 = 0; i1 < 5; i1++)
        {

            //s += $"\n line{i1} : ";

            if (i1 % 2 == 0)
            {
                s += $"\n\n line{i1} : ";
            }
            else
            {
                s += $"\n\n <color=#666666>line{i1}</color> : ";
            }

            for (int i2 = 0; i2 < 6; i2++)
            {
                if (i1 % 2 == 0)
                {
                    if (ar[i1, i2] <= 0)
                    {
                        s += $"{ar[i1, i2]}; ";
                    }
                    else
                    {
                        s += $"<color=#00FF00>{ar[i1, i2]};</color> ";
                    }
                }
                else
                {
                    if (ar[i1, i2] <= 0)
                    {
                        s += $"{ar[i1, i2]}; ";
                    }
                    else
                    {
                        s += $"<color=#FFFF00>{ar[i1, i2]};</color> ";
                    }
                }
            }
        }

        return s;
    }
}
