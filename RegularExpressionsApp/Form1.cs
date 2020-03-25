using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegularExpressionsApp
{
    public partial class Form1 : Form
    {
        Regex regex;
        MatchCollection matchCollection;
        int textStart = 0;
        int nextPosition = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.DetectUrls = false;

            richTextBox1.Text = @" 
             352-901-2058
             352-800-2324
             352-900-2222
             
             David Allen Morrow
             quickbasic22@yahoo.com
             quickbaic44@outlook.com
             
             434Dmorrow69
             _DMorrow69
             @52Morrow900.com
          
             Dell Computer Corporation
             IBM Computer Corporation
             HP Computer Corporation
 
             http://www.pepsi.com
             https://github.com/quickbasic22/MvcMovie.git
             https://github.com/quickbasic22/PictureProgram.git
             https://github.com/quickbasic22/NewProject.git
             http://Dmorrow.Facebook.com
             http://www.Dell.net            
           ";
            lblInfo.Text = "";
            lblInfo2.Text = "";
            lblInfo3.Text = "";
        }

        private void resetText()
        {
            richTextBox1.Text = @" 
             352-901-2058
             352-800-2324
             352-900-2222
             
             David Allen Morrow
             quickbasic22@yahoo.com
             quickbaic44@outlook.com
             
             434Dmorrow69
             _DMorrow69
             @52Morrow900.com
          
             Dell Computer Corporation
             IBM Computer Corporation
             HP Computer Corporation
 
             http://www.pepsi.com
             https://github.com/quickbasic22/MvcMovie.git
             https://github.com/quickbasic22/PictureProgram.git
             https://github.com/quickbasic22/NewProject.git
             http://Dmorrow.Facebook.com
             http://www.Dell.net            
           ";

            richTextBox1.DetectUrls = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetText();

            regex = new Regex(textBox1.Text, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            matchCollection = regex.Matches(richTextBox1.Text);
            int i = 0;
            
            richTextBox1.Multiline = true;
            
            if (matchCollection.Count >= 1)
            {
                //textStart = richTextBox1.Find(matchCollection[0].ToString());
                //richTextBox1.SelectionFont = new Font("Verdana", 10, FontStyle.Bold);
                //richTextBox1.SelectionColor = Color.Orange;
                //int nextPosition = textStart + richTextBox1.SelectedText.Length;

                for (i = 0; i < matchCollection.Count; i++)
                {
                    nextPosition = matchCollection[i].Length + textStart;
                    if (nextPosition != -1)
                    textStart = richTextBox1.Find(matchCollection[i].Value, nextPosition, RichTextBoxFinds.None);
                    richTextBox1.SelectionFont = new Font("Verdana", 10, FontStyle.Bold);
                    richTextBox1.SelectionColor = Color.Orange;
                    
                }

            }
                foreach (Match item in matchCollection)
                {
                    
                    lblInfo.Text += item.Value.ToString() + "\r\n";
                       
                }

            
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            var items = matchCollection.Count;
            var count = richTextBox1.SelectedText.Length;
            var matchEnum = matchCollection.GetEnumerator();
            var selectedText = richTextBox1.SelectedText;
            lblInfo2.Text = selectedText;
            var start = richTextBox1.SelectionStart;
            var wordlength = matchCollection[0].Length;
            richTextBox1.Copy();
            Bitmap bitmap = new Bitmap(1000, 800);

            richTextBox1.DrawToBitmap(bitmap, richTextBox1.ClientRectangle);

            bitmap.Save(@"C:\Users\David\Documents\Programming Projects\PowerShell  C# Classes\RichTextBoxBitMap.bmp");


            int currentPos = 0;
            int begin = 0;

            richTextBox1.DeselectAll();

            while (matchEnum.MoveNext())
            {
                
                currentPos = richTextBox1.Find(matchEnum.Current.ToString(), currentPos, RichTextBoxFinds.None);
                begin = matchEnum.Current.ToString().Length;
                currentPos += begin;

                richTextBox1.Select(currentPos, begin);
                richTextBox1.SelectedText = textBox2.Text;
                lblInfo3.Text += richTextBox1.SelectedText + "\r\n";
            }
            
        }
    }
}
