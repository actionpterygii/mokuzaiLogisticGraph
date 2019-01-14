using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
//using MathNet.Numerics.Statistics;


namespace gurafu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        // gengazou yomikomi
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.ImageLocation = textBox1.Text;
        }

        // fuxirutagazou yomikomi
        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox6.Text = openFileDialog1.FileName;
            }
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.ImageLocation = textBox6.Text;
        }

        // keisannn
        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "")
            {
                textBox3.Text = "0";
            }
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }


            // atai wo hennkann shite syutoku
            float Alpha = Convert.ToSingle(textBox2.Text);
            float Beta1 = Convert.ToSingle(textBox3.Text);
            float Beta2 = Convert.ToSingle(textBox4.Text);
            float Beta3 = Convert.ToSingle(textBox5.Text);

            // futatuno gazouwo bittomappuni
            Bitmap Gen = new Bitmap(textBox1.Text);
            Bitmap Fux = new Bitmap(textBox6.Text);

            // de-ta wo kakunou surutoko
            int nData = (Gen.Height * Gen.Width) + (Gen.Height * Gen.Width);
            double[] Fushidata = new double[nData];
            double[] Soreigaidata = new double[nData];
            int p = 0, q = 0;

            // ikkai de-ta ireruyatu
            double Fdata = 0;
            double Sdata = 0;

            // g Goukei
            // h Heikin
            // n NijyouMo-mennto
            // b Bunsan
            // s De-tasuu
            double Fg = 0;
            double Sg = 0;
            double Fh = 0;
            double Sh = 0;
            double Fn = 0;
            double Sn = 0;
            double Fb = 0;
            double Sb = 0;
            double Fs = 0;
            double Ss = 0;
            int cnt = 0;

            // gurafu no ookisa
            double MaxData = 0;
            double MinData = 50;



            // gurafu no settei
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            Series Fushi = new Series();
            Series Soreigai = new Series();
            Fushi.LegendText = "Fushi";
            Soreigai.LegendText = "Soreigai";
            Fushi.ChartType = SeriesChartType.Line;
            Soreigai.ChartType = SeriesChartType.Line;
            ChartArea area1 = new ChartArea();
            area1.AxisX.Title = "Value";
            area1.AxisY.Title = "Dots";
            Fushi.Color = Color.FromArgb(220, 255, 0, 0);
            Soreigai.Color = Color.FromArgb(220,0,0,255);
            Fushi.BorderWidth = 1;
            Soreigai.BorderWidth = 1;

            area1.AxisX.Interval = 2;
            area1.AxisX.MajorGrid.Interval = 1;
            area1.AxisX.MajorGrid.LineColor = Color.Gray;
            area1.AxisX.MinorGrid.Enabled = true;
            area1.AxisX.MinorGrid.Interval = 0.2;
            area1.AxisX.MinorGrid.LineColor = Color.LightGray;

            area1.AxisY.IsLogarithmic = true;
            area1.AxisY.LogarithmBase = 10;
            area1.AxisY.MajorGrid.LineColor = Color.Gray;
            area1.AxisY.MinorGrid.Enabled = true;
            area1.AxisY.MinorGrid.Interval = 1;
            area1.AxisY.MinorGrid.LineColor = Color.LightGray;


            

            // RGBHSV wo dashite atai wo keisan suru
            for (int i = pictureBox1.Image.Width - pictureBox1.Image.Width ; i < pictureBox1.Image.Width ; i++)
            {
                for (int j = pictureBox1.Image.Height - pictureBox1.Image.Height ; j < pictureBox1.Image.Height ; j++)
                {
                    System.Drawing.Color gARGB = Gen.GetPixel(i, j);

                    double gR = gARGB.R;
                    double gG = gARGB.G;
                    double gB = gARGB.B;

                    double max = Math.Max(gR, Math.Max(gG, gB));
                    double min = Math.Min(gR, Math.Min(gG, gB));

                    double mm = max - min;

                    double H = 0;
                    double S = 0;
                    double V = 0;

                    if (max == min)
                    {
                        H = 0;
                    }
                    else if (max == gR)
                    {
                        H = (60 * (gG - gB) / mm + 360) % 360;
                    }
                    else if (max == gG)
                    {
                        H = (60 * (gB - gR) / mm) + 120;
                    }
                    else
                    {
                        H = (60 * (gR - gB) / mm) + 240;
                    }

                    if (max == 0)
                    {
                        S = 0;
                    }
                    else
                    {
                        S = (255 * (mm / max));
                    }

                    V = max;


                    H = SSGN(H);
                    S = SSGN(S);
                    V = SSGN(V);




                    // Fushi ka douka shiraberutame ni akaku nurareteiruka wo miru
                    System.Drawing.Color sARGB = Fux.GetPixel(i, j);

                    double sR = sARGB.R;
                    double sG = sARGB.G;
                    double sB = sARGB.B;
                    string Text = comboBox1.Text;

                    if ( sR > 250 && sG < 10 && sB <10 )
                    {
                        if(Text == "HSV")
                        {
                            Fdata = Alpha + (Beta1 * H) + (Beta2 * S) + (Beta3 * V);
                        }
                        else if(Text == "HS")
                        {
                            Fdata = Alpha + (Beta1 * H) + (Beta2 * S);
                        }
                        else if (Text == "HV")
                        {
                            Fdata = Alpha + (Beta1 * H) + (Beta3 * V);
                        }
                        else if (Text == "SV")
                        {
                            Fdata = Alpha + (Beta2 * S) + (Beta3 * V);
                        }
                        else if (Text == "H")
                        {
                            Fdata = Alpha + (Beta1 * H);
                        }
                        else if (Text == "S")
                        {
                            Fdata = Alpha + (Beta2 * S);
                        }
                        else if (Text == "V")
                        {
                            Fdata = Alpha + (Beta3 * V);
                        }

                        Fg += Fdata;
                        Fn += (Fdata * Fdata);
                        Fs++;
                        Fushidata[p] = Convert.ToDouble( SSGN(Fdata*10.0)/10.0 );
                        p++;
                        MaxData = System.Math.Max(MaxData, Fdata);
                        MinData = System.Math.Min(MinData, Fdata);
                    }

                    else
                    {
                        if (Text == "HSV")
                        {
                            Sdata = Alpha + (Beta1 * H) + (Beta2 * S) + (Beta3 * V);
                        }
                        else if (Text == "HS")
                        {
                            Sdata = Alpha + (Beta1 * H) + (Beta2 * S);
                        }
                        else if (Text == "HV")
                        {
                            Sdata = Alpha + (Beta1 * H) + (Beta3 * V);
                        }
                        else if (Text == "SV")
                        {
                            Sdata = Alpha + (Beta2 * S) + (Beta3 * V);
                        }
                        else if (Text == "H")
                        {
                            Sdata = Alpha + (Beta1 * H);
                        }
                        else if (Text == "S")
                        {
                            Sdata = Alpha + (Beta2 * S);
                        }
                        else if (Text == "V")
                        {
                            Sdata = Alpha + (Beta3 * V);
                        }

                        Sg += Sdata;
                        Sn += (Sdata * Sdata);
                        Ss++;
                        Soreigaidata[q] = Convert.ToDouble( SSGN(Sdata*10.0)/10.0 );
                        q++;
                        MaxData = System.Math.Max(MaxData, Sdata);
                        MinData = System.Math.Min(MinData, Sdata);
                    }

                } // j ru-pu owari
            
            }     // i ru-pu owari

            
            // fushi add
            for(int fi = -500;fi <= 500;fi++)
            {
                cnt = 0;
                double fih = fi;
                fih = fih / 10;
                for (int fj = 0; fj <= p; fj++)
                {
                    if (Fushidata[fj] == fih)
                    {
                        cnt++;
                    }  
                }
                if(cnt == 0)
                {
                    cnt = 1;
                }
                Fushi.Points.Add(new DataPoint(fih, cnt));
            }

            // soreigai add
            for (int si = -500; si <= 500; si++)
            {
                cnt = 0;
                double sih = si;
                sih = sih / 10;
                for (int sj = 0; sj <= q; sj++)
                {
                    if (Soreigaidata[sj] == sih)
                    {
                        cnt++;
                    }
                }
                if(cnt == 0)
                {
                    cnt = 1;
                }
                Soreigai.Points.Add(new DataPoint(sih, cnt));
            }



            // Bunsan wo keisan
            Fh = 0;
            Sh = 0;
            Fh = Fg / Fs;
            Sh = Sg / Ss;
            Fb = (Fn / Fs) - (Fh * Fh);
            Sb = (Sn / Ss) - (Sh * Sh);
            textBox7.Text = Fb.ToString();
            textBox10.Text = Sb.ToString();

            // Heikin wo keisan
            Fh = SSGN( (Fg / Fs) * 10.0) / 10.0;
            Sh = SSGN( (Sg / Ss) * 10.0) / 10.0;
            textBox8.Text = Fh.ToString();
            textBox9.Text = Sh.ToString();

            // Gurafu no ookisa
            MaxData = SSGN((MaxData * 10.0) / 10.0);
            MinData = SSGN((MinData * 10.0) / 10.0);
            area1.AxisX.Maximum = MaxData + 1;
            area1.AxisX.Minimum = MinData - 1;


            // Gurafu wo byouga
            chart1.ChartAreas.Add(area1);
            chart1.Series.Add(Fushi);
            chart1.Series.Add(Soreigai);
            textBox11.Text = p.ToString();
            textBox12.Text = q.ToString();

        }



        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }






        // syoduten daiitii de ShiSyaGoNyu suruyatu (kansu)
        int SSGN(double m)
        {
            return (int)(m < 0.0 ? m - 0.5 : m + 0.5);
        }


        



        // hozon
        // ttp://stackoverflow.com/questions/11055258/how-to-use-savefiledialog-for-saving-images-in-c

        // gurafu wo hozonn
        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Bmp;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }
                chart1.SaveImage(sfd.FileName, format);
            }
        }


        // Form1 wo hozon
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Images|*.png;*.bmp;*.jpg";
            ImageFormat format = ImageFormat.Bmp;
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = System.IO.Path.GetExtension(sfd.FileName);
                switch (ext)
                {
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                }
                bmp.Save(sfd.FileName, format);
            }
        }





        // Hani Sentaku no baai kuwaeru yatu
        /* 

        // hanni senntaku no yatu
        Point MD = new Point();
        Point MU = new Point();

        // Form1 no zahyou syutoku suru yatu
        Point WD = new Point();
        Point WU = new Point();

        // hanni senntaku wo shitatoki (Down)
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MD.X = e.X;
            MD.Y = e.Y;
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            WD = this.PointToClient(sp);
            
        }

        // hanni senntaku wo shitatoki (Up)
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MU.X = e.X;
            MU.Y = e.Y;
            System.Drawing.Point sp = System.Windows.Forms.Cursor.Position;
            WU = this.PointToClient(sp);
         * 
         * ika,onajiyouni.
         * 
         * 
         * 
         * // PictureBox sizemode zoom de iti syutoku suruyatu (kansu)
        // ttp://blog.livedoor.jp/slothgreed/archives/29097771.html
        private bool GetImagePos(int mouseX, int mouseY, out Point pos)
        {
            pos = new Point();
            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;
            float aspect = this.pictureBox1.Width;
            float imageScale = (float)this.pictureBox1.Width / width;
            if (imageScale > (float)this.pictureBox1.Height / height)
            {
                imageScale = (float)this.pictureBox1.Height / height;
            }
            float scaledWidth = width * imageScale;
            float scaledHeight = height * imageScale;

            // Compute the offset of the image to center it in the picture box
            float imageX = (this.pictureBox1.Width - scaledWidth) / 2.0f;
            float imageY = (this.pictureBox1.Height - scaledHeight) / 2.0f;
       
            // Test the coordinate in the picture box against the image bounds
            if (mouseX < imageX || imageX + scaledWidth < mouseX)
            {
                return false;
            }
            if (mouseY < imageY || imageY + scaledHeight < mouseY)
            {
                return false;
            }

            // Compute the normalized (0..1) coordinates in image space
            pos.X = (int)((mouseX - imageX) / imageScale);
            pos.Y = (int)((mouseY - imageY) / imageScale);

            return true;
        }

        */

    }
}
