using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace ControleDeChamados.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TelaChamados tela = new TelaChamados();

            while (true)
            {
                char opcao = tela.ExibirMenu();

                if (opcao == 'S')
                    break;

                switch (opcao)
                {
                    case '1':
                        tela.RegistrarChamado();
                        break;

                    case '2':
                        tela.VisualizarChamados();
                        break;

                    case '3':
                        tela.EditarChamado();
                        break;

                    case '4':
                        tela.ExcluirRegistro();
                        break;
                }
            }
        }

        public class RepositorioChamados
        {
            public Chamado[] chamados = new Chamado[100];
            public int contadorChamados = 0;

            public void RegistrarChamado(Chamado chamado)
            {
                chamados[contadorChamados] = chamado;

                contadorChamados++;
            }

            public Chamado[] SelecionarChamados()
            {
                return chamados;
            }

            public bool EditarChamado(int idSelecionado, Chamado chamadoAtualizado)
            {
                Chamado chamadoSelecionado = SelecionarChamadoPorId(idSelecionado);

                if (chamadoSelecionado == null)
                    return false;

                chamadoSelecionado.id = chamadoAtualizado.id;
                chamadoSelecionado.titulo = chamadoAtualizado.titulo;
                chamadoSelecionado.descricao = chamadoAtualizado.descricao;
                chamadoSelecionado.equipamento = chamadoAtualizado.equipamento;
                chamadoSelecionado.dataAbertura = chamadoAtualizado.dataAbertura;

                return true;
            }

            public Chamado SelecionarChamadoPorId(int idSelecionado)
            {
                for (int i = 0; i < chamados.Length; i++)
                {
                    Chamado chamado = chamados[i];

                    if (chamado == null)
                        continue;

                    if (chamado.id == idSelecionado)
                        return chamado;
                }

                return null;
            }

            internal bool ExcluirChamado(int idSelecionado)
            {
                for (int i = 0; i < chamados.Length; i++)
                {
                    if (chamados[i].id == null)
                        continue;

                    if (chamados[i].id == idSelecionado)
                    {
                        chamados[i] = null;
                        return true;
                    }
                }

                return false;
            }
        }

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

        public class Chamado
        {
            public int id;
            public string titulo;
            public string descricao;
            public string equipamento;
            public DateTime dataAbertura;
        }
    }
}
