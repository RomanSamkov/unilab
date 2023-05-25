using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lab1 : MonoBehaviour
{
    public int[,] A = new int[10, 10];
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
        K = Random.Range(0, 10);

        //numTMP.text = $"switching column number {K}";
        numTMP.text = $"Меняем местами значения 1-столбца и {K}-стобца";

        A = GenerateArray(0, 10);
        txt1.text = ArrayToString(A, "A", false);
        txt2.text = ArrayToString(ColumnSwitcher(A, K), "A", true);
    }

    int[,] GenerateArray(int min, int max)
    {
        int[,] ans = new int[10, 10];

        for (int i1 = 0; i1 < 10; i1++)
        {
            for (int i2 = 0; i2 < 10; i2++)
            {
                ans[i1, i2] = Random.Range(min, max);
            }
        }

        return ans;
    }

    int[,] ColumnSwitcher(int[,] ar, int n)
    {
        int[,] ans = new int[10, 10];


        System.Array.Copy(ar, ans, 100);
        

        for (int i1 = 0; i1 < 10; i1++)
        {
            //Debug.Log($"line{i1}, switching ans[{i1}, 1]({ans[i1, 1]}) to ar[i1, {n}]({ar[i1, n]}) ");
            ans[i1, 1] = ar[i1, n];
            //Debug.Log($"line{i1}, switching ans[{i1}, {n}]({ans[i1, n]}) to ar[i1, 1]({ar[i1, 1]}) ");
            ans[i1, n] = ar[i1, 1];
        }

        return ans;
    }

    string ArrayToString(int[,] ar, string name, bool switched)
    {
        string s = $"Массив {name}:\n";

        for (int i1 = 0; i1 < 10; i1++)
        {
            s += $"\n line{i1} : ";
            for (int i2 = 0; i2 < 10; i2++)
            {
                

                if (i2 == 1)
                {
                    if (!switched)
                    {
                        s += $"<color=#E32022>{ar[i1, i2]};</color> ";
                    }
                    else
                    {
                        s += $"<color=#FFF138>{ar[i1, i2]};</color> ";
                    }
                }
                else if (i2 == K)
                {
                    if (!switched)
                    {
                        s += $"<color=#FFF138>{ar[i1, i2]};</color> ";
                    }
                    else
                    {
                        s += $"<color=#E32022>{ar[i1, i2]};</color> ";
                    }
                }
                else
                {
                    s += $"{ar[i1, i2]}; ";
                }
            }
        }

        return s;
    }


}
