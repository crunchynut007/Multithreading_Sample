using System;
using System.Threading;

namespace Multithreading_Sample
{
	class MultiThreadedSummation
	{
		readonly int numberOfProcessors = Environment.ProcessorCount;
		int[] portionResults = new int[Environment.ProcessorCount];


		public long SumArray(int[] arrayDataToCheck) {
			//splits dataset into equal block sizes. Must be divisible by your processor count
			int datasetPortionLength = arrayDataToCheck.Length / numberOfProcessors;

			//generates new threads for each portion. [numberOfProcessors - 1] since processor index 0-7 not 1-8 on 4 cores + HT
			Thread[] threads = new Thread[numberOfProcessors];
			for (int i = 0; i < numberOfProcessors - 1; i++) {
				threads[i] = new Thread(() => SumOfEachPortionInDataset(i, arrayDataToCheck, datasetPortionLength));
				threads[i].Start();
			}

			//conclude all threads and return the sum of all elements
			for (int i = 0; i < numberOfProcessors - 1; i++) {
				threads[i].Join();
			}
			return SumAllPortionResults();
		}

		//This method is called for each created thread
		private void SumOfEachPortionInDataset (int threadIndex, int[] data, int portionElementSize) {
			int thisPortionTotal = 0;
			for (int i = threadIndex * portionElementSize; i < (threadIndex + 1) * portionElementSize; i++) {
					thisPortionTotal += data[i];
			}
			lock (portionResults) {
				portionResults[threadIndex] = thisPortionTotal;
			}
		}


		private int SumAllPortionResults() {
			int total = 0;
			for (int i = 0; i < portionResults.Length; i++) {
				total += portionResults[i];
			}
			return total;
		}
	}
}
