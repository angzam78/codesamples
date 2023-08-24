/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;

public static class LargestIntegerExponent
{
  public static int GetExponent(int n, int p)
  {
    if (p<=1)   
      throw new ArgumentOutOfRangeException(); 
    
    // largest possible value of p^x = n, so largest x = logp n 
    int x = (int)Math.Log(Math.Abs(n),p);
    
    while(n % Math.Pow(p,x) != 0) x--;
    
    return x;
  }
}