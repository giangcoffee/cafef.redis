using System;
using System.Collections.Generic;
using System.Linq;


namespace CafeF.Redis.Entity
{
    #region FileObject Models
    [Serializable]
    //key format: "FileObject:{0}:FileUrl" / "fileobjectid:{0}:Object"
    public class FileObject //Thông tin file đính kèm - Gắn vào đối tượng Reports
    {
       // public string Key { get; set; } // key của object này là FileUrl
        public string FileName { get; set; }
        public string FileUrl { get; set; }
    }
	#endregion	
	
}