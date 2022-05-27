using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billiards
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }


        private void button_start_Click(object sender, EventArgs e)
        {
            start_GameForm();
           
        }
        private void start_GameForm()
        {
            Billiard gameform = new Billiard();
            gameform.Show();

            DateTime currentUpdateTime;
            DateTime lastUpdateTime;
            TimeSpan frameTime;

            currentUpdateTime = DateTime.Now;
            lastUpdateTime = DateTime.Now;

            while (gameform.Created == true)
            {
                currentUpdateTime = DateTime.Now;
                frameTime = currentUpdateTime - lastUpdateTime;
                if (frameTime.TotalMilliseconds > 20)
                {
                    Application.DoEvents();
                    gameform.UpdateWorld();
                    gameform.Refresh();
                    lastUpdateTime = DateTime.Now;
                }
            }
            
        }
        private void button_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {

        }

        private void правилоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Игра представляет собой упрощенный русский бильярд. Вместо 16 шаров только 11. За каждый забитый шар игрок получает 100 очков. Игрок использует для удара только биток только во время первого удара, дальше можно бить любым шаром. Нельзя забивать шар, которым производился удар. Когда на поле остается один шар, игра заканчивается.");
        }

        private void ОбАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Работу выполнил студент группы 3043 - Алексеев Павел" + "\nalekseev_pv@gkl-kemerovo.ru");
        }
    }
}
