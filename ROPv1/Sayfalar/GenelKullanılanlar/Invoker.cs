using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ROPv1
{
    public class Invoker
    {
        public OpenFileDialog InvokeDialog;
        private Thread InvokeThread;
        private DialogResult InvokeResult;

        public Invoker()
        {
            InvokeDialog = new OpenFileDialog();
            InvokeThread = new Thread(new ThreadStart(InvokeMethod));
            InvokeThread.SetApartmentState(ApartmentState.STA);
            InvokeResult = DialogResult.None;

            InvokeDialog.Filter = "png dosyaları (*.png)|*.png";

            string image_outputDir = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            DirectoryInfo df = new DirectoryInfo(Application.StartupPath + @"\resimler\");

            if (!df.Exists) // klasör yoksa oluştur
            {
                // create new directory
                DirectoryInfo di = Directory.CreateDirectory(image_outputDir + @"\resimler\");
            }

            //seçim ekranında ilk, oluşan klasör açılsın
            string path = Application.StartupPath + @"\resimler";
            InvokeDialog.InitialDirectory = path;

            InvokeDialog.Title = "Resim Dosyası Seçiniz";
        }

        public DialogResult Invoke()
        {
            InvokeThread.Start();
            InvokeThread.Join();
            return InvokeResult;
        }

        private void InvokeMethod()
        {
            InvokeResult = InvokeDialog.ShowDialog();
        }
    }
}
