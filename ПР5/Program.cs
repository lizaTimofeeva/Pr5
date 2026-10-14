using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР5
{

    abstract class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Email { get; private set; }

        public Person(string name, int age, string email)
        {
            Name = name;
            Age = age;
            Email = email;
        }

        public abstract void ShowInfo();
    }

    class Student : Person
    {
        public string Group { get; private set; }

        public Student(string name, int age, string email, string group) : base(name, age, email)
        {
            Group = group;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Студент: " + Name + " | Возраст: " + Age + " | Почта: " + Email + " | Группа: " + Group);
        }
    }

    class Teacher : Person
    {
        public string Job { get; private set; }

        public Teacher(string name, int age, string email, string job) : base(name, age, email)
        {
            Job = job;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Преподаватель: " + Name + " | Возраст: " + Age + " | Почта: " + Email + " | Должность: " + Job);
        }
    }

    class Course
    {
        public string Title { get; private set; }
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; private set; } = new List<Student>();

        public Course(string title)
        {
            Title = title;
        }

        public void ShowInfo()
        {
            string teacherName = "не назначен";
            if (Teacher != null)
            {
                teacherName = Teacher.Name;
            }
            Console.WriteLine("Курс: " + Title + " | Преподаватель: " + teacherName + " | Студентов: " + Students.Count);
        }
    }

    internal class Program
    {
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Course> courses = new List<Course>();

        static void PrintTitle(string title)
        {
            Console.Clear();
            Console.WriteLine("=== " + title + " ===");
            Console.WriteLine();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
            Console.ReadKey(true);
        }

        static void PrintStudents()
        {
            int i = 1;
            foreach (Student student in students)
            {
                Console.Write(i + ". ");
                student.ShowInfo();
                i++;
            }
        }

        static void PrintTeachers()
        {
            int i = 1;
            foreach (Teacher teacher in teachers)
            {
                Console.Write(i + ". ");
                teacher.ShowInfo();
                i++;
            }
        }

        static void PrintCourses()
        {
            int i = 1;
            foreach (Course course in courses)
            {
                Console.Write(i + ". ");
                course.ShowInfo();
                i++;
            }
        }

        static string ReadText(string label)
        {
            string text = "";
            while (string.IsNullOrWhiteSpace(text))
            {
                Console.Write(label + ": ");
                text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("Поле \"" + label + "\" не может быть пустым");
                }
            }
            return text.Trim();
        }

        static int ReadAge()
        {
            int age = 0;
            while (age < 1 || age > 80)
            {
                Console.Write("Возраст: ");
                if (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Введите число");
                    age = 0;
                }
                else if (age < 1 || age > 80)
                {
                    Console.WriteLine("Возраст должен быть от 1 до 80");
                }
            }
            return age;
        }

        static string ReadEmail()
        {
            string email = "";
            while (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.Write("Почта: ");
                email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                {
                    Console.WriteLine("Почта не может быть пустой и обязательно должна содержать @");
                }
            }
            return email.Trim();
        }

        static int ReadIndex(int count)
        {
            int number = 0;
            while (number < 1 || number > count)
            {
                Console.Write("Введите номер: ");
                if (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > count)
                {
                    Console.WriteLine("Нет такого номера, введите от 1 до " + count);
                    number = 0;
                }
            }
            return number - 1;
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Университет =====");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Добавить преподавателя");
                Console.WriteLine("3. Создать курс");
                Console.WriteLine("4. Показать всех студентов");
                Console.WriteLine("5. Показать всех преподавателей");
                Console.WriteLine("6. Показать все курсы");
                Console.WriteLine("7. Записать студента на курс");
                Console.WriteLine("8. Назначить преподавателя на курс");
                Console.WriteLine("9. Курсы студента");
                Console.WriteLine("10. Студенты курса");
                Console.WriteLine("11. Все люди университета");
                Console.WriteLine("0. Выход");
                Console.WriteLine();
                Console.Write("Выберите пункт: ");

                int n;
                if (!int.TryParse(Console.ReadLine(), out n))
                {
                    n = -1;
                }

                if (n == 0)
                {
                    return;
                }
                else if (n == 1)
                {
                    PrintTitle("Добавление студента");
                    string name = ReadText("Имя");
                    int age = ReadAge();
                    string email = ReadEmail();
                    string group = ReadText("Группа");

                    students.Add(new Student(name, age, email, group));
                    Console.WriteLine();
                    Console.WriteLine("Студент " + name + " добавлен");
                }
                else if (n == 2)
                {
                    PrintTitle("Добавление преподавателя");
                    string name = ReadText("Имя");
                    int age = ReadAge();
                    string email = ReadEmail();
                    string job = ReadText("Должность");

                    teachers.Add(new Teacher(name, age, email, job));
                    Console.WriteLine();
                    Console.WriteLine("Преподаватель " + name + " добавлен");
                }
                else if (n == 3)
                {
                    PrintTitle("Создание курса");
                    string title = "";
                    bool ex = true;
                    while (ex == true)
                    {
                        title = ReadText("Название");
                        ex = false;
                        foreach (Course course in courses)
                        {
                            if (course.Title.ToLower() == title.ToLower())
                            {
                                ex = true;
                            }
                        }
                        if (ex == true)
                        {
                            Console.WriteLine("Курс с таким названием уже существует");
                        }
                    }

                    courses.Add(new Course(title));
                    Console.WriteLine();
                    Console.WriteLine("Курс \"" + title + "\" создан");
                }
                else if (n == 4)
                {
                    PrintTitle("Все студенты");
                    if (students.Count == 0)
                    {
                        Console.WriteLine("Студенты ещё не добавлены");
                    }
                    else
                    {
                        PrintStudents();
                    }
                }
                else if (n == 5)
                {
                    PrintTitle("Все преподаватели");
                    if (teachers.Count == 0)
                    {
                        Console.WriteLine("Преподаватели ещё не добавлены");
                    }
                    else
                    {
                        PrintTeachers();
                    }
                }
                else if (n == 6)
                {
                    PrintTitle("Все курсы");
                    if (courses.Count == 0)
                    {
                        Console.WriteLine("Курсы ещё не созданы");
                    }
                    else
                    {
                        PrintCourses();
                    }
                }
                else if (n == 7)
                {
                    PrintTitle("Запись студента на курс");
                    if (students.Count == 0)
                    {
                        Console.WriteLine("Сначала добавьте хотя бы одного студента");
                    }
                    else if (courses.Count == 0)
                    {
                        Console.WriteLine("Сначала создайте хотя бы один курс");
                    }
                    else
                    {
                        Console.WriteLine("Выберите студента:");
                        PrintStudents();
                        Student stud = students[ReadIndex(students.Count)];

                        Console.WriteLine();
                        Console.WriteLine("Выберите курс:");
                        PrintCourses();
                        Course cours = courses[ReadIndex(courses.Count)];

                        Console.WriteLine();
                        if (cours.Students.Contains(stud))
                        {
                            Console.WriteLine("Студент " + stud.Name + " уже записан на курс \"" + cours.Title + "\"");
                        }
                        else
                        {
                            cours.Students.Add(stud);
                            Console.WriteLine("Студент " + stud.Name + " записан на курс \"" + cours.Title + "\"");
                        }
                    }
                }
                else if (n == 8)
                {
                    PrintTitle("Назначение преподавателя на курс");
                    if (teachers.Count == 0)
                    {
                        Console.WriteLine("Сначала добавьте хотя бы одного преподавателя");
                    }
                    else if (courses.Count == 0)
                    {
                        Console.WriteLine("Сначала создайте хотя бы один курс");
                    }
                    else
                    {
                        Console.WriteLine("Выберите преподавателя:");
                        PrintTeachers();
                        Teacher teach = teachers[ReadIndex(teachers.Count)];

                        Console.WriteLine();
                        Console.WriteLine("Выберите курс:");
                        PrintCourses();
                        Course cours = courses[ReadIndex(courses.Count)];

                        Console.WriteLine();
                        if (cours.Teacher == teach)
                        {
                            Console.WriteLine(teach.Name + " уже ведёт курс \"" + cours.Title + "\"");
                        }
                        else
                        {
                            cours.Teacher = teach;
                            Console.WriteLine(teach.Name + " теперь ведёт курс \"" + cours.Title + "\"");
                        }
                    }
                }
                else if (n == 9)
                {
                    PrintTitle("Курсы студента");
                    if (students.Count == 0)
                    {
                        Console.WriteLine("Студенты ещё не добавлены");
                    }
                    else
                    {
                        Console.WriteLine("Выберите студента:");
                        PrintStudents();
                        Student stud = students[ReadIndex(students.Count)];

                        Console.WriteLine();
                        Console.WriteLine("Курсы студента " + stud.Name + ":");
                        bool found = false;
                        foreach (Course course in courses)
                        {
                            if (course.Students.Contains(stud))
                            {
                                Console.WriteLine("- " + course.Title);
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            Console.WriteLine("Студент не записан ни на один курс");
                        }
                    }
                }
                else if (n == 10)
                {
                    PrintTitle("Студенты курса");
                    if (courses.Count == 0)
                    {
                        Console.WriteLine("Курсы ещё не созданы");
                    }
                    else
                    {
                        Console.WriteLine("Выберите курс:");
                        PrintCourses();
                        Course cours = courses[ReadIndex(courses.Count)];

                        Console.WriteLine();
                        if (cours.Students.Count == 0)
                        {
                            Console.WriteLine("На курс \"" + cours.Title + "\" никто не записан");
                        }
                        else
                        {
                            Console.WriteLine("Студенты курса \"" + cours.Title + "\":");
                            int i = 1;
                            foreach (Student student in cours.Students)
                            {
                                Console.Write(i + ". ");
                                student.ShowInfo();
                                i++;
                            }
                        }
                    }
                }
                else if (n == 11)
                {
                    PrintTitle("Все люди университета");
                    List<Person> all = new List<Person>();
                    all.AddRange(students);
                    all.AddRange(teachers);

                    if (all.Count == 0)
                    {
                        Console.WriteLine("В университете пока нет людей");
                    }
                    else
                    {
                        int i = 1;
                        foreach (Person p in all)
                        {
                            Console.Write(i + ". ");
                            p.ShowInfo();
                            i++;

                        }
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Такого пункта нет, введите число от 0 до 11");
                }

                Pause();
            }
        }
    }
}
