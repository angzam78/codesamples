/* 
  MIT License
  Copyright (c) 2023 angzam78 (Angelo Zammit)
*/

public class TTTSolver
{
  public static int[] TurnMethod(int[][] board, int player)
  {     
    int[] result = {3, 3}; // this result should never be returned
    
    int nextplayer = player == 1 ? 2 : 1;
    
    for (int i = 0 ; i < 3 ; i++)
      for (int j = 0 ; j < 3 ; j++)
        if(board[i][j] == 0)
        {
          int movewinner = PredictOutcome(board, player, i, j);
          if (movewinner == player || movewinner == 0)
          {
            result[0]=i;
            result[1]=j;
            i = j = 3; // stop searching
          }
        }
    
    return result;
  }
  
  private static int PredictOutcome(int[][] board, int player, int x, int y)
  {
    board[x][y] = player; // simulate move
    int winner = GetWinner(board);
    
    if (winner == -1)
    {
      winner = player; // ...unless we predict otherwise
      int nextplayer = player == 1 ? 2 : 1;
      
      // go through all possible opponent responses
      for (int i = 0 ; i < 3 ; i++) {
        for (int j = 0 ; j < 3 ; j++) {
          if (board[i][j] == 0)
          {
            int movewinner = PredictOutcome (board, nextplayer, i, j);
            
            // take note of worst outcome
            if (movewinner == nextplayer)
            {
              winner = nextplayer;
              i = j = 3; // stop searching
            }
            else if (movewinner == 0)
            {
              winner = 0;
            }
          }
        }
      }
    }

    board[x][y] = 0; // undo simulated move
    return winner;
  }

  private static int GetWinner(int[][] board)
  {
    int d1 = 1, d2 = 1;
    bool empty = false;
    for (int i = 0; i < 3; i++)
    {
      d1 *= board[i][i];
      d2 *= board[2 - i][i];

      int row = 1, col = 1;

      for (int j = 0; j < 3; j++)
      {
        row *= board[i][j];
        col *= board[j][i];
      }

      if (row == 1 || col == 1) return 1;
      if (row == 8 || col == 8) return 2;
      if (row == 0 || col == 0) empty = true;
    }
    
    if (d1 == 1 || d2 == 1) return 1;
    if (d1 == 8 || d2 == 8) return 2;
    if (empty) return -1;

    return 0;
  }
}