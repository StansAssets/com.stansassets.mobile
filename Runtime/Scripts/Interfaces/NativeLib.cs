
namespace com.stansassets.mobile
{
    public class NativeLib
    {
        static NativeAPI m_Api;

        public static NativeAPI API {
            get {
                if (m_Api == null) {
#if UNITY_IPHONE
                    m_Api = new IOS_NativeAPI();
#endif
                }
                return m_Api;
            }
        }
    } 
}