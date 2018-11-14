using System;
using System.Data;
using System.IO;

namespace BLL
{
    public class Extension
    {
        StreamWriter myWriter;
        StreamReader myReader;
        int Schedule_ID = 0;
        public bool Export_to_XML(DataSet myExcelData, string activeSheet,string ChannelName, string dirLacation)
        {
            myReader = new StreamReader("Schedule ID.txt");
            Schedule_ID = Convert.ToInt32(myReader.ReadLine());
            myReader.Close();

            myWriter = new StreamWriter($"{dirLacation}");

            myWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF - 8\" ?>");
            myWriter.WriteLine("<synergytoepg xmlns=\"http://SourceSytem.Hostname.Interfaces/folder/1.0\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            myWriter.WriteLine($"<datefrom>{(myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[6] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[7] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[4] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[5] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[0] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[1] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[2] + "" + (myExcelData.Tables[activeSheet].Rows[1][0]).ToString()[3]}</datefrom>");
            myWriter.WriteLine($"<dateto>{(myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[6] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[7] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[4] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[5] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[0] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[1] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[2] + "" + (myExcelData.Tables[activeSheet].Rows[myExcelData.Tables[activeSheet].Rows.Count - 1][0]).ToString()[3]}</dateto>");
            myWriter.WriteLine($"<channelname>{ChannelName}</channelname>");
            myWriter.WriteLine($"<channelstarttime>{myExcelData.Tables[activeSheet].Rows[1][1].ToString().Substring(11, 8)}</channelstarttime>");
            
            for (int i = 1; i < myExcelData.Tables[activeSheet].Rows.Count; i++)
            {
                myWriter.WriteLine($"<schedule id=\"{++Schedule_ID}\">");
                myWriter.WriteLine($"\t<programmetitle>{myExcelData.Tables[activeSheet].Rows[i][2]}</programmetitle>");
                myWriter.WriteLine($"\t<programmenumber></programmenumber>");
                myWriter.WriteLine($"\t<episodetitle></episodetitle>");
                myWriter.WriteLine($"\t<episodenumber>{myExcelData.Tables[activeSheet].Rows[i][5]}</episodenumber>");
                myWriter.WriteLine($"\t<seriesnumber>{myExcelData.Tables[activeSheet].Rows[i][6]}</seriesnumber>");
                myWriter.WriteLine($"\t<yearofrelease></yearofrelease>");
                myWriter.WriteLine($"\t<directorname></directorname>");
                myWriter.WriteLine($"\t<castname></castname>");
                myWriter.WriteLine($"\t<scheduledate>{(myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[6] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[7] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[4] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[5] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[0] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[1] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[2] + "" + (myExcelData.Tables[activeSheet].Rows[i][0]).ToString()[3]}</scheduledate>");
                myWriter.WriteLine($"\t<schedulestarttime>{(myExcelData.Tables[activeSheet].Rows[i][1]).ToString().Substring(11, 8)}</schedulestarttime>");

                try
                {
                    myWriter.WriteLine($"\t<scheduleendtime>{myExcelData.Tables[activeSheet].Rows[i + 1][1].ToString().Substring(11, 8)}</scheduleendtime>");
                }
                catch
                {
                    myWriter.WriteLine($"\t<scheduleendtime></scheduleendtime>");
                }

                myWriter.WriteLine($"\t<classification>{myExcelData.Tables[activeSheet].Rows[i][3]}</classification>");
                myWriter.WriteLine($"\t<synopsis1>{myExcelData.Tables[activeSheet].Rows[i][4]}</synopsis1>");
                myWriter.WriteLine($"\t<synopsis2></synopsis2>");
                myWriter.WriteLine($"\t<synopsis3></synopsis3>");
                myWriter.WriteLine($"\t<genre></genre>");
                myWriter.WriteLine($"\t<colour></colour>");
                myWriter.WriteLine($"\t<country></country>");
                myWriter.WriteLine($"\t<language></language>");
                myWriter.WriteLine("</schedule>");
            }
            
            myWriter.WriteLine("</synergytoepg>");
            
            myWriter.Close();

            myWriter = new StreamWriter("Schedule ID.txt");
            myWriter.WriteLine(Schedule_ID);
            myWriter.Close();

            return true;
        }
    }
}
