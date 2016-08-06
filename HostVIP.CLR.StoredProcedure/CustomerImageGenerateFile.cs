using System;
using System.Data.SqlClient;
using System.Drawing;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;


public partial class StoredProcedures
{
    [SqlProcedure]
    public static void CustomerImageGenerateFile()
    {
        using (var connection = new SqlConnection("context connection=true"))
        {

            connection.Open();

            #region Retrieving Path

            var command = new SqlCommand("SELECT PropertyValue FROM misc WHERE propertyName='ImageStoragePath'", connection);
            string filename;

            

            SqlDataReader reader = command.ExecuteReader();

            string ImageStorage = String.Empty;
            

            if (reader == null) return;


            while (reader.Read())
            {
                const int idxImageStoragaPeth = 0;
                ImageStorage = reader.GetString(idxImageStoragaPeth);
            }

            reader.Close();

            

          
            #endregion

            command = new SqlCommand("select ImageId,height,width,FileName,Stream from image where Islocal=0", connection);
            reader = command.ExecuteReader();


            while (reader.Read())
            {
                filename = reader.GetString(3);
                try
                {

                    const int idxHeight = 1;
                    const int idxWidth = 2;
                    const int idxStream = 4;

                    if(reader[idxStream]==null) continue;

                    byte[] stream =stream = (byte[])reader[idxStream];
                    
                    string pathAndFileName = Path.Combine(ImageStorage, filename);

                    

                    if (!File.Exists(pathAndFileName))
                    {
                        
                        MemoryStream st=new MemoryStream(stream);
                        

                        Bitmap bmp = new Bitmap(st);
                       
                        bmp.Save(pathAndFileName,ImageFormat.Jpeg);
                      

                    }
                }
                catch (Exception ex)
                {

                    Debug.Write(ex.Message);
                }


            }

        }

    }

};
