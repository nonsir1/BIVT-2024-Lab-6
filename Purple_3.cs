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
            public double[] Marks
            {
                get
                {
                    if (_marks == null)
                        return null;
                    double[] copy = new double[_marks.Length];
                    Array.Copy(_marks, copy, _marks.Length);
                    return copy;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null)
                        return null;
                    int[] copy = new int[_places.Length];
                    Array.Copy(_places, copy, _places.Length);
                    return copy;
                }
            }
            public int Score => _places == null ? 0 : _places.Sum();

            private int TopPlace => _places == null ? 0 : _places.Min();
            private double TotalSum => _marks == null ? 0 : _marks.Sum();

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
                
                for (int i = 0; i < 7; i++)
                {
                    Participant[] arr = participants
                        .OrderByDescending(p => p._marks[i])
                        .ThenBy(p => (p._places != null && p._places.Length > 0) ? p._places[p._places.Length - 1] : int.MaxValue)
                        .ToArray();
                    
                    Array.Copy(arr, participants, participants.Length);
                    
                    for (int j = 0; j < participants.Length; j++)
                    {
                        participants[j]._places[i] = j + 1;
                    }
                }
                Participant[] finalSorted = participants
                    .OrderBy(p => (p._places != null && p._places.Length > 0) ? p._places[p._places.Length - 1] : int.MaxValue)
                    .ToArray();
                Array.Copy(finalSorted, participants, participants.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                var sorted = array
                    .OrderBy(p => p.Score)
                    .ThenBy(p => p.TopPlace)
                    .ThenByDescending(p => p.TotalSum)
                    .ToArray();
                Array.Copy(sorted, array, sorted.Length);
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
                Console.WriteLine();
            }
        }
    }
}
