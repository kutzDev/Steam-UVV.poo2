namespace Simple_Punch_Out_Game_MOO_ICT
{
    public partial class Form1 : Form
    {
        int i = 1;
        Image currentEnemyPunch1;
        Image currentEnemyPunch2;
        Image currentEnemyBlock;
        Image currentEnemyStand;

        bool playerBlock = false;
        bool enemyBlock = false;
        Random random = new Random();
        int enemySpeed = 5;
        int index = 0;
        int playerHealth = 100;
        int enemyHealth = 100;
        List<string> enemyAttack = new List<string> { "left", "right", "block" };



        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ChangeEnemy()
        {
            int enemyChoice = random.Next(0, 2);

            if (enemyChoice == 0)
            {
                currentEnemyPunch1 = Properties.Resources.enemy_punch1;
                currentEnemyPunch2 = Properties.Resources.enemy_punch2;
                currentEnemyBlock = Properties.Resources.enemy_block;
                currentEnemyStand = Properties.Resources.enemy_stand;
            }
            else
            {
                currentEnemyPunch1 = Properties.Resources.don_punch1;
                currentEnemyPunch2 = Properties.Resources.don_punch2;
                currentEnemyBlock = Properties.Resources.don_block;
                currentEnemyStand = Properties.Resources.don_stand;
            }
            boxer.Image = currentEnemyStand;
            boxer.Refresh();
        }

        private void BoxerAttackTImerEvent(object sender, EventArgs e)
        {

            index = random.Next(0, enemyAttack.Count);

            switch (enemyAttack[index].ToString())
            {
                case "left":
                    boxer.Image = currentEnemyPunch1;
                    enemyBlock = false;

                    if (boxer.Bounds.IntersectsWith(player.Bounds) && playerBlock == false)
                    {
                        playerHealth -= 5;
                    }

                    break;

                case "right":

                    boxer.Image = currentEnemyPunch2;
                    enemyBlock = false;

                    if (boxer.Bounds.IntersectsWith(player.Bounds) && playerBlock == false)
                    {
                        playerHealth -= 5;
                    }
                    break;

                case "block":

                    boxer.Image = currentEnemyBlock;
                    enemyBlock = true;

                    break;
            }


        }

        private void BoxerMoveTimerEvent(object sender, EventArgs e)
        {
            // set up both health bars
            if (playerHealth > 1)
            {
                playerHealthBar.Value = playerHealth;
            }
            if (enemyHealth > 0)
            {
                boxerHealthBar.Value = enemyHealth;
            }


            // move the boxer

            boxer.Left += enemySpeed;

            if (boxer.Left > 430)
            {
                enemySpeed = -5;
            }
            if (boxer.Left < 220)
            {
                enemySpeed = 5;
            }

            // check for the end of game scenario

            if (enemyHealth < 1)
            {
                BoxerAttackTimer.Stop();
                BoxerMoveTimer.Stop();
                MessageBox.Show("You Win, Click OK to Play Again", "Moo Says: ");
                ResetGame();
            }
            if (playerHealth < 1)
            {
                BoxerAttackTimer.Stop();
                BoxerMoveTimer.Stop();
                MessageBox.Show("Tough Rob Wins, Click OK to Play Again", "Moo Says: ");
                ResetGame();
            }


        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.Image = Properties.Resources.boxer_left_punch;
                playerBlock = false;

                if (player.Bounds.IntersectsWith(boxer.Bounds) && enemyBlock == false)
                {
                    enemyHealth -= 5;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                player.Image = Properties.Resources.boxer_right_punch;
                playerBlock = false;

                if (player.Bounds.IntersectsWith(boxer.Bounds) && enemyBlock == false)
                {
                    enemyHealth -= 5;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                player.Image = Properties.Resources.boxer_block;
                playerBlock = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            player.Image = Properties.Resources.boxer_stand;
            playerBlock = false;
        }

        private void ResetGame()
        {
            i++;
            playerHealth = 100;
            enemyHealth = 200 * i;
            boxerHealthBar.Maximum = enemyHealth; 
            boxer.Left = 400;
            ChangeEnemy();
            BoxerAttackTimer.Start();
            BoxerMoveTimer.Start();
        }

        private void boxerHealthBar_Click(object sender, EventArgs e)
        {

        }
    }
}