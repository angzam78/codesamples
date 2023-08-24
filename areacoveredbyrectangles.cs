/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;
using System.Collections.Generic;

namespace Solution
{
  public static class RectangleFitting
  {
    private class Event  
    {  
      public int x;
      public int y;
      public bool begin;
      public Event pair;

      public Event(int[] r, bool b)
      {
        x = b ? r[0] : r[2];
        y = b ? r[1] : r[3];
        begin = b;
      }
    }

    public static long Calculate(IEnumerable<int[]> rectangles)
    {   
      List<Event> events = new List<Event>(); 

      foreach (int[] rect in rectangles)
      {
        Event bottomleft = new Event(rect,true);
        Event topright = new Event(rect,false);

        bottomleft.pair = topright;
        topright.pair = bottomleft;

        events.Add(bottomleft);   
        events.Add(topright);
      }

      long area = 0;
      
      List<Event> activeset = new List<Event>();
      
      events.Sort((e1,e2) => e1.x.CompareTo(e2.x));
      
      for(int index = 0 ; index < events.Count-1 ; index++)
      {
        if(events[index].begin)
        {
          activeset.Add(events[index]);
          activeset.Add(events[index].pair);
        }
        else
        {
          activeset.Remove(events[index]);
          activeset.Remove(events[index].pair);
        }

        if (events[index].x != events[index+1].x && activeset.Count != 0)
        {
          int count = 0;
          long sweepline = 0;
          Event previous = null;

          activeset.Sort((e1,e2) => e1.y.CompareTo(e2.y));
          
          foreach (Event current in activeset)
          {
            if (count == 0) previous = current;
            count += current.begin ? +1 : -1;
            if (count == 0) sweepline += current.y - previous.y;
          }
          
          area += sweepline * (events[index+1].x - events[index].x) ;
        }
      }
      
      return area;
    }
  }
}