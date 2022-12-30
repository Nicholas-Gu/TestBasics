// Cpp播放器与状态模式.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//
#define _CRT_SECURE_NO_WARNINGS 1
#include <iostream>
#include "PlayerContext.h"

using namespace std;

int main()
{
    cout << "Cpp播放器与状态模式!\n";
	//参考：https://www.toutiao.com/article/7175388556536414759/?app=news_article&timestamp=1672374363&use_new_style=1&req_id=20221230122602475B404682021306EF28&group_id=7175388556536414759&pseries_type=0&pseries_style_type=2&pseries_id=7168301543152845351&wxshare_count=1&tt_from=weixin&utm_source=weixin&utm_medium=toutiao_android&utm_campaign=client_share&share_token=71da55e1-4654-432e-a92c-e5e872fe0f22&source=m_redirect

	
	//play_init();
	PlayerContext playerContext;
	int input;
	
	while (1) {
		cout << "Press key:\n";
		cout << " 0, Stop key:\n";
		cout << " 1, Play key:\n";
		cout << " 2, Pause key:\n";
		scanf("%d", &input);
		getchar();
		switch (input) {
			case 0:
				playerContext.stop();
				break;
			case 1:
				playerContext.play();
				break;
			case 2:
				playerContext.pause();
				break;
		}
	}

	return 0;
}