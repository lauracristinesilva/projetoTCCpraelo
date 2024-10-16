using infinitysky.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace infinitysky.Repository
{
    public class PlanosRepositorio : IPlanosRepositorio
    {
        private readonly string _conexaoMySQL;

        public PlanosRepositorio(IConfiguration conf)
        {
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public IEnumerable<Planos> ObterTodosPlanos()
        {
            List<Planos> planoList = new List<Planos>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM plano_tbl;", conexao);

                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    planoList.Add(
                        new Planos
                        {
                            IdPlano = Convert.ToInt64(dr["id_plano"]),
                            Nome = (string)dr["nome_plano"],
                            HospedagemPlano = (string)dr["hospedagem_plano"],
                            CursoPlano = (string)dr["curso_plano"],
                            InstituicaoPlano = (string)dr["instituicao_plano"],
                            PeriodoPlano = (string)dr["periodo_plano"],
                            DescricaoPlano = (string)dr["descricao_plano"],
                            image_plano = (string)dr["image_plano"],
                            Valor = Convert.ToDecimal(dr["valor"]),
                            IdPais = Convert.ToInt32(dr["id_pais"]),
                            ParcelaPlano = (string)dr["parcela_plano"]
                        });
                }
                return planoList;
            }
        }

        public List<Planos> ObterPlanosAleatorios(int IdPlano)
        {
            List<Planos> planoList = new List<Planos>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                // Chama a procedure
                MySqlCommand cmd = new MySqlCommand("SelecionarTresPrimeirosPlanos", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPlano", IdPlano);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    planoList.Add(new Planos
                    {
                        IdPlano = Convert.ToInt64(dr["id_plano"]),
                        Nome = dr["nome_plano"].ToString(),
                        HospedagemPlano = dr["hospedagem_plano"].ToString(),
                        CursoPlano = dr["curso_plano"].ToString(),
                        InstituicaoPlano = dr["instituicao_plano"].ToString(),
                        PeriodoPlano = dr["periodo_plano"].ToString(),
                        DescricaoPlano = dr["descricao_plano"].ToString(),
                        image_plano = dr["image_plano"].ToString(),
                        Valor = Convert.ToDecimal(dr["valor"]),
                        IdPais = Convert.ToInt32(dr["id_pais"]),
                        ParcelaPlano = dr["parcela_plano"].ToString()
                    });
                }
                conexao.Close();
            }
            return planoList;
        }

        public List<Planos> ObterRestante(int IdPlano)
        {
            List<Planos> planoList = new List<Planos>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                // Chama a procedure
                MySqlCommand cmd = new MySqlCommand("SelecionarSeisPlanos", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPlano", IdPlano);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    planoList.Add(new Planos
                    {
                        IdPlano = Convert.ToInt64(dr["id_plano"]),
                        Nome = dr["nome_plano"].ToString(),
                        HospedagemPlano = dr["hospedagem_plano"].ToString(),
                        CursoPlano = dr["curso_plano"].ToString(),
                        InstituicaoPlano = dr["instituicao_plano"].ToString(),
                        PeriodoPlano = dr["periodo_plano"].ToString(),
                        DescricaoPlano = dr["descricao_plano"].ToString(),
                        image_plano = dr["image_plano"].ToString(),
                        Valor = Convert.ToDecimal(dr["valor"]),
                        IdPais = Convert.ToInt32(dr["id_pais"]),
                        ParcelaPlano = dr["parcela_plano"].ToString()
                    });
                }
                conexao.Close();
            }
            return planoList;
        }

        public void Adicionar(Planos plano)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO plano_tbl (nome_plano, hospedagem_plano, curso_plano, instituicao_plano, periodo_plano, descricao_plano, imagem_plano, id_pais, valor) VALUES (@nome_plano, @Hospedagem, @Curso, @Instituicao, @Periodo, @Descricao, @Imagem, @IdPais, @Valor);", conexao);
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = plano.Nome;
                cmd.Parameters.Add("@Hospedagem", MySqlDbType.VarChar).Value = plano.HospedagemPlano;
                cmd.Parameters.Add("@Curso", MySqlDbType.VarChar).Value = plano.CursoPlano;
                cmd.Parameters.Add("@Instituicao", MySqlDbType.VarChar).Value = plano.InstituicaoPlano;
                cmd.Parameters.Add("@Periodo", MySqlDbType.VarChar).Value = plano.PeriodoPlano;
                cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = plano.DescricaoPlano;
                cmd.Parameters.Add("@Imagem", MySqlDbType.VarChar).Value = plano.image_plano;
                cmd.Parameters.Add("@IdPais", MySqlDbType.Int32).Value = plano.IdPais;
                cmd.Parameters.Add("@Valor", MySqlDbType.Decimal).Value = plano.Valor;
                cmd.Parameters.Add("@Parcela", MySqlDbType.VarChar).Value = plano.ParcelaPlano;


                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public Planos ObterPlano(long Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM plano_tbl WHERE id_plano=@IdPlano", conexao);
                cmd.Parameters.Add("@IdPlano", MySqlDbType.Int64).Value = Id;

                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Planos plano = new Planos();

                if (dr.Read())
                {
                    plano.IdPlano = Convert.ToInt64(dr["id_plano"]);

                    plano.Nome = (string)dr["nome_plano"];
                    plano.HospedagemPlano = (string)dr["hospedagem_plano"];
                    plano.CursoPlano = (string)dr["curso_plano"];
                    plano.InstituicaoPlano = (string)dr["instituicao_plano"];
                    plano.PeriodoPlano = (string)dr["periodo_plano"];
                    plano.DescricaoPlano = (string)dr["descricao_plano"];
                    plano.image_plano = (string)dr["imagem_plano"];
                    plano.Valor = Convert.ToDecimal(dr["valor"]);
                    plano.IdPais = Convert.ToInt32(dr["id_pais"]);
                    plano.ParcelaPlano = (string)dr["parcela_plano"];

                }
                return plano;
            }
        }

        public IEnumerable<Planos> PesquisaPlanos(string nome)
        {
            List<Planos> planoList = new List<Planos>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM plano_tbl WHERE curso_plano LIKE @Nome;", conexao);
                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = "%" + nome + "%"; // Para busca parcial

                MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sd.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    planoList.Add(
                        new Planos
                        {
                            IdPlano = Convert.ToInt64(dr["id_plano"]),
                            Nome = (string)dr["nome_plano"],
                            HospedagemPlano = (string)dr["hospedagem_plano"],
                            CursoPlano = (string)dr["curso_plano"],
                            Valor = Convert.ToDecimal(dr["valor"]),
                            image_plano = (string)dr["image_plano"],
                            IdPais = Convert.ToInt32(dr["id_pais"]),
                            ParcelaPlano = (string)dr["parcela_plano"]

                        });
                }
                return planoList;
            }
        }

        public IEnumerable<Planos> ObterPlanosPorPaisId(int paisId)
        {
            List<Planos> planoList = new List<Planos>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                // Chama a procedure
                MySqlCommand cmd = new MySqlCommand("SelecionarPlanosPorPaisIdsO", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaisId", paisId);

                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    planoList.Add(new Planos
                    {
                        IdPlano = Convert.ToInt64(dr["id_plano"]),
                        Nome = dr["nome_plano"].ToString(),
                        HospedagemPlano = dr["hospedagem_plano"].ToString(),
                        CursoPlano = dr["curso_plano"].ToString(),
                        InstituicaoPlano = dr["instituicao_plano"].ToString(),
                        PeriodoPlano = dr["periodo_plano"].ToString(),
                        DescricaoPlano = dr["descricao_plano"].ToString(),
                        image_plano = dr["image_plano"].ToString(),
                        Valor = Convert.ToDecimal(dr["valor"]),
                        IdPais = Convert.ToInt32(dr["id_pais"]),
                        ParcelaPlano = dr["parcela_plano"].ToString()
                    });
                }
                conexao.Close();
            }
            return planoList;
        }

    

public void Apagar(long Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM plano_tbl WHERE id_plano = @IdPlano;", conexao);
                cmd.Parameters.Add("@IdPlano", MySqlDbType.Int64).Value = Id;

                cmd.ExecuteNonQuery();
            }
        }

      
    }
}