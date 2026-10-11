namespace AutoGUI
{
    public partial class Form1 : Form
    {
        List<Auto> autos = new List<Auto>();

        // listBox1   = gyártók (bal oldal)
        // listBox2   = keresés eredménye (jobb oldal)
        // comboBox2  = karosszéria típusa
        // comboBox1  = váltó típusa
        // textBox1, textBox2 = évek száma (tól - ig)

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            button1.Click += Button1_Click;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // 11. beolvasás + gyártók
            Read();
            GyartokKiirasa();
            listBox1.SelectedIndex = 0;

            // 12. karosszériák, nincs kiválasztva semmi
            KarosszeriakKiirasa();

            // 13. váltók, nincs kiválasztva semmi
            ValtokKiirasa();

            // 14. ajtók száma: egyik sincs kiválasztva
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            this.ActiveControl = listBox1; // hogy ne a rádiógomb kapja a fókuszt (az bejelölné)

            // 15. az évek mezői üresek
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Read()
        {
            using (StreamReader o = new StreamReader("../../../forras/carsData.csv"))
            {
                o.ReadLine();
                while (!o.EndOfStream)
                {
                    autos.Add(new Auto(o.ReadLine()));
                }
            }
        }

        private void GyartokKiirasa()
        {
            listBox1.Items.Clear();
            foreach (var auto in autos)
            {
                string gyarto = auto.Gyarto.GyartoName;
                if (!listBox1.Items.Contains(gyarto))
                {
                    listBox1.Items.Add(gyarto);
                }
            }
        }

        private void KarosszeriakKiirasa()
        {
            comboBox2.Items.Clear();
            foreach (var auto in autos)
            {
                string karosszeria = auto.Karosszeria.KarosszeriaName;
                if (!comboBox2.Items.Contains(karosszeria))
                {
                    comboBox2.Items.Add(karosszeria);
                }
            }
        }

        private void ValtokKiirasa()
        {
            comboBox1.Items.Clear();
            foreach (var auto in autos)
            {
                string valto = auto.Valto.ValtoName;
                if (!comboBox1.Items.Contains(valto))
                {
                    comboBox1.Items.Add(valto);
                }
            }
        }

        // 16. Keresés gomb
        private void Button1_Click(object? sender, EventArgs e)
        {
            // 17. előző találatok törlése
            listBox2.Items.Clear();

            string gyarto = listBox1.SelectedItem.ToString();
            string karosszeria = comboBox2.SelectedItem.ToString();
            string valto = comboBox1.SelectedItem.ToString();

            int ajtok = 0;
            if (radioButton1.Checked) ajtok = 2;
            else if (radioButton2.Checked) ajtok = 3;
            else if (radioButton3.Checked) ajtok = 4;
            else if (radioButton4.Checked) ajtok = 5;

            int minKor = int.Parse(textBox1.Text);
            int maxKor = int.Parse(textBox2.Text);
            int aktualisEv = DateTime.Now.Year;

            foreach (var auto in autos)
            {
                int kor = aktualisEv - auto.Evjarat;

                if (auto.Gyarto.GyartoName == gyarto &&
                    auto.Karosszeria.KarosszeriaName == karosszeria &&
                    auto.Valto.ValtoName == valto &&
                    auto.Ajtok == ajtok &&
                    kor >= minKor && kor <= maxKor)
                {
                    listBox2.Items.Add($"{auto.Azonosito}. - {auto.Evjarat} - {auto.Modell} - {auto.KilometeroraAllas} km - {auto.Ar} CAD");
                }
            }
        }
    }
}
