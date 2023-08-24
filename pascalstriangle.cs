/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;
using System.Collections.Generic;

public static class PascalTriangle
{
  public static List<int> PascalsTriangle(int n)
  {
    List<int> triangle = new List<int>();
    triangle.Add (1);
    int prev = 0; // index of previous row in List
    for (int row = 1; row < n; row++) 
    {
      triangle.Add(1);
      for (int col = 1; col < row; col++) 
      {
        triangle.Add( triangle[prev++] + triangle[prev] );
      }
      prev++;
      triangle.Add(1);
    }
    return triangle;
  }
} 