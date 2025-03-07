using System;
using System.Linq;

namespace Lab_6
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _firstName;
            private string _lastName;
            private double[] _marks;
            private int[] _places;
            private int _judgeCount;

            public string Name => _firstName;
            public string Surname => _lastName;
            public double[] Marks => _marks ?? new double[0];
            public int[] Places => _places ?? new int[0];
            public int Score => _places?.Sum() ?? 0;
            public int TopPlace => _places?.Length > 0 ? _places.Min() : 0;
            public double TotalMark => _marks?.Sum() ?? 0;

            public Participant(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _marks = new double[7];
                _places = new int[7];
                _judgeCount = 0;
            }

            public void Evaluate(double result)
            {
                if (_judgeCount < 7)
                {
                    _marks[_judgeCount] = result;
                    _judgeCount++;
                }
            }

            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                for (int judge = 0; judge < 7; judge++)
                {
                    var sorted = participants.Where(p => p.Marks.Length > judge)
                                             .Select((p, index) => new { Index = index, Score = p.Marks[judge] })
                                             .OrderByDescending(p => p.Score)
                                             .ToArray();
                    
                    for (int rank = 0; rank < sorted.Length; rank++)
                    {
                        participants[sorted[rank].Index]._places[judge] = rank + 1;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                Array.Sort(array, (a, b) =>
                {
                    int scoreComparison = a.Score.CompareTo(b.Score);
                    if (scoreComparison != 0) return scoreComparison;
                    
                    for (int i = 0; i < 7; i++)
                    {
                        if (a.Places.Length > i && b.Places.Length > i && a.Places[i] != b.Places[i])
                            return a.Places[i].CompareTo(b.Places[i]);
                    }
                    
                    return b.TotalMark.CompareTo(a.TotalMark);
                });
            }

            public void Print()
            {
                Console.WriteLine($"{_firstName} {_lastName}");
                Console.Write("Marks: ");
                foreach (double mark in Marks)
                {
                    Console.Write(mark + "  ");
                }
                Console.WriteLine();
                Console.Write("Places: ");
                foreach (int place in Places)
                {
                    Console.Write(place + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Total Score: " + Score);
                Console.WriteLine("Top Place: " + TopPlace);
                Console.WriteLine("Total Marks: " + TotalMark);
                Console.WriteLine();
            }
        }
    }
}
