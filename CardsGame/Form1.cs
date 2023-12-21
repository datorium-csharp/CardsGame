using System.Runtime.InteropServices;

namespace CardsGame
{
    public partial class Form1 : Form
    {
        Random randomNumberGenerator = new Random();
        List<PictureBox> cards = new List<PictureBox>();
        int deltaX = 0;
        int deltaY = 0;
        bool mouseHold = false;

        public Form1()
        {
            InitializeComponent();
            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            this.BackColor = Color.Green;
            this.Text = "Cards!!";
        }

        private string SelectFolder()
        {
            var selectFolderDialog = new FolderBrowserDialog();
            var result = selectFolderDialog.ShowDialog();

            if (result == DialogResult.OK)
            { 
                return selectFolderDialog.SelectedPath;
            }

            return null;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            string folderPath = SelectFolder();
            string[] fileNames = Directory.GetFiles(folderPath);

            foreach (string fileName in fileNames)
            {
                PictureBox card = new PictureBox();
                card.Width = 100;
                card.Height = 200;

                card.Image = Image.FromFile(fileName);
                card.SizeMode = PictureBoxSizeMode.StretchImage;

                card.Top = randomNumberGenerator.Next(400);
                card.Left = randomNumberGenerator.Next(800);

                card.MouseDown += Card_MouseDown;
                card.MouseMove += Card_MouseMove;
                card.MouseUp += Card_MouseUp;

                cards.Add(card);
                this.Controls.Add(card);
            }
        }

        private void Card_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHold = false;
        }

        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox card = (PictureBox)sender;

            if (e.Button == MouseButtons.Left)
            {
                mouseHold = true;
                deltaX = e.X;
                deltaY = e.Y;
            }
        }

        private void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseHold)
            {
                return;
            }

            PictureBox card = (PictureBox)sender;
            card.Left = card.Left + e.X - deltaX;
            card.Top = card.Top + e.Y - deltaY;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
