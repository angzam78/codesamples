/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

public class HumanTimeFormat{
  public static string formatDuration(int seconds){
    
    if (seconds == 0) 
      return "now";
                         // compiler should precompute values below
    int    [] time = { 365*24*60*60, 24*60*60,  60*60,      60,       1     };
    string [] unit = {    "year",      "day",   "hour",  "minute", "second" };

    string result = "";
    bool separate = false;   
    
    for (int i = 0 ; i < time.Length ; i++) {
      int amount = seconds / time[i];
      seconds %= time[i];
      if(amount != 0) {
        if (separate)
          if (seconds == 0)
            result += " and ";
          else
            result += ", ";
        result += amount + " " + unit[i];
        if (amount > 1) {
          result += 's';
        }
        separate = true;
      }
    }
    
    return result;
  }
}