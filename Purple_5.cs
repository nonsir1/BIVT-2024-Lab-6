using System;
using System.Linq;

namespace Lab_6
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _chTrait;
            private string _concept;

            public string Animal => _animal;
            public string CharacterTrait => _chTrait;
            public string Concept => _concept;

            public Response(string animal, string characterTrait, string concept)
            {
                _animal = animal;
                _chTrait = characterTrait;
                _concept = concept;
            }

            public int CountVotes(Response[] res, int qNum)
            {
                if (res == null || qNum < 1 || qNum > 3) return 0;

                string localAnimal = _animal;
                string localTrait = _chTrait;
                string localConcept = _concept;
                
                int count = 0;
                foreach (var r in res)
                {
                    if (r.Equals(default(Response))) continue;
                    
                    if (qNum == 1 && r.Animal != null && _animal != null && r.Animal == _animal)
                    {
                        count++;
                    }
                    else if (qNum == 2 && r.CharacterTrait != null && _chTrait != null && r.CharacterTrait == _chTrait)
                    {
                        count++;
                    }
                    else if (qNum == 3 && r.Concept != null && _concept != null && r.Concept == _concept)
                    {
                        count++;
                    }
                }
                return count;
            }

            public void Print()
            {
                Console.WriteLine($"Animal: {_animal}, Character Trait: {_chTrait}, Concept: {_concept}");
            }
        }

        public class Research
        {
            private string _name;
            private Response[] _res = new Response[0];
            private int _count;

            public string Name => _name;
            public Response[] Responses => _res.Take(_count).ToArray();

            public Research(string name)
            {
                _name = name;
                _res = new Response[10];
                _count = 0;
            }

            public void Add(string[] a)
            {
                if (a == null || a.Length != 3 || a.Any(item => item == null)) return;

                if (_count >= _res.Length)
                {
                    Array.Resize(ref _res, _res.Length * 2);
                }
                _res[_count++] = new Response(a[0], a[1], a[2]);
            }

            public string[] GetTopResponses(int q)
            {
                if (_res == null || q < 1 || q > 3) return new string[0];

                return _res.Take(_count)
                    .GroupBy(r => q == 1 ? r.Animal : q == 2 ? r.CharacterTrait : r.Concept)
                    .Where(g => !string.IsNullOrEmpty(g.Key))
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => g.Key)
                    .ToArray();
            }

            public void Print()
            {
                Console.WriteLine($"======== {_name} ========");
                foreach (var response in _res.Take(_count))
                {
                    response.Print();
                }
            }
        }
    }
}
