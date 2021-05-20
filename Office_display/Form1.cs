using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.IO.Ports;
using System.Xml;
using System.Threading;

namespace Office_display
{
    public partial class Form1 : Form
    {

        //ConnectionString-ek
        String pda_conn = ConfigurationManager.AppSettings["pda_connectionstring"];
        String repair_conn = ConfigurationManager.AppSettings["repair_connectionstring"] + "rep123.;";
        String prod_conn = ConfigurationManager.AppSettings["prod_connectionstring"] + "BalKoc%22;";
        String amplio_conn = ConfigurationManager.AppSettings["amplio_connectionstring"] + "BalKoc%22;";
        String mes_conn = ConfigurationManager.AppSettings["mes_connectionstring"] + "MoMe19l.;";
        String pass = "LauraStar2017;";

        //Ciklusváltozó más-más információ megjelenítéséhez
        int i = 1;
        int j = 1;

        //Az idő figyelésének gyakorisága appconfig-ból
        //int timequery_timer_interval = Int32.Parse(ConfigurationManager.AppSettings["timequery_timer_interval"]);
        //A böngésző frissítésének időzítője
        int browser_timer_interval = Int32.Parse(ConfigurationManager.AppSettings["browser_timer_interval"]);
        //A program időjárás master-ként működik
        Boolean browser_timer_enabled = Boolean.Parse(ConfigurationManager.AppSettings["is_weathermaster"]);
        //Az információs sáv vezérléséhez tartozó időzítő
        int info_timer_interval = Int32.Parse(ConfigurationManager.AppSettings["info_timer_interval"]);
        //Fagyveszély-jelzés alsó határa
        int temp_LO = Int32.Parse(ConfigurationManager.AppSettings["temp_low_limit"]);
        //A hőségre figyelmezető jelzés határa
        int temp_HI = Int32.Parse(ConfigurationManager.AppSettings["temp_high_limit"]);
        //Az értékek frissítés óta eltelt, maximális megengedett ideje
        Double time_diff_MAX = Double.Parse(ConfigurationManager.AppSettings["values_accepted_time"]);
        //A hőmérsékleti adat más programokkal történő megosztásának elérési útja
        String weatherText_path = ConfigurationManager.AppSettings["temperature_shared_path"];
        //A debugbox láthatósága
        Boolean debugBox_visible = Boolean.Parse(ConfigurationManager.AppSettings["debugbox_visible"]);
        //A multirepair adatok láthatósága
        Boolean MR_visible = Boolean.Parse(ConfigurationManager.AppSettings["multirepair_visible"]);
        //A javításta váró, regisztrált gépek számának láthatósága (S és G)
        Boolean ForRep_visible = Boolean.Parse(ConfigurationManager.AppSettings["srs_for_repair_visible"]);
        //A soros port használatának engedélyezése
        Boolean senddata_enabled = Boolean.Parse(ConfigurationManager.AppSettings["serial_enabled"]);
        //A soros port kiválasztása
        String serial_port = ConfigurationManager.AppSettings["portname"];
        //Az időjárási adatokat szolgáltató honlap url-je
        //String weather_url = "http://" + ConfigurationManager.AppSettings["weather_url"];
        String WeatherURL = "http://api.openweathermap.org/data/2.5/weather?id=3050594&units=metric&mode=xml&APPID=9706a07e4d4fedc41043a14bafdaea08";

        String repQtyStr = "SELECT COUNT (DISTINCT inc_headers.incident_number) AS Count_REP FROM public.inc_headers, status_log" +
                           " WHERE inc_headers.rma = 'N' AND status_log.incident_number = inc_headers.incident_number AND" +
                           " status_log.status_code = 103 AND status_log.enter BETWEEN ? AND ?;";

        String refurbQtyStr = "SELECT COUNT (DISTINCT inc_headers.incident_number) AS Count_REFURB FROM public.inc_headers, status_log" +
                          " WHERE inc_headers.rma = 'Y' AND status_log.incident_number = inc_headers.incident_number AND" +
                          " (status_log.status_code = 103 OR status_log.status_code = 203) AND status_log.enter BETWEEN ? AND ?;";

        String MRQtyStr = "SELECT COUNT (DISTINCT inc_headers.incident_number) AS Count_MR FROM public.inc_headers, status_log" +
                          " WHERE inc_headers.rma = 'N' AND status_log.incident_number = inc_headers.incident_number AND" +
                          " status_log.technician_code IS NULL AND inc_headers.multi_repair IS NOT NULL AND" +
                          " (status_log.status_code = 100 OR status_log.status_code = 110) AND status_log.enter BETWEEN ? AND ?;";

        String TempValueUpdateStr = "UPDATE settings SET value = @temperature WHERE setting = 'temperature';";

        String MRCorrStr = "SELECT value FROM settings WHERE setting = 'MR_corr';";

        String clockQuery = "SELECT * FROM clock WHERE time = @current_time;";

        String UpdateSunrise = "UPDATE settings SET value = @sun WHERE setting = 'sunrise';";

        String UpdateRefreshTime = "UPDATE settings SET value = @refresh WHERE setting = 'last_update';";

        String UpdateSunset = "UPDATE settings SET value = @sun WHERE setting = 'sunset';";

        String TemperatureFromDatabase = "SELECT value from settings WHERE setting = 'temperature';";

        String TimeStampFromDataBase = "SELECT value from settings WHERE setting = 'last_update';";

        String SunriseFromDataBase = "SELECT value FROM settings WHERE setting = 'sunrise';";

        String SunsetFromDataBase = "SELECT value FROM settings WHERE setting = 'sunset';";

        String SForRepair = "SELECT COUNT (status.enter) AS COUNT_S_For_Rep FROM status WHERE status.status_code = 100 AND status.technician_code" +
                            " IS NULL AND status.machine_type = 'S';";

        String GForRepair = "SELECT COUNT (status.enter) AS COUNT_G_For_Rep FROM status WHERE status.status_code = 100 AND status.technician_code" +
                            " IS NULL AND status.machine_type = 'G';";

