using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                //compares the left array with the right array using the CompareTo method it checks if the number is smaller by seeing if the result is less than or equals to it
                if ((firstArr[leftIndex]).CompareTo(secondArr[rightIndex]) <= 0) {
                    arr[currentIndex] = firstArr[leftIndex];
                    leftIndex++;
                } else {
                    arr[currentIndex] = secondArr[rightIndex];
                    rightIndex++;
                }
                currentIndex++;
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
        public static void Main() {
            double[] arr = { 5, 4, 3, 2, 1, 0, 20, 30, 50, 40, 100, 120, 60, 50, 40, 8};
            String[] stringArr = { "hi", "i", "am", "screwed", "hi" , "this", "is", "weird", "a", "b", "he"};
            MergeSort<double> ms = new MergeSort<double>();
            MergeSort<string> stringversion = new MergeSort<string>();
            Console.WriteLine("array unsorted is: \n");
            for (int i = 0; i < arr.Length; i++) {
                Console.Write(arr[i] + " ");
            }
            ms.mergeSort(arr, 0, arr.Length-1);

            Console.WriteLine("\n");
            Console.WriteLine("\n sorted array is : \n");
            for(int i = 0; i < arr.Length; i++) {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine("string array unsorted is: \n");
            for (int i = 0; i < stringArr.Length; i++) {
                Console.Write(stringArr[i] + " ");
            }
            stringversion.mergeSort(stringArr, 0, stringArr.Length - 1);
            Console.WriteLine("\n");
            Console.WriteLine("\n sorted string array is : \n");
            for (int i = 0; i < stringArr.Length; i++) {
                Console.Write(stringArr[i] + " ");
            }
            Console.ReadLine();
        }
    }
}
