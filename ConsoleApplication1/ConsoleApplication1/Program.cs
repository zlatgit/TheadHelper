using System;
using System.Threading;

namespace ThreadWithParameters
{
    class Program
    {
        //C# 给多线程传参的三种方式
        //方式一：使用ParameterizedThreadStart委托
        //方式二：创建自定义类
        //方式三：利用lambda表达式
        static void Main(string[] args)
        {
            Method1();
            Method2();
            Method3();
            Console.Read();
        }

        #region 方式一：使用ParameterizedThreadStart委托
        static void Method1()
        {
            string hello = "hello world";
            //这里也可简写成Thread thread = new Thread(ThreadMainWithParameters);
            //但是为了让大家知道这里用的是ParameterizedThreadStart委托，就没有简写了
            Thread thread = new Thread(new ParameterizedThreadStart(ThreadMainWithParameters));
            thread.Start(hello);
        }
        static void ThreadMainWithParameters(object obj)
        {
            string str = obj as string;
            if (!string.IsNullOrEmpty(str))
                Console.WriteLine("1.Running in a thread,received: {0}", str);
        }
        #endregion

        #region 方式二：创建自定义类
        static void Method2()
        {
            MyThread myThread = new MyThread("hello world");
            Thread thread = new Thread(myThread.ThreadMain);
            thread.Start();
        }
        #endregion

        #region 方式三：利用lambda表达式
        static void Method3()
        {
            string hello = "hello world";
            //如果写成Thread thread = new Thread(ThreadMainWithParameters(hello));这种形式，编译时就会报错
            Thread thread = new Thread(() => ThreadMainWithParameters(hello));
            thread.Start();
        }
        static void ThreadMainWithParameters(string str)
        {
            Console.WriteLine("3.Running in a thread,received: {0}", str);
        }
        #endregion
    }

    public class MyThread
    {
        private string data;
        public MyThread(string data)
        {
            this.data = data;
        }
        public void ThreadMain()
        {
            Console.WriteLine("2.Running in a thread,data: {0}", data);
        }
    }
}