using infinitysky.Models;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace infinitysky.Repository
{
    public class ClienteRepository : IClienteRepository
    {

        //declarando a varival de da string de conxão

        private readonly string? _conexaoMySQL;

        //metodo da conexão com banco de dados
        public ClienteRepository(IConfiguration conf) => _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");

        //Login Cliente(metodo )

        public Cliente Login(string Email, string Senha)
        {
            //usando a variavel conexao 
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                //abre a conexão com o banco de dados
                conexao.Open();

                // variavel cmd que receb o select do banco de dados buscando email e senha
                MySqlCommand cmd = new MySqlCommand("select * from Cliente_tbl where email_cliente = @email_cliente and senha_cliente = @senha_cliente", conexao);

                //os paramentros do email e da senha 
                cmd.Parameters.Add("@email_cliente", MySqlDbType.VarChar).Value = Email;
                cmd.Parameters.Add("@senha_cliente", MySqlDbType.VarChar).Value = Senha;

                //Le os dados que foi pego do email e senha do banco de dados
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                //guarda os dados que foi pego do email e senha do banco de dados
                MySqlDataReader dr;

                //instanciando a model cliente
                Cliente cliente = new Cliente();
                //executando os comandos do mysql e passsando paa a variavel dr
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                //verifica todos os dados que foram pego do banco e pega o email e senha
                while (dr.Read())
                {

                    cliente.Email = Convert.ToString(dr["email_cliente"]);
                    cliente.Senha = Convert.ToString(dr["senha_cliente"]);
                }
                return cliente;
            }
        }

        //Cadastrar CLIENTE

        public void Cadastrar(Cliente cliente) //instanciando a classe 
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))

            {
                conexao.Open();

                //Primeiro temos que inserir na tabela Cliente 
                // Inserir o telefone na tabela Telefone_tbl e recuperar o id gerado
                MySqlCommand cmdTelefone = new MySqlCommand("INSERT INTO Telefone_tbl (telefone_cliente) VALUES (@telefone_cliente); SELECT LAST_INSERT_ID();", conexao);
                cmdTelefone.Parameters.Add("@telefone_cliente", MySqlDbType.VarChar).Value = cliente.Telefone;
                int idTelefone = Convert.ToInt32(cmdTelefone.ExecuteScalar());

                // Agora insira o cliente na tabela Cliente_tbl usando o id gerado para o telefone
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Cliente_tbl (nome_cliente, id_telefone, data_nascimento, cpf_cliente, email_cliente, senha_cliente) VALUES (@nome_cliente, @id_telefone, @data_nascimento, @cpf_cliente, @email_cliente, @senha_cliente)", conexao);

                cmd.Parameters.Add("@nome_cliente", MySqlDbType.VarChar).Value = cliente.Nome;
                cmd.Parameters.Add("@id_telefone", MySqlDbType.Int32).Value = idTelefone;  // Use o idTelefone gerado pela inserção anterior
                cmd.Parameters.Add("@data_nascimento", MySqlDbType.Date).Value = cliente.Data_Nascimento;
                cmd.Parameters.Add("@cpf_cliente", MySqlDbType.VarChar).Value = cliente.Cpf_Cliente;
                cmd.Parameters.Add("@email_cliente", MySqlDbType.VarChar).Value = cliente.Email;
                cmd.Parameters.Add("@senha_cliente", MySqlDbType.VarChar).Value = cliente.Senha;



                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }
    }


}