using System;
using System.Linq;

namespace Lab_6
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _firstName;
            private string _lastName;
            private int _jumpDistance;
            private int[] _styleScores;

            public string Name => _firstName;
            public string Surname => _lastName;
            public int Distance => _jumpDistance;
            public int[] Marks
            {
                get
                {
                    if (_styleScores == null) return default(int[]);
                    var copy = new int[_styleScores.Length];
                    Array.Copy(_styleScores, copy, _styleScores.Length);
                    return copy;
                }
            }

            public double Result
            {
                get
                {
                    if (_styleScores == null) return 0;
                    int[] tempArray = new int[5];
                    Array.Copy(_styleScores, tempArray, 5);
                    Array.Sort(tempArray);
                    int styleScore = tempArray.Skip(1).Take(3).Sum();
                    
                    int distanceScore = 60 + (_jumpDistance - 120) * 2;
                    if (distanceScore < 0) distanceScore = 0;
                    
                    return styleScore + distanceScore;
                }
            }

            public Participant(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _jumpDistance = 0;
                _styleScores = new int[5];
            }

            public void Jump(int distance, int[] marks)
            {
                if (marks == null || marks.Length != 5) return;
                _jumpDistance = distance;
                Array.Copy(marks, _styleScores, marks.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 1; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;
                    while (j >= 0 && array[j].Result < key.Result)
                    {
                        array[j + 1] = array[j];
                        j--;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_firstName} {_lastName}");
                Console.WriteLine($"Distance: {_jumpDistance}");
                Console.Write("Marks: ");
                foreach (int mark in _styleScores)
                {
                    Console.Write(mark + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Score: " + Result);
                Console.WriteLine();
            }
        }
    }
}
