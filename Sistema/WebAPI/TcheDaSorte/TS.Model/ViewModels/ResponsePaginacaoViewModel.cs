using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.Model.ViewModels
{
    public class ResponsePaginacaoViewModel<T>
    {
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public List<T> Resultados { get; set; }

        public ResponsePaginacaoViewModel(int totalItens, 
            int totalPaginas, 
            int paginaAtual, 
            int tamanhoPagina,
            List<T> resultados)
        {
            TotalItens = totalItens;
            TotalPaginas = totalPaginas;
            PaginaAtual = paginaAtual;
            TamanhoPagina = tamanhoPagina;
            Resultados = resultados;
        }
    }
}
