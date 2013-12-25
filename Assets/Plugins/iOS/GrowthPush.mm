//
//  GrowthPush.mm
//
//
//  Created by Cuong Do on 11/5/13.
//
//

#import <UIKit/UIKit.h>
#import <GrowthPush/GrowthPush.h>
#import "App42PushHandlerInternal.h"

NSString* NSStringFromCharString(const char* in_str) {

    NSString* str = [NSString stringWithCString:in_str encoding:NSUTF8StringEncoding];
    return str;

}

extern "C" void _easyGrowthPush_setApplicationId(int applicationID, const char* secret, int environment, bool debug, int option) {

    [EasyGrowthPush setApplicationId:applicationID secret:NSStringFromCharString(secret) environment:environment debug:debug option:option];

}

extern "C" void _growthPush_setApplicationId(int applicationID, const char* secret, int environment, bool debug, int option) {

    [GrowthPush setApplicationId:applicationID secret:NSStringFromCharString(secret) environment:environment debug:debug];

}

extern "C" void _easyGrowthPush_requestDeviceToken() {

    [EasyGrowthPush requestDeviceToken];

}

extern "C" void _easyGrowthPush_setDeviceToken(const char* deviceToken) {

    NSString *str = NSStringFromCharString(deviceToken);
    str = [str lowercaseString];
    NSMutableData *data= [NSMutableData new];
    unsigned char whole_byte;
    char byte_chars[3] = {'\0','\0','\0'};
    int i = 0;
    int length = str.length;
    while (i < length-1) {
        char c = [str characterAtIndex:i++];
        if (c < '0' || (c > '9' && c < 'a') || c > 'f')
            continue;
        byte_chars[0] = c;
        byte_chars[1] = [str characterAtIndex:i++];
        whole_byte = strtol(byte_chars, NULL, 16);
        [data appendBytes:&whole_byte length:1];
        
    }

    [EasyGrowthPush setDeviceToken:data];

}


extern "C" void _easyGrowthPush_trackEvent(const char* name) {

    [EasyGrowthPush trackEvent:NSStringFromCharString(name)];

}

extern "C" void _easyGrowthPush_trackEvent_value(const char* name, const char* value) {

    [EasyGrowthPush trackEvent:NSStringFromCharString(name) value:NSStringFromCharString(value)];

}

extern "C" void _easyGrowthPush_setTag(const char* name) {

    [EasyGrowthPush setTag:NSStringFromCharString(name)];

}

extern "C" void _easyGrowthPush_setTag_value(const char* name, const char* value) {

    [EasyGrowthPush setTag:NSStringFromCharString(name) value:NSStringFromCharString(value)];

}

extern "C" void _easyGrowthPush_setDeviceTags() {

    [EasyGrowthPush setDeviceTags];

}

extern "C" void _easyGrowthPush_clearBadge() {

    [EasyGrowthPush clearBadge];

}