/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;

public class RomanConvert
{
	public static string Solution(int number)
	{
		char[] symbol = {  'M', 'D', 'C', 'L', 'X','V','I' };
		int[]  weight = { 1000, 500, 100,  50,  10, 5 , 1  };
	
		string result = "";
    
		int index = 0, subtract = 0; 
		
		while (number > 0){
    
			while(number >= weight[index] - subtract){
				number -= weight[index] - subtract;
				result += symbol[index];
			}
      
			int subindex = (index&~1)+2;
      
			if(number > 0 && number >= weight[index] - weight[subindex]){
				subtract = weight[subindex];
				result += symbol[subindex];
			}else{
				subtract = 0;
				index++;
			}
		}
    
    return result; 
	}
}