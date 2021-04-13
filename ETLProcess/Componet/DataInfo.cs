using System;

namespace ETLProcess.Component{
    public class DataInfo{
        public DataInfo(string filepath,string accountID, DateTime date){
            FilePath = filepath;
            AccountID = accountID;
            Date = date;
        }

        public string FilePath;
        public string AccountID;
        public DateTime Date;
    }


}