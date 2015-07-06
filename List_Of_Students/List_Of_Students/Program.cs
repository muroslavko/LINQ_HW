using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace List_Of_Students
{
    class Program
    {
        static Program()
        {
            Users = new List<User>();
            TestWorks = new List<TestWork>();
        }
        public static List<User> Users { get; set; }
        public static List<TestWork> TestWorks { get; set; }
        static void Main(string[] args)
        {
            CreateData();

            //Список людей, которые прошли тесты
            var listPass = from item in TestWorks
                           where item.Mark >= item.Test.PassMark
                           group item.User by item.User.Name
                               into u
                               select u.First();
            foreach (var item in listPass)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            //Список тех, кто прошли тесты успешно и уложилися во время.
            var listPassTime = from item in TestWorks
                               where item.Time <= item.Test.MaxTime && item.Mark >= item.Test.PassMark
                               group item.User by item.User.Name
                                   into u
                                   select u.First();
            foreach (var item in listPassTime)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            //Список людей, которые прошли тесты успешно и не уложились во время
            var listPassButTime = from item in TestWorks
                                  where item.Time > item.Test.MaxTime && item.Mark >= item.Test.PassMark
                                  group item.User by item.User.Name
                                      into u
                                      select u.First();
            foreach (var item in listPassButTime)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine();
            //Список студентов по городам. (Из Львова: 10 студентов, из Киева: 20)
            var listFrom = new List<User>();
            listFrom.AddRange(Users.Where(item => item.Sity == "Lviv").Take(10));
            listFrom.AddRange(Users.Where(item => item.Sity == "Kyiv").Take(20));
            foreach (var item in listFrom)
            {
                Console.WriteLine("{0}  {1}", item.Name, item.Sity);
            }
            Console.WriteLine();
            //Список успешных студентов по городам.
            var listPassFrom = (from u in TestWorks
                                where (u.Time <= u.Test.MaxTime && u.Mark >= u.Test.PassMark)
                                group u.User by u.User.Name
                                    into g
                                    select g.First()).OrderBy(x => x.Sity);
            foreach (var item in listPassFrom)
            {
                Console.WriteLine("{0}  {1}", item.Name, item.Sity);
            }
            Console.WriteLine();
            //Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории.
            var test1 = from item in TestWorks
                        group item by item.User.Name
                            into g
                            select new
                            {
                                Name = g.Key,
                                Works = g.Select(item =>
                                    new
                                    {
                                        Name = item.Test.Name,
                                        Mark = item.Mark,
                                        Time = item.Time,
                                        Persent = item.Mark * 10
                                    })
                            };
            foreach (var nameGroup in test1)
            {
                Console.WriteLine("Name: {0}", nameGroup.Name);
                foreach (var test in nameGroup.Works)
                {
                    Console.WriteLine("\t{0}, {1}, {2}%, {3}", test.Name, test.Mark, test.Persent,test.Time);
                }
            }
            Console.ReadLine();
        }

        static void CreateData()
        {
            var user1 = new User() { Name = "Oleh", Sity = "Lviv" };
            var user2 = new User() { Name = "Petro", Sity = "Kyiv" };
            var user3 = new User() { Name = "Vasyl", Sity = "Lviv" };
            var user4 = new User() { Name = "Maxim", Sity = "Kyiv" };
            var user5 = new User() { Name = "Dmytro", Sity = "Lviv" };
            var user6 = new User() { Name = "Oleksander", Sity = "Lviv" };
            var user7 = new User() { Name = "Mykhailo", Sity = "Lviv" };
            var user8 = new User() { Name = "Yevheniya", Sity = "Lviv" };
            var user9 = new User() { Name = "Lyubochka", Sity = "Lviv" };
            var user10 = new User() { Name = "Mykhaila", Sity = "Lviv" };
            var user11 = new User() { Name = "Marina", Sity = "Lviv" };
            var user12 = new User() { Name = "Mykhaila", Sity = "Kyiv" };
            var user13 = new User() { Name = "Oleh", Sity = "Kyiv" };
            var user14 = new User() { Name = "Petro", Sity = "Kyiv" };
            var user15 = new User() { Name = "Vasyl", Sity = "Kyiv" };
            var user16 = new User() { Name = "Maxim", Sity = "Lviv" };
            var user17 = new User() { Name = "Dmytro", Sity = "Kyiv" };
            var user18 = new User() { Name = "Oleksander", Sity = "Kyiv" };
            var user19 = new User() { Name = "Mykhailo", Sity = "Kyiv" };
            var user20 = new User() { Name = "Yevheniya", Sity = "Kyiv" };
            var user21 = new User() { Name = "Lyubochka", Sity = "Lviv" };
            var user22 = new User() { Name = "Mykhaila", Sity = "Kyiv" };
            var user23 = new User() { Name = "Marina", Sity = "Kyiv" };
            Users.AddRange(new List<User>()
            {
                user1,user2,user3,user4,user5,user6,user7,user8,user9,user10,user11,user12,
                user13,user14,user15,user16,user17,user18,user19,user20,user21,user22,user23
            });
            var test1 = new Test() { Categoty = Category.DB, MaxTime = 60, PassMark = 5, Name = "SQL" };
            var test2 = new Test() { Categoty = Category.JS, MaxTime = 60, PassMark = 5, Name = "JS Basic" };
            var test3 = new Test() { Categoty = Category.Net, MaxTime = 60, PassMark = 5, Name = "Net Basic" };
            var test4 = new Test() { Categoty = Category.PHP, MaxTime = 60, PassMark = 5, Name = "PHP Basic" };
            var test5 = new Test() { Categoty = Category.OOP, MaxTime = 60, PassMark = 5, Name = "OOP Basic" };

            TestWorks.AddRange(
                new List<TestWork>()
                {
                    new TestWork(){Mark = 4,Test=test1,Time= 45,User=user1},
                    new TestWork(){Mark = 10,Test=test1,Time= 45,User=user2},
                    new TestWork(){Mark = 4,Test=test1,Time= 45,User=user4},
                    new TestWork(){Mark = 10,Test=test1,Time= 45,User=user5},
                    new TestWork(){Mark = 10,Test=test3,Time= 45,User=user5},
                    new TestWork(){Mark = 4,Test=test5,Time= 45,User=user5},
                    new TestWork(){Mark = 10,Test=test1,Time= 61,User=user6}
                }
                );
        }
    }
}
