using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading_Sample
{
	class Program
	{
		//static int[] dataset = new int[16];
		static int[] dataset = { 1, 2, 5, 9, 29, 89, 149, 500 };
		static long Totalsum = 0;

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
			//PopulateArray(dataset);
			Stopwatch watch = new Stopwatch();
			watch.Start();
			Totalsum = NonthreadedSum(dataset);
			watch.Stop();
			Console.WriteLine(Totalsum);
			Console.WriteLine(watch.Elapsed);
			watch.Reset();

			Totalsum = 0;
			Summation Adder = new Summation();
			watch.Start();
			Totalsum = Adder.SumArray(dataset);
			Console.WriteLine(value: Totalsum);
			watch.Stop();
			Console.WriteLine(watch.Elapsed);

			Console.ReadKey();
		}
	}
}
