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
                Console.WriteLine($"{_firstName} {_lastName} - Time: {_time}");
            }
        }

        public struct Group
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[10];
                _count = 0;
            }

            public Group(Group group)
            {
                _name = group._name;
                _sportsmen = new Sportsman[group._sportsmen.Length];
                Array.Copy(group._sportsmen, _sportsmen, group._sportsmen.Length);
                _count = group._count;
            }

            public void Add(Sportsman sportsman)
            {
                if (_count >= _sportsmen.Length)
                {
                    Array.Resize(ref _sportsmen, _sportsmen.Length * 2);
                }
                _sportsmen[_count] = sportsman;
                _count++;
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
                Add(group._sportsmen);
            }

            public void Sort()
            {
                for (int i = 1; i < _count; i++)
                {
                    Sportsman key = _sportsmen[i];
                    int j = i - 1;
                    while (j >= 0 && _sportsmen[j].Time > key.Time)
                    {
                        _sportsmen[j + 1] = _sportsmen[j];
                        j--;
                    }
                    _sportsmen[j + 1] = key;
                }
            }

            public static Group Merge(Group group1, Group group2)
            {
                Group mergedGroup = new Group("Финалисты");
                
                for (int i = 0; i < group1._count; i++)
                {
                    mergedGroup.Add(group1._sportsmen[i]);
                }
                
                for (int i = 0; i < group2._count; i++)
                {
                    mergedGroup.Add(group2._sportsmen[i]);
                }
            
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
