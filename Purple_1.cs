using System;
using System.Linq;

namespace Lab_6
{
    class Purple_1 {
        public struct Participant
        {
            private string _firstName;
            private string _lastName;
            private double[] _coef;
            private int[,] _scores;
            private int _attemptCount;

            public string Name => _firstName;
            public string Surname => _lastName;
            public double[] Coefs
            {
                get
                {
                    if (_coef == null) return default(double[]);
                    var copy = new double[_coef.Length];
                    Array.Copy(_coef, copy, _coef.Length);
                    return copy;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_scores == null) return default(int[,]);
                    var matrixCopy = new int[_scores.GetLength(0), _scores.GetLength(1)];
                    Array.Copy(_scores, matrixCopy, _scores.Length);
                    return matrixCopy;
                }
            }

            public double TotalScore
            {
                get
                {
                    if (_scores == null || _coef == null) return 0;
                    double total = 0;
                    for (int i = 0; i < _scores.GetLength(0); i++)
                    {
                        int[] tempArray = new int[7];
                        for (int j = 0; j < _scores.GetLength(1); j++)
                            tempArray[j] = _scores[i, j];
                        
                        Array.Sort(tempArray);
                        double s = tempArray.Skip(1).Take(5).Sum();
                        s *= _coef[i];
                        total += s;
                    }
                    return Math.Round(total, 2);
                }
            }

            public Participant(string name, string surname)
            {
                _firstName = name;
                _lastName = surname;
                _coef = new double[] { 2.5, 2.5, 2.5, 2.5 };
                _scores = new int[4, 7];
                _attemptCount = 0;
            }

            public void SetCriterias(double[] coefs)
            {
                if (coefs == null || coefs.Length != 4) return;
                Array.Copy(coefs, _coef, coefs.Length);
            }

            public void Jump(int[] marks)
            {
                if (marks == null || marks.Length != 7 || _attemptCount >= 4) return;
                for (int i = 0; i < _scores.GetLength(1); i++)
                {
                    _scores[_attemptCount, i] = marks[i];
                }
                _attemptCount++;
            }

            public static void Sort(Participant[] arr)
            {
                if (arr == null) return;
                for (int i = 1; i < arr.Length; i++)
                {
                    Participant key = arr[i];
                    int j = i - 1;
                    while (j >= 0 && arr[j].TotalScore < key.TotalScore)
                    {
                        arr[j + 1] = arr[j];
                        j--;
                    }
                    arr[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_firstName} {_lastName}");
                Console.Write("Coefs: ");
                foreach (double coef in _coef)
                {
                    Console.Write(coef + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("Marks:");
                for (int i = 0; i < _scores.GetLength(0); i++)
                {
                    for (int j = 0; j < _scores.GetLength(1); j++)
                    {
                        Console.Write(_scores[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("TotalScore: " + TotalScore);
                Console.WriteLine();
            }
        }
    }
}