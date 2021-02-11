#import "ISN_Foundation.h"

//--------------------------------------
// Extentions
//--------------------------------------

@implementation NSData (Base64)
+ (NSData *)InitFromBase64String:(NSString *)aString {
    return [[NSData alloc] initWithBase64EncodedString:aString options:NSDataBase64DecodingIgnoreUnknownCharacters];
}

- (NSString *)AsBase64String {
    return [self base64EncodedStringWithOptions:0];
}
@end


@implementation NSDictionary (JSON)
- (NSString *)AsJSONString {
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:self options:0  error:&error];
    if (!jsonData) {
        return @"{}";
    } else {
        return [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    }
}
@end


//--------------------------------------
// Mono Callback
//--------------------------------------

typedef void (*MonoPCallbackDelegate)(UnityAction action, const char* data);

static MonoPCallbackDelegate _monoPCallbackDelegate = NULL;

FOUNDATION_EXPORT void ISN_RegisterCallbackDelegate(MonoPCallbackDelegate callbackDelegate) {
    _monoPCallbackDelegate = callbackDelegate;
}

//--------------------------------------
// extern "C"
//--------------------------------------

static NSMutableDictionary *objectsRefStorage = nil;

#ifdef __cplusplus
extern "C" {
#endif

    int _ISN_SaveObjectRef(NSObject* object) {

        if(objectsRefStorage == nil) {
            objectsRefStorage = [[NSMutableDictionary alloc] init];
        }

        NSUInteger hash =[object hash];
        NSNumber* num = [NSNumber numberWithUnsignedInteger:hash];
        [objectsRefStorage setObject:object forKey:num];

        return num.intValue;
    }

    NSObject* _ISN_GetObjectRef(int hash) {
        NSNumber *num  = [NSNumber numberWithInt:hash];
        return [objectsRefStorage objectForKey:num];
    }
    
    char* _ISN_ConvertToChar(NSString* nsString) {
        const char* string = [nsString UTF8String];
        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }

    NSString* _ISN_ConvertBoolToString(BOOL value) {
        return value ? @"true" : @"false";
    }

    NSString* _ISN_ConvertToString(char* data) {
        return data == NULL ? [NSString stringWithUTF8String: ""] : [NSString stringWithUTF8String: data];
    }

    NSString* _ISN_ConvertToBase64(NSData* data) {
        return [data base64EncodedStringWithOptions:0];
    }

    // Этот метод можно объявить в каком-нибудь классе
    void _ISN_SendCallbackToUnity(UnityAction callback, NSString* data) {
        if(callback == NULL)
            return;

        if(data == NULL) {
            data = @"";
        }
//        [ISN_Logger LogCallbackInvoke:data];

        // Переводим исполнение в Unity (главный) поток
        dispatch_async(dispatch_get_main_queue(), ^{
            if(_monoPCallbackDelegate != NULL)
                _monoPCallbackDelegate(callback, [data cStringUsingEncoding:NSUTF8StringEncoding]);
        });
    }
#if __cplusplus
}   // Extern C
#endif











