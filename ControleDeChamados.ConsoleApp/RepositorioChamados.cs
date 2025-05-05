namespace ControleDeChamados.ConsoleApp
{
    internal partial class Program
    {
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
    }
}
