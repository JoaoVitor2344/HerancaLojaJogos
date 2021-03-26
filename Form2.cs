using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HerançaLojajogos
{
    public partial class Form2 : HerançaLojajogos.Form1
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\Arquivos\\Projetos\\HerançaLojajogos\\Banco_Heranca.mdf;Integrated Security=True;Connect Timeout=30");

        public void CarregaDgvPlataforma()
        {
            String query = "SELECT * FROM estoque WHERE codigo = 2";
            SqlCommand cmd = new SqlCommand(query, con);
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable plataforma = new DataTable();
            da.Fill(plataforma);
            dgvJogos.DataSource = plataforma;
            con.Close();
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CarregaDgvPlataforma();
            for (int i = 0; i < cbxTipo.Items.Count; i++)
            {
                cbxTipo.Items.Clear();
            }
            cbxTipo.Items.Add("Console");
            cbxTipo.Items.Add("Portátil");
            cbxTipo.Items.Add("Mobile");
            cbxTipo.Items.Add("Computador");
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "SELECT * FROM estoque WHERE id = @id AND codigo = 2";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", this.txtId.Text);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtId.Text = rd["id"].ToString();
                    txtNome.Text = rd["nome"].ToString();
                    cbxTipo.Text = rd["tipo"].ToString();
                    txtDev.Text = rd["desenvolvedora"].ToString();
                    txtPreco.Text = rd["preco"].ToString();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Nenhum registro encontrado!", "Sem registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "INSERT INTO estoque(nome, tipo, desenvolvedora, preco, codigo) VALUES(@nome, @tipo, @dev, @preco, @codigo)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@tipo", cbxTipo.Text);
                cmd.Parameters.AddWithValue("@dev", txtDev.Text);
                cmd.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(txtPreco.Text);
                cmd.Parameters.AddWithValue("@codigo", 2);
                con.Open();
                cmd.ExecuteNonQuery();
                String pesquisa = "DELETE FROM estoque WHERE nome = @nome AND codigo = 1";
                SqlCommand cmdExcluir = new SqlCommand(pesquisa, con);
                cmdExcluir.CommandType = CommandType.Text;
                cmdExcluir.Parameters.AddWithValue("@nome", txtNome.Text);
                cmdExcluir.ExecuteNonQuery();
                con.Close();
                CarregaDgvPlataforma();
                MessageBox.Show("Cadastro Realizado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            String query = "UPDATE estoque SET nome = @nome, tipo = @tipo, desenvolvedora = @dev, preco = @preco WHERE id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@tipo", cbxTipo.Text);
            cmd.Parameters.AddWithValue("@dev", txtDev.Text);
            cmd.Parameters.AddWithValue("@preco", txtPreco.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            CarregaDgvPlataforma();
            MessageBox.Show("Registro atualizado com sucesso!", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            txtId.Text = "";
            txtNome.Text = "";
            cbxTipo.Text = "";
            txtDev.Text = "";
            txtPreco.Text = "";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            String query = "DELETE FROM estoque WHERE id = @id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", this.txtId.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            CarregaDgvPlataforma();
            MessageBox.Show("Registro apagado com sucesso!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            txtId.Text = "";
            txtNome.Text = "";
            cbxTipo.Text = "";
            txtDev.Text = "";
            txtPreco.Text = "";
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtDev.Text = "";
            txtPreco.Text = "";
            cbxTipo.Text = "";
        }
    }
}
