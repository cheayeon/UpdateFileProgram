using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpdateFileProgram.Models;

namespace UpdateFileProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<FileInfoModel> CopyFileInfoList;
        string PublicPath = "";
        string PrivatePath = "";

        public MainWindow()
        {
            InitializeComponent();

            CopyFileInfoList = new List<FileInfoModel>();
        }

        private void Enter_privatePath(object sender, RoutedEventArgs e)
        {
            PrivatePath = privateFilePath.Text;
        }

        private void Enter_publicPath(object sender, RoutedEventArgs e)
        {
            PublicPath = publicFilePath.Text;
        }

        private void Update_FileList(object sender, RoutedEventArgs e)
        {
            FileCopyList.Items.Clear();

            DirectoryInfo publicfileDirectoryInfo = new DirectoryInfo(PublicPath);
            DirectoryInfo privatefileDirectoryInfo = new DirectoryInfo(PrivatePath);

            foreach (FileInfo checkFile in publicfileDirectoryInfo.GetFiles())
            {
                bool deffer = false;
                foreach (FileInfo file in privatefileDirectoryInfo.GetFiles())
                {
                    if (checkFile.Name == file.Name)
                    {
                        deffer = true;

                        if (File.Exists(PrivatePath + "\\" + file.Name))
                        {
                            if (checkFile.LastWriteTime != file.LastWriteTime)
                            {
                                deffer = false;
                            }
                        }
                        break;
                    }
                }

                if (!deffer)
                {
                    FileInfoModel notexistfile = new FileInfoModel(checkFile.Name, checkFile.FullName);
                    FileCopyList.Items.Add(notexistfile);
                }
            }
        }

        private void FileListItemClick(object sender, RoutedEventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            if (ck.IsChecked == true)
            {
                FileInfoModel copyfile = ck.DataContext as FileInfoModel;
                if (copyfile != null)
                    CopyFileInfoList.Add(copyfile);

            }
            else
            {
                FileInfoModel copyfile = ck.DataContext as FileInfoModel;
                if (copyfile != null)
                    CopyFileInfoList.Remove(copyfile);
            }
        }

        private void Update_Copy(object sender, RoutedEventArgs e)
        {
            foreach (FileInfoModel file in CopyFileInfoList)
            {
                try
                {
                    System.IO.File.Copy(file.FilePath, PrivatePath + "\\" + file.FileName, true);
                }
                catch (Exception)
                {
                    System.IO.File.Copy(file.FilePath, PrivatePath + "\\" + file.FileName);

                }
            }
        }

        private void DownLoad_Copy(object sender, RoutedEventArgs e)
        {
            foreach (FileInfoModel file in CopyFileInfoList)
            {
                try
                {
                    System.IO.File.Copy(PrivatePath + "\\" + file.FileName, file.FilePath, true);
                }
                catch (Exception)
                {
                    System.IO.File.Copy(PrivatePath + "\\" + file.FileName, file.FilePath);

                }
            }
        }
    }
}