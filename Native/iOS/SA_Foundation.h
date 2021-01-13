#import "SA_JSONModel.h"

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
    NSString* SA_ConvertToString(char* data);
    NSString* SA_ConvertBoolToString(BOOL value);
    
    void SA_SendCallbackToUnity(UnityAction callback, NSString* data);
    NSString* SA_ConvertToBase64(NSData* data);

    // MARK: Data Transderer
    void* SA_GetPointerForFile(char *url, int *size);
    void* SA_GetDataPointerFromBuffer(int hash, int* size);
    int SA_SaveDataByPointerInBuffer(CFTypeRef pointer, int size);
    void* SA_GetDataByPointer(CFTypeRef pointer, int size);
    int SA_SaveDataInBuffer(NSData *data);
    void SA_ClearBuffer();
    void SA_ReleaseData(void* pointer);
    void SA_RemoveDataFromBuffer(int hash);
    NSData* SA_SendFileByPointer(CFTypeRef pointer, int size);
    
#if __cplusplus
}   // Extern C
#endif

