#ifndef SINGLETON
#define SINGLETON

//参考 https://www.toutiao.com/article/6900836383498764803/?app=news_article&timestamp=1651973615&use_new_style=1&req_id=202205080933340101411300190C40F433&group_id=6900836383498764803&wxshare_count=1&tt_from=weixin&utm_source=weixin&utm_medium=toutiao_android&utm_campaign=client_share&share_token=c53864f4-bedc-45fd-bdbf-1889ba61aabc

class Singleton {
public:
	static Singleton& getInstance() {
		static Singleton instance;
		return instance;
	}

	void printTest() {

	}
private:
	Singleton(){} //防止外部调用构造创建对象
	Singleton(Singleton const& singleton); //阻止拷贝创建对象
	Singleton& operator=(Singleton const&singleton); //阻止赋值对象
};

#endif // !SINGLETON

//Singleton & Singleton::operator=(Singleton const & singleton)
//{
//	// TODO: 在此处插入 return 语句
//}
