using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HerançaLojajogos
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\Arquivos\\Projetos\\HerançaLojajogos\\Banco_Heranca.mdf;Integrated Security=True;Connect Timeout=30");

        public void CarregaDgvJogo()
        {
            String query = "SELECT * FROM estoque WHERE codigo = 1" ;
            SqlCommand cmd = new SqlCommand(query, con);
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable jogo = new DataTable();
            da.Fill(jogo);
            dgvJogos.DataSource = jogo;
            con.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDgvJogo();
            cbxTipo.Items.Add("FPS");
            cbxTipo.Items.Add("RPG");
            cbxTipo.Items.Add("Esporte");
            cbxTipo.Items.Add("MOBA");
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "SELECT * FROM estoque WHERE id = @id AND codigo = 1";
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
                cmd.Parameters.AddWithValue("@codigo", 1);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Cadastro Realizado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregaDgvJogo();
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
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Convert.ToInt32(txtId.Text);
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@tipo", cbxTipo.Text);
            cmd.Parameters.AddWithValue("@dev", txtDev.Text);
            cmd.Parameters.AddWithValue("@preco", SqlDbType.Decimal).Value = Convert.ToDecimal(txtPreco.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            CarregaDgvJogo();

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
            CarregaDgvJogo();
            MessageBox.Show("Registro apagado com sucesso!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            con.Close();
            txtId.Text = "";
            txtNome.Text = "";
            cbxTipo.Text = "";
            txtDev.Text = "";
            txtPreco.Text = "";
        }

        private void dgvJogos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvJogos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvJogos.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNome.Text = row.Cells[1].Value.ToString();
                cbxTipo.Text = row.Cells[2].Value.ToString();
                txtDev.Text = row.Cells[3].Value.ToString();
                txtPreco.Text = row.Cells[4].Value.ToString();
            }
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
