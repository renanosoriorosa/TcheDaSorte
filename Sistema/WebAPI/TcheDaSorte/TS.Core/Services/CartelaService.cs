using TS.Core.Interfaces;
using TS.Model.Interfaces;
using TS.Data.Interfaces;
using TS.Models.Models.Validations;
using AutoMapper;
using TS.Model.Models;
using TS.Model.ViewModels;
using TS.Data.Repository;

namespace TS.Core.Services
{
    public class CartelaService : BaseService, ICartelaService
    {
        private readonly ICartelaRepository _CartelaRepository;
        private readonly IMapper _mapper;

        public CartelaService(ICartelaRepository CartelaRepository,
                              IMapper mapper,
                              INotificador notificador) : base(notificador)
        {
            _CartelaRepository = CartelaRepository;
            _mapper = mapper;
        }

        public async Task Adicionar(Cartela Cartela)
        {
            if (!ExecutarValidacao(new CartelaValidation(), Cartela)) return;

            await _CartelaRepository.Adicionar(Cartela);
        }

        public async Task Atualizar(Cartela Cartela)
        {
            if (!ExecutarValidacao(new CartelaValidation(), Cartela)) return;

            await _CartelaRepository.Atualizar(Cartela);
        }

        public async Task Remover(int id)
        {
            var obj = await _CartelaRepository.ObterPorId(id);
            await _CartelaRepository.Remover(obj);
        }

        public void Dispose()
        {
            _CartelaRepository?.Dispose();
        }

        public async Task CriarCartelasParaPremio(int idPremio, int numeroCartelas, double preco)
        {
            for (int i = 1; i <= numeroCartelas; i++)
            {
                await Adicionar(GerarCartelaRandomica(idPremio, preco));
            }
        }

        public Cartela GerarCartelaRandomica(int idPremio, double preco)
        {
            List<int> nuemrosCartela = GerarNumerosRandomicos(5);

            return new Cartela(idPremio, preco, 
                nuemrosCartela[0],
                nuemrosCartela[1],
                nuemrosCartela[2],
                nuemrosCartela[3],
                nuemrosCartela[4]
                );
        }

        public List<int> GerarNumerosRandomicos(int quantidadeNumero)
        {
            Random random = new Random();
            List<int> numeros = new List<int>();

            while (numeros.Count < quantidadeNumero)
            {
                int numero = random.Next(1, 10); // Define o intervalo de números aleatórios desejado

                if (!numeros.Contains(numero))
                {
                    numeros.Add(numero);
                }
            }

            return numeros;
        }

        public async Task RemoverTodasAsCartelasDoPremio(int idPremio)
        {
            var cartelas = await _CartelaRepository.ObterTodosPorPremioId(idPremio);

            foreach (var cartela in cartelas)
            {
                await Remover(cartela.Id);
            }
        }

        public async Task<List<CartelaViewModel>> ObterTodosPorPremioId(int idPremio)
        {
            return _mapper.Map<List<CartelaViewModel>>(await _CartelaRepository
                .ObterTodosPorPremioId(idPremio));
        }

        public async Task<ResponsePaginacaoViewModel<CartelaViewModel>> ObterTodosPorIdUsuario(
            int idUsuario, 
            int pagina, 
            int tamanhoPagina)
        {
            // Aplica a paginação
            var totalItens = await TotalRegistros();
            var totalPaginas = (int)Math.Ceiling(totalItens / (double)tamanhoPagina);

            var cartelas = _mapper.Map<List<CartelaViewModel>>(
                await _CartelaRepository
                .ObterTodosPorIdUsuario(idUsuario, pagina, tamanhoPagina));

            // Cria um objeto para retorno com os dados paginados
            return new ResponsePaginacaoViewModel<CartelaViewModel>(totalItens,
                totalPaginas, pagina, tamanhoPagina, cartelas);
        }

        public async Task<CartelaViewModel> ObterPorId(int idCartela)
        {
            return _mapper.Map<CartelaViewModel>(await _CartelaRepository.ObterPorId(idCartela));
        }

        public async Task<CartelaViewModel> ObterTodosOsDadosPorId(int idCartela)
        {
            return _mapper.Map<CartelaViewModel>(await _CartelaRepository
                .ObterTodosOsDadosPorId(idCartela));
        }

        public async Task<Cartela> ObterCartelaPorId(int idCartela)
        {
            return await _CartelaRepository.ObterPorId(idCartela);
        }

        public async Task<int> TotalRegistros()
        {
            return await _CartelaRepository.TotalRegistros();
        }

        public async Task<List<Cartela>> ObterTodosDisponiveisPraSorteioAsNoTracking(int idPremio)
        {
            return await _CartelaRepository.ObterTodosDisponiveisPraSorteioAsNoTracking(idPremio);
        }
    }
}