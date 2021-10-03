using LINQ_IEnumerable_IQueryable_Yield.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_IEnumerable_IQueryable_Yield
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Where
            //WhereDemo();
            #endregion

            #region Select and SelectMany

            var customers = new[]
            {
                new Customer()
                {
                    Id = 1,
                    Name = "Cristian",
                    Phones = new[] { new Phone {Number="1324",Type= PhoneType.Cell}, new Phone { Number = "23131", Type = PhoneType.Cell } , new Phone { Number = "4525325", Type = PhoneType.Home } }
                },
                 new Customer()
                {
                     Id=2,
                    Name = "Andrei",
                    Phones = new[] { new Phone {Number="13232",Type= PhoneType.Cell}, new Phone { Number = "2111", Type = PhoneType.Cell } }
                }
            };

           // var customerNames = customers.Select(c => c.Name);

            var customerNames = customers.MySelect(c => c.Name);

            foreach (var name in customerNames)
            {
               // Console.WriteLine(name);
            }


            //var customerPhones = customers.SelectMany(c => c.Phones);
            var customerPhones = customers.MySelectMany(c => c.Phones);

            foreach (var phone in customerPhones)
            {
               // Console.WriteLine(phone.Number);
            }

            #endregion

            #region Join

            var addresses = new[]
           {
                new Address{Id=1, CustomerId=2, Street="123 Street", City="City1"},
                new Address {Id=2, CustomerId=2, Street="457 Street", City="City2"}
            };

            var customerwithaddress = customers.NewJoin(addresses
                , c => c.Id
                , a => a.CustomerId
                , (c, a) => new { c.Name, a.Street, a.City });

            foreach (var item in customerwithaddress)
            {
                Console.WriteLine($"{item.Name} - {item.Street} - {item.City}");
            }
            #endregion
            #region Take

            IEnumerable<int> items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //var firstNthItems = items.Take(3);

            var firstNthItems = items.MyTake(3);

            foreach (var item in firstNthItems)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Taming the Infinite using IEnumerable

            var mySequence = EnumerateFibonacci()
                .Skip(10)
                .Take(10);


            foreach(var value in mySequence)
            {
              //  Console.WriteLine(value);
            }



            #endregion

            #region Piping filters to IEnumerable before materialization (deffered execution)
            //Same deffered execution happens when using IQueryable , piping multiple filters on an IQueryable and the SQL is being generated only when materialized.


            IEnumerable<int> rawList = ReturnRawList();

            IEnumerable<int> onlyEvenNumbers = Filter1(rawList);

            IEnumerable<int> onlyEvenNumbersWithout8 = Filter2(onlyEvenNumbers);

            IEnumerable<int> onlyEvenNumbersWithout8AndGreatedThan6 = Filter3(onlyEvenNumbersWithout8);


            //Forcing materialization because of foreach's moveNext() call
            foreach(var item in onlyEvenNumbersWithout8AndGreatedThan6)
            {
              // Console.WriteLine(item);
            }

            #endregion
        }

        private static IEnumerable<int> Filter2(IEnumerable<int> list)
        {
            return list.Where(item => item != 8);
        }
        private static IEnumerable<int> Filter3(IEnumerable<int> list)
        {
            return list.Where(item => item > 6 );
        }

        private static IEnumerable<int> Filter1(IEnumerable<int> rawList)
        {
            return rawList.Where(item => item % 2 == 0);
        }

        private static IEnumerable<int> ReturnRawList()
        {
            IEnumerable<int> items = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            return items;
        }

        public static IEnumerable<long> EnumerateFibonacci()
        {
            long current = 1;
            long previous = 0;

            while (true)
            {
                yield return current;

                long temp = previous;
                previous = current;
                current += temp;
            }
        }
        private static void WhereDemo()
        {
            var items = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            /*
            var evenItems = items.Where(item => item % 2 == 0);
            */

            var evenItems = items.MyWhere(item => item % 2 == 0);//breakpoint wont be hit in MyWhere at this line because of the yield keyword
            foreach (var item in evenItems)
            {
                Console.WriteLine(item);
            }
        }
    }
}
