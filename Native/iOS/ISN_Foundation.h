#import "JSONModel.h"

//--------------------------------------
// Extentions
//--------------------------------------


@interface NSData (Base64)
+ (NSData *)InitFromBase64String:(NSString *)aString;
- (NSString *)AsBase64String;
@end

@interface NSDictionary (JSON)
- (NSString *)AsJSONString;
@end


//--------------------------------------
// Mono Callback
//--------------------------------------

typedef const void* UnityAction;
void SendCallbackDataToUnity(UnityAction callback, NSString* data);



//--------------------------------------
// extern "C"
//--------------------------------------


#ifdef __cplusplus
extern "C" {
#endif
    char* SA_ConvertToChar(NSString* nsString);
    NSString* ISN_ConvertToString(char* data);
    NSString* ISN_ConvertBoolToString(BOOL value);
    
    void ISN_SendCallbackToUnity(UnityAction callback, NSString* data);
    NSString* ISN_ConvertToBase64(NSData* data);

    // MARK: Data Transderer
    void* ISN_GetPointerForFile(char *url, int *size);
    void* ISN_GetDataPointerFromBuffer(int hash, int* size);
    int ISN_SaveDataByPointerInBuffer(CFTypeRef pointer, int size);
    void* ISN_GetDataByPointer(CFTypeRef pointer, int size);
    int ISN_SaveDataInBuffer(NSData *data);
    void ISN_ClearBuffer();
    void ISN_ReleaseData(void* pointer);
    void ISN_RemoveDataFromBuffer(int hash);
    NSData* ISN_SendFileByPointer(CFTypeRef pointer, int size);
    
#if __cplusplus
}   // Extern C
#endif

