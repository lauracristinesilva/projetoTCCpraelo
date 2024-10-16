using infinitysky.Models;
using infinitysky.Repository;
using InfinitySky.Libraries.Login;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace infinitysky.Controllers
{
    public class HomeController : Controller
    {
        
        //[HttpPost]
        //public ActionResult SubmitForm(string nome, string tel, string email, string mensagem)
        //{
            // Aqui você pode processar os dados ou enviar por email
            // Exemplo de processamento:
         //   ViewBag.Message = "Formulário enviado com sucesso!";
         //   return View();
       // }
    
        private readonly ILogger<HomeController> _logger;

        // Interfaces para cliente e login 
        private IClienteRepository? _clienteRepositorio;
        private LoginCliente _loginCliente;

        //interface para planos
        private IPlanosRepositorio _planosRepositorio;

        //interface para pais
        private IPaisRepositorio _paisRepositorio;


        public HomeController(ILogger<HomeController> logger, IClienteRepository clienteRepositorio, LoginCliente loginCliente, IPlanosRepositorio planosRepositorio, IPaisRepositorio paisRepositorio)
        {
            _logger = logger;
            _clienteRepositorio = clienteRepositorio;
            _loginCliente = loginCliente;
            _planosRepositorio = planosRepositorio;
            _paisRepositorio = paisRepositorio;

        }

        public IActionResult Index(int IdPlano)
        {
                     
            var tresPrimeirosPlanos = _planosRepositorio.ObterPlanosAleatorios(1); 
            var restantePlanos = _planosRepositorio.ObterRestante(3); 


            //Chamando classe especifica com os nomes dos países
            var viewModel = new PlanosPrimeiraParte
            {
                TresPrimeirosPlanos = tresPrimeirosPlanos,
                RestantePlanos = restantePlanos
,

                // Adicione mais países aqui
            };

            return View(viewModel);

        }

        public IActionResult Plano(int idPais)
        {
            //Adicionando os Id's (mudar depois) -> precisa estar de acordo com o banco de dados 
            var planosCanada = _planosRepositorio.ObterPlanosPorPaisId(1);
            var planosPortugal = _planosRepositorio.ObterPlanosPorPaisId(2);
            var planosEstadosUnidos = _planosRepositorio.ObterPlanosPorPaisId(3);
            var planosArgentina = _planosRepositorio.ObterPlanosPorPaisId(4);
            var planosItalia = _planosRepositorio.ObterPlanosPorPaisId(5);
            var planosEspanha = _planosRepositorio.ObterPlanosPorPaisId(6);
            var planosAlemanha = _planosRepositorio.ObterPlanosPorPaisId(7);
            var planosAustralia = _planosRepositorio.ObterPlanosPorPaisId(8);
            var planosInglaterra = _planosRepositorio.ObterPlanosPorPaisId(9);
            var planosFranca = _planosRepositorio.ObterPlanosPorPaisId(10);
            var planosIrlanda = _planosRepositorio.ObterPlanosPorPaisId(11);
            var planosJapao = _planosRepositorio.ObterPlanosPorPaisId(12);
            var planosCoreiadoSul = _planosRepositorio.ObterPlanosPorPaisId(13);
            var canada = _paisRepositorio.ObterPais(1);
            var portugal = _paisRepositorio.ObterPais(2);
            var eua = _paisRepositorio.ObterPais(3);
            var argentina = _paisRepositorio.ObterPais(4);
            var italia = _paisRepositorio.ObterPais(5);
            var espanha = _paisRepositorio.ObterPais(6);
            var alemanha = _paisRepositorio.ObterPais(7);
            var australia = _paisRepositorio.ObterPais(8);
            var inglaterra = _paisRepositorio.ObterPais(9);
            var franca = _paisRepositorio.ObterPais(10);
            var irlanda = _paisRepositorio.ObterPais(11);
            var japao = _paisRepositorio.ObterPais(12);
            var coreia = _paisRepositorio.ObterPais(12);




            //Chamando classe especifica com os nomes dos países
            //Primeiro o nome da Ienuberable
            //Depois chama as variávels criadas acima que 
            // As variaveis estão armazenando o Metodo criado (planorepositorio) e o Id de cada pais
            var viewModel = new PlanosPorPaisViewModel
            {
                PlanosCanadá = planosCanada,
                PlanosPortugal = planosPortugal,
                PlanosEstadosUnidos = planosPortugal,
                PlanosArgentina = planosArgentina,
                PlanosItalia = planosItalia,
                PlanosEspanha = planosEspanha,
                PlanosAlemanha = planosAlemanha,
                PlanosAustrália = planosAustralia,
                PlanosInglaterra = planosInglaterra,
                PlanosFrança = planosFranca,
                PlanosIrlanda = planosIrlanda,
                PlanosJapão = planosJapao,
                PlanosCoreiadoSul = planosCoreiadoSul,
                Canada = canada,
                Portugal = portugal,
                EUA = eua,
                Argentina = argentina,
                Italia = italia,
                Espanha = espanha,
                Alemanha = alemanha,
                Australia = australia,
                Inglaterra = inglaterra,
                Franca = franca,
                Irlanda = irlanda,
                Japao = japao,
                Coreia = coreia,

            };

            return View(viewModel);

        }

        public IActionResult Detalhes(int Id)
        {
            var pais = _paisRepositorio.ObterPais(Id); // Verifique se _paisRepositorio.ObterPais(id) retorna um objeto válido

            if (pais == null)  // Se o país não for encontrado, lide com isso adequadamente
            {
                return NotFound();
            }

            // Buscar os planos associados a este país
            var planos = _planosRepositorio.ObterPlanosPorPaisId(Id);

            // Passar o país e os planos para a view
            var viewModel = new PaisesDetalhadosViewModel 
            {
                Pais = pais,
                Planos = (List<Planos>)planos
            };

            return View(viewModel);
        }


        public IActionResult AreaAdm()
        {
            return View();
        }


        public IActionResult PainelCliente()
        {
            return RedirectToAction(nameof(DadosCliente));
        }

        public IActionResult DadosCliente()
        {
            // Criando um novo método 
            // Retorna na página a lista de todos os clientes 
            //return View(_clienteRepositorio.TodosClientes());
            return View();
        }

        // Página de Login
        public IActionResult Login()
        {

            return View();
        }

        // Carrega a a tela login
        // Página de Login
        [HttpPost]
        public IActionResult Login(Cliente cliente)
        {
            // Verifica se é o administrador
            if (cliente.Email == "adm@gmail.com" && cliente.Senha == "adm123")
            {
                return RedirectToAction(nameof(AreaAdm)); // Redireciona para a área do administrador
            }

            // Tenta autenticar como cliente comum
            Cliente loginDB = _clienteRepositorio.Login(cliente.Email, cliente.Senha);

            if (loginDB != null && loginDB.Email != null && loginDB.Senha != null)
            {
                _loginCliente.Login(loginDB); // Loga o cliente no sistema
                return RedirectToAction(nameof(PainelCliente)); // Redireciona para o painel do cliente
            }
            else
            {
                // Retorna mensagem de erro
                ViewData["msg"] = "Dados errados";
                return View();
            }
        }


        public IActionResult CadastrarCliente()
        {
            return View();
        }

        //Vai vir como se fosse uma copia 
        //inicialmente com erro -> ligar com a model Cliente -> erro sai 
        // TODAS AS MODELS COM LETRAS MAIUSCULAS 
        //QUANDO FOR INSTANCIAR COMO COM LETRA minuscula


        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente) //criando uma instãncia 
        {
            //MÉTODO CADASTRAR

            //Para priorizar
            _clienteRepositorio.Cadastrar(cliente); //pegando a instancia de cima 

            return RedirectToAction(nameof(PainelCliente)); //nameoff-> variavel que busca alguma coisa
        }


        //Criando uma nova view para a pagina sobre nós/
        public IActionResult Sobre()
        {
            return View();
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
