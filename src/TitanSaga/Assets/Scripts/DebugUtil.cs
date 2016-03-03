using UnityEngine;
using System.Collections;

public class DebugUtil
{
	public	static void MemoryLog(string tag, bool gc) {
		if (gc) {
			System.GC.Collect();
		}

		uint maxHeap = Profiler.GetMonoHeapSize();
		uint useHeap = Profiler.GetMonoUsedSize();
		uint maxUnity = Profiler.GetTotalReservedMemory();
		uint useUnity = Profiler.GetTotalAllocatedMemory();

		var builder = new System.Text.StringBuilder();
		builder.AppendFormat("[Profiler] H:{0}/{1}", ConvertBytesString(useHeap), ConvertBytesString(maxHeap));
		builder.AppendFormat(", U:{0}/{1}, T:{2}", ConvertBytesString(useUnity), ConvertBytesString(maxUnity), tag);
		Debug.Log(builder.ToString());
	}

	public	static string ConvertBytesString(double number) {
		if (number < 1024) {
			return string.Format("{0:0.00} B", number);
		} else if (number < 1048576) {
			return string.Format("{0:0.00} KB", number / 1024);
		} else if (number < 1073741824) {
			return string.Format("{0:0.00} MB", number / 10048576);
		} else {
			return string.Format("{0:0.00} GB", number / 1073741824);
		}
	}
}

