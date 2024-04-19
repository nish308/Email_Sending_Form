using System.Net;
using System.Net.Mail;

namespace EmailSendingWinApp
{
    public partial class Form1 : Form
    {
        List<string> attachments = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string Receiver = txtEmailTo.Text;
                string Subject = txtSubject.Text;
                string Message = txtBody.Text;

                //Here You Have To Add 2FA In Your Gmail Account And Then You Have To Generate App Password 
                string Email = "Your Email Id";
                string Password = "Generated App Password";
                string Host = "smtp.gmail.com";
                int Port = 587;

                using (MailMessage mail = new MailMessage(Email, Receiver, Subject, Message))
                {
                    foreach (string filename in attachments)
                    {
                        mail.Attachments.Add(new Attachment(filename));
                    }

                    using (SmtpClient smtp = new SmtpClient(Host, Port))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(Email, Password);
                        smtp.Send(mail);
                        MessageBox.Show("Email Sent Successfully!", "Success");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}","Error");
            }
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    attachments.Add(filename);
                    txtForFile.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
                }
            }
        }
    }
}