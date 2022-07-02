using System;
using System.Text;

namespace EditorHtml
{
    public static class Editor
    {
        public static void Show()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("MODO EDITOR");
            Console.WriteLine("-----------");
            Start();
        }

        public static void Start()
        {
            var file = new StringBuilder();

            do
            {
                file.Append(Console.ReadLine());
                file.Append(Environment.NewLine);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);

            Console.WriteLine("------------------------");
            Console.WriteLine(" Deseja salvar o arquivo?");
            Editor.Salvar(file.ToString());
            Viewer.Show(file.ToString());
            

        }

        public static void Salvar(string text)
        {
            string basePath = Directory.GetCurrentDirectory();
            basePath = $"{basePath}\\arquivos";

            if (!Directory.Exists(basePath))
                Directory.CreateDirectory($"{basePath}");

            Console.Clear();
            Console.WriteLine("Qual o caminho para salvar o arquivo?");
            var path = Console.ReadLine();
            path = $"{basePath}\\{path}.txt";

            using (var file = new StreamWriter(path))
            {
                file.Write(text);
            }

            Console.WriteLine($"Arquivo {path} salvo com sucesso!");
            Console.ReadLine();
            Menu.Show();
        }
    }
}