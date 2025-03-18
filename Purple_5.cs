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

            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3)
                    return 0;

                string currentAnswer = questionNumber switch
                {
                    1 => _animal,
                    2 => _chTrait,
                    3 => _concept,
                    _ => null
                };

                if (string.IsNullOrEmpty(currentAnswer))
                    return 0;

                int count = 0;
                foreach (var r in responses)
                {
                    string ans = questionNumber switch
                    {
                        1 => r.Animal,
                        2 => r.CharacterTrait,
                        3 => r.Concept,
                        _ => null
                    };

                    if (!string.IsNullOrEmpty(ans) && ans == currentAnswer)
                        count++;
                }
                return count;
            }

            public void Print()
            {
                Console.WriteLine($"Animal: {_animal}, Character Trait: {_chTrait}, Concept: {_concept}");
            }
        }

        public struct Research
        {
            private string _name;
            private Response[] _res;

            public string Name => _name;
            public Response[] Responses => _res;

            public Research(string name)
            {
                _name = name;
                _res = new Response[0];
            }

            public void Add(string[] answers)
            {
                if (answers == null || answers.Length != 3)
                    return;
                Array.Resize(ref _res, _res.Length + 1);
                _res[_res.Length - 1] = new Response(answers[0], answers[1], answers[2]);
            }

            public string[] GetTopResponses(int question)
            {
                if (question < 1 || question > 3)
                    return new string[0];

                return _res
                    .Where(r => question == 1 ? !string.IsNullOrEmpty(r.Animal)
                            : question == 2 ? !string.IsNullOrEmpty(r.CharacterTrait)
                            : !string.IsNullOrEmpty(r.Concept))
                    .GroupBy(r => question == 1 ? r.Animal
                                   : question == 2 ? r.CharacterTrait
                                   : r.Concept)
                    .OrderByDescending(g => g.Count())
                    .Take(5)
                    .Select(g => g.Key)
                    .ToArray();
            }

            public void Print()
            {
                Console.WriteLine($"======== {_name} ========");
                foreach (var response in _res)
                {
                    response.Print();
                }
            }
        }
    }
}
