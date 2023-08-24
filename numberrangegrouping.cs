/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

public class RangeExtraction
{
    public static string Extract(int[] args)
    {
        if (args.Length == 0) 
          return string.Empty;
    
        string result = args[0].ToString();
        
        bool range = false;
        for(int i = 1 ; i < args.Length - 1 ; i++) {
          if( ( range || args[i]-1 == args[i-1]) && args[i]+1 == args[i+1] ) {
            if(!range)
              result += '-';
            range = true;
          } else { 
            if(!range) 
              result += ',';
            result += args[i]; 
            range = false;
         }
        }
        
        if(!range) 
          result += ',';     
        result += args[args.Length-1];
        
        return result;
    }
}