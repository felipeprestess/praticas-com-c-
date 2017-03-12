using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

namespace EscrevendoDados
{
    class Program
    {
        static void Main(string[] args)
        {
            string nomeCompleto = string.Empty;
            string email = string.Empty;
            string endereco = string.Empty;
            int idade = 0;

            Console.Write("Digite seu nome completo: ",Environment.NewLine);
            nomeCompleto = Console.ReadLine();
            Console.Write("Digite sua idade: ", Environment.NewLine);
            idade = Convert.ToInt32(Console.ReadLine());
            Console.Write("Digite seu email: ", Environment.NewLine);
            email = Console.ReadLine();
            Console.Write("Digite o seu endereço: ", Environment.NewLine);
            endereco = Console.ReadLine();


            using (FileStream input = File.Create(string.Format("{0}.txt", nomeCompleto)))
            using (StreamWriter writer = new StreamWriter(input))
            {
                writer.WriteLine(string.Format("Log criado dia {0}", DateTime.Now.ToString("dd/MM/yyy HH:mm:ss")));
                writer.WriteLine(string.Format("Nome completo: {0}", nomeCompleto));
                writer.WriteLine(string.Format("Idade: {0} anos", idade));
                writer.WriteLine(string.Format("Email: {0}", email));
                writer.WriteLine(string.Format("Endereço: {0}", endereco));

                
            }

            Process.Start("notepad.exe", string.Format("{0}.txt", nomeCompleto));

            Console.ReadKey();
        }
    }
}
