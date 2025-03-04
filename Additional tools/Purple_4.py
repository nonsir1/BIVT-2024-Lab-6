input_text = """Name
Surname
Time
Анастасия
Жаркова
112,49
Александр
Павлов
472,11
Степан
Свиридов
213,92
Игорь
Сидоров
102,13
Евгения
Сидорова
263,21
Мария
Сидорова
350,75
Лев
Петров
248,68
Савелий
Козлов
325,28
Егор
Свиридов
300,00
Оксана
Жаркова
252,16
Светлана
Петрова
402,20
Полина
Петрова
397,33
Екатерина
Павлова
384,94
Юлия
Полевая
8,09
Евгения
Павлова
480,52"""

lines = input_text.split("\n")[3:]
participants = []
i = 0

while i < len(lines):
    first_name = lines[i]
    last_name = lines[i + 1]
    marks = []
    i += 2
    
    while i < len(lines) and "," in lines[i]:
        marks.append(float(lines[i].replace(",", ".")))
        i += 1
    
    participants.append((first_name, last_name, marks))

output = []
for idx, (first_name, last_name, marks) in enumerate(participants):
    output.append(f'group2.Add(new Purple_4.Sportsman("{first_name}", "{last_name}"));')
    for mark in marks:
        output.append(f'group2.Sportsmen[{idx}].Run({mark});')

print("\n".join(output))
