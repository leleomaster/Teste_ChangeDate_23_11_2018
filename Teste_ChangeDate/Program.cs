using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_ChangeDate
{
    class Program
    {
        static void Main(string[] args)
        {
            TesteMetodo_ChangeDate();
        }

        private static void TesteMetodo_ChangeDate()
        {
            string dataTeste = "01/03/2010 02:00";
            long valorMinutos = -4000;
            long dias = valorMinutos / 1440;
            long horas = (valorMinutos - (dias * 24 * 60)) / 60;
            long minutos = valorMinutos - ((dias * 24 * 60) + (horas * 60));


            Console.WriteLine($"============    {dataTeste} - {valorMinutos} minutos   ===============");

            Console.WriteLine($"4000 minutos equivale a {dias} dias, {horas} horas e {minutos} minutos.");

            Console.WriteLine("\n\tAdicionar data e hora\n");

            Console.WriteLine(ChangeDate(dataTeste, '+', valorMinutos));

            Console.WriteLine();

            Console.WriteLine(ChangeDate(dataTeste, '+', 40000));

            Console.WriteLine();

            Console.WriteLine(ChangeDate(dataTeste, '+', 5560259));


            Console.WriteLine("\n\tRemover data e hora\n");

            Console.WriteLine(ChangeDate(dataTeste, '-', valorMinutos));

            Console.WriteLine();

            Console.WriteLine(ChangeDate(dataTeste, '-', 40000));

            Console.WriteLine();

            Console.WriteLine(ChangeDate(dataTeste, '-', 5560259));

            Console.ReadKey();
        }

        public static string ChangeDate(string date, char op, long value)
        {
            string data = string.Empty;

            if(value < 0)
            {
                value = value * (-1);
            }

            if (op == '+')
            {
                data = AddMinutes(date, value);
            }
            else if (op == '-')
            {
                data = RemoveMinutes(date, value);
            }
            else
            {
                data = "";
            }

            return data;
        }

        public static string AddMinutes(string date, long min)
        {
            string dataMudada = date;

            if (min > 0)
            {
                long minutos = Convert.ToInt64(date.Substring(14, 2));
                long horas = Convert.ToInt64(date.Substring(11, 2));
                long dias = Convert.ToInt64(date.Substring(0, 2));
                long mes = Convert.ToInt64(date.Substring(3, 2));
                long ano = Convert.ToInt64(date.Substring(6, 4));

                bool continuar = true;
                int count = 1;
                while (continuar)
                {
                    if (count <= min)
                    {
                        minutos += 1;

                        DefinirValor(ref minutos, 60, ref horas, ref minutos, valorPadrao: 0);
                        DefinirValor(ref horas, 24, ref dias, ref horas, valorPadrao: 0);

                        long diasDoMesAtual = ObterDiasDoMes(mes);

                        DefinirValor(ref dias, diasDoMesAtual, ref mes, ref dias);

                        DefinirValor(ref mes, 12, ref ano, ref mes);

                        count++;
                    }
                    else
                    {
                        continuar = false;
                    }
                }

                dataMudada = $"{dias.ToString().PadLeft(2, '0')}/{mes.ToString().PadLeft(2, '0')}/{ano.ToString().PadLeft(4, '0')} {horas.ToString().PadLeft(2, '0')}:{minutos.ToString().PadLeft(2, '0')}";
            }
            return dataMudada;
        }

        private static void DefinirValor(ref long variavelComparacao, long valorComparacao, ref long variavelSomar, ref long variavelPadrao, long valorSomar = 1, long valorPadrao = 1)
        {
            if (variavelComparacao == valorComparacao)
            {
                variavelSomar += valorSomar;
                variavelPadrao = valorPadrao;
            }
        }

        private static void DefinirValorSubtracao(ref long variavelComparacao, ref long variavelSubtrair, ref long variavelPadrao, long valorSubtrair = 1, long valorPadrao = 1)
        {
            if (variavelComparacao < 0)
            {
                variavelSubtrair -= valorSubtrair;
                variavelPadrao = valorPadrao;
            }
        }
        
        public static string RemoveMinutes(string date, long min)
        {
            string dataMudada = date;

            if (min > 0)
            {
                long minutos = Convert.ToInt64(date.Substring(14, 2));
                long horas = Convert.ToInt64(date.Substring(11, 2));
                long dias = Convert.ToInt64(date.Substring(0, 2));
                long mes = Convert.ToInt64(date.Substring(3, 2));
                long ano = Convert.ToInt64(date.Substring(6, 4));

                bool continuar = true;
                int count = 1;
                while (continuar)
                {
                    if (count <= min)
                    {
                        minutos -= 1;

                        DefinirValorSubtracao(ref minutos, ref horas, ref minutos, valorPadrao: 59); 
                        DefinirValorSubtracao(ref horas, ref dias, ref horas, valorPadrao: 23);                     

                        if (dias < 0)
                        {
                            mes -= 1;

                            long diasDoMesAtual = ObterDiasDoMes(mes);
                            dias = diasDoMesAtual - 1;
                        }

                        DefinirValorSubtracao(ref mes, ref ano, ref mes, valorPadrao: 12);

                        count++;
                    }
                    else
                    {
                        continuar = false;
                    }
                }

                dataMudada = $"{dias.ToString().PadLeft(2, '0')}/{mes.ToString().PadLeft(2, '0')}/{ano.ToString().PadLeft(4, '0')} {horas.ToString().PadLeft(2, '0')}:{minutos.ToString().PadLeft(2, '0')}";
            }
            return dataMudada;
        }

        private static long ObterDiasDoMes(long mes)
        {
            long quantidadeDiasDoMes = 0;

            if (mes == 2)
            {
                quantidadeDiasDoMes = 28;
            }
            else if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
            {
                quantidadeDiasDoMes = 30;
            }
            else
            {
                quantidadeDiasDoMes = 31;
            }

            return quantidadeDiasDoMes;
        }

    }
}
