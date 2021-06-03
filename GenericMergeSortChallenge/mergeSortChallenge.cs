using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
/**
 * Author Tom Kent-Peterson
 * mergeSortChallenge is creating a generic class which implements a generic merge sort algorithm, 
 * this is my first time using generics, however it is meant for general purpose.
 * 
 **/
namespace mergeSortChallenge {
    public class MergeSort<T> where T : System.IComparable<T> {

        /*  mergeSort takes the array arr and recursively sorts the arrays from start to end
         *  the two ints start and end mark the first element and the last element of the array
         *  this allows the arrays to be split in the recursive methods.
         *  each recursive call of mergeSort will sort each half of the array.
         *  the merge method will merge the two arrays once they've been sorted.
         */
        public void mergeSort<T>(T[] arr, int start, int end) where T : IComparable<T> {
            if (start < end) {
                int midPoint = (start + end) / 2;
                mergeSort(arr, start, midPoint);
                mergeSort(arr, midPoint + 1, end);
                merge(arr, start, midPoint, end);
            }
        }
        /* merge takes the subarrays from arr and fills them up from the mid point which is specified as mid
         * the ints left and right allow me to use each end of the array as a marker while mid allows me to seperate them
         * into the firstArr and secondArr variables seen below.
         * currentIndex allows me to keep track of where the sorting process is currently
         */
        public void merge<T>(T[] arr, int left, int mid, int right) where T : IComparable<T>{
            int leftIndex = 0, rightIndex = 0;
            int currentIndex = left;
            //creates 2 arrays for the left and right side of arr
            T[] firstArr = new T[mid - left + 1];
            T[] secondArr = new T[right - mid];
            //fills in both the first and second array made above
            for (int i = 0; i < firstArr.Length; i++) {
                firstArr[i] = arr[left + i];
            }
            for (int i = 0; i < secondArr.Length; i++) {
                secondArr[i] = arr[mid + 1 + i]; //error
            }
            //compares the left and right index to their respective array anything which isn't caught in the sorting gets inserted after this loop
            while (leftIndex < firstArr.Length && rightIndex < secondArr.Length) {
                //fixes an issue if the user attempts to compare a null value, this works because if two null values exist beside each other no swap will happen
                if((firstArr[leftIndex]) != null && firstArr[rightIndex] != null){
                    //compares the left array with the right array using the CompareTo method it checks if the number is smaller by seeing if the result is less than or equals to it
                    if ((firstArr[leftIndex]).CompareTo(secondArr[rightIndex]) <= 0) {
                        arr[currentIndex] = firstArr[leftIndex];
                        leftIndex++;
                    } else {
                        arr[currentIndex] = secondArr[rightIndex];
                        rightIndex++;
                    }
                    currentIndex++;
                //if any of the values are null do this
                } else {
                    if(firstArr[leftIndex] == null){
                        arr[currentIndex] = firstArr[leftIndex];
                        leftIndex++;
                    } else {
                        arr[currentIndex] = firstArr[leftIndex];
                        rightIndex++;
                    }
                    currentIndex++;
                }
            }
            while (leftIndex < firstArr.Length) {
                arr[currentIndex++] = firstArr[leftIndex++];
            }
            while (rightIndex < secondArr.Length) {
                arr[currentIndex++] = secondArr[rightIndex++];
            }
        }

    }
    public class Program {
        //The main method takes the users input and based on what the user entered will return a sorted array of the data type specified in the console
        public static void Main() {
            bool dataEntered = false;
            string inputType = "";
            string data = "";
            string[] words;
            MergeSort<int> intVersion = new MergeSort<int>();
            MergeSort<double> doubleVersion = new MergeSort<double>();
            MergeSort<string> stringVersion = new MergeSort<string>();
            MergeSort<float> floatVersion = new MergeSort<float>();
            Console.WriteLine("Enter a number to choose data type to sort: 1 = string, 2 = int, 3 = double, 4 = float \n");
            while(!dataEntered){
                inputType = Console.ReadLine();
                if (inputType.Equals("1")) {
                    Console.WriteLine("Enter strings to sort seperated by spaces: \n");
                    data = Console.ReadLine();
                    dataEntered = true;
                } else if (inputType.Equals("2")) {
                    Console.WriteLine("Enter ints to sort seperated by spaces: \n");
                    data = Console.ReadLine();
                    dataEntered = true;
                } else if (inputType.Equals("3")) {
                    Console.WriteLine("Enter doubles to sort seperated by spaces: \n");
                    data = Console.ReadLine();
                    dataEntered = true;
                } else if (inputType.Equals("4")) {
                    Console.WriteLine("Enter floats to sort seperated by spaces: \n");
                    data = Console.ReadLine();
                    dataEntered = true;
                } else {
                    Console.WriteLine("invalid input please try again \n");
                }
            }
            //prevents the user from breaking the split function with too many spaces
            data = Regex.Replace(data, @"\s+", " ");
            words = data.Split(' ');
            //sorts out the new array depending on the datatype
            int[] arrayInt = new int[words.Length];
            float[] arrayFloat = new float[words.Length];
            double[] arrayDouble = new double[words.Length];
            for (int i = 0; i < words.Length; i++) {
                if (inputType == "2") {
                    try{
                        arrayInt[i] = int.Parse(words[i]);
                    } catch (FormatException) {
                        Console.WriteLine("one or more of the entries is not a number exiting... \n");
                        Console.ReadLine();
                        return;
                    }
                } else if (inputType == "3") {
                    try {
                        arrayDouble[i] = double.Parse(words[i]);
                    } catch (FormatException) {
                        Console.WriteLine("one or more of the entries is not a number exiting... \n");
                        Console.ReadLine();
                        return;
                    }
                } else if (inputType == "4") {
                    try {
                        arrayFloat[i] = float.Parse(words[i]);
                    } catch (FormatException) {
                        Console.WriteLine("one or more of the entries is not a number exiting... \n");
                        Console.ReadLine();
                        return;
                    }
                }
            }

            Console.WriteLine("array unsorted is: \n");
            for (int i = 0; i < words.Length; i++) {
                Console.Write(words[i] + " ");
            }
            //call based on what the datatype is
            if(inputType == "1")
                stringVersion.mergeSort(words, 0, words.Length - 1);
            if (inputType == "2")
                intVersion.mergeSort(arrayInt, 0, arrayInt.Length - 1);
            if(inputType == "3")
                doubleVersion.mergeSort(arrayDouble, 0, arrayDouble.Length - 1);
            if(inputType == "4")
                floatVersion.mergeSort(arrayFloat, 0, arrayFloat.Length - 1);

            //after sorting
            Console.WriteLine("\n");
            Console.WriteLine("\n sorted array is : \n");
            for(int i = 0; i < words.Length; i++) {
                if(inputType == "1")
                    Console.Write(words[i] + " ");
                if (inputType == "2")
                    Console.Write(arrayInt[i] + " ");
                if (inputType == "3")
                    Console.Write(arrayDouble[i] + " ");
                if (inputType == "4")
                    Console.Write(arrayFloat[i] + " ");
            }

            Console.ReadLine();
        }
    }
    
}
