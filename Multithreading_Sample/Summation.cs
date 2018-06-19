using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading_Sample
{
	class Summation
	{
		readonly int numberOfProcessors = Environment.ProcessorCount;
		int[] portionResults = new int[Environment.ProcessorCount];
		public int Totalsummings = 0;


		public long SumArray(int[] data) {
			int portionlength = data.Length / numberOfProcessors;
			Thread[] threads = new Thread[numberOfProcessors];
			for (int i = 0; i < numberOfProcessors - 1; i++) {
				threads[i] = new Thread(() => Sumyourportion(i, data, portionlength));
				threads[i].Start();

			}
			for (int i = 0; i < numberOfProcessors - 1; i++) {
				threads[i].Join();

			}
			Totalsummings = SumAllPortions();
			return Totalsummings;
		}


		private void Sumyourportion(int portionNumber, int[] array, int length) {
			int subarraytotal = 0;
			lock (array.SyncRoot) {
				for (int i = portionNumber * length; i < (portionNumber + 1) * length; i++) {
					subarraytotal += array[i];
				}
			}
			lock (portionResults.SyncRoot) {
				portionResults[portionNumber] = subarraytotal;
			}

		}


		private int SumAllPortions() {
			int totalsum = 0;

			for (int i = 0; i < portionResults.Length; i++) {
				totalsum += (int)portionResults[i];
			}
			return totalsum;
		}
	}
}
