// TestCppBasics.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string.h>

using namespace std;

class student {
public :
	char* name;
	char* addr;
	long long number;
public :
	student() {}
	student(char* pn, char* pa, long long n) {
		cout << "构造函数" << endl;
		name = new char[32];
		strcpy(name, pn);
		addr = new char[32];
		strcpy(addr, pa);
		number = n;
	}
	~student() {
		cout << "析构函数 name=" << name << endl;
		delete[] name;
		delete[] addr;
	}
	void print() {
		cout << "name = " << name << endl;
		cout << "addr = " << addr << endl;
		cout << "number = " << number << endl;
	}
};

struct {
	char name[40];
	int age;
} person, person_copy;

int main(void)
{
	/*student *ps;
	ps = new student("wkfx", "Shenzhen", 13411111111);
	student stud(*ps);
	delete ps;
	stud.print();*/
	

	/*
	cout << "bool:" << typeid(bool).name() << endl;
	cout << "char:" << typeid(char).name() << endl;
	cout << "short:" << typeid(short).name() << endl;
	cout << "int:" << typeid(int).name() << endl;
	cout << "long:" << typeid(long).name() << endl;
	cout << "float:" << typeid(float).name() << endl;
	cout << "double:" << typeid(double).name() << endl;
	char a = 129;
	printf("%d\n", a);
	int n1 = 10;
	int n2 = 0x0A;
	printf("n1=%d, n2=%d\n", n1, n2);
	double b1 = 12.34; //8个字节
	float f1 = 12.34; //没带f，提示警告，从“double”到“float”截断
	float f2 = 12.34f; //4个字节
	float f;
	scanf("%f", &f);
	printf("%f\n", f);
	*/

	////char与ASCII码输出，及char数组
	//printf("%c %c %c\n", 1, '1', 49); //ASCII码为1没有对应的字符

	//int a = 10;
	//printf("%d \n", a > 11); //0
	//printf("%c\n", 65);
	//char ch1 = 65;
	//char ch2 = 'A';
	//char ch3 = 0x41;
	//printf("%d\n", ch1);
	//printf("%d\n", ch2);
	//printf("%d\n", ch3);
	////字符数组
	//char str1[6] = { 'H', 'e', 'l', 'l', 'o', '\0'};
	//char str2[6] = "Hello"; //注：右边字符个数必须要 小于 左边定义的字符数组个数
	//printf("%s\n", str1);
	//printf("%s\n", str2);

	//int a = 4;
	//int b = 6;
	//int *pa = &a;
	//int *pb = &b;
	////std::swap<int>(a, b); //C++中的模板，调用时可省去<类型>
	//std::swap(a, b);
	//cout << a << '\t' << b << endl;
	//cout << *pa << '\t' << *pb << endl;

	//std::swap<int*>(pa, pb); //交换的是指针的指向
	////std::swap(pa, pb);
	//std::cout << a << '\t' << b << std::endl;
	//std::cout << *pa << '\t' << *pb << std::endl;

	//int *p = new int;
	//*p = 121;
	/*int *p = new int(121);
	int *pa = new int[5];
	memset(pa, 1, 5 * sizeof(int));
	cout << *p << endl;
	cout << pa[0] << endl;*/


	/////测试memcpy的使用
	//char myname[] = "Pierre de Fermat";
	//memcpy(person.name, myname, strlen(myname) + 1); //使用memcpy拷贝字符串
	//person.age = 46;

	//memcpy(&person_copy, &person, sizeof(person)); //使用memcpy拷贝结构体对象

	//printf("person_copy:%s,%d \n", person_copy.name, person_copy.age);



	/*float money;
	int i = 1;
	int year;
	printf("第一年存多少钱？\n");
	scanf("%f", &money);
	getchar();
	printf("存多少年?\n");
	scanf("%d", &year);
	while (i <= year) {
		money = money * 1.05;
		i = i + 1;
	}
	printf("%d年后取出%f\n",year, money);*/

	//system("pause");
	getchar();
	return 0;
}