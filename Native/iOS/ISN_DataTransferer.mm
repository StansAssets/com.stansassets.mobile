//
//  SA_DataTransferer.m
//  Unity-iPhone
//
//  Created by Roman on 5.01.2021.
//

#import <Foundation/Foundation.h>
#import "ISN_Foundation.h"

static NSMutableDictionary *dataBuffer = nil;

extern "C" {
    void* _ISN_GetPointerForFile(char *url, int *size) {
        NSURL *nsurl = [NSURL fileURLWithPath:[NSString stringWithUTF8String:url]];
        NSError *error;
        if (error) {
            NSLog(@"We got error when tried to read file - %@", error.description);
            return nil;
        }
        NSMutableData *data = [NSMutableData dataWithContentsOfURL:nsurl options:NSDataReadingMapped error:&error];
        *size = (int)data.length;
        return [data mutableBytes];
    }
    
    void* _ISN_GetDataPointerFromBuffer(int hash, int* size) {
        if (dataBuffer) {
            NSNumber *num  = [NSNumber numberWithInt:hash];
            NSMutableData *data = [dataBuffer objectForKey:num];
            
            if (!data) return nil;
            
            *size = (int)data.length;
            return [data mutableBytes];
        }
        return nil;
    }
    
    void* _ISN_GetDataByPointer(CFTypeRef pointer, int size) {
        NSMutableData *data = [NSMutableData dataWithBytes:pointer length:(NSUInteger)size];
        return [data mutableBytes];
    }
    
    void* _ISN_GetNSMutableDataByPointer(NSMutableData* rawData,  int* size) {
        *size = (int)rawData.length;
        return [rawData mutableBytes];
    }
    
    int _ISN_SaveDataByPointerInBuffer(CFTypeRef pointer, int size) {
        NSData *data = [NSData dataWithBytes:pointer length:(NSUInteger)size];
        return ISN_SaveDataInBuffer(data);
    }
    
    int _ISN_SaveDataInBuffer(NSData *data) {
        NSInteger hash = data.hash;
        if(dataBuffer == nil) {
            dataBuffer = [[NSMutableDictionary alloc] init];
        }
        NSNumber* num = [NSNumber numberWithUnsignedInteger:hash];
        [dataBuffer setObject:data forKey:num];
        return num.intValue;
    }
    
    void _ISN_ClearBuffer() {
        dataBuffer = nil;
    }
    
    void _ISN_ReleaseData(void* pointer) {
        CFBridgingRelease(pointer);
    }
    
    void _ISN_RemoveDataFromBuffer(int hash) {
        NSNumber *num  = [NSNumber numberWithInt:hash];
        [dataBuffer removeObjectForKey:num];
    }
    
    NSData* _ISN_SendFileByPointer(CFTypeRef pointer, int size) {
        return [NSData dataWithBytes:pointer length:(NSUInteger)size];
    }
}
