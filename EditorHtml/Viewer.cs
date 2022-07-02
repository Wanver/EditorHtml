using System;
using System.Text.RegularExpressions;
using System.IO;

namespace EditorHtml
{
    public static class Viewer
    {

        public static void Show(string text)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WriteLine("MODO VISUALIZACAO");
            Console.WriteLine("-----------------");

            Replace(text);
            Console.WriteLine("-----------------");
            Console.ReadKey();
            Menu.Show();
        }

        public static void Replace(string text)
        {
            var strong = new Regex(@"<\s*strong[^>]*>(.*?)<\s*/\s*strong>");
            var words = text.Split(' ');

            for (var i = 0; i < words.Length; i++)
            {
                if (strong.IsMatch(words[i]))
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;                    
                    Console.Write(
                        words[i].Substring(
                            words[i].IndexOf('>') + 1,
                            (
                            (words[i].LastIndexOf('<') - 1) -
                            words[i].IndexOf('>')
                            )
                        )
                    );
                    Console.Write(" ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(words[i]);
                    Console.Write(" ");
                }
            }

        }
        public static void Abrir()
        {
            bool choiceRight = false;
            string path = string.Empty;
            string basePath = Directory.GetCurrentDirectory();
            basePath = $"{basePath}\\arquivos";
            int result;
            string[] arrayFiles = new string[] { };

            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Qual arquivo você dejesa abrir?");

            ListFiles(basePath, arrayFiles);

            if (arrayFiles.Length < 1)
            {
                Console.WriteLine("Não existem arquivos para serem abertos.");
                Console.ReadLine();
                Menu.Show();
            }

            result = returnChoice();

            while (!choiceRight)
            {
                try
                {
                    path = arrayFiles[result - 1];
                    choiceRight = true;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Você digitou uma opção inválida. Escolha de 1 - {arrayFiles.Length}");
                    Console.WriteLine("Qual arquivo você dejesa abrir?");
                    int.TryParse(Console.ReadLine(), out result);
                }
            }

            using (var file = new StreamReader(path))
            {
                string text = file.ReadToEnd();
                Replace(text);
                //Console.WriteLine(text);
            }

            Console.WriteLine("");
            Console.ReadLine();
            Menu.Show();
        }

        private static int returnChoice()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine($"{result} não é um inteiro, por isso não podemos prosseguir. Digite uma opção válida!");
            }

            return result;
        }

        private static string[] ListFiles(string basePath, string[] arrayFiles)
        {
            if (Directory.Exists(basePath))
            {
                arrayFiles = Directory.GetFiles(basePath);

                for (int i = 0; i < arrayFiles.Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {arrayFiles[i]}");
                }
            }
            else
                Directory.CreateDirectory($"{basePath}\\arquivos");
            return arrayFiles;
        }
    }
}