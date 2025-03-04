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
            public double[] Marks => _marks;
            public int[] Places => _places;
            public int Score => _places.Sum();
            public int TopPlace => _places.Min();
            public double TotalMark => _marks.Sum();

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
                    var sorted = participants.Select((p, index) => new { Index = index, Score = p.Marks[judge] })
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
                for (int i = 1; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;
                    while (j >= 0)
                    {
                        int scoreComparison = array[j].Score.CompareTo(key.Score);
                        if (scoreComparison < 0)
                            break;
                        if (scoreComparison == 0)
                        {
                            for (int k = 0; k < 7; k++)
                            {
                                if (array[j].Places[k] != key.Places[k])
                                {
                                    if (array[j].Places[k] < key.Places[k])
                                        break;
                                }
                            }
                            if (array[j].Marks.Sum() > key.Marks.Sum())
                                break;
                        }
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_firstName} {_lastName}");
                Console.Write("Marks: ");
                foreach (double mark in _marks)
                {
                    Console.Write(mark + "  ");
                }
                Console.WriteLine();
                Console.Write("Places: ");
                foreach (int place in _places)
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
