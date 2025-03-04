input_text = """Name
Surname
Marks
Виктор
Полевой
5,93
5,44
1,20
0,28
1,57
1,86
5,89
Алиса
Козлова
1,68
3,79
3,62
2,76
4,47
4,26
5,79
Ярослав
Зайцев
2,93
3,10
5,46
4,88
3,99
4,79
5,56
Савелий
Кристиан
4,20
4,69
3,90
1,67
1,13
5,66
5,40
Алиса
Козлова
3,27
2,43
0,90
5,61
3,12
3,76
3,73
Алиса
Луговая
0,75
1,13
5,43
2,07
2,68
0,83
3,68
Александр
Петров
3,78
3,42
3,84
2,19
1,20
2,51
3,51
Мария
Смирнова
1,35
3,40
1,85
2,02
2,78
3,23
3,03
Полина
Сидорова
0,55
5,93
0,75
5,15
4,35
1,51
2,77
Татьяна
Сидорова
3,86
0,19
0,46
5,14
5,37
0,94
0,84"""

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
    output.append(f'participants3[{idx}] = new Purple_3.Participant("{first_name}", "{last_name}");')
    for mark in marks:
        output.append(f'participants3[{idx}].Evaluate({mark});')

print("\n".join(output))
