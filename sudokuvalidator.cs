/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

using System;

public class Sudoku
{
  public static bool ValidateSolution(int[][] board)
  {
    
    //check rows and columns
    for(int i = 0 ; i < 9 ; i++){
      bool [] row = new bool[10];
      bool [] col = new bool[10];
      for(int j = 0 ; j < 9 ; j++){
        if(board[i][j] == 0 ||  board[j][i] == 0 || 
           row[board[i][j]] == true ||   col[board[j][i]] == true) 
             return false;
        row[board[i][j]] = true;
        col[board[j][i]] = true;
      }
    }
       
    //check subgrids
    for(int i = 0 ; i < 9 ; i+=3) {
      for(int j = 0 ; j < 9 ; j+=3) {
        bool [] grid = new bool[10];
        for(int x = i ; x < i+3 ; x++ ) {
          for(int y = j ; y < j+3 ; y++ ) { 
            if(grid[board[x][y]] == true) 
              return false;
            grid[board[x][y]] = true;
          }
        }
      }
    }
    return true;
  }
}