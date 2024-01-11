using TS.Core.Interfaces;
using TS.Model.ViewModels;
using TS.Data.Interfaces;
using TS.Models.Models.Validations;
using AutoMapper;
using TS.Model.Models;
using TS.Model.Interfaces;
using System.Linq;

namespace TS.Core.Services
{
    public class PremioService : BaseService, IPremioService
    {
        private readonly IPremioRepository _PremioRepository;
        private readonly ICartelaService _cartelaService;
        private readonly IMapper _mapper;

        public PremioService(IPremioRepository PremioRepository,
                              IMapper mapper,
                              ICartelaService cartelaService,
                              INotificador notificador) : base(notificador)
        {
            _PremioRepository = PremioRepository;
            _mapper = mapper;
            _cartelaService = cartelaService;
        }

        public async Task Adicionar(Premio Premio)
        {
            if (!ExecutarValidacao(new PremioValidation(this), Premio)) return;

            await _PremioRepository.Adicionar(Premio);
        }

        public async Task Atualizar(Premio Premio)
        {
            if (!ExecutarValidacao(new PremioValidation(this), Premio)) return;

            await _PremioRepository.Atualizar(Premio);
        }

        public async Task Remover(int id)
        {
            var obj = await _PremioRepository.ObterPorId(id);
            await _PremioRepository.Remover(obj);
        }

        public bool CodigoExistente(string codigo)
        {
            return _PremioRepository.CodigoExistente(codigo);
        }

        public void Dispose()
        {
            _PremioRepository?.Dispose();
        }

        public async Task<ResponsePaginacaoViewModel<PremioViewModel>> ObterPremiosDisponiveisAsNoTracking(
            int pagina, 
            int tamanhoPagina)
        {
            // Aplica a paginação
            var totalItens = await TotalRegistros();
            var totalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina);

            var premios = _mapper.Map<List<PremioViewModel>>(
                await _PremioRepository
                .ObterPremiosDisponiveisAsNoTracking(pagina, tamanhoPagina));

            // Cria um objeto para retorno com os dados paginados
            return new ResponsePaginacaoViewModel<PremioViewModel>(totalItens,
                totalPaginas, pagina, tamanhoPagina, premios);
        }

        public async Task<CartelaViewModel> SortearCartela(int idPremio)
        {
            var cartelasDisponiveis = await _cartelaService
                .ObterTodosDisponiveisPraSorteioAsNoTracking(idPremio);

            if (!cartelasDisponiveis.Any())
            {
                Notificar("Nenhuma Cartela está disponivél pra ser sorteada");
                return null;
            }

            List<int> numerosSorteados = _cartelaService.GerarNumerosRandomicos(5);

            var cartelaSorteada = SortearCartela(numerosSorteados, cartelasDisponiveis);

            while (cartelaSorteada is null && numerosSorteados.Count < 10)
            {
                numerosSorteados.Add(GerarOutroNumeroRandomicoNaoSorteado(numerosSorteados));
                cartelaSorteada = SortearCartela(numerosSorteados, cartelasDisponiveis);
            }

            if(cartelaSorteada is null)
            {
                
            }

            try
            {
                cartelaSorteada.SetarComoSorteada();
                await _cartelaService.Atualizar(cartelaSorteada);

                var premio = await _PremioRepository.ObterPorIdAsNoTracking(idPremio);
                premio.SetaParaSorteado();
                premio.SetarDadosSorteados(numerosSorteados);
                await Atualizar(premio);
            }
            catch (Exception)
            {
                Notificar("Tivemos um problema para realizar o sorteoi, tente novamente.");
                return null;
            }

            return _mapper.Map<CartelaViewModel>(cartelaSorteada);
        }

        private int GerarOutroNumeroRandomicoNaoSorteado(List<int> numerosSorteados)
        {
            var novoNumero = _cartelaService.GerarNumerosRandomicos(1);

            while (numerosSorteados.Contains(novoNumero.FirstOrDefault()))
                novoNumero = _cartelaService.GerarNumerosRandomicos(1);

            return novoNumero.FirstOrDefault();
        }

        private static Cartela SortearCartela(List<int> numeros, List<Cartela> cartelas)
        {
            foreach (var cartela in cartelas)
            {
                var numeroCartela = new List<int> 
                                    { cartela.PrimeiroNumero,
                                        cartela.SegundoNumero,
                                        cartela.TerceiroNumero, 
                                        cartela.QuartoNumero,
                                        cartela.QuintoNumero};
                // Verifica se os 5 números da lista correspondem aos números da cartela
                if (numeroCartela.All(n => numeros.Contains(n)))
                {
                    return cartela; // Se correspondem, retorna essa cartela
                }
            }

            return null; // Retorna null se não encontrar nenhuma cartela com esses números
        }

        public async Task<PremioViewModel> ObterPremioECartelasAsNoTracking(int idPremio)
        {
            return _mapper.Map<PremioViewModel>(await _PremioRepository.ObterPremioECartelasAsNoTracking(idPremio));
        }

        public async Task<PremioViewModel> ObterPorIdAsNoTracking(int idPremio)
        {
            return _mapper.Map<PremioViewModel>(await _PremioRepository.ObterPorIdAsNoTracking(idPremio));
        }

        public async Task<int> TotalRegistros()
        {
            return await _PremioRepository.TotalRegistros();
        }
    }
}