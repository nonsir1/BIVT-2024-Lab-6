using System;
using System.Linq;

namespace Lab_6
{
    public class Purple_4
    {
        public struct Sportsman
        {
            private string _firstName;
            private string _lastName;
            private double _time;
            private bool _run;

            public string Name => _firstName;
            public string Surname => _lastName;
            public double Time => _time;

            public Sportsman(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _time = double.MaxValue;
                _run = false;
            }

            public void Run(double time)
            {
                if (!_run)
                {
                    _time = time;
                    _run = true;
                }
            }

            public void Print()
            {
                if (_run)
                    Console.WriteLine($"{_firstName} {_lastName} - Time: {_time}");
                else
                    Console.WriteLine($"{_firstName} {_lastName} - Time: DNS");
            }
        }

        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen.Take(_count).ToArray();
            public int Count => _count;

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
                _count = 0;
            }

            public Group(Group group)
            {
                _name = group._name;
                _sportsmen = group._sportsmen.Take(group._count).ToArray();
                _count = group._count;
            }

            public void Add(Sportsman sportsman)
            {
                if (_count >= _sportsmen.Length)
                {
                    Array.Resize(ref _sportsmen, Math.Max(1, _sportsmen.Length * 2));
                }
                _sportsmen[_count++] = sportsman;
            }

            public void Add(Sportsman[] sportsmen)
            {
                foreach (var sportsman in sportsmen)
                {
                    Add(sportsman);
                }
            }

            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }

            public void Sort()
            {
                Array.Sort(_sportsmen, 0, _count, Comparer<Sportsman>.Create((s1, s2) => s1.Time.CompareTo(s2.Time)));
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group mergedGroup = new Group("Финалисты");
                mergedGroup.Add(group1);
                mergedGroup.Add(group2);
                mergedGroup.Sort();
                return mergedGroup;
            }

            public void Print()
            {
                Console.WriteLine($"=========== {_name} ===========");
                for (int i = 0; i < _count; i++)
                {
                    _sportsmen[i].Print();
                }
            }
        }
    }
}
