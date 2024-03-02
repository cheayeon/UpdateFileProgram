using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateFileProgram.Models
{
    internal class FileInfoModel
    {
        public string FileName { get; set; } = "";
        public string FilePath { get; set; } = "";
        public bool IsChoice { get; set; } = false;

        public FileInfoModel() { }
        public FileInfoModel(string name, string path, bool choice = false)
        {
            FileName = name;
            FilePath = path;
            IsChoice = choice;
        }
    }
}
