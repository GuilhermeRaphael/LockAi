using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LockAi.Models.Enuns
{
    public enum SituacaoPlanoLocacao
    {
        Ativo = 1,     // O plano está ativo e disponível para contratação
        Inativo = 2,   // O plano está inativo e não pode ser usado
        Pendente = 3   // O plano foi criado mas ainda não foi ativado
    }
}