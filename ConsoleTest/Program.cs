using System;
using System.Collections.Generic;
using Algorithms;

namespace ConsoleTest
{
    public class UnitOfWork
    {
        public void DoSmthAwesome(int value)
        {
            Console.WriteLine("Awesome! " + value);
        }

        public void DoSmthBoring(int value)
        {
            Console.WriteLine("Boring " + value);
        }
    }

    public class Api
    {
        int kintamasis;
        int value;

        UnitOfWork u;

        public static Action<int> DoSomething;

        public Api(int kintamasis)
        {
            this.kintamasis = kintamasis;
            u = new UnitOfWork();

            switch (kintamasis)
            {
                case 0:
                    DoSomething = new Action<int>(u.DoSmthAwesome);
                    break;
                case 1:
                    DoSomething = new Action<int>(u.DoSmthBoring);
                    break;
            }

        }

        public void dok()
        {
            Api.DoSomething(value);
        }
    }


    public static


    class Program
    {
        
        static void Main(string[] args)
        {
            //Api api = new Api(1);
            //api.dok();

            List<int> listq = new List<int>();
            listq

                List<int> list = new List<int>();
            var list2 = new List<int>();
            

            Algorithm algorithm = new Algorithm(10, 10, 10, 10, FirstPopulationType.Random, MutateType.Smart);

            Console.WriteLine(algorithm.ToStringAll());
        }

        
    }
}
