namespace _10_ufo
{
    public partial class Form1 : Form
    {
        Point ps;
        Point pe;
        PointF p;
        double[] dists = new double[10];
        int c = 1;
        int step = 5;
        public Form1()
        {
            InitializeComponent();
            ps = new Point(49, 51);
            pe = new Point(993, 797);
            p = ps;
            for (int i = 0; i < 10; i++)
            {
                dists[i] = Math.Sqrt(Math.Pow(pe.X - ps.X, 2) + Math.Pow(pe.Y - ps.Y, 2));
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.ScaleTransform(0.5f, 0.5f);
            DrawPoint(g, ps, Color.Black);
            DrawPoint(g, pe, Color.Black);
            DrawPoint(g, p, Color.Blue);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double alpha = Atn(Abs(pe.Y - ps.Y) / Abs(pe.X - ps.X), c+1);
            p.X += step * (float)Cos(alpha, c+1);
            p.Y += step * (float)Sin(alpha, c+1);
            Invalidate();
            double dist = Math.Sqrt(Math.Pow(pe.X - p.X, 2) + Math.Pow(pe.Y - p.Y, 2));
            if (dist < dists[c - 1])
            {
                dists[c - 1] = dist;
            }
            else
            {
                c++;
                p = ps;
            }
            if (c > 10)
            {
                timer1.Enabled = false;
                ScottPlot.Plot plt = new ScottPlot.Plot();
                plt.Add.Scatter(Enumerable.Range(1, 10).ToArray(), dists);
                plt.Save("../../../../dia.png", 1000, 1000);
            }
        }

        double Sin(double x, int n)
        {
            double res = 0;
            for (int i = 1; i <= n; i++)
            {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / Factorial(2 * i - 1);
            }
            return res;
        }
        double Cos(double x, int n)
        {
            double res = 0;
            for (int i = 1; i <= n; i++)
            {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 2) / Factorial(2 * i - 2);
            }
            return res;
        }
        double Atn(double x, int n)
        {
            if (Math.Abs(x) > 1) return Math.Sign(x) * Math.PI / 2 - Atn(1 / x, n);
            double res = 0;
            for (int i = 1; i <= n; i++)
            {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / (2 * i - 1);
            }
            return res;
        }
        double Abs(double x)
        {
            return Math.Abs(x);
        }
        long Factorial(int n)
        {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }
        void DrawPoint(Graphics g, PointF p, Color c)
        {
            g.DrawEllipse(new Pen(c, 5), p.X, p.Y, 20, 20);
        }
    }
}
