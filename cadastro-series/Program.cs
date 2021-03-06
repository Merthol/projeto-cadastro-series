using System;
using static System.Console;

namespace cadastro_series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main(string[] args)
        {
            string opcaoUsuario;
            WriteLine("DIO Séries - Seja bem vindo!!!");
            do
            {
                opcaoUsuario = ObterOpcaoUsuario();
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        WriteLine();
                        WriteLine("Tecle ENTER para continuar.");
                        ReadLine();
                        break;
                    case "2":
                        InserirSeries();
                        WriteLine();
                        WriteLine("Tecle ENTER para continuar.");
                        ReadLine();
                        break;
                    case "3":
                        if (AtualizarSeries())
                            WriteLine("Série atualizada com sucesso.");
                        else
                            WriteLine("ID não encontrado.");

                        WriteLine();
                        WriteLine("Tecle ENTER para continuar.");
                        ReadLine();
                        break;
                    case "4":
                        if (ExcluirSeries())
                            WriteLine("Série excluída com sucesso.");
                        else
                            WriteLine("ID não encontrado.");

                        WriteLine();
                        WriteLine("Tecle ENTER para continuar.");
                        ReadLine();
                        break;
                    case "5":
                        VisualizarSeries();
                        break;
                    case "6":
                        Clear();
                        break;

                    default:
                        WriteLine("Opção inválida. Por favor, tente novamente.");
                        break;
                }
            } while (opcaoUsuario != "X");
        }

        private static void VisualizarSeries()
        {
            Write("Digite o id da série que deseja visualizar: ");
            int indiceSerie = int.Parse(ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            WriteLine(serie);
        }

        private static bool ExcluirSeries()
        {
            Write("Digite o id da série que deseja excluir: ");
            int indiceSerie = int.Parse(ReadLine());

            if (indiceSerie >= repositorio.ProximoId())
                return false;

            repositorio.Exclui(indiceSerie);
            return true;
        }

        private static bool AtualizarSeries()
        {
            Write("Digite o id da série que deseja atualizar: ");
            int indiceSerie = int.Parse(ReadLine());

            if (indiceSerie >= repositorio.ProximoId())
                return false;

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(ReadLine());

            Write("Digite o Título da Série: ");
            string entradaTitulo = ReadLine();

            Write("Digite o Ano de Lançamento da Série: ");
            int entradaAno = int.Parse(ReadLine());

            Write("Digite a descrição da Série: ");
            string entradaDescricao = ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);

            return true;
        }

        private static void InserirSeries()
        {
            WriteLine("inserir nova série");

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(ReadLine());

            Write("Digite o Título da Série: ");
            string entradaTitulo = ReadLine();

            Write("Digite o Ano de Lançamento da Série: ");
            int entradaAno = int.Parse(ReadLine());

            Write("Digite a descrição da Série: ");
            string entradaDescricao = ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Insere(novaSerie);
        }

        private static void ListarSeries()
        {
            WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.StatusExcluido();
                WriteLine("#ID {0}: - {1}{2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? " - *Excluido*" : ""));
            }
        }

        private static string ObterOpcaoUsuario()
        {
            WriteLine();
            WriteLine("Informe a opção desejada:");

            WriteLine("1 - Listar séries");
            WriteLine("2 - Inserir nova série");
            WriteLine("3 - Atualizar série");
            WriteLine("4 - Excluir série");
            WriteLine("5 - Visualizar série");
            WriteLine("C - Limpar Tela");
            WriteLine("X - Sair");
            WriteLine();

            string opcaoUsuario = ReadLine().ToUpper();
            WriteLine();
            return opcaoUsuario;
        }
    }
}

