import java.io.*;
import java.util.*;

/*
* Created 20/04/2018
* Created by Henry Morrison-Jones and Tom Kent-Peterson.
* Reads a set of lights and their connection arrangement into a 2D Array from a file.
* Then transforms matrix into reduced row echelon to find solution for how to turn them all off.
*/
public class LightsSolver{
  private static int[][] connections;
  private static int[][] identity;
  private int[] gameState;
  private int[] solution;
  private int nodes;

/* Constructor.
* @param n, int determining the number of lights in the Circuit.
*/
  public LightsSolver(int n){
    this.nodes = n;
    connections = new int[nodes][nodes];
    identity = new int[nodes][nodes];
    gameState = new int[nodes];
    solution = new int[nodes];

    //Updates adjacency matrix to show that each light is toggled by its own switch.
    for(int i = 0; i < nodes; i++){
      connections[i][i] = 1;
      identity[i][i] = 1;
      gameState[i] = 1; //all lights are initially on in the gamestate matrix.
    }
  }

/* Default Constructor*/
  public LightsSolver(){}


/* Adds a connection edge from one light to another (switch for v controls W).
* @param v, the primary light who's switch it is.
* @param w, the secondary light who's also controlled by v's switch.
*/
  public void addEdge(int v, int w){
    connections[v][w] = 1;
  }


/*
* Main method reads list of lights and connections from blank .txt file provided in command-line argument.
* Calls Gaussian Elimination Algorithm.
* @param args, command-line arguments.
*/
  public static void main(String args[]){
    BufferedReader br;
    String line = "";
    String filename = "";
    LightsSolver l = new LightsSolver();

    //If argument provided, intiialise filename, else request filename from user.
    if(args.length > 0){
      filename = args[0];
    }else{
      System.err.println("Please enter a filename");
      System.exit(0);
    }

    try{
      br = new BufferedReader(new FileReader(filename));
      try{
        line = br.readLine();
        char [] nodes = line.replaceAll(" ", "").toCharArray();
        l = new LightsSolver(nodes.length);
        line = br.readLine();
        char [] edges = line.replaceAll(" " , "").toCharArray();

        for(int i = 0; i< edges.length; i+=2){
          l.addEdge((int)edges[i]-65, (int)edges[i+1]-65);
        }

      }catch(IOException e){
        System.err.println(e);
        System.exit(0);
      }
    }catch(FileNotFoundException e){
      System.err.println(e);
      System.exit(0);
    }
    //Call Gaussian Elimination.
    l.rowReduce();
  }

/*
* Carries out Gaussian Elimination on the adjacency array and carries simultaneous operations
* on the identity matrix.
* Will output either the switches required to solve or error if no solution.
*/
public void rowReduce(){
  int [] visited = new int[nodes];
  for(int col = 0; col < nodes; col++){
    for(int row = 0; row < nodes; row++){
      //If contains a 1 and lies in an unvisited row
      if(connections[row][col] == 1 && visited[row] == 0){
        visited[row] = 1;
        //look in each row within that column for other 1's
        for(int i = 0; i < nodes; i++){
          //if another row has a 1 in that column then set that row equal to the sum of the two rows.
          if(connections[i][col] == 1 && i != row){
            addRows(row, i);
          }
        }
        //If gamestate also contains a 1 in that column, carry out operations on itself and solution array.
        if(gameState[col] == 1){
          for(int i = 0; i < nodes; i++){
            gameState[i] += connections[row][i];
            gameState[i] %= 2;
            solution[i] += identity[row][i];
            solution[i] %=2;
          }
        }
        break;
      }
    }
  }
  if(gameStateIsNull()){
    printSolution();
  }else{
    System.out.println("No solution is possible for this Circuit.");
  }
}

/*
* Updates a row with a 1 in the same column as a pivot row to be equal to the
* sum of the two rows.
* Carries row operations out on identity matrix also.
* @param row, the current pivot row for Gaussian elimination.
* @param i, the row which has a 1 in the same column of the pivot row.
*/
public void addRows(int row, int i){
  for(int j = 0; j< nodes; j++){
    identity[i][j] += identity[row][j];
    identity[i][j] %= 2;
    connections[i][j] += connections[row][j];
    connections[i][j] %= 2;
  }
}

/*
* Returns true or false if the gameState array is null (meaning the solution has been found).
*/
public boolean gameStateIsNull(){
  for(int i = 0; i < nodes; i++){
    if(gameState[i] != 0){
      return false;
    }
  }
  return true;
}

/*
* Prints out the solution vector(array) in a user-friendly format.
*/
public void printSolution(){
  char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".toCharArray();
  System.out.print("To turn off lights, toggle switches ");
  for(int i = 0; i < nodes; i++){
    if(solution[i] == 1){
      System.out.print(alphabet[i] + " ");
    }
  }
  System.out.println("in any order.");
}


}
