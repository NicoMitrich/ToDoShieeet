using System.Data;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToDoShieeet;

namespace ToDooshka
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //проработать логирование datetime.now при создании todo

            string path = "C:\\Users\\Mitrich\\source\\repos\\ToDoShieeet\\ToDoShieeet\\Data.json";            
            string? jsonString = File.ReadAllText(path);

            TodoBoard? board = JsonConvert.DeserializeObject<TodoBoard>(jsonString);

            Executer exe = new Executer(board);
            exe.Run();
            string jsonResult = JsonConvert.SerializeObject(exe.Board, Formatting.Indented);            
            File.WriteAllText(path, jsonResult);
        }
    }
    class Executer
    {
        public TodoBoard? Board { get; }
        public Executer(TodoBoard? board)
        {
            if (board != null)
                Board = board;
            else
                Board = new TodoBoard("MitrichBoard", 1);
        }

        public void Run()
        {
            bool isOn = true;
            while (isOn)
            {
                int input = ShowOptions();
                Console.Clear();
                switch (input)
                {
                    case 1:
                        ShowAllTodos(Board.Todos);
                        break;
                    case 2:
                        AddTodo(Board.Todos);
                        break;
                    case 3:
                        UpdateTodo(Board.Todos);
                        break;
                    case 4:
                        DeleteTodo(Board.Todos);
                        break;
                    case 5:
                        CheckTheBox(Board.Todos);
                        break;
                    case 6:
                        isOn = false;
                        Console.WriteLine("Завершение работы");
                        break;
                    default:
                        Console.WriteLine("\nВведите один из вариантов");
                        break;
                }


                Console.ReadLine();
                Console.Clear();
            }
        }
        public int ShowOptions()
        {
            Console.WriteLine("1. Показать все задачи");
            Console.WriteLine("2. Добавить задачу");
            Console.WriteLine("3. Обновить задачу");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("5. Выполнить задачу");
            Console.WriteLine("6. Выход");

            return CheckIntInput("\nВыберите опцию: ");

        }
        public void ShowAllTodos(List<Todo> todos)
        {
            if (todos.Count == 0)
            {
                Console.WriteLine("В настоящее время задач нет.");
            }
            else
            {
                int count = 0;
                foreach (var todo in todos)
                {
                    count++;
                    if (todo.IsDone == true)
                        Console.WriteLine($"{count}. [X] (ID - {todo.Id}) {todo.Title} — {todo.Description}");
                    else
                        Console.WriteLine($"{count}. [ ] (ID - {todo.Id}) {todo.Title} — {todo.Description}");
                }
            }
        }
        public void AddTodo(List<Todo> todos)
        {
            int count = 0;
            Console.WriteLine("Введите заголовок задачи");
            string? inputTitle = Console.ReadLine();
            Console.WriteLine("Введите описание задачи");
            string? inputDescription = Console.ReadLine();

            List<int> ids = new List<int>();
            foreach (var todo in todos) { ids.Add(todo.Id); }
            while (ids.Contains(count)) { count++; }

            todos.Add(new Todo(count, Convert.ToString(DateTime.Now), false, inputTitle, inputDescription));

            Console.WriteLine("Задача добавлена");
        }
        public void UpdateTodo(List<Todo> todos)
        {
            ShowAllTodos(todos);

            bool isFound = false;
            while (isFound == false)
            {
                int input = CheckIntInput("Введите ID задачи");

                foreach (var item in todos)
                {
                    if (item.Id == input)
                    {
                        isFound = true;
                        Console.WriteLine("Введите новый заголовок");
                        item.Title = Console.ReadLine();
                        Console.WriteLine("Введите новое описание");
                        item.Description = Console.ReadLine();
                    }
                }
            }
            Console.WriteLine("Задача обновлена");
        }
        public void DeleteTodo(List<Todo> todos)
        {
            ShowAllTodos(todos);
            bool isFound = false;
            while (isFound == false)
            {
                int input = CheckIntInput("Введите ID задачи");
                for (int i = 0; i < todos.Count; i++)
                {
                    if (todos[i].Id == input)
                    {
                        isFound = true;
                        todos.Remove(todos[i]);
                    }
                }
            }
            Console.WriteLine("Задача удалена");
        }
        public void CheckTheBox(List<Todo> todos)
        {
            ShowAllTodos(todos);

            bool isFound = false;
            while (isFound == false)
            {
                int input = CheckIntInput("Введите ID задачи, которую вы выполнили");

                for (int i = 0; i < todos.Count; i++)
                {
                    if (todos[i].Id == input)
                    {
                        isFound = true;
                        todos[i].IsDone = true;
                    }
                }
            }
            Console.WriteLine("Задача помечена выполненной");

        }

        public int CheckIntInput(string question)
        {
            Console.WriteLine(question);
            string? input = Console.ReadLine();
            while (!int.TryParse(input, out int result))
            {
                Console.WriteLine("Введите один из предложенных номеров");
                input = Console.ReadLine();
            }
            return Convert.ToInt32(input);
        }
    }
}