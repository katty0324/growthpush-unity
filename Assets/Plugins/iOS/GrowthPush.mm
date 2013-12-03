//
//  GrowthPush.mm
//
//
//  Created by Cuong Do on 11/5/13.
//
//

#import <UIKit/UIKit.h>
#import <GrowthPush/GrowthPush.h>

extern "C" void _easyGrowthPush_setApplicationId(int appID, const char* secrect, bool debug, int option)
{
    NSString* str_secrect = [NSString stringWithCString:secrect encoding:NSUTF8StringEncoding];
    [EasyGrowthPush setApplicationId:appID secret:str_secrect environment:kGrowthPushEnvironment debug:debug];
}

extern "C" void _easyGrowthPush_setApplicationId_option(int appID, const char* secrect, bool debug, int option)
{
    NSString* str_secrect = [NSString stringWithCString:secrect encoding:NSUTF8StringEncoding];
    [EasyGrowthPush setApplicationId:appID secret:str_secrect environment:kGrowthPushEnvironment debug:debug option:option];
}

