using System;

namespace com.stansassets.mobile
{
    /// <summary>
    /// This is an entry point for data transfer between the native part and unity.
    /// We transfer data between native part and unity by using pointers to data,
    /// in order to minimize data that we really send.
    /// </summary>>
    public class DataTransferManager
    {
        /// <summary>
        /// This method give us ability to get some data from native by given <c>url</c>.
        /// Under the hood, we just try to get a pointer to data by given <c>url</c>, and then
        /// from this pointer, we get data in Unity part.
        /// </summary>
        /// <param name="url"> String url to to some data that we want to get.</param>
        /// <returns> byte[] array with data in it. If we couldn't get any data it will return null.</returns>
        public static byte[] GetDatBytURL(string url) {
            return NativeLib.API.GetDataFromUrl(url);
        }

        /// <summary>
        /// This method returns some data that we saved in buffer at native part.
        /// At native we can save some data into the buffer and get <c>hash</c> to saved data.
        /// And then get that data back by this hash if we need it.
        /// </summary>
        /// <param name="hash"> int hash to data in the buffer.</param>
        /// <returns> byte[] array with data in it. If we couldn't get any data it will return null.</returns>
        public static byte[] GetDataFromBuffer(int hash) {
            return NativeLib.API.GetDataFromBuffer(hash);
        }

        /// <summary>
        /// This method returns data by the given <c>pointer</c> and <c>size</c>.
        /// <c>Size</c> can be 0 by default if you don't know it.
        /// At iOS part if you don't set <c>size</c>, it try to cast <c>pointer</c> to NSMutableData and get mutableBytes from it,
        /// if you set <c>size</c>, it try to create NSMutableData from <c>pointer</c> and give <c>size</c> and then get mutableBytes from it.
        /// </summary>
        /// <param name="pointer"> Pointer to data that you want to get.</param>
        /// <param name="size"> Size of the data by the given pointer, can be 0 by default.</param>
        /// <returns> byte[] array with data in it. If we couldn't get any data it will return null.</returns>
        public static byte[] GetDataByPointer(IntPtr pointer, int size = 0) {
            return NativeLib.API.GetDataFromPointer(pointer, size);
        }

        /// <summary>
        /// This method returns pointer and <c>size</c> of the data by give <c>url</c>.
        /// </summary>
        /// <param name="url"> String URL to data.</param>
        /// <param name="size"> Size of the data.</param>
        /// <returns> Return pointer, and size in <c>size</c> parameter. If we find data by <c>url</c> it will return IntPtr.Zero.</returns>
        public static IntPtr GetPointerToData(string url, out int size) {
            return NativeLib.API.GetPointerFromUrl(url, out size);
        }

        /// <summary>
        /// This method saves <c>data</c> into buffer in native part and returns hash to it.
        /// Under the hood in Unity we get pointer to the <c>data</c> and size of it, and then send it to native part.
        /// At iOS part we crate NSData from the given pointer and size and save this data into buffer and return it hash. 
        /// </summary>
        /// <param name="data"> Array of byte data.</param>
        /// <returns> It returns hash to data in buffer if we couldn't save data in buffer ut will return 0.</returns>
        public static int SaveDataInBuffer(byte[] data) {
            return NativeLib.API.SaveDataInBuffer(data);
        }

        /// <summary>
        /// This method release data by given <c>pointer</c>. In order to minimize memory leaks.
        /// But it not really a save method, under the hood in iOS part it just do this - <code>CFBridgingRelease(pointer)</code>.
        /// CFBridgingRelease moves a non-Objective-C <c>pointer</c> to Objective-C and also transfers ownership to ARC.
        /// So don't try to this is data that belongs to ARC already, because it can cause an error.
        /// </summary>
        /// <param name="pointer"> Pointer to data.</param>
        public static void ReleaseData(IntPtr pointer) {
            NativeLib.API.ReleaseData(pointer);
        }

        /// <summary>
        /// This method remove data from buffer at native part by given <c>hash</c>.
        /// </summary>
        /// <param name="hash"> Hash to some data in buffer.</param>
        public static void RemoveDataFromBuffer(int hash) {
            NativeLib.API.RemoveDataFromBuffer(hash);
        }
        
        /// <summary>
        /// This method clears buffer at the native part.
        /// </summary>
        public static void ClearBuffer() {
            NativeLib.API.ClearBuffer();
        }
    }
}