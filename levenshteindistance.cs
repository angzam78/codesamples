/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System.Collections.Generic;

public class LevenshteinDist
{
    private IEnumerable<string> words;

    public LevenshteinDist(IEnumerable<string> words)
    {
        this.words = words;
    }

    public string FindMostSimilar(string term)
    {
        string similar = "";
        int divergence = int.MaxValue;

        foreach (string word in words)
        {   
            int distance = LevenshteinDistance(term, word);
            if( distance < divergence  ) 
            {
                divergence = distance;
                similar = word;
            }
        }
  
        return similar;
    }
    
    private int LevenshteinDistance(string str1, string str2)
    {
        int [] dist = new int[str2.Length+1];
        for (int i = 0 ; i <= str2.Length ; i++) dist[i] = i;

        for (int i = 1 ; i <= str1.Length ; i++) 
        {
          int prev = dist[0];
          dist[0] = i;

          for (int j = 1 ; j <= str2.Length ; j++) 
          {
            int min = prev + (str1[i-1] == str2[j-1] ? 0 : 1); // substitution
            if (dist[j] + 1 < min) min = dist[j] + 1; // deletion
            if (dist[j-1] + 1 < min) min = dist[j-1] + 1; // insertion
            
            prev = dist[j];
            dist[j] = min;
          }
        }

        return dist[str2.Length];
     }
    
}
