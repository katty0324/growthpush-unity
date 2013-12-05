//
//  GrowthPush.mm
//
//
//  Created by Cuong Do on 11/5/13.
//
//

#import <UIKit/UIKit.h>
#import <GrowthPush/GrowthPush.h>

/*
typedef void (*DidRequestDeviceToken)(const char*);

@interface UnityGrowthPushAppDelegate : NSObject <UIApplicationDelegate>
@property DidRequestDeviceToken didRequestDeviceToken;
@end


@implementation UnityGrowthPushAppDelegate
@synthesize didRequestDeviceToken;

- (void) willPerformApplication:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    didRequestDeviceToken((const char*)[deviceToken bytes]);
}

- (void) willPerformApplication:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error {
    didRequestDeviceToken(nil);
}

@end
*/

//static UnityGrowthPushAppDelegate *UnityGPInstance = nil;

extern "C" void _easyGrowthPush_setApplicationId(int appID, const char* secrect, bool debug)
{
    NSString* str_secrect = [NSString stringWithCString:secrect encoding:NSUTF8StringEncoding];
    [EasyGrowthPush setApplicationId:appID secret:str_secrect environment:kGrowthPushEnvironment debug:debug];
}

extern "C" void _easyGrowthPush_setApplicationId_option(int appID, const char* secrect, bool debug, int option)
{
    NSString* str_secrect = [NSString stringWithCString:secrect encoding:NSUTF8StringEncoding];
    [EasyGrowthPush setApplicationId:appID secret:str_secrect environment:kGrowthPushEnvironment debug:debug option:option];
}

extern "C" void _growthPush_setApplicationId(int appID, const char* secrect, int environment, bool debug, int option)
{
    NSString* str_secrect = [NSString stringWithCString:secrect encoding:NSUTF8StringEncoding];
    [GrowthPush setApplicationId:appID secret:str_secrect environment:environment debug:debug];
}

/*
extern "C" void _growthPush_requestDeviceToken(DidRequestDeviceToken callback)
{
    if(UnityGPInstance == nil)
    {
        UnityGPInstance = [[UnityGrowthPushAppDelegate alloc] init];
        UnityGPInstance.didRequestDeviceToken = callback;
    }
    
    [GrowthPush requestDeviceToken];
}
*/

extern "C" void _growthPush_setDeviceToken(const char* deviceToken)
{
    [GrowthPush setDeviceToken:[NSData dataWithBytes:deviceToken length:strlen(deviceToken)]];
}


extern "C" void _growthPush_trackEvent(const char* name)
{
    [GrowthPush trackEvent:[NSString stringWithCString:name encoding:NSUTF8StringEncoding]];
}

extern "C" void _growthPush_trackEvent_value(const char* name, const char* value)
{
    [GrowthPush trackEvent:[NSString stringWithCString:name encoding:NSUTF8StringEncoding]
                     value:[NSString stringWithCString:value encoding:NSUTF8StringEncoding]];
}

extern "C" void _growthPush_setTag(const char* name)
{
    [GrowthPush setTag:[NSString stringWithCString:name encoding:NSUTF8StringEncoding]];
}

extern "C" void _growthPush_setTag_value(const char* name, const char* value)
{
    [GrowthPush setTag:[NSString stringWithCString:name encoding:NSUTF8StringEncoding]
                 value:[NSString stringWithCString:value encoding:NSUTF8StringEncoding]];
}

extern "C" void _growthPush_setDeviceTags()
{
    [GrowthPush setDeviceTags];
}

extern "C" void _growthPush_clearBadge()
{
    [GrowthPush clearBadge];
}