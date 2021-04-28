using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //.{=Kkq
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text)) dataGridView.DataSource = betaMartBindingSource;
                else
                {
                    var query = from o in this.appData.BetaMart
                                where o.NamaBarang.Contains(txtSearch.Text) || o.BeratBarang.Contains(txtSearch.Text) || 
                                o.TanggalMasuk.Contains(txtSearch.Text) || o.Supplier.Contains(txtSearch.Text) ||
                                o.Penerima.Contains(txtSearch.Text) || o.JumlahBarang.Contains(txtSearch.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            //.{=Kkq
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    betaMartBindingSource.RemoveCurrent();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtNamaBarang.Focus();
                this.appData.BetaMart.AddBetaMartRow(this.appData.BetaMart.NewBetaMartRow());
                betaMartBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betaMartBindingSource.ResetBindings(false);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtNamaBarang.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Anda Yakin Ingin Menghapusnya ?", "message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                betaMartBindingSource.RemoveCurrent();

        }
        //.{=Kkq
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                betaMartBindingSource.EndEdit();
                betaMartTableAdapter.Update(this.appData.BetaMart);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                betaMartBindingSource.ResetBindings(false);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.BetaMart' table. You can move, or remove it, as needed.
            this.betaMartTableAdapter.Fill(this.appData.BetaMart);
            betaMartBindingSource.DataSource = this.appData.BetaMart;
        }
    }
}
