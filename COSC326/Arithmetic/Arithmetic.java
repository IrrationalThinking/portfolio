/*@author Tom Kent-Peterson
  student_id 4732190
  Arithmetic.java
  special thanks to this website for giving me a good place to start
  https://www.geeksforgeeks.org/root-to-leaf-path-sum-equal-to-a-given-number/
*/

import java.util.Scanner;

public class Arithmetic{
    /*The simple method takes the numbers target and ordering type entered
      by the user.
      
      It will then recurrsively iterate through the array using the index variable to keep track of its position
      either adding or multiplying the last number seen with the current number.

      The path taken is recorded so the output can be consistent with what path was taken
      to get to the answer.

      To save memory when a recursive path reaches a number beyond its scope the path is
      discarded.

      When the required target is found the method checks the index is consistent
      with the end of the array if it is. The letter, target, the numbers entered
      and the arithmetic used to get to the answer are printed out and the
      method returns true.

      If the method returns false it returns impossible to the user in the main method.
     */
    public static boolean simple(int[] numbers, int target, int currentVal, int[] rememberedVals,
				 int index, int cameFrom, char[] path, char switchUsed){
	
	boolean ans = false;
	if(index == 0){
	    rememberedVals[index] = numbers[index];
	}else if(cameFrom == 1){
	    rememberedVals[index] = rememberedVals[index-1] + numbers[index];
	    path[index-1] = '+';
	}else if(cameFrom == 2){
	    rememberedVals[index] = rememberedVals[index-1] * numbers[index];
	    path[index-1] = '*';
	}
	currentVal = rememberedVals[index];
	//System.out.println("vals are " + currentVal + " " + index);
	if(index == numbers.length-1){
	    if(currentVal == target){
		System.out.print(switchUsed + " " + target + " ");
		for(int i = 0; i < numbers.length; i++){
		    try{
			System.out.print(path[i-1] + " " + numbers[i] + " ");
		    }catch(IndexOutOfBoundsException e){
			System.out.print(numbers[i] + " ");
		    }
		}
		System.out.println();
		return true;
	    }
	}else{
	    if(currentVal <= target){
		
		ans = ans || simple(numbers, target, 0, rememberedVals, index+1, 1, path, switchUsed);
	      
		ans = ans || simple(numbers, target, 0, rememberedVals, index+1, 2, path, switchUsed);
		
	    }
	}
	return ans;
    }

    
    /*The natural method takes the numbers target and ordering type entered
      by the user.
      
      It keeps track of the sum of the numbers that should be added and multiplyed
      together in the adding and multi variables when finding the target these
      are added together.
      
      It will then recurrsively iterate through the array using the index variable to keep
      track of its position storing the multiplied values and adding values seperately
      to prevent left to right ordering from occuring.

      The path taken is recorded so the output can be consistent with what path was taken
      to get to the answer.

      It will like the simple method recurrsively search through all possiblties
      stopping a search if the number ends up going over the target value saving
      memory.

      symbolUsed is also set during every iteration this is used for keeping track
      of what symbol came before it wither it was a times or plus this prevents
      some edge cases that end up happening with larger values.

      When the required target is found the method checks the index is consistent
      with the end of the array if it is. The letter, target, the numbers entered
      and the arithmetic used to get to the answer are printed out and the
      method returns true.
      
      If the method returns false it returns impossible to the user in the main method.
    */
    public static boolean natural(int[] numbers, int target, int index, int cameFrom,
				  int adding, int multi, int currentVal, char[] path,
				  char switchUsed){
	boolean ans = false;
	if(cameFrom == 0){
	    multi = numbers[index];
	}
	if(cameFrom == 1){
	    path[index-1] = '+';
	    if(index == 1){
		adding = numbers[index-1];
	    }else{
		adding += multi;

	    }
	    multi = numbers[index];

	}else if(cameFrom == 2){
	    if(multi == 0){
		multi = 1;
	    }
	    if(index == 1){
		multi = numbers[index-1];
		multi *= numbers[index];
	    }else{
		multi *= numbers[index];
	    }
	    /*evalute plus after multi*/
	    path[index-1] = '*';
	}
	currentVal = adding+multi;
	//	System.out.println(currentVal);
	if(index == numbers.length-1){
	    String answer = "";
	    int total = 0;
	    if(currentVal == target){
		ans = true;
		System.out.print(switchUsed + " " + target + " ");
		for(int i = 0; i < numbers.length; i++){
		    try{
			answer += (path[i-1] + " " + numbers[i] + " ");
		    }catch(IndexOutOfBoundsException e){
			answer += (numbers[i] + " ");
		    }
		}
		System.out.println(answer);
		//return ans;
	    }
	}else{
	    if(currentVal <= target){
			if(index == 0){
				multi = 0;
				currentVal = 0;
			}
			ans = ans || natural(numbers, target, index+1, 1, adding, multi, currentVal, path, switchUsed);
		
			ans = ans || natural(numbers, target, index+1, 2, adding, multi, currentVal, path, switchUsed);
	    }
	}
	return ans;
    }
    /*Main method of the program sets up all of the inputs to be cast into the methods above.
      
      Takes 2 lines of input as Strings using scanners then splits them into the required variables
      via parsing.

      The array size is the length of the 1st lines input this is because we know that we will not
      need more space than that as all calculations done are determined on the amount of numbers
      inputted.

      Checks if the methods are false if they are it prints impossible. If not it prints the result
      in the other method.
     */
    public static void main(String args[]){

	Scanner line1 = new Scanner(System.in);
	//Scanner line2 = new Scanner(System.in);
	while(line1.hasNextLine()){
	    try{
		int target;
		char switcher = ' ';
		//System.out.println("first line");
		String result1 = line1.nextLine();
		String[] input1Result = result1.split(" ");
		int[] numbersToUse = new int[input1Result.length];
		int[] rememberedVals = new int[numbersToUse.length];
		char[] pathing = new char[numbersToUse.length-1];
		//int[] lastValRemembered = new int[numbersToUse.length];
		for(int i = 0; i < input1Result.length; i++){
		    numbersToUse[i] = Integer.parseInt(input1Result[i]);
		}
	    
		//System.out.println("second line");
		String result2 = line1.nextLine();

		String[] input2Result = result2.split(" ");
		target = Integer.parseInt(input2Result[0]);
	    
		for(int i = 0; i < result2.length(); i++){
		    if(result2.charAt(i) == 'N'){
			switcher = result2.charAt(i);
		    }else if(result2.charAt(i) == 'L'){
			switcher = result2.charAt(i);
		    }
		}
	    
		if(switcher == 'N'){
		    if(!natural(numbersToUse, target, 0, 0, 0, 0, 0, pathing, switcher)){
			System.out.println(switcher + " " + target + " Impossible");
		    }
		}else if(switcher == 'L'){
		    if(!simple(numbersToUse, target, 0, rememberedVals, 0, 0, pathing, switcher)){
			System.out.println(switcher + " " + target + " Impossible");
		    }
		}else{
		    System.out.println("switch is not valid");
		}
	    }catch(NumberFormatException e){
		break;
	    }
	}
    }
}

