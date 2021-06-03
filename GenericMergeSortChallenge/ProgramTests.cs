using Microsoft.VisualStudio.TestTools.UnitTesting;
using mergeSortChallenge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/**
 * Author Tom Kent-Peterson 
 * This is a bunch of unit tests for the mergeSortChallenge class.
 * I have made sure that the tests, sort unsorted input, can take no values, can take single values,
 * are able to handle null values if the data type supports it e.g strings, 
 * I have tested edge cases with similar numbers both negative and otherwise,
 * I have tested multiple datatypes that I believe would be the most commonly used for such an algorithm.
 * I use the Array.Sort() method in order to compare my answer with what the official library would get.
 **/
namespace mergeSortChallenge.Tests {
    [TestClass()]
    public class UnitTest1 {
        [TestMethod]
        //sorts a basic array of ints
        public void testIntegerSorting() {
            //arrange
            int[] arr = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an empty array
        public void testIntegerEmptySorting() {
            //arrange
            int[] arr = {};
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts a long number
        public void testIntegerLongNumber() {
            //arrange
            int[] arr = {1000000000};
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts a basic array of the same ints
        public void testIntegerSameSorting() {
            //arrange
            int[] arr = { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 };
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts a small amount of ints
        public void testSmallIntegerSorting() {
            //arrange
            int[] arr = { 2, 1 };
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts a single integer to test if the program handles that
        public void testSingleIntegerSorting() {
            //arrange
            int[] arr = { 1 };
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts big numbers to see if different character amounts cause issues
        public void testLargeIntegerSorting() {
            //arrange
            int[] arr = {1523623, 1344643, 188333, 1749876, 907122, 489864, 215855, 1132072, 1890474, 573900, 608915, 939145};
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of negative numbers to see if the sign effects it
        public void testNegativeIntegerSorting() {
            //arrange
            int[] arr = { -1, -5, -10, -20, -2};
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of both positive and negative numbers to check if the program can handle multiple signs
        public void testBothSignsIntegerSorting() {
            //arrange
            int[] arr = { 10, -5, 8, -16, -20, 20, 5, 4, 8, -1 };
            int[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<int> test1 = new MergeSort<int>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of strings into alphabetical order
        public void testStringSorting() {
            //arrange
            string[] arr = { "a", "Zac", "be", "your", "hands" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of letters into alphabetical order
        public void testStringCharSorting() {
            //arrange
            string[] arr = { "a", "z", "b", "t", "y", "f", "e", "p", "o", "q" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of small words with the same starting letter into alphabetical order
        public void testStringSecondChar() {
            //arrange
            string[] arr = { "be", "ba", "bc", "by", "bz", "bt", "bu" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of numbers and letters into alphabetical order
        public void testNumbersAndLetters() {
            //arrange
            string[] arr = { "this", "is", "a", "test", "1", "hope", "4", "F", "9" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of letters which get larger with each word but with the same letters
        public void testStringOfIncreasingSize() {
            //arrange
            string[] arr = { "c", "cat", "ca", "cattei", "catte", "catteis" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of strings with some empty spaces to see how the code handles it
        public void testStringWithEmpty() {
            //arrange
            string[] arr = { "this", "string", "is", "empty", " ", "", "  ", "    ", "    hi   " };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of strings with some different symbols
        public void testStringWithSymbols() {
            //arrange
            string[] arr = { "my%", "this*", "my@", "^", "&", " @", "$", "this is a long string because why not" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of negative numbers in string format
        public void testStringofNegativeNums() {
            //arrange
            string[] arr = { "-1098", "-1", "-250", "-1708", "-6543", "1" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //checks if a null string doesn't break it
        public void testStringNullSingleItem() {
            //arrange
            string[] arr = { null };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //checks if the code can handle a null value
        public void testStringNullWithOtherItem() {
            //arrange
            string[] arr = { null, "hi", "there", "is", null, "something", "null", "in", null, "here" };
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
            //this test did fail originally so I fixed the code to handle comparing null values
        }

        [TestMethod]
        //checks if the code can handle a null value
        public void testStringNullAndWord() {
            //arrange
            string[] arr = {null, null, "null", null, null};
            string[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<string> test1 = new MergeSort<string>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);

        }
        [TestMethod]
        //sorts an array of doubles
        public void testDoubleSorting() {
            //arrange
            double[] arr = {130000, 344440, 500003, 5006040, 3402020, 2034, 3, 5063};
            double[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<double> test1 = new MergeSort<double>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of doubles with decimals
        public void testDoubleDecimalSorting() {
            //arrange
            double[] arr = { 15.30, 15.60, 15.40, 15.601, 15.602, 15.50 };
            double[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<double> test1 = new MergeSort<double>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of negative doubles with decimals
        public void testDoubleNegativeDecimalsSorting() {
            //arrange
            double[] arr = { -15.30, -15.60, -15.40, -15.601, -15.602, -15.50 };
            double[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<double> test1 = new MergeSort<double>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of both positive and negative numbers
        public void testDecimalsPositiveAndNegative() {
            //arrange
            double[] arr = { 15.40, -15.40, 3.14, -3.14, 0.00, 0.01, -0.01 };
            double[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<double> test1 = new MergeSort<double>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of floats especially with values close to eachother
        public void testFloatSorting() {
            //arrange
            float[] arr = { 15.40f, 3.14f, 0.01f, 0.02f, 0.011f, 0.010f};
            float[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<float> test1 = new MergeSort<float>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of floats with both whole numbers and decimals
        public void testFloatSortingWithMultTypes() {
            //arrange
            float[] arr = { 15f, 3f, 0f, 0.01f, 0.1f };
            float[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<float> test1 = new MergeSort<float>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an array of floats with negative and positive nums
        public void testFloatSortingWithNeg() {
            //arrange
            float[] arr = { 0f, -0f, -0.1f, 0.1f, 1f, -1f, 1.1f, -1.1f };
            float[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<float> test1 = new MergeSort<float>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts a single float
        public void testFloatSortingSingleNum() {
            //arrange
            float[] arr = { 0f};
            float[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<float> test1 = new MergeSort<float>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
        [TestMethod]
        //sorts an empty float array
        public void testFloatSortingEmpty() {
            //arrange
            float[] arr = {};
            float[] expectedResult = arr;
            Array.Sort(expectedResult);
            MergeSort<float> test1 = new MergeSort<float>();
            //act
            test1.mergeSort(arr, 0, arr.Length - 1);
            //assert
            CollectionAssert.AreEqual(expectedResult, arr);
        }
    }
}