using System;

namespace com.stansassets.mobile
{
    public interface NativeAPI
    {
        IntPtr GetPointerFromUrl(string url, out int size);
        byte[] GetDataFromUrl(string url);
        byte[] GetDataFromPointer(IntPtr pointer, int size);
        byte[] GetDataFromBufferr(int hash);

        int SaveDataInBuffer(byte[] data);

        void ReleaseData(IntPtr pointer);
        void RemoveDataFromBuffer(int hash);
    }
}