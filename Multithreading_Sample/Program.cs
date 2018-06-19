using System;
using System.Diagnostics;

namespace Multithreading_Sample
{
	class Program
	{
		//static int[] dataset = { 1, 2, 5, 9, 29, 89, 149, 500 };
		static int[] dataset = new int[200000000];
		static long Totalsum = 0;

		//Generate random numbers from 0 to 10 for the data set 
		public static void PopulateArray(int[] array) {
			Random rand = new Random();
			for (int i = 0; i < array.Length; i++) {
				array[i] = rand.Next(10);
			}
		}

		public static long NonthreadedSum(int[] array) {
			long sum = 0;
			for (int i = 0; i < array.Length; i++) {
				sum += array[i];
			}
			return sum;
		}

		static void Main(string[] args) {
			PopulateArray(dataset);
			Stopwatch watch = new Stopwatch();
			MultiThreadedSummation MultithreadedSum = new MultiThreadedSummation();

			//totals the elements of the array on a single thread
			watch.Start();
			Totalsum = NonthreadedSum(dataset);
			watch.Stop();
			Console.WriteLine(Totalsum);
			Console.WriteLine(watch.Elapsed);
			watch.Reset();

			Totalsum = 0;

			//totals the elements of the array on multiple threads
			watch.Start();
			Totalsum = MultithreadedSum.SumArray(dataset);
			watch.Stop();
			Console.WriteLine(value: Totalsum);
			Console.WriteLine(watch.Elapsed);

			Console.ReadKey();
		}
	}
}