        String BalanceQuery = System.IO.File.ReadAllText("balance_sql.txt");

        String GetCountOfTestedAppliancesOfCurrentModel = ConfigurationManager.AppSettings["EOL_tested_items"];
        String GetCurrentlyEOLTestedModelQuery = ConfigurationManager.AppSettings["currently_EOL_tested_model"];
        String TestedModelsQuery = ConfigurationManager.AppSettings["tested_models"];
        String SumQtyOfTestedModels = ConfigurationManager.AppSettings["sumqty_of_tested_models"];
        String QtyProducedByLineQuery = ConfigurationManager.AppSettings["quantities_produced_by_line"];
        String PlannedQtyQuery = "SELECT other FROM dbo.codes WHERE type = 'P' AND MES_code = 12;";

        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        Double redHI = Double.Parse(ConfigurationManager.AppSettings["red_HI"]);
        Double orangeHI = Double.Parse(ConfigurationManager.AppSettings["orange_HI"]);
        Double yellowHI = Double.Parse(ConfigurationManager.AppSettings["yellow_HI"]);
        Double darkgreenHI = Double.Parse(ConfigurationManager.AppSettings["darkgreen_HI"]);

        //Kapcsolat definiálása a PDA-szerverhez
        OdbcConnection pda_connection = new OdbcConnection();

        //Kapcsolat definiálása a repair adabázishoz
        SqlConnection repair_connection = new SqlConnection();

        //Kapcsolat definiálása a production adabázishoz
        SqlConnection prod_connection = new SqlConnection();

        //Kapcsolat definiálása az Amplio adabázishoz
        SqlConnection amplio_connection = new SqlConnection();

        //Kapcsolat definiálása a MES/production adatbázishoz
        SqlConnection mes_connection = new SqlConnection();

        public Form1()
        {
            InitializeComponent();

            //A Form inicializálása, a képenyőfelbontádshoz történő beállítása
            FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            debugBox.Text += "Képernyőfelbontás: " + Screen.PrimaryScreen.Bounds.Width.ToString() + " x " + Screen.PrimaryScreen.Bounds.Height.ToString() + "\r\n";

            double widthScale = (double)(screenWidth) / 800;
            double heightScale = (double)(screenHeight) / 600;

            //A vízszintes és a függőleges felbontást még korrigálni kell
            widthScale = widthScale * 1.33;
            heightScale = heightScale * 1.33;

            //Az egyes vezérlőelemek méretének és pozíciójának megváltoztatása a képernyőméret függvényében
            for (int i = 0; i <= this.Controls.Count - 1; i++)
            {
                this.Controls[i].Width = (int)(Controls[i].Width * widthScale);
                this.Controls[i].Height = (int)(Controls[i].Height * heightScale);
                this.Controls[i].Left = (int)(Controls[i].Left * widthScale);
                this.Controls[i].Top = (int)(Controls[i].Top * heightScale);
            }

            //A vezérlőelemek fontjainak képernyő méretéhez történő igazítása
            ChangeHeaderFont((widthScale + heightScale) / 2);
            ChangeHourAndMinFont((widthScale + heightScale) / 2);
            ChangeSecondsFont((widthScale + heightScale) / 2);
            //MRLabel.ForeColor = Color.Red;
            ChangeCounterHeaderFont((widthScale + heightScale) / 2);
            ChangeFooterFont((widthScale + heightScale) / 2);
            //MRQtyBox.ForeColor = Color.Red;
            //MRPercentBox.ForeColor = Color.Red;

            //Csatlakozás a PDA-szerverhez
            /* pda_connection.ConnectionString = pda_conn + pass;

            try
            {
                pda_connection.Open();
                debugBox.Text += "Csatlakoztatva a PDA-szerverhez.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "Nem sikerült csatlakozni a PDA-szerverhez.\r\n";
            }
            */
            //Csatlakozás a repair adatbázishoz
            repair_connection.ConnectionString = repair_conn;

            try
            {
                repair_connection.Open();
                debugBox.Text += "Csatlakoztatva a repair adatbázishoz.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "Nem sikerült csatlakozni a repair adatbázishoz.\r\n";
            }

            //Csatlakozás a production adatbázishoz
            prod_connection.ConnectionString = prod_conn;

            try
            {
                prod_connection.Open();
                debugBox.Text += "Csatlakoztatva a production adatbázishoz.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "Nem sikerült csatlakozni a production adatbázishoz.\r\n";
            }

            //Csatlakozás az Amplio-adatbázishoz
            amplio_connection.ConnectionString = amplio_conn;

            try
            {
                amplio_connection.Open();
                debugBox.Text += "Csatlakoztatva az Amplio adatbázishoz.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "Nem sikerült csatlakozni az Amplio adatbázishoz.\r\n" + ex;
            }

            //Csatlakozás a MES/production adatbázishoz
            mes_connection.ConnectionString = mes_conn;

            try
            {
                mes_connection.Open();
                debugBox.Text += "Csatlakoztatva a MES/production adatbázishoz.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "Nem sikerült csatlakozni a MES/production adatbázishoz.\r\n";
            }

            //timeTimer inicializálása (az idő alapján az esetleges esemény lekérdezése)
            //timeTimer.Interval = timequery_timer_interval;
            //timeTimer.Enabled = true;

            //browserTimer inicializálása
            browserTimer.Interval = browser_timer_interval;

            //infotimer (newsTimer) inicializásásá
            NewsTimer.Interval = info_timer_interval;

            //webBrowser1 inicializálása
            //webBrowser1.Navigate(weather_url);

            //debugBox láthatósága
            debugBox.Visible = debugBox_visible;

            //Soros port konfigurálása
            serialPort1.PortName = serial_port;

            if (senddata_enabled)
            {
                try
                {
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A port megnyitása sikertelen! Indítsa újra a programot.");
                }
            }
            //A böngésző időzítőjének engedélyezése/tiltása
            //Engedélyezve: a program végzi az időjárási adatok lekérését
            browserTimer.Enabled = browser_timer_enabled;

        }

