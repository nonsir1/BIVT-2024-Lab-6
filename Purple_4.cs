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
                _time = 0;
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
            public Sportsman[] Sportsmen => _sportsmen;

            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
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
                Array.Resize(ref _sportsmen, _count + 1);
                _sportsmen[_count] = sportsman;
                _count++;
            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null) return;
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
                if (_sportsmen == null || _count == 0) return;
                _sportsmen = _sportsmen.OrderBy(s => s.Time).ToArray();
            }

            public static Group Merge(Group group1, Group group2)
            {
                group1.Sort();
                group2.Sort();

                Sportsman[] g1 = group1.Sportsmen;
                Sportsman[] g2 = group2.Sportsmen;
                int n1 = g1?.Length ?? 0, n2 = g2?.Length ?? 0;
                Sportsman[] merged = new Sportsman[n1 + n2];
                int i = 0, j = 0, k = 0;
                while (i < n1 && j < n2)
                {
                    if (g1[i].Time <= g2[j].Time)
                    {
                        merged[k++] = g1[i++];
                    }
                    else
                    {
                        merged[k++] = g2[j++];
                    }
                }
                while (i < n1)
                {
                    merged[k++] = g1[i++];
                }
                while (j < n2)
                {
                    merged[k++] = g2[j++];
                }
                Group result = new Group("Финалисты");
                result._sportsmen = merged;
                result._count = merged.Length;
                return result;
            }

            public void Print()
            {
                Console.WriteLine($"=========== {_name} ===========");
                if (_sportsmen != null)
                {
                    for (int i = 0; i < _count; i++)
                    {
                        _sportsmen[i].Print();
                    }
                }
            }
        }
    }
}