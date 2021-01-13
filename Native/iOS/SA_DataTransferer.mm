//
//  SA_DataTransferer.m
//  Unity-iPhone
//
//  Created by Roman on 5.01.2021.
//

#import <Foundation/Foundation.h>
#import "SA_Foundation.h"

static NSMutableDictionary *dataBuffer = nil;

extern "C" {
    void* SA_GetPointerForFile(char *url, int *size) {
        NSURL *nsurl = [NSURL fileURLWithPath:[NSString stringWithUTF8String:url]];
        NSError *error;
        NSMutableData *data = [NSMutableData dataWithContentsOfURL:nsurl options:NSDataReadingMapped error:&error];
        if (error) {
            NSLog(@"We got error when tried to read file - %@", error.description);
        }
        *size = (int)data.length;
        return [data mutableBytes];
    }
    
    void* SA_GetDataPointerFromBuffer(int hash, int* size) {
        if (dataBuffer) {
            NSNumber *num  = [NSNumber numberWithInt:hash];
            NSMutableData *data = [dataBuffer objectForKey:num];
            
            if (!data) return nil;
            
            *size = (int)data.length;
            return [data mutableBytes];
        }
        return nil;
    }
    
    void* SA_GetDataByPointer(CFTypeRef pointer, int size) {
        NSMutableData *data = [NSMutableData dataWithBytes:pointer length:size];
        return [data mutableBytes];
    }
    
    int SA_SaveDataByPointerInBuffer(CFTypeRef pointer, int size) {
        NSData *data = [NSData dataWithBytes:pointer length:(NSUInteger)size];
        return SA_SaveDataInBuffer(data);
    }
    
    int SA_SaveDataInBuffer(NSData *data) {
        NSInteger hash = data.hash;
        if(dataBuffer == nil) {
            dataBuffer = [[NSMutableDictionary alloc] init];
        }
        NSNumber* num = [NSNumber numberWithUnsignedInteger:hash];
        [dataBuffer setObject:data forKey:num];
        return num.intValue;
    }
    
    void SA_ClearBuffer() {
        dataBuffer = nil;
    }
    
    void SA_ReleaseData(void* pointer) {
        CFBridgingRelease(pointer);
    }
    
    void SA_RemoveDataFromBuffer(int hash) {
        NSNumber *num  = [NSNumber numberWithInt:hash];
        [dataBuffer removeObjectForKey:num];
    }
    
    NSData* SA_SendFileByPointer(CFTypeRef pointer, int size) {
        return [NSData dataWithBytes:pointer length:(NSUInteger)size];
    }
}