        //A DateTime időzitő lefutásakor
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateText.Text = DateTime.Now.ToString("yyyy.MM.dd, ddd");
            HourAndMinBox.Text = DateTime.Now.ToString("HH:mm");
            secondsBox.Text = DateTime.Now.ToString("ss");
            //A kurzort a debugBox-ba tesszük
            CursorBox.Focus();
        }

        public void ChangeHeaderFont(double scale)
        {
            DateText.Font = new Font(DateText.Font.FontFamily.Name, (float)(double.Parse(DateText.Font.Size.ToString()) * scale), FontStyle.Bold);
            TempText.Font = new Font(DateText.Font.FontFamily.Name, (float)(double.Parse(TempText.Font.Size.ToString()) * scale), FontStyle.Bold);
            InfoTextBox.Font = new Font(DateText.Font.FontFamily.Name, (float)(double.Parse(InfoTextBox.Font.Size.ToString()) * scale), FontStyle.Bold);
        }

        public void ChangeHourAndMinFont(double scale)
        {
            HourAndMinBox.Font = new Font(HourAndMinBox.Font.FontFamily.Name, (float)(double.Parse(HourAndMinBox.Font.Size.ToString()) * scale), FontStyle.Bold);
        }

        public void ChangeSecondsFont(double scale)
        {
            secondsBox.Font = new Font(secondsBox.Font.FontFamily.Name, (float)(double.Parse(secondsBox.Font.Size.ToString()) * scale), FontStyle.Bold);
        }

        public void ChangeCounterHeaderFont(double scale)
        {
            //RepLabel.Font = new Font(RepLabel.Font.FontFamily.Name, (float)(double.Parse(RepLabel.Font.Size.ToString()) * scale), FontStyle.Bold);
            //RefurbLabel.Font = new Font(RefurbLabel.Font.FontFamily.Name, (float)(double.Parse(RefurbLabel.Font.Size.ToString()) * scale), FontStyle.Bold);
            //MRLabel.Font = new Font(MRLabel.Font.FontFamily.Name, (float)(double.Parse(MRLabel.Font.Size.ToString()) * scale), FontStyle.Bold);
            //DoneLabel.Font = new Font(DoneLabel.Font.FontFamily.Name, (float)(double.Parse(DoneLabel.Font.Size.ToString()) * scale), FontStyle.Bold);
        }

