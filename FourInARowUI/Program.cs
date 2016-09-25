using System;
using System.Collections.Generic;
using System.Text;

namespace FourInARowUI
{
    class Program
    {
        const int n = 100;
        public static void Main()
        {
            List<int> arr = new List<int>();
            arr.Add(5);
            Console.Write("this is the result: " + arr + "\n");
            
           // WindowsFormsUI windowUI = new WindowsFormsUI();
           // windowUI.Start();


            //Random rnd = new Random();
            //int randomNum;
            //int[] Arr = new int[n];

            //for (int i = 0; i < n; i++) Arr[i] = i + 1;

            //for (int i = n; i > 0; i--)
            //{
            //    randomNum = rnd.Next(n - 1) % i;
            //    Console.WriteLine(Arr[randomNum]);

            //    int temp = Arr[i - 1];
            //    Arr[i - 1] = Arr[randomNum];
            //    Arr[randomNum] = temp;
            //}


        }
    }
}
