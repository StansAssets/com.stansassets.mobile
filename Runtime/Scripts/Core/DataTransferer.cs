using System;

namespace com.stansassets.mobile
{
    /// This should be updated, or changed
    public class DataTransferer
    {
        public static byte[] GetDatBytURL(string url) {
            return NativeLib.API.GetDataFromUrl(url);
        }

        public static byte[] GetDataByPointer(IntPtr pointer, int size) {
            return NativeLib.API.GetDataFromPointer(pointer, size);
        }

        public static IntPtr GetPointerToData(string url, out int size) {
            return NativeLib.API.GetPointerFromUrl(url, out size);
        }
    }
}