        public void ChangeFooterFont(double scale)
        {
            //Model1Box.Font = new Font(Model1Box.Font.FontFamily.Name, (float)(double.Parse(Model1Box.Font.Size.ToString()) * scale), FontStyle.Bold);
            //Model2Box.Font = new Font(Model2Box.Font.FontFamily.Name, (float)(double.Parse(Model2Box.Font.Size.ToString()) * scale), FontStyle.Bold);
            //Model3Box.Font = new Font(Model3Box.Font.FontFamily.Name, (float)(double.Parse(Model3Box.Font.Size.ToString()) * scale), FontStyle.Bold);
            //Model4Box.Font = new Font(Model4Box.Font.FontFamily.Name, (float)(double.Parse(Model4Box.Font.Size.ToString()) * scale), FontStyle.Bold);

            //Model1QtyBox.Font = new Font(Model1QtyBox.Font.FontFamily.Name, (float)(double.Parse(Model1QtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            //Model2QtyBox.Font = new Font(Model2QtyBox.Font.FontFamily.Name, (float)(double.Parse(Model2QtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            //Model3QtyBox.Font = new Font(Model3QtyBox.Font.FontFamily.Name, (float)(double.Parse(Model3QtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            //Model4QtyBox.Font = new Font(Model4QtyBox.Font.FontFamily.Name, (float)(double.Parse(Model4QtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);

            TestedQtyBox.Font = new Font(TestedQtyBox.Font.FontFamily.Name, (float)(double.Parse(TestedQtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            IronQtyBox.Font = new Font(IronQtyBox.Font.FontFamily.Name, (float)(double.Parse(IronQtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            GeneratorQtyBox.Font = new Font(GeneratorQtyBox.Font.FontFamily.Name, (float)(double.Parse(GeneratorQtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            FinishedQtyBox.Font = new Font(FinishedQtyBox.Font.FontFamily.Name, (float)(double.Parse(FinishedQtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            PlannedQtyBox.Font = new Font(PlannedQtyBox.Font.FontFamily.Name, (float)(double.Parse(PlannedQtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            BalanceText.Font = new Font(BalanceText.Font.FontFamily.Name, (float)(double.Parse(BalanceText.Font.Size.ToString()) * scale), FontStyle.Bold);

            //MRQtyBox.Font = new Font(MRQtyBox.Font.FontFamily.Name, (float)(double.Parse(MRQtyBox.Font.Size.ToString()) * scale), FontStyle.Bold);
            //MRPercentBox.Font = new Font(MRPercentBox.Font.FontFamily.Name, (float)(double.Parse(MRPercentBox.Font.Size.ToString()) * scale), FontStyle.Bold);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            float x1_h = (float)(screenWidth * 0.06);
            float y1_h = (float)(screenHeight * 0.15);
            float x2_h = (float)(screenWidth * 0.98);
            float y2_h = (float)(screenHeight * 0.15);

            /*float x1_f = (float)(screenWidth * 0.06);
            float y1_f = (float)(screenHeight * 0.6);
            float x2_f = (float)(screenWidth * 0.98);
            float y2_f = (float)(screenHeight * 0.6);*/

            float x1_f2 = (float)(screenWidth * 0.06);
            float y1_f2 = (float)(screenHeight * 0.8);
            float x2_f2 = (float)(screenWidth * 0.98);
            float y2_f2 = (float)(screenHeight * 0.8);

            Graphics line = e.Graphics;
            Pen p = new Pen(Color.Black, 4);
            //line.DrawLine(p, x1_f, y1_f, x2_f, y2_f);
            line.DrawLine(p, x1_h, y1_h, x2_h, y2_h);
            line.DrawLine(p, x1_f2, y1_f2, x2_f2, y2_f2);
            line.Dispose();
        }

        //Adatkapcsolatok lezárása, kilépés
        private void DateText_Click(object sender, EventArgs e)
        {
            try
            {
                pda_connection.Close();
            }
            catch (Exception ex)
            {

            }

            try
            {
                repair_connection.Close();
            }
            catch (Exception ex)
            {

            }

            try
            {
                prod_connection.Close();
            }
            catch (Exception ex)
            {

            }

            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {

            }

            Application.Exit();
        }

        //A darabszám-lekérdezések időzítője
        private void timer2_Tick(object sender, EventArgs e)
        {
            //Model1Box.Text = GetCurrentlyEOLTestedModel(amplio_connection, GetCurrentlyEOLTestedModelQuery);
            TestedQtyBox.Text = GetCountOfTestedAppliances(amplio_connection, SumQtyOfTestedModels);

            IronQtyBox.Text = GetCountOfFinishedItems(mes_connection, QtyProducedByLineQuery, 8);
            GeneratorQtyBox.Text = GetCountOfFinishedItems(mes_connection, QtyProducedByLineQuery, 10);
            FinishedQtyBox.Text = GetCountOfFinishedItems(mes_connection, QtyProducedByLineQuery, 12);
            PlannedQtyBox.Text = GetPlannedQty(mes_connection, PlannedQtyQuery);

            BalanceText.Text = GetBalance(mes_connection, BalanceQuery);
            ColorBalanceText(BalanceText);
            SelectEmoji();


            //Model1Box.Text = GetEOLTestedModels(amplio_connection, TestedModelsQuery, 1);
            //Model2Box.Text = GetEOLTestedModels(amplio_connection, TestedModelsQuery, 2);
            //Model3Box.Text = GetEOLTestedModels(amplio_connection, TestedModelsQuery, 3);
            //Model4Box.Text = GetEOLTestedModels(amplio_connection, TestedModelsQuery, 4);

            //Model1QtyBox.Text = GetQtyOfEOLTestedModels(amplio_connection, TestedModelsQuery, 1);
            //Model2QtyBox.Text = GetQtyOfEOLTestedModels(amplio_connection, TestedModelsQuery, 2);
            //Model3QtyBox.Text = GetQtyOfEOLTestedModels(amplio_connection, TestedModelsQuery, 3);
            //Model4QtyBox.Text = GetQtyOfEOLTestedModels(amplio_connection, TestedModelsQuery, 4);

            //GetCountOfIncidents(pda_connection, repQtyStr, 1);
            //GetCountOfIncidents(pda_connection, refurbQtyStr, 2);
            /*
            //A multirepair és a javításra váró gépek darabszám-információjának megjelenítése
            if ((j % 2 == 1) && (MR_visible))
            {
                MRQtyBox.ForeColor = Color.Red;
                MRPercentBox.ForeColor = Color.Red;
                MRLabel.Text = "Multirepair";
                MRLabel.ForeColor = Color.Red;

                GetCountOfIncidents(pda_connection, MRQtyStr, 3);

                float mr_percentage = CalculateMRPercentage(Int32.Parse(MRQtyBox.Text), Int32.Parse(RepQtyBox.Text), mssql_connection, MRCorrStr);
                MRPercentBox.Text = "(" + ((float)((int)(mr_percentage * 100)) / 100).ToString() + "%)";
            }
            else if (ForRep_visible)
            {
                MRLabel.Text = "  Kiadható";
                MRLabel.ForeColor = Color.Green;
                MRQtyBox.ForeColor = Color.Green;
                GetCountOfSForRepair(pda_connection, SForRepair);

                MRPercentBox.ForeColor = Color.Green;
                GetCountOfGForRepair(pda_connection, GForRepair);
            }

            //A j változó "reset"-je
            if (j > 99)
            {
                j = 1;
            }

            j++;
            */

        }

        /*
        void GetCountOfIncidents(OdbcConnection conn, String query_str, int id)
        {
            OdbcCommand command = new OdbcCommand();
            command.Connection = conn;
            command.CommandText = query_str;

            command.Parameters.AddWithValue("@today1", DateTime.Now.ToString("yyyy.MM.dd") + " 0:00:00");
            command.Parameters.AddWithValue("@today2", DateTime.Now.ToString("yyyy.MM.dd") + " 23:59:59");

            OdbcDataReader reader = (null);

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (id == 1)
                    {
                        RepQtyBox.Text = (reader["COUNT_Rep"].ToString());
                    }
                    if (id == 2)
                    {
                        RefurbQtyBox.Text = (reader["COUNT_Refurb"].ToString());
                    }
                    if (id == 3)
                    {
                        MRQtyBox.Text = (reader["COUNT_MR"].ToString());
                    }
                }
            }
            catch (Exception ex)

            {
                debugBox.Text += "A darabszám-lekérdezés nem sikerült.\r\n";
            }

            if (reader != null)
            {
                reader.Close();
            }
            command.Cancel();
        }
        */

        private void timeTimer_Tick(object sender, EventArgs e)
        {
           
        }

        /*
        public float CalculateMRPercentage(int mr_qty, int rep_qty, SqlConnection mssql_connection, String mr_corr_query)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = mr_corr_query;

            try
            {
                String corr_value_str = "0";

                SqlDataReader reader = (null);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    corr_value_str = reader["value"].ToString();
                }
                reader.Close();

                int corr_value = Int32.Parse(corr_value_str);
                debugBox.Text += "A korrekciós tényező: " + corr_value.ToString() + "\r\n";

                MRQtyBox.Text = (mr_qty - corr_value).ToString();

                float mr_percentage;

                if (rep_qty == 0)
                {
                    mr_percentage = 0;
                }
                else
                {
                    mr_percentage = ((float)(mr_qty - corr_value)) / ((float)(rep_qty)) * 100;
                    debugBox.Text += "Az mr_percentage értéke: " + mr_percentage.ToString() + "\r\n";
                }

                command.Cancel();

                if (mr_percentage < 0)
                {
                    mr_percentage = 0;
                }
                return mr_percentage;

            }
            catch (Exception ex)
            {
                debugBox.Text += "Az időt figyelő lekérdezés nem sikerült.\n" + ex;
                float mr_percentage = (float)(-999);

                command.Cancel();

                return mr_percentage;
            }



        }
        */

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {

        }

        //A böngésző időzítője, a hőmérséklet lekérdezése
        private void browserTimer_Tick(object sender, EventArgs e)
        {
            /*webBrowser1.Refresh();

            if (webBrowser1.Document.Body != null)
            {
                browserText.Text = webBrowser1.Document.Body.InnerText.ToString();

                try
                {
                    /*String templine = browserText.Lines[8];
                    debugBox.Text += "A hőmérséklet sora: " + templine;
                    String sunline = browserText.Lines[6];
                    String timestampline = browserText.Lines[21];
                    int tempstart = (templine.IndexOf('"', 22));
                    tempBox.Text = templine.Substring(22, tempstart - 22);
                    tempBox.Text = tempBox.Text.Replace('.', ',');

                    float temp_float = float.Parse(tempBox.Text);
                    int temp_value = (int)(temp_float + 0.5);
                    tempBox.Text = temp_value.ToString();*/

            /*UpdateTemperature(tempBox.Text);*/
            /*
            int sunrise_end = (sunline.IndexOf('"', 22));
            String sunrise = sunline.Substring(24, sunrise_end - 24);

            int sunset_end = (sunline.IndexOf('"', 55));
            String sunset = sunline.Substring(50, sunset_end - 50);

            UpdateSunriseSunset(sunrise, sunset);*/

            /*
            int timestamp_end = (timestampline.IndexOf('"', 30));
            String refreshed_time = timestampline.Substring(32, timestamp_end - 32);

            debugBox.Text += "Utolsó frissítés: " + refreshed_time + "\r\n";

            UpdateLastRefreshedTime(refreshed_time); */

            /*ShareTemperature(weatherText_path, temp_value.ToString());

            //A sikeres időjárás-lekérdezést jelző piktogram megjelenítése

            ConnectionPic.Image = Image.FromFile("signal.jpg");
        }
        catch (Exception ex)
        {
            //MessageBox.Show("Az időjárási adatok letöltése nem sikerült.");
            ConnectionPic.Image = Image.FromFile("no_signal.jpg");
        }*/


            try
            {
                XmlDocument XMLDoc = new XmlDocument();
                XMLDoc.Load(WeatherURL);

                String temperature = XMLDoc.DocumentElement.SelectSingleNode("temperature").Attributes["value"].Value;
                debugBox.Text += "A hőmérséklet: " + temperature + Environment.NewLine;

                String sunrise = XMLDoc.DocumentElement.SelectSingleNode("city/sun").Attributes["rise"].Value;
                sunrise = sunrise.Substring(11, 5);
                debugBox.Text += "A napkelte: " + sunrise + Environment.NewLine;

                String sunset = XMLDoc.DocumentElement.SelectSingleNode("city/sun").Attributes["set"].Value;
                sunset = sunset.Substring(11, 5);
                debugBox.Text += "A napnyugta: " + sunset + Environment.NewLine;

                String refreshed_time = XMLDoc.DocumentElement.SelectSingleNode("lastupdate").Attributes["value"].Value;
                refreshed_time = refreshed_time.Substring(11, 5);
                debugBox.Text += "Utolsó frissítés: " + refreshed_time + Environment.NewLine;
                //String humidity = XMLDoc.DocumentElement.SelectSingleNode("humidity").Attributes["value"].Value;
                //String city = XMLDoc.DocumentElement.SelectSingleNode("city").Attributes["name"].Value;

                temperature = temperature.Replace('.', ',');
                float temp_float = float.Parse(temperature);
                int temp_value = (int)(temp_float + 0.5);
                temperature = temp_value.ToString();
                debugBox.Text += "A hőmérséklet string: " + temperature + Environment.NewLine; 

                UpdateTemperature(temperature);

                UpdateSunriseSunset(sunrise, sunset);

                UpdateLastRefreshedTime(refreshed_time);

                //ShareTemperature(weatherText_path, temp_value.ToString());

                ConnectionPic.Image = Image.FromFile("signal.jpg");
            }

            catch (Exception ex)
            {
                //MessageBox.Show("Az időjárási adatok letöltése nem sikerült.");
                ConnectionPic.Image = Image.FromFile("no_signal.jpg");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //A hőmérsékleti adat mentése adatbázisba
        public void UpdateTemperature(String temp)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = prod_connection;

            command.CommandText = TempValueUpdateStr;

            command.Parameters.AddWithValue("@temperature", temp);

            try
            {
                command.ExecuteNonQuery();
                debugBox.Text += "A hőmérsékleti adat mentése sikerült.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "A hőmérsékleti adat mentése sikertelen.\r\n" + ex + Environment.NewLine;
            }
            command.Cancel();
        }

        //A napkelte/napnyugta időpontok lekérdezése
        public void UpdateSunriseSunset(String sunrise, String sunset)
        {
            String sunRiseHours = sunrise.Substring(0, 2);
            String sunRiseMins = sunrise.Substring(3, 2);

            String sunSetHours = sunset.Substring(0, 2);
            String sunSetMins = sunset.Substring(3, 2);

            int sunRiseHours_int = int.Parse(sunRiseHours);
            int sunSetHours_int = int.Parse(sunSetHours);

            DateTime datetime_now = DateTime.Now;
            Boolean dst = TimeZoneInfo.Local.IsDaylightSavingTime(datetime_now);

            if (dst)
            {
                sunRiseHours_int += 2;
                sunSetHours_int += 2;
            }
            else            {
                sunRiseHours_int += 1;
                sunSetHours_int += 1;
            }

            String sunrise_str = sunRiseHours_int.ToString() + ":" + sunRiseMins;
            debugBox.Text += "A napkelte időpontja: " + sunrise_str + "\r\n";
            String sunset_str = sunSetHours_int.ToString() + ":" + sunSetMins;
            debugBox.Text += "A nyugta időpontja: " + sunset_str + "\r\n";

            UpdateSunriseSunsetInDatabase(UpdateSunrise, sunrise_str);
            UpdateSunriseSunsetInDatabase(UpdateSunset, sunset_str);

        }

        //A napkelte, napnyugta időpontok mentése adatázisba
        void UpdateSunriseSunsetInDatabase(String query_str, String value)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = prod_connection;

            command.CommandText = query_str;

            command.Parameters.AddWithValue("@sun", value);

            try
            {
                command.ExecuteNonQuery();
                debugBox.Text += "A nap járásához kapcsolódó adat mentése sikerült.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "A nap járásához kapcsolódó adat mentése sikertelen.\r\n";
            }
            command.Cancel();
        }

        //Az időjárási adatok keletkezésének ideje
        void UpdateLastRefreshedTime(String time)
        {
            String RefreshHours = time.Substring(0, 2);
            String RefreshMins = time.Substring(3, 2);

            int RefreshHours_int = int.Parse(RefreshHours);

            DateTime datetime_now = DateTime.Now;
            Boolean dst = TimeZoneInfo.Local.IsDaylightSavingTime(datetime_now);

            if (dst)
            {
                RefreshHours_int += 2;
                if (RefreshHours_int > 23)
                {
                    RefreshHours_int = RefreshHours_int - 24;
                }
            }
            else
            {
                RefreshHours_int += 1;
                if (RefreshHours_int > 23)
                {
                    RefreshHours_int = RefreshHours_int - 24;
                }
            }

            String refresh_str = System.DateTime.Now.ToString("yyyy.MM.dd") + " " + RefreshHours_int.ToString() + ":" + RefreshMins;
            debugBox.Text += "A frissítés időpontja: " + refresh_str + "\r\n";

            UpdateRefreshedTime(UpdateRefreshTime, refresh_str);
        }

        //Az időjárási adatok frissítési idejének mentése adatbázisba
        void UpdateRefreshedTime(String query_str, String value)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = prod_connection;

            command.CommandText = query_str;

            command.Parameters.AddWithValue("@refresh", value);

            try
            {
                command.ExecuteNonQuery();
                debugBox.Text += "Az utolsó frissítés időpontjának mentése sikerült.\r\n";
            }
            catch (Exception ex)
            {
                debugBox.Text += "Az utolsó frissítés időpontjának mentése sikertelen.\r\n";
            }
            command.Cancel();
        }

        //A hőmérséklet lekérdezése adatbázisból
        String Temperature(SqlConnection mssql_connection, String query_str)
        {
            String temp_value = "";

            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = query_str;

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    temp_value = reader["value"].ToString();
                }
                debugBox.Text += "A hőmérséklet adatbázisból vett értéke: " + temp_value + "°C\r\n";

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A hőmérsékleti adat kiolvasása adatbázisból nem sikerült.\r\n";
            }

            if (Int32.Parse(temp_value) <= temp_LO)
            {
                try
                {
                    Pictogram2.Image = Image.FromFile("snowflake.jpg");
                    Pictogram2.Visible = true;
                }
                catch(Exception ex)
                {
                    Pictogram2.Visible = false;
                }
               
            }

            else if (Int32.Parse(temp_value) >= temp_HI)
            {
                try
                {
                    Pictogram2.Image = Image.FromFile("hot.jpg");
                    Pictogram2.Visible = true;
                }
                catch (Exception ex)
                {
                    Pictogram2.Visible = false;
                }

            }

            else
            {
                Pictogram2.Visible = false;
            }


            return temp_value;
        }

        //Az idójárási adatokat megmutató ciklus időzítője
        private void NewsTimer_Tick(object sender, EventArgs e)
        {
            if (WeatherDatasValid(prod_connection, TimeStampFromDataBase))
            {
                if (i % 2 == 1)
                {
                    TempText.Text = Temperature(prod_connection, TemperatureFromDatabase) + "°C";
                    try
                    {
                        Pictogram.Image = Image.FromFile("thermometer.jpg");
                        Pictogram.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        Pictogram.Visible = false;
                    }
                }

                if (i % 2 == 0)
                {
                    //TempText.Text = Sunrise(prod_connection, SunriseFromDataBase);
                    TempText.Text = DateTime.Now.ToString("HH:mm");
                    try
                    {
                        Pictogram.Image = Image.FromFile("clock_logo.jpg");
                        Pictogram.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        Pictogram.Visible = false;
                    }

                }
                /*
                if (i % 3 == 0)
                {
                    TempText.Text = Sunset(prod_connection, SunsetFromDataBase);
                    try
                    {
                        Pictogram.Image = Image.FromFile("sunset.jpg");
                        Pictogram.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        Pictogram.Visible = false;
                    }

                }*/

                i++;
                if (i > 2)
                {
                    i = 1;
                }

                debugBox.Text += "A hőmérséklet: " + Temperature(prod_connection, TemperatureFromDatabase) + "°C\r\n";
                debugBox.Text += "A napkelte időpontja: " + Sunrise(prod_connection, SunriseFromDataBase) + "\r\n";
                debugBox.Text += "A napnyugta időpontja: " + Sunset(prod_connection, SunsetFromDataBase) + "\r\n";
            }
            else
            {
                Pictogram.Visible = false;
                TempText.Text = "";
            }
        }

        //A napkelte adat lekérdezése
        String Sunrise(SqlConnection mssql_connection, String query_str)
        {
            String sunrise_value = "";

            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = query_str;

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sunrise_value = reader["value"].ToString();
                }
                debugBox.Text += "A napkelte adatbázisból vett időpontja:" + sunrise_value +"\r\n";

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A napkelte időpontjának kiolvasása adatbázisból nem sikerült.\r\n";
            }

            return sunrise_value;
        }

        //A napnyugta adat lekérdezése
        String Sunset(SqlConnection mssql_connection, String query_str)
        {
            String sunset_value = "";

            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = query_str;

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    sunset_value = reader["value"].ToString();
                }
                debugBox.Text += "A napnyugta adatbázisból vett időpontja:" + sunset_value + "\r\n";

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A napnyugta időpontjának kiolvasása adatbázisból nem sikerült.\r\n";
            }

            return sunset_value;
        }

        //AutoScroll debugBox
        private void debugBox_TextChanged(object sender, EventArgs e)
        {
            debugBox.SelectionStart = debugBox.Text.Length;
            debugBox.ScrollToCaret();

            if (debugBox.Lines.Count() > 100)
            {
                debugBox.Clear();
            }
        }

        //Az időjárás információ aktualitásának ellenőrzése
        Boolean WeatherDatasValid(SqlConnection mssql_connection, String query_str)
        {
            String age_of_data = "";
            Boolean data_valid = false;

            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = query_str;

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    age_of_data = reader["value"].ToString();
                }
                debugBox.Text += "Az adatok érkezésének adatbázisból vett időpontja:" + age_of_data + "\r\n";

                reader.Close();

                DateTime data_born = DateTime.Parse(age_of_data);
                debugBox.Text += "Az adatok érkezésének konvertált időpontja:" + data_born.ToString() + "\r\n";
                DateTime current_time = DateTime.Now;

                double calculated_difference = current_time.Subtract(data_born).TotalMinutes;

                debugBox.Text += "A kalkulált időkülönbség: " + calculated_difference.ToString() + "\r\n";

                if (calculated_difference < time_diff_MAX)
                {
                    data_valid = true;
                }
                else
                {
                    data_valid = false;
                }
            }
            catch (Exception ex)
            {
                debugBox.Text += "Az adatok érkezési időpontjának lekérdezése az adatbázisból nem sikerült.\r\n" + ex;
                data_valid = false;
            }

            return data_valid;
        }

        //Ha az óra:perc változik, ránéz a clock táblára, hogy nincs-e kiírandó üzenet
        //Amennyiben szükséges és a soros kommunikáció engedélyezve van, logikai "1"-et küld a soros portra
        private void HourAndMinBox_TextChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = prod_connection;
            command.CommandText = clockQuery;
            //command.Parameters.AddWithValue("@current_time", HourAndMinBox.Text + ":" + secondsBox.Text);
            command.Parameters.AddWithValue("@current_time", System.DateTime.Now.ToString("HH:mm"));

            //debugBox.Text += "A figyelendő időpont: " + HourAndMinBox.Text + ":" + secondsBox.Text + "\r\n";
            debugBox.Text += "A figyelendő időpont: " + System.DateTime.Now.ToString("HH:mm") + "\r\n";

            try
            {

                SqlDataReader reader = (null);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Boolean if_valid = Boolean.Parse(reader["active"].ToString());
                    Boolean normal_bell_requested = Boolean.Parse(reader["normal_bell"].ToString());
                    Boolean dashed_bell_requested = Boolean.Parse(reader["dashed_bell"].ToString());

                    if (if_valid)
                    {
                        InfoTextBox.Text = reader["text_to_display"].ToString();

                        if (normal_bell_requested)
                        {
                            SendDataToArduino("a");
                        }

                        if (dashed_bell_requested)
                        {
                            SendDataToArduino("b");
                        }

                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "Az időt figyelő lekérdezés nem sikerült.\n" + ex;
            }

            command.Cancel();
        }

        /*
        //Hőmérsékleti adat megosztása (amennyiben érvényes) más programok számára
        public void ShareTemperature(String path, String temp_value)
        {
            String hours = DateTime.Now.ToString("HH");
            String mins = DateTime.Now.ToString("mm");
            String lines = null;

            int mins_of_day = Int32.Parse(mins) + (Int32.Parse(hours) * 60);

            if (WeatherDatasValid(mssql_connection, TimeStampFromDataBase))
            {
                lines = "#Hőmérséklet:\r\n" + temp_value +"\r\n" + "#Időbélyeg:\r\n" + (mins_of_day - 5).ToString();
            }
            else
            {
                lines = "#Hőmérséklet:\r\n" + "\r\n" + "#Időbélyeg:\r\n" + (mins_of_day - 5).ToString();
            }

            System.IO.File.WriteAllText(@path + "weather.txt", lines);
            debugBox.Text += "A fájl mentése sikeres.\r\n";
        }
        */

        private void DateText_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void MRPercentBox_TextChanged(object sender, EventArgs e)
        {

        }

        /*
        public void GetCountOfSForRepair(OdbcConnection conn, String query_str)
        {
            OdbcCommand command = new OdbcCommand();
            command.Connection = conn;
            command.CommandText = query_str;

            OdbcDataReader reader = (null);

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                        MRQtyBox.Text = "S  " + (reader["COUNT_S_For_Rep"].ToString());
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            debugBox.Text += "A javításra váró S gépek számának lekérdezáse sikeres.\r\n";
                }
            }
            catch (Exception ex)

            {
                debugBox.Text += "A javításra váró S gépek számának lekérdezése nem sikerült.\r\n" + ex;
            }

            if (reader != null)
            {
                reader.Close();
            }
            command.Cancel();
        }
        */

        /*
        public void GetCountOfGForRepair(OdbcConnection conn, String query_str)
        {
            OdbcCommand command = new OdbcCommand();
            command.Connection = conn;
            command.CommandText = query_str;

            OdbcDataReader reader = (null);

            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MRPercentBox.Text = "G  " + (reader["COUNT_G_For_Rep"].ToString());
                    debugBox.Text += "A javításra váró G gépek számának lekérdezáse sikeres.\r\n";
                }
            }
            catch (Exception ex)

            {
                debugBox.Text += "A javításra váró G gépek számának lekérdezése nem sikerült.\r\n" + ex;
            }

            if (reader != null)
            {
                reader.Close();
            }
            command.Cancel();
        }
        */

        //Csengetés (logikai "1" küldése a soros portra)
        public void SendDataToArduino(String code)
        {
            try
            {
                serialPort1.Write(code);
                try
                {
                    //Pictogram2.Image = Image.FromFile("bell.jpg");
                    //Pictogram2.Visible = true;
                }
                catch (Exception ex)
                {
                    //Pictogram2.Visible = false;
                }
            }
            catch
            {
                debugBox.Text += "Nem sikerült a parancs soros portra történő kiküldése.\r\n";
            }
        }

        //A memóriahasználat csökkentésére a debugBox tartalma ürítésre kerül
        private void DebugTimer_Tick(object sender, EventArgs e)
        {
            debugBox.Clear();
        }

        String GetCountOfTestedAppliances(SqlConnection conn, String query)
        {
            String qty = "";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = query;

            command.Parameters.AddWithValue("@start_date", DateTime.Now.ToString("yyyy.MM.dd") + " 0:00:00");
            command.Parameters.AddWithValue("@end_date", DateTime.Now.ToString("yyyy.MM.dd") + " 23:59:59");

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    qty = reader["tested_count"].ToString();
                }
                debugBox.Text += "A sikeresen letesztelt mennyiség: " + qty + Environment.NewLine;

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A letesztelt mennyiség lekérdezése nem sikerült." + ex + Environment.NewLine;
            }

            return qty;
        }

        String GetEOLTestedModels(SqlConnection conn, String query, int row)
        {
            int counter = 0;

            String finaltestedmodel = "";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = query;

            command.Parameters.AddWithValue("@start_date", DateTime.Now.ToString("yyyy.MM.dd") + " 0:00:00");
            command.Parameters.AddWithValue("@end_date", DateTime.Now.ToString("yyyy.MM.dd") + " 23:59:59");

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    counter++;
                    if (counter == row)
                    {
                        finaltestedmodel = reader["itemCode"].ToString();
                    }
                }
                debugBox.Text += "A tesztelt modell: " + finaltestedmodel + Environment.NewLine;

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A jelenleg végtesztelt modell lekérdezése nem sikerült." + ex + Environment.NewLine;
            }

            return finaltestedmodel;
        }

        String GetQtyOfEOLTestedModels(SqlConnection conn, String query, int row)
        {
            int counter = 0;

            String finaltestedmodel = "";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = query;

            command.Parameters.AddWithValue("@start_date", DateTime.Now.ToString("yyyy.MM.dd") + " 0:00:00");
            command.Parameters.AddWithValue("@end_date", DateTime.Now.ToString("yyyy.MM.dd") + " 23:59:59");

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    counter++;
                    if (counter == row)
                    {
                        finaltestedmodel = reader["qty"].ToString();
                    }
                }
                debugBox.Text += "A mennyiség: " + finaltestedmodel + Environment.NewLine;

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A jelenleg végtesztelt modell lekérdezése nem sikerült." + ex + Environment.NewLine;
            }

            return finaltestedmodel;
        }


