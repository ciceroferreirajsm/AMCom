using System;

namespace Questao5.Domain.Entities
{
    /// <summary>
    /// Representa um movimento bancário em uma conta corrente.
    /// </summary>
    public class Movimento
    {
        /// <summary>
        /// Identificador único do movimento.
        /// </summary>
        /// <value>O ID do movimento.</value>
        public int idmovimento { get; set; }

        /// <summary>
        /// Identificador da conta corrente associada ao movimento.
        /// </summary>
        /// <value>O ID da conta corrente.</value>
        public string idcontacorrente { get; set; }

        /// <summary>
        /// Valor do movimento bancário.
        /// </summary>
        /// <value>O valor do movimento.</value>
        public decimal valor { get; set; }

        /// <summary>
        /// Tipo do movimento bancário (exemplo: "Débito", "Crédito").
        /// </summary>
        /// <value>O tipo do movimento.</value>
        public string tipomovimento { get; set; }

        /// <summary>
        /// Data e hora do movimento.
        /// </summary>
        /// <value>A data e hora em que o movimento foi realizado.</value>
        public DateTime datamovimento { get; set; }
    }
}
