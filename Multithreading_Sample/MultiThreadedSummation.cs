using System;
using System.Threading;

namespace Multithreading_Sample
{
	class MultiThreadedSummation
	{
		readonly int numberOfProcessors = Environment.ProcessorCount;
		private int[] portionResults = new int[Environment.ProcessorCount];
		private Object myLock = new Object();


		public long SumDataset(int[] arrayDataToCheck) {
			//splits dataset into equal block sizes. Must be divisible by your processor count
			int datasetPortionLength = arrayDataToCheck.Length / numberOfProcessors;

			//generates new threads for each portion.
			Thread[] threads = new Thread[numberOfProcessors];
			for (int i = 0; i < numberOfProcessors; i++) {
				int id = i;
				Thread thread = new Thread(() => SumOfEachPortionInDataset(id, arrayDataToCheck, datasetPortionLength));
				threads[i] = thread;
				//threads[i].IsBackground = true;
				threads[i].Start();
			}

			//conclude all threads and return the sum of all elements
			for (int i = 0; i < numberOfProcessors; i++) {
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
			lock (myLock) {
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
