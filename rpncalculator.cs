/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;
using System.Collections.Generic;

public class Calc
{
  public double evaluate(String expr)
  {
    double result = 0;

    if(expr != string.Empty) {
    
      Stack<double> rpnstack = new Stack<double>();
    
      string[] elements = expr.Split(' ');
    
      for(int i = 0 ; i < elements.Length ; i++){
        switch(elements[i]){
          case "+" :  rpnstack.Push( rpnstack.Pop() + rpnstack.Pop());
            break;
          case "-" :  double subtractor = rpnstack.Pop();
                      rpnstack.Push( rpnstack.Pop() - subtractor);
            break;
          case "*" :  rpnstack.Push( rpnstack.Pop() * rpnstack.Pop());
            break;
          case "/" :  double divisor = rpnstack.Pop();
                      rpnstack.Push( rpnstack.Pop() / divisor );
            break;
          default:    rpnstack.Push(double.Parse(elements[i]));
            break;
        }
      }
      result = rpnstack.Pop();
    }
    return result;
  }
}