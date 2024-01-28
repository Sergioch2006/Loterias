using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfereSorteios
{
    public class ConfereArrayNumerosSena
    {
        public static void GeraEConfere()
        {
            // Número de valores aleatórios a serem gerados
            int quantidadeNumeros = 6;

            // Gerar um conjunto de 6 números aleatórios entre 0 e 60
            int[] numerosAleatorios = GerarConjuntoNumerosAleatorios(0, 60, quantidadeNumeros);

            Console.WriteLine("Conjunto de números aleatórios gerados:");

            // Exibir os números aleatórios gerados
            ExibirConjuntoNumeros(numerosAleatorios);

            // Solicitar ao usuário que digite 6 números
            Console.WriteLine("\nDigite 6 números:");

            // Ler os números digitados pelo usuário
            int[] numerosDigitados = new int[quantidadeNumeros];
            for (int i = 0; i < quantidadeNumeros; i++)
            {
                Console.Write($"Número {i + 1}: ");
                numerosDigitados[i] = Convert.ToInt32(Console.ReadLine());
            }

            // Verificar se os números digitados correspondem aos números aleatórios gerados
            int quantidadeCorrespondente = ContarNumerosCorrespondentes(numerosAleatorios, numerosDigitados);

            if (quantidadeCorrespondente == quantidadeNumeros)
            {
                Console.WriteLine("Parabéns! Você digitou corretamente todos os 6 números aleatórios.");
            }
            else if (quantidadeCorrespondente >= 5)
            {
                Console.WriteLine($"Você digitou corretamente pelo menos 5 dos 6 números aleatórios.");
            }
            else if (quantidadeCorrespondente >= 4)
            {
                Console.WriteLine($"Você digitou corretamente pelo menos 4 dos 6 números aleatórios.");
            }
            else
            {
                Console.WriteLine("Os números digitados não correspondem aos números aleatórios gerados. Tente novamente.");
            }
        }

        // Função para contar quantos números em dois conjuntos correspondem
        static int ContarNumerosCorrespondentes(int[] conjunto1, int[] conjunto2)
        {
            int quantidadeCorrespondente = 0;

            for (int i = 0; i < conjunto1.Length; i++)
            {
                if (conjunto1[i] == conjunto2[i])
                {
                    quantidadeCorrespondente++;
                }
            }

            return quantidadeCorrespondente;
        }

        // Função para gerar um conjunto de números aleatórios dentro de um intervalo, sem repetições
        static int[] GerarConjuntoNumerosAleatorios(int min, int max, int quantidade)
        {
            if (quantidade > (max - min + 1))
            {
                throw new ArgumentException("A quantidade de números a serem gerados deve ser menor ou igual à diferença entre o máximo e o mínimo mais um.");
            }

            int[] numerosAleatorios = new int[quantidade];
            Random random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                int numeroAleatorio;

                do
                {
                    numeroAleatorio = random.Next(min, max + 1);
                } while (Array.IndexOf(numerosAleatorios, numeroAleatorio) != -1);

                numerosAleatorios[i] = numeroAleatorio;
            }

            return numerosAleatorios;
        }

        // Função para obter os números mais repetidos em ordem decrescente
        static List<int> ObterNumerosMaisRepetidos(List<int[]> tabela)
        {
            Dictionary<int, int> contagemNumeros = new Dictionary<int, int>();

            // Contar a frequência de cada número na tabela
            foreach (var linha in tabela)
            {
                foreach (var numero in linha)
                {
                    if (contagemNumeros.ContainsKey(numero))
                    {
                        contagemNumeros[numero]++;
                    }
                    else
                    {
                        contagemNumeros[numero] = 1;
                    }
                }
            }

            // Ordenar os números com base em suas contagens em ordem decrescente
            List<int> numerosOrdenados = contagemNumeros.OrderByDescending(pair => pair.Value)
                                                         .Select(pair => pair.Key)
                                                         .ToList();

            return numerosOrdenados;
        }

        // Função para analisar a probabilidade estatística de um novo conjunto
        static double AnalisarProbabilidade(List<int[]> tabela, int[] novoConjunto)
        {
            Dictionary<int, int> contagemNumeros = new Dictionary<int, int>();

            // Contar a frequência de cada número na tabela
            foreach (var linha in tabela)
            {
                foreach (var numero in linha)
                {
                    if (contagemNumeros.ContainsKey(numero))
                    {
                        contagemNumeros[numero]++;
                    }
                    else
                    {
                        contagemNumeros[numero] = 1;
                    }
                }
            }

            // Calcular a probabilidade de correspondência para o novo conjunto
            double probabilidade = 1.0;

            foreach (var numero in novoConjunto)
            {
                if (contagemNumeros.ContainsKey(numero))
                {
                    probabilidade *= contagemNumeros[numero] / (double)tabela.Count;
                }
                else
                {
                    // Se um número não estiver na tabela, a probabilidade é zero
                    probabilidade = 0.0;
                    break;
                }
            }

            return probabilidade;
        }

        // Função para analisar a probabilidade estatística de um novo conjunto considerando a distribuição ao longo do tempo
        static double AnalisarProbabilidade(List<int[]> tabela, int[] novoConjunto, int vezesPorSemana, int totalDias)
        {
            Dictionary<int, int> contagemNumeros = new Dictionary<int, int>();

            // Contar a frequência de cada número na tabela
            foreach (var linha in tabela)
            {
                foreach (var numero in linha)
                {
                    if (contagemNumeros.ContainsKey(numero))
                    {
                        contagemNumeros[numero]++;
                    }
                    else
                    {
                        contagemNumeros[numero] = 1;
                    }
                }
            }

            // Calcular a probabilidade de correspondência para o novo conjunto considerando a distribuição ao longo do tempo
            double probabilidade = 1.0;

            foreach (var numero in novoConjunto)
            {
                if (contagemNumeros.ContainsKey(numero))
                {
                    // Ajustar a probabilidade com base na distribuição ao longo do tempo
                    probabilidade *= contagemNumeros[numero] / (double)tabela.Count * (totalDias / 7.0) / vezesPorSemana;
                }
                else
                {
                    // Se um número não estiver na tabela, a probabilidade é zero
                    probabilidade = 0.0;
                    break;
                }
            }

            return probabilidade;
        }

        // Função para exibir um conjunto de números
        static void ExibirConjuntoNumeros(int[] numeros)
        {
            foreach (int numero in numeros)
            {
                Console.Write($"{numero} ");
            }
            Console.WriteLine();
        }

        // Função para verificar se dois conjuntos de números correspondem
        static bool VerificarCorrespondencia(int[] conjunto1, int[] conjunto2)
        {
            if (conjunto1.Length != conjunto2.Length)
            {
                return false;
            }

            for (int i = 0; i < conjunto1.Length; i++)
            {
                if (conjunto1[i] != conjunto2[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
