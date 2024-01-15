using System.Data;
using System.Windows.Forms;
using Domen.Entiteti;
using Domen.Funkcionalnosti;

namespace ParfemUI
{
    public partial class ProdajaParfema : Form
    {
        private readonly ParfemService parfemService;
        public ProdajaParfema(ParfemService parfemService)
        {
            this.parfemService = parfemService;
            InitializeComponent();
            dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            dataGridView1.CellClick += dataGridView1_CellClick;
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            await PopuniDataGridViewAsync();
        }
        private async void RestartujPolja()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        private async Task PopuniDataGridViewAsync()
        {
            var parfemi = await parfemService.SviParfemi();

            DataTable table = new DataTable();

            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Brend", typeof(string));
            table.Columns.Add("Ime", typeof(string));
            table.Columns.Add("Pol", typeof(string));

            foreach (var parfem in parfemi)
            {
                table.Rows.Add(parfem.ID, parfem.Ime, parfem.Brend, parfem.Pol);
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.DataSource = table;
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow selektovaniRed = dataGridView1.Rows[e.RowIndex];
                textBox4.Text = selektovaniRed.Cells[0].Value.ToString();
                textBox1.Text = selektovaniRed.Cells[1].Value.ToString();
                textBox2.Text = selektovaniRed.Cells[2].Value.ToString();
                textBox3.Text = selektovaniRed.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        private async void button4_Click(object sender, EventArgs e)
        {}
        private async void button3_Click(object sender, EventArgs e)
        {}

        private async void button1_Click(object sender, EventArgs e)
        {}

        private async void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string proizvodjac = textBox2.Text;
                string ime = textBox1.Text;
                string pol = textBox3.Text;


                Parfem parfem = new Parfem(guid, proizvodjac, ime, pol);

                await parfemService.DodajParfem(parfem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Dodavanje parfema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Parfem je uspešno dodat!");
            await PopuniDataGridViewAsync();
            RestartujPolja();
        }

        private async void button2_Click(object sender, EventArgs e){}

        private async void button3_Click_1(object sender, EventArgs e)
        {
            if (Guid.TryParse(textBox4.Text, out Guid parsedGuid))
            {
                try
                {
                    Parfem parfemzaIzmeniti = new(Guid.NewGuid(),
                        textBox2.Text,
                        textBox1.Text,
                        textBox3.Text
                        );

                    await parfemService.IzmeniParfem(parsedGuid, parfemzaIzmeniti);
                    MessageBox.Show("Uspešno je izmenjen parfem!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Izmena parfema nije uspela", "Izmena parfema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            await PopuniDataGridViewAsync();
            RestartujPolja();


        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Obrisati izabrani parfem?", "Brisanje parfema", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No) return;

            if (String.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Nijedan parfem nije izabran!");
                return;
            }

            if (Guid.TryParse(textBox4.Text, out Guid parsedGuid))
            {
                try
                {
                    await parfemService.Obrisi(parsedGuid);
                    MessageBox.Show("Parfem je uspešno obrisan!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Brisanje parfema nije uspelo", "Brisanje parfema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            await PopuniDataGridViewAsync();
            RestartujPolja();


        }

        private async void button1_Click_1(object sender, EventArgs e)
        {

            IEnumerable<Parfem>? filtrirano;

            try
            {
                if (textBox1.Text != "")
                {
                    filtrirano = await parfemService.SviParfemi(textBox1.Text);
                }

                else if (textBox2.Text != "")
                {
                    filtrirano = await parfemService.SviParfemi(null, textBox2.Text);
                }
                else if (textBox3.Text != "")
                {
                    filtrirano = await parfemService.SviParfemi(null, null, textBox3.Text);
                }
                else
                {
                    filtrirano = null;
                }
            }
            catch (Exception)
            {
                throw new ArgumentNullException();
            }

            if (filtrirano is not null)
            {
                DataTable table = new DataTable();

                table.Columns.Add("Id", typeof(Guid));
                table.Columns.Add("Brend", typeof(string));
                table.Columns.Add("Ime", typeof(string));
                table.Columns.Add("Pol", typeof(string));

                foreach (var item in filtrirano)
                {
                    table.Rows.Add(item.ID, item.Brend, item.Ime, item.Pol);
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.DataSource = table;
            }




        }
    }
}


    
       

        

        

        

        


        

        

        

        

        

        

       

        
   