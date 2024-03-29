﻿#if UNITY_IPHONE || UNITY_TVOS
#define API_ENABLED
#endif
using System;
using UnityEngine;
#if API_ENABLED
using System.Runtime.InteropServices;
#endif

namespace com.stansassets.mobile
{
    public class IOS_NativeAPI: NativeAPI 
    {

#if API_ENABLED
        const string k_DllName = "__Internal";

        [DllImport(k_DllName)]
        static extern IntPtr _ISN_GetPointerForFile(string url, out int size);

        [DllImport(k_DllName)]
        static extern IntPtr _ISN_GetDataPointerFromBuffer(int hash, out int size);

        [DllImport(k_DllName)]
        static extern int _ISN_SaveDataByPointerInBuffer(IntPtr pointer, int size);

        [DllImport(k_DllName)]
        static extern IntPtr _ISN_GetDataByPointer(IntPtr pointer, int size);

        [DllImport(k_DllName)]
        static extern IntPtr _ISN_GetNSMutableDataByPointer(IntPtr pointer, out int size);

        [DllImport(k_DllName)]
        static extern void _ISN_ReleaseData(IntPtr pointer);

        [DllImport(k_DllName)]
        static extern void _ISN_RemoveDataFromBuffer(int hash);

        [DllImport(k_DllName)]
        static extern void _ISN_ClearBuffer();
#endif

        public IntPtr GetPointerFromUrl(string url, out int size) {
#if API_ENABLED
            return _ISN_GetPointerForFile(url, out size);
#else
            size = 0;
            return IntPtr.Zero;
#endif
        }
        
        public byte[] GetDataFromUrl(string url) {
#if API_ENABLED
            int size = 0;
            var pointer = GetPointerFromUrl(url, out size);
            if (size == 0) {
                return null;
            }
            var data = new byte[size];
            Marshal.Copy(pointer, data, 0, size);
            return data;
#else
            return null;
#endif
        }

        public int SaveDataInBuffer(byte[] data) {
#if API_ENABLED
            var pointer = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, pointer, data.Length);
            return _ISN_SaveDataByPointerInBuffer(pointer, data.Length);
#else
            return 0;
#endif
        }

        public byte[] GetDataFromBuffer(int hash) {
#if API_ENABLED
            int size = 0;
            var pointer = _ISN_GetDataPointerFromBuffer(hash, out size);
            if (size == 0) {
                return null;
            }
            var data = new byte[size];
            Marshal.Copy(pointer, data, 0, size);
            return data;
#else
            return null;
#endif
        }

        public byte[] GetDataFromPointer(IntPtr pointer, int size) {
#if API_ENABLED
            var dataHandler = size == 0 ?
                _ISN_GetNSMutableDataByPointer(pointer, out size) :
                _ISN_GetDataByPointer(pointer, size);
            if (size == 0) {
                return null;
            }
            var data = new byte[size];
            Marshal.Copy(dataHandler, data, 0, size);
            ReleaseData(pointer);
            return data;
#else
            return null;
#endif
        }

        public void ReleaseData(IntPtr pointer) {
#if API_ENABLED
            _ISN_ReleaseData(pointer);
#endif
        }

        public void RemoveDataFromBuffer(int hash) {
#if API_ENABLED
            _ISN_RemoveDataFromBuffer(hash);
#endif
        }

        public void ClearBuffer() {
#if  API_ENABLED
            _ISN_ClearBuffer();
#endif
        }
    }
}