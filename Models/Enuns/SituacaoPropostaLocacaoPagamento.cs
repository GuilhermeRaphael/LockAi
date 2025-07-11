using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Models.Enuns
{
    public enum SituacaoPropostaLocacaoPagamento
    {
        Pendente = 1,       // Aguardando pagamento ou upload do comprovante
        Enviado = 2,        // Comprovante enviado, aguardando validação
        Aprovado = 3,       // Comprovante verificado e pagamento aceito
        Reprovado = 4,      // Comprovante rejeitado ou inválido
        Cancelado = 5       // Pagamento cancelado pelo usuário ou pelo sistema
    }
}