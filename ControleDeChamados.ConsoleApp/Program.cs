using System.Drawing;
using System.Runtime.Intrinsics.X86;

namespace ControleDeChamados.ConsoleApp
{
    internal partial class Program
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
    }
}
