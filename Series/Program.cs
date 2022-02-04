using System;
using static System.Console;

namespace Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();

        static void Main()
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1": 
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":      
                        Clear();
                        break;

                    default:
                        WriteLine("Essa opção não tem aqui não!");
                        break;
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }

            WriteLine("Obrigado por encher nosso bolso de grana!!");
            WriteLine("Saindo aqui, até mais !!");
            
        }

        

        private static void ListarSeries()
        {
            WriteLine("Listando séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                WriteLine("Nenhuma série cadastrada. :(  Ainda...");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                WriteLine("#ID {0}: - {1} - {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }       
        }

        private static void InserirSerie()
        {
            WriteLine("Inserindo série" + Environment.NewLine);

            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Write("Digite o número de uns dos gêneros acima : ");
            int entradaGenero = int.Parse(ReadLine());

            Write("Agora o nome da série: ");
            string entradaTitulo = ReadLine();

            Write("E o ano de início da série: ");
            int entradaAno = int.Parse(ReadLine());

            Write("Falta pouco, digite a descrição da série: ");
            string entradaDescricao = ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            
            repositorio.Insere(novaSerie);
            WriteLine(Environment.NewLine + "Sua série já ta lá, confira!!");
           
        }

        private static void AtualizarSerie()
        {
            WriteLine("Digite o id da série que será atualizada: ");
            int indiceSerie = int.Parse(ReadLine());
            
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            }
            Write(Environment.NewLine + "Digite o número de uns dos gêneros acima : ");
            int entradaGenero = int.Parse(ReadLine());

            Write("Agora o nome da série: ");
            string entradaTitulo = ReadLine();

            Write("E o ano de início da série: ");
            int entradaAno = int.Parse(ReadLine());

            Write("Falta pouco, digite a descrição da série: ");
            string entradaDescricao = ReadLine();

            Serie atualiarSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualiarSerie);
            
        }

        private static void ExcluirSerie()
        {
            WriteLine("Digite o id da série que deseja excluir: ");
            int indiceSerie = int.Parse(ReadLine());

            WriteLine("Confirma pra mim se quer apagar essa série: SIM ou NAO");
            string confirmacaoExcluir = ReadLine();

            if (confirmacaoExcluir.ToUpper() == "SIM")
            {
                repositorio.Exclui(indiceSerie);
                WriteLine("Excluíndo série..." + Environment.NewLine);
            }
            else
            {
                WriteLine("Cancelando o processo aqui...");
            }

        }

        private static void VisualizarSerie()
        {
            WriteLine("Fala aí pra mim o id da série para visualizar: ");
            int indiceSerie = int.Parse(ReadLine());

            var serie = repositorio.RetornarPorId(indiceSerie);
            WriteLine(serie.ToString());
            
        }



        private static string ObterOpcaoUsuario()
        {
            WriteLine();
            WriteLine("As Séries são nossas mas quem manda é você!!" + Environment.NewLine);
            WriteLine("Informe a opção desejada!");

            WriteLine("1- Listar séries");
            WriteLine("2- Inserir nova série");
            WriteLine("3- Atualizar série");
            WriteLine("4- Excluir série");
            WriteLine("5- Visualizar série");
            WriteLine("C- Limpar tela");
            WriteLine("X- Sair");
            WriteLine();

            string opcaoUsuario = ReadLine().ToUpper();
            WriteLine();
            return opcaoUsuario;
        }
    }
}