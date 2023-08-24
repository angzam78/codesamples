/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System.Collections.Generic;

public class LargeFactorials
{
  public static string Factorial(int n)
  {
    string result = "";
    if (n >= 0)
    {
      int carry = 0;
      List<byte> factorial = new List<byte>();
      
      factorial.Add(1);
    
      for (int i = 2 ; i <= n ; i++)
      {
        for (int j = 0 ; j < factorial.Count ; j++)
        {
          int prod = factorial[j] * i + carry;
          factorial[j] = (byte)(prod % 10);
          carry = prod / 10;
        }
        while(carry > 0){
          factorial.Add((byte)(carry % 10));
          carry /= 10;      
        }
      }
    
      factorial.Reverse();
      result = string.Join("", factorial);
    }
    return result;
  }
}