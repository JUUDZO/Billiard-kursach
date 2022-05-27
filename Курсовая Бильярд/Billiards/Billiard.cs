using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Billiards
{
    public partial class Billiard : Form
    {
        private List<Ball> _balls;
        private Ball _whiteBall;
        private Ball _white1;
        private Ball _white2;
        private Ball _white3;
        private Ball _white4;
        private Ball _white5;
        private Ball _white6;
        private Ball _white7;
        private Ball _white8;
        private Ball _white9;
        private Ball _white10;
        private bool _mouseDown;
        private Rectangle[] _upBorderRects;
        private Rectangle[] _downBorderRects;
        private Rectangle _rightBorderRect;
        private Rectangle _leftBorderRect;



        private Point _mouseLocation;

        float width = 870;
        float height = 506;

        float _ballDiametr = 30f;
        float _ballRadius = 15f;

        int _maxBallVelocity = 30;
        float _ballDumping = 0.96f;

        //ТЕКУЩИЙ ШАР
        private Ball currentBall;
        //ПЕРВЫЙ УДАР
        private bool firstHit = true;

        private Vector[] _holesCenters;

        int schet = 0;

        public Billiard()
        {
            InitializeComponent();
            width = this.width - 30;
            height = this.height - 50;

            _upBorderRects = new Rectangle[] { upBorder1.Bounds, upBorder2.Bounds };
            Controls.Remove(upBorder1);
            Controls.Remove(upBorder2);

            _downBorderRects = new Rectangle[] { downBorder1.Bounds, downBorder2.Bounds };
            Controls.Remove(downBorder1);
            Controls.Remove(downBorder2);

            _rightBorderRect = rightBorder.Bounds;
            _leftBorderRect = leftBorder.Bounds;
            Controls.Remove(rightBorder);
            Controls.Remove(leftBorder);

            _holesCenters = new Vector[]
            {
               getControlCenter(hole1),
               getControlCenter(hole2),
               getControlCenter(hole3),
               getControlCenter(hole4),
               getControlCenter(hole5),
               getControlCenter(hole6),
            };

            Controls.Remove(hole1);
            Controls.Remove(hole2);
            Controls.Remove(hole3);
            Controls.Remove(hole4);
            Controls.Remove(hole5);
            Controls.Remove(hole6);
            
            _whiteBall = new Ball(Color.IndianRed, new Vector(getControlCenter(kickBall).X, getControlCenter(kickBall).Y, 0));
            _white1 = new Ball(Color.White, new Vector(getControlCenter(white1).X, getControlCenter(white1).Y, 0));
            _white2 = new Ball(Color.White, new Vector(getControlCenter(white2).X, getControlCenter(white2).Y, 0));
            _white3 = new Ball(Color.White, new Vector(getControlCenter(white3).X, getControlCenter(white3).Y, 0));
            _white4 = new Ball(Color.White, new Vector(getControlCenter(white4).X, getControlCenter(white4).Y, 0));
            _white5 = new Ball(Color.White, new Vector(getControlCenter(white5).X, getControlCenter(white5).Y, 0));
            _white6 = new Ball(Color.White, new Vector(getControlCenter(white6).X, getControlCenter(white6).Y, 0));
            _white7 = new Ball(Color.White, new Vector(getControlCenter(white7).X, getControlCenter(white7).Y, 0));
            _white8 = new Ball(Color.White, new Vector(getControlCenter(white8).X, getControlCenter(white8).Y, 0));
            _white9 = new Ball(Color.White, new Vector(getControlCenter(white9).X, getControlCenter(white9).Y, 0));
            _white10 = new Ball(Color.White, new Vector(getControlCenter(white10).X, getControlCenter(white10).Y, 0));

            currentBall = _whiteBall;

            _balls = new List<Ball>();
            _balls.Add(_whiteBall);
            _balls.Add(_white1);
            _balls.Add(_white2);
            _balls.Add(_white3);
            _balls.Add(_white4);
            _balls.Add(_white5);
            _balls.Add(_white6);
            _balls.Add(_white7);
            _balls.Add(_white8);
            _balls.Add(_white9);
            _balls.Add(_white10);

            Controls.Remove(kickBall);
            Controls.Remove(white1);
            Controls.Remove(white2);
            Controls.Remove(white3);
            Controls.Remove(white4);
            Controls.Remove(white5);
            Controls.Remove(white6);
            Controls.Remove(white7);
            Controls.Remove(white8);
            Controls.Remove(white9);
            Controls.Remove(white10);
            
            
        }

        //ЛИНИЯ ПРИЦЕЛА ОТ БИТКА
        private void Form_Paint(object sender, PaintEventArgs e)
        {
            DrawBall(e.Graphics);

            if (_mouseDown)
            {
                Point ball = new Point((int)currentBall.position.X, (int)currentBall.position.Y);
                Pen pen = new Pen(Color.Red, 3f);
                

                if (vtor % 2 == 1)
                {
                    pen = new Pen(Color.Blue, 3f);
                }
                else
                {
                    pen = new Pen(Color.Red, 3f);
                }
                e.Graphics.DrawLine(pen, ball, _mouseLocation);
                pen.Dispose();
            }
        }
        //ОТРИСОВКА ШАРОВ
        public void DrawBall(Graphics g)
        {
            Pen currentPen = new Pen(Color.White, 1);
            Pen blackPen = new Pen(Color.Black, 1);
            SolidBrush solidBrush = new SolidBrush(Color.White);

            float positionX = 0;
            float positionY = 0;
            RectangleF outerRect;

            foreach (Ball ball in _balls)
            {
                positionX = (float)ball.position.X - _ballRadius;
                positionY = (float)ball.position.Y - _ballRadius;
                outerRect = new RectangleF(positionX, positionY, _ballDiametr, _ballDiametr);



                solidBrush.Color = ball.color;
                g.FillEllipse(solidBrush, outerRect);
                g.DrawEllipse(ball == currentBall ? currentPen : blackPen, outerRect);
            }

            blackPen.Dispose();
            solidBrush.Dispose();
            currentPen.Dispose();

            

            richTextBox1.Text = $"{(_whiteBall.position.X.ToString("F"), _whiteBall.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white1.position.X.ToString("F"), _white1.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white2.position.X.ToString("F"), _white2.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white3.position.X.ToString("F"), _white3.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white4.position.X.ToString("F"), _white4.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white5.position.X.ToString("F"), _white5.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white6.position.X.ToString("F"), _white6.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white7.position.X.ToString("F"), _white7.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white8.position.X.ToString("F"), _white8.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white9.position.X.ToString("F"), _white9.position.Y.ToString("F"))}";
            richTextBox1.Text += $"\n{(_white10.position.X.ToString("F"), _white10.position.Y.ToString("F"))}";

        }
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _mouseLocation = e.Location;
            this.MouseMove += GameForm_MouseMove;
            this.MouseUp += GameForm_MouseUp;

            if (firstHit) return;

            //ДЕЛАЕМ ТЕКУЩИЙ ШАР, КОТОРЫЙ ВЫБРАЛИ
            foreach (Ball ball in _balls)
            {
                var x = (float)ball.position.X - _ballRadius;
                var y = (float)ball.position.Y - _ballRadius;
                var outerRect = new RectangleF(x, y, _ballDiametr, _ballDiametr);
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(outerRect);
                    if (path.IsVisible(e.Location))
                    {
                        currentBall = ball;
                        Invalidate();
                        break;
                    }
                }
            }
        }
        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseLocation = e.Location;
        }

        private void GameForm_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
            this.MouseMove -= GameForm_MouseMove;
            this.MouseUp -= GameForm_MouseUp;

 
            Vector mouse = new Vector((double)_mouseLocation.X, (double)_mouseLocation.Y, 0);
            currentBall.velocity = (currentBall.position - mouse);
            vtor++;
            button4.PerformClick();
            
            //ПЕРВЫЙ УДАР
            firstHit = false;
        }

        internal void UpdateWorld()
        {
            foreach (Ball ball in _balls)
            {
                if (ball.velocity.Length() > _maxBallVelocity)
                {
                    ball.velocity = ball.velocity.Unit() * _maxBallVelocity;
                }

                ball.position += ball.velocity;

                ball.velocity *= _ballDumping;
                if (ball.velocity.Length() < 0.1f) ball.velocity = Vector.zero;
            }


            for (int i = 0; i < _balls.Count; i++)
            {
                Ball firstBall = _balls[i];
                for (int j = i + 1; j < _balls.Count; j++)
                {
                    Ball secondBall = _balls[j];

                    double dist = (firstBall.position - secondBall.position).Length();
                    if (dist < _ballDiametr)
                    {
                        double offset = _ballDiametr - dist;
                        Vector direction = (firstBall.position - secondBall.position).Unit();
                        Vector forse = direction * offset;

                        firstBall.position += forse;
                        secondBall.position -= forse;

                        firstBall.velocity += forse;
                        secondBall.velocity -= forse;
                    }
                }
            }


            foreach (Ball ball in _balls)
            {
                updateBorderCollisions(ball);
            }

            foreach (Ball ball in _balls)
            {
                if (checkBallInHoles(ball))
                {
                    _balls.Remove(ball);
                    Refresh();

                    //ЕСЛИ БИТОК ЗАБИТ
                    if (schet == 0 || schet % 2 == 0)
                    {
                        if (ball == currentBall)
                        {
                            if (vtor == 0 || vtor % 2 == 0)
                                MessageBox.Show("Вы (Player 1) проиграли! И набрали " + $"\n{k - 100}" + " ОЧКОВ!!!");
                            else
                                MessageBox.Show("Вы (Player 2) проиграли! И набрали " + $"\n{k2 - 100}" + " ОЧКОВ!!!");
                            Close();
                        }
                    }
                    else
                    {
                        //ЕСЛИ ОСТАЛСЯ ОДИН ШАР
                        if (_balls.Count == 1)
                        {
                            if (k > k2)
                            {
                                textBox1.Text = ($"{k}");
                                MessageBox.Show("ПОЗДРАВЛЯЕМ!!!\nВы (Player 1) выиграли партию и набрали " + $"\n{k}" + " ОЧКОВ!!!");
                            }
                            else if (k < k2)
                            {
                                textBox2.Text = ($"{k}");
                                MessageBox.Show("ПОЗДРАВЛЯЕМ!!!\nВы (Player 1) выиграли партию и набрали " + $"\n{k2}" + " ОЧКОВ!!!");
                            }
                            else if (k == k2)
                            {
                                MessageBox.Show("ПОЗДРАВЛЯЕМ!!!\nУ Вас НИЧЬЯ!!! Вы оба выиграли партию и набрали " + $"\n{k2}" + " ОЧКОВ!!!");
                            }
                            Close();
                        }
                    }

                    break;
                }
            }

        }

        //ВЗАИМОДЕЙСТВИЕ С БОРТАМИ
        private void updateBorderCollisions(Ball ball)
        {

            if (_rightBorderRect.Contains((int)(ball.position.X + _ballRadius), (int)ball.position.Y))
            {
                ball.velocity.X *= -1;
                ball.position.X = _rightBorderRect.X - _ballRadius;

            }

            if (_leftBorderRect.Contains((int)(ball.position.X - _ballRadius), (int)ball.position.Y))
            {
                ball.velocity.X *= -1;
                ball.position.X = _leftBorderRect.Right + _ballRadius;
            }


            foreach (Rectangle rect in _upBorderRects)
            {
                if (rect.Contains((int)ball.position.X, (int)(ball.position.Y - _ballRadius)))
                {
                    ball.velocity.Y *= -1;
                    ball.position.Y = rect.Bottom + _ballRadius;
                }
            }

            foreach (Rectangle rect in _downBorderRects)
            {
                if (rect.Contains((int)ball.position.X, (int)(ball.position.Y + _ballRadius)))
                {
                    ball.velocity.Y *= -1;
                    ball.position.Y = rect.Y - _ballRadius;
                }
            }
        }
        int k = 0;
        int k2 = 0;

        int perv = 0;
        int vtor = 0;
        private bool checkBallInHoles(Ball ball)
        {
            foreach (Vector v in _holesCenters)
            {
                if ((v - ball.position).Length() <= _ballRadius)
                {
                    if (vtor == 0 || vtor % 2 == 0)
                    {
                        k = k + 100;
                        perv++;
                    }
                    else
                    {
                        k2 = k2 + 100;
                        perv++;
                    }
                    return true;
                }
            }
            if (vtor == 0 || vtor % 2 == 0)
            {
                textBox1.Text = ($"{k}");
            }
            else
            {
                textBox2.Text = ($"{k2}");
            }

            if (perv > 0)
            {
                vtor--;
                perv--;
            }

            return false;
        }

        private Vector getControlCenter(Control control)
        {
            double X = control.Location.X + control.Width * 0.5f;
            double Y = control.Location.Y + control.Height * 0.5f;
            return new Vector(X, Y, 0);
        }

        private void button1_Click(object sender, System.EventArgs e) 
        {
            Application.Restart();
        }


        public class Ball
        {
            public Vector position;
            public Color color;
            public Vector velocity;

            

            public Ball(Color color, Vector position)
            {
                this.position = position;
                this.color = color;
                this.velocity = new Vector();
            }

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            schet++;
            if(schet == 0 || schet % 2 == 0)
            {
                (sender as Button).BackColor = Color.DodgerBlue;
                (sender as Button).ForeColor = Color.Black;
            }
            else
            {
                (sender as Button).BackColor = Color.Red;
                (sender as Button).ForeColor = Color.White;
            }
        }

        public void button4_Click(object sender, System.EventArgs e)
        {
            if (vtor % 2 == 1)
            {
                (sender as Button).BackColor = Color.DodgerBlue;
                (sender as Button).ForeColor = Color.Black;
                (sender as Button).Text = "Player 1";
            }
            else
            {
                (sender as Button).BackColor = Color.Red;
                (sender as Button).ForeColor = Color.White;
                (sender as Button).Text = "Player 2";
            }
        }



        private void OpenSave_Click(object sender, System.EventArgs e)
        {            
            StreamReader reader = new StreamReader(@"C:\Users\Public\Сохранение.txt");
            string s = "";
            double x = 0;
            double y = 0;

            textBox1.Text = "0";
            textBox2.Text = "0";

            foreach (Ball b in _balls)
            {
                if (!reader.EndOfStream)
                {
                    s = reader.ReadLine();
                    int i = s.IndexOf(',');
                    x = double.Parse(s.Substring(1, i - 1));
                    y = double.Parse(s.Substring(i + 2, s.Length - i - 3));

                    b.velocity.X = 0;
                    b.velocity.Y = 0;

                    b.position.X = x;
                    b.position.Y = y;
                }
            }
            reader.Close();
        }

        private void LoadSave_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Название файла ОБЯЗАТЕЛЬНО 'Сохранение', путь файла обязательно (C:/Users/Public)");
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, richTextBox1.Text);
            MessageBox.Show("Файл сохранен");
        }
    }
}
