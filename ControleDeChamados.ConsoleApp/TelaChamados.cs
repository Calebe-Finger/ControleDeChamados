namespace ControleDeChamados.ConsoleApp
{
    internal partial class Program
    {
        public class TelaChamados
        {
            public RepositorioChamados reposiChama = new RepositorioChamados();

            int id = 0;

            public char ExibirMenu()
            {
                Console.Clear();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Controle de Chamados");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("O que deseja ser feito: ");
                Console.WriteLine("1 - Registrar Chamado");
                Console.WriteLine("2 - Visualizar Chamados");
                Console.WriteLine("3 - Editar Chamados");
                Console.WriteLine("4 - Excluir Chamados");
                Console.WriteLine("S - Sair");


                char opcao = Console.ReadLine().ToUpper()[0];
                return opcao;
            }

            public void RegistrarChamado()
            {
                Console.Clear();    
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Registrar Chamado");
                Console.WriteLine("------------------------------------");

                Chamado chamado = ObterDados();

                reposiChama.RegistrarChamado(chamado);

                Console.WriteLine($"\nO chamado: \"{chamado.titulo}\" foi registrado com sucesso!");
                Console.ReadLine();
            }

            private Chamado ObterDados()
            {
                Console.WriteLine("O ID do seu chamado é " + id);
                id++;

                Console.WriteLine("Qual o título do seu chamado: ");
                string titulo = Console.ReadLine();

                Console.WriteLine("Qual a descrição do seu chamado (o que ele precisa resolver)");
                string descricao = Console.ReadLine();

                Console.WriteLine("Qual equipamento precisa ser consertado/substituido");
                string equipamento = Console.ReadLine();

                Console.WriteLine("Qual a data que está sendo aberto o chamado");
                DateTime dataAbertura = Convert.ToDateTime(Console.ReadLine());

                Chamado chamado = new Chamado();
                chamado.id = id;
                chamado.titulo = titulo;
                chamado.descricao = descricao;
                chamado.equipamento = equipamento;
                chamado.dataAbertura = dataAbertura;

                return chamado;
            }

            public void VisualizarChamados()
            {
                Console.Clear();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Visualizar Chamados");
                Console.WriteLine("------------------------------------");

                Console.WriteLine(
                    "{0, -4} | {1, -16} | {2, -44} | {3, -22} | {4, -4}",
                    "Id", "Título", "Descrição", "Equipamento(s)", "Data de Abertura"
                );

                Chamado[] chamados = reposiChama.SelecionarChamados();

                for (int i = 0; i < chamados.Length; i++)
                {
                    Chamado c = chamados[i];

                    if (c == null)
                        continue;

                    Console.WriteLine(
                        "{0, -4} | {1, -16} | {2, -44} | {3, -22} | {4, -4}",
                        c.id, c.titulo, c.descricao, c.equipamento, c.dataAbertura.ToShortDateString()
                    );
                }
                Console.ReadLine();
            }

            internal void EditarChamado()
            {
                Console.Clear();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Editar Chamado");
                Console.WriteLine("------------------------------------");

                VisualizarChamados();

                Console.WriteLine("Informe o ID do chamado que deseja editar: ");
                int idSelecionado = Convert.ToInt32(Console.ReadLine());

                Chamado chamadoAtualizado = ObterDados();

                bool conseguiuEditar = reposiChama.EditarChamado(idSelecionado, chamadoAtualizado);

                if (!conseguiuEditar)
                {
                    Console.WriteLine("Não foi posssível encontrar o registro selecionado.");
                    Console.ReadLine();

                    return;
                }

                Console.WriteLine($"\nO chamado: \"{chamadoAtualizado.titulo}\" foi editado com sucesso!");
                Console.ReadLine();
            }

            internal void ExcluirRegistro()
            {
                Console.Clear();
                Console.WriteLine("----------------------------");
                Console.WriteLine("Exclusão de Chamado");
                Console.WriteLine("----------------------------");

                VisualizarChamados();

                Console.WriteLine("Informe o ID do chamado que deseja excluir:");
                int idSelecionado = Convert.ToInt32(Console.ReadLine());

                bool conseguiuExcluir = reposiChama.ExcluirChamado(idSelecionado);

                if (!conseguiuExcluir)
                {
                    Console.WriteLine("Não foi posssível encontrar o registro selecionado.");
                    Console.ReadLine();

                    return;
                }

                Console.WriteLine($"\nO chamado foi excluído com sucesso!");
                Console.ReadLine();
            }
        }
    }
}
