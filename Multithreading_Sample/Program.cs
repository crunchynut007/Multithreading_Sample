﻿using System;
using System.Diagnostics;

namespace Multithreading_Sample
{
	class Program
	{
		//static int[] dataset = { 1, 2, 5, 9, 29, 89, 149, 500 };
		static int[] dataset = new int[40000000];
		static long totalSum = 0;

		//Generate random numbers from 0 to 9 for the data set 
		public static void PopulateDataset(int[] yourDataset) {
			Random rand = new Random();
			for (int i = 0; i < yourDataset.Length; i++) {
				yourDataset[i] = rand.Next(10);
			}
		}

		public static long NonThreadedSumOfDataset(int[] array) {
			long sum = 0;
			for (int i = 0; i < array.Length; i++) {
				sum += array[i];
			}
			return sum;
		}

		static void Main(string[] args) {
			PopulateDataset(dataset);
			Stopwatch watch = new Stopwatch();
			MultiThreadedSummation MultithreadedSum = new MultiThreadedSummation();

			//totals the elements of the array on a single thread
			watch.Start();
			totalSum = NonThreadedSumOfDataset(dataset);
			watch.Stop();
			Console.WriteLine(totalSum);
			Console.WriteLine(watch.Elapsed);
			watch.Reset();

			totalSum = 0;

			//totals the elements of the array on multiple threads
			watch.Start();
			totalSum = MultithreadedSum.SumDataset(dataset);
			watch.Stop();
			Console.WriteLine(value: totalSum);
			Console.WriteLine(watch.Elapsed);

			Console.ReadKey();
		}
	}
}