        String GetCountOfFinishedItems(SqlConnection conn, String query, int line)
        {
            String produced_count = null;

            SqlCommand command = new SqlCommand();
            command.Connection = conn;

            command.CommandText = query;

            command.Parameters.AddWithValue("@line", line);

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                   produced_count = reader["finished"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "Az elkészült termékek számának lekérdezése nem sikerült." + ex + Environment.NewLine;
                produced_count = "";
            }

            return produced_count;
        }

        //A tervezett mennyiség lekérdezése adatbázisból
        String GetPlannedQty(SqlConnection mssql_connection, String query_str)
        {
            String planned = "0";

            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = query_str;

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    planned = reader["other"].ToString();
                }
                debugBox.Text += "A tervezett mennyiség:" + planned + " db" + "\r\n";

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A tervezett mennyiség kiolvasása adatbázisból nem sikerült." + ex +"\r\n";
            }

            return planned;
        }

        //A balansz számítása
        String GetBalance(SqlConnection mssql_connection, String query_str)
        {
            String balance = "0";

            SqlCommand command = new SqlCommand();
            command.Connection = mssql_connection;

            command.CommandText = query_str;

            try
            {
                SqlDataReader reader = null;
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    balance = reader["actual_difference_int"].ToString();
                    GV.DifferenceComparedToPlan = Double.Parse(reader["dev"].ToString());
                }
                debugBox.Text += "Az aktuális eltérés a tervtől:" + balance + " db" + "\r\n";
                debugBox.Text += "A százalékos eltérés: " + GV.DifferenceComparedToPlan + Environment.NewLine;

                reader.Close();
            }
            catch (Exception ex)
            {
                debugBox.Text += "A tervtől való eltérés kalkulációja nem sikerült." + ex + "\r\n";               
            }

            return balance;
        }

        //Coloring of actual balance based on the value
        void ColorBalanceText(TextBox box)
        {
            try
            {
                if (Int32.Parse(box.Text) < 0)
                {
                    box.ForeColor = Color.Red;
                }
                else
                {
                    box.ForeColor = Color.DarkGreen;
                    box.Text = "+" + box.Text;
                }
            }
            catch(Exception ex)
            {
                BalanceText.Text = "";
            }

            
        }

        //
        void SelectEmoji()
        {
            try
            {
                if ((GV.DifferenceComparedToPlan >= yellowHI) && (GV.DifferenceComparedToPlan < darkgreenHI))
                {
                    EmojiBox.Image = Image.FromFile("4.jpg");
                }
                else if (GV.DifferenceComparedToPlan >= darkgreenHI)
                    {
                        EmojiBox.Image = Image.FromFile("5.jpg");
                    }
                else if ((GV.DifferenceComparedToPlan < yellowHI) && (GV.DifferenceComparedToPlan >= orangeHI))
                    {
                        EmojiBox.Image = Image.FromFile("3.jpg");
                    }
                else if ((GV.DifferenceComparedToPlan < orangeHI) && (GV.DifferenceComparedToPlan >= redHI))
                    {
                        EmojiBox.Image = Image.FromFile("2.jpg");
                    }
                else
                    {
                        EmojiBox.Image = Image.FromFile("1.jpg");
                    }
            }
            catch(Exception ex)
            {
                debugBox.Text += "Az emoji betöltése nem sikerült" + Environment.NewLine;
            }
            
        }

    }
}

//Global variables
public static class GV
{
    public static Double DifferenceComparedToPlan = 0;
}