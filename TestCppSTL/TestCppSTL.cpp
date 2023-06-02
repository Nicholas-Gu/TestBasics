#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

void myPrint(int val)
{
    cout << val << endl;
}

///vector存放内置数据类型时的，三种遍历方式
void testVectorWithInt()
{
    vector<int> v;
    v.push_back(10);
    v.push_back(20);
    v.push_back(30);
    v.push_back(40);

    //通过迭代器访问容器中的数据
    vector<int>::iterator itBegin = v.begin();//初始迭代器  指向容器中的第一个元素
    vector<int>::iterator itEnd = v.end();//结束迭代器 指向容器中最后一个元素的下一个位置

    cout << "vector(动态数组)的三种遍历方式：首尾迭代器+while循环" << endl;
    cout << "第一种遍历方式：" << endl;
    while (itBegin != itEnd)
    {
        cout << *itBegin << endl;
        itBegin++;
    }

    cout << "第二种遍历方式：其实是第一种的变体，把上面的整合到一起，且while改为for循环" << endl;
    for (vector<int>::iterator it=v.begin(); it!=v.end(); it++)
    {
        cout << *it << endl;
    }

    cout << "第三种遍历方式：利用STL提供的遍历算法for_each，注：要#include<algorithm>标准算法的头文件" << endl;
    for_each(v.begin(), v.end(), myPrint);
}

class Person 
{
public:
    string m_Name;
    int m_Age;
    Person(string name, int age)
    {
        this->m_Name = name;
        this->m_Age = age;
    }
};

//vector 容器存放自定义数据类型
void testVectorWithCustomClass()
{
    vector<Person> v;
    Person p1 = {"张三",10};
    Person p2 = {"王超",20};
    Person p3 = {"李哥",50};
    Person p4 = {"陆军",30};
    Person p5 = {"小明",40};
    v.push_back(p1);
    v.push_back(p2);
    v.push_back(p3);
    v.push_back(p4);
    v.push_back(p5);

    for (vector<Person>::iterator it = v.begin(); it != v.end(); it++)
    {
        cout << (*it).m_Name << ", " << it->m_Age << "岁" << endl; //(*it).和it->同样都能访问到成员变量
    }
}


int main()
{
    //std::cout << "Hello CPP STL!\n";
    
    //testVectorWithInt();
    testVectorWithCustomClass();

    getchar();
    return 0;
}