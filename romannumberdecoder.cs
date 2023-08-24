/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;

public class RomanDecode
{
  public static int Solution(string roman)
  {
    char[] symbol = {  'M', 'D', 'C', 'L', 'X','V','I' };
    int[]  weight = { 1000, 500, 100,  50,  10, 5 , 1  };
    
    int res = 0, rom = 0, num = 0, sub = 0;
    
    while (rom < roman.Length){
      int pre = (num&~1)+2;
      
      if (roman[rom] == symbol[num]){
        res += weight[num] - sub;
        sub = 0;
        rom++;
      }else if (rom < roman.Length-1 && roman[rom+1] == symbol[num]){
        sub = weight[pre];
        rom++;
      }else{
        num++;
      }      
    }

    return res; 
  }
}