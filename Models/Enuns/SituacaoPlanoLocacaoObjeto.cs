namespace LockAi.Models.Enuns
{
    public enum SituacaoPlanoLocacaoObjeto
    {
        Disponivel = 1,       // O plano está disponível para o objeto
        Indisponivel = 2,     // O plano está indisponível
        Vinculado = 3,        // O plano está vinculado e ativo no objeto
        Desassociado = 4,     // O plano foi desassociado do objeto
        EmManutencao = 5      // O plano está em manutenção e não pode ser usado
    }
}