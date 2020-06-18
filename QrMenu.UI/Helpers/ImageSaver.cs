using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace QrMenu.UI.Helpers
{
    public static class ImageSaver
    {
        /// <summary>
        /// Gelen fotografları /Medias/gorseladı/görselAdı_name şeklinde kaydeder.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<string> SaveImage(HttpPostedFileBase[] images,string name)
        {
            List<string> imagePaths = new List<string>();
            if (images[0]!=null )
            {
                foreach (var item in images)
                {
                    string imageName = item.FileName;
                    string[] fileNameByDots = imageName.Split('.');
                    string extansion = fileNameByDots[fileNameByDots.Length - 1];
                    imageName.Replace(extansion,"");
                    using (FileNamer namer= new FileNamer())
                    {
                        imageName=namer.ConvertTRCharToENChar(imageName);
                       name= namer.ConvertTRCharToENChar(name);
                    }
                    string newFileName = name+"_"+ imageName;
                    string filePath = HttpContext.Current.Server.MapPath("~/Medias/" + name);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string finalName = Path.Combine(filePath, newFileName);
                    if (File.Exists(finalName+"."+extansion))
                    {
                        finalName = finalName + DateTime.Now.ToString("ddMMyyyy_HHmmss");
                    }
                    try
                    {
                        item.SaveAs(finalName + "." + extansion);
                        string dbPath = ("/Medias/") + name + "/" + newFileName + "." + extansion;
                        imagePaths.Add(dbPath);
                        return imagePaths;
                    }
                    catch (Exception e)
                    {
                        imagePaths.Add("Dosya Kaydedilemedi");
                        imagePaths.Add(e.Message);
                        return imagePaths;



                    }



                }
            }
            else
            {
                imagePaths.Add("Görsel Yok");
            }
            
            return imagePaths;
        }

        /// <summary>
        /// Dosyaları dosyaAdı_Deleted diye yeni bi klasöre taşır. Sunucudan silmez
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string DeleteImage(string path,string name)
        {

            string imageServerLocation = HttpContext.Current.Server.MapPath(path);
            string[] filenameSeperated = path.Split('/');
            string fileName = filenameSeperated[filenameSeperated.Length - 1];
            string parent =Directory.GetParent(imageServerLocation).ToString();
            string deletedFolderName = parent + "_" + "Deleted";
            //string imageFolderName = "";
            //for (int i = 0; i < filenameSeperated.Length - 1; i++)
            //{
            //    imageFolderName += filenameSeperated[i] + "/";
            //}
            //Directory.CreateDirectory(deletedFolderName);
            Directory.Move(parent, deletedFolderName);
            using (FileNamer namer= new FileNamer())
            {
                name = namer.ConvertTRCharToENChar(name);
            }
            return "/Medias/"+ name+ "_Deleted" + "/" + fileName;
        }

        /// <summary>
        /// Dosyaları tamamen siler.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string DeleteImage(string path)
        {
            try
            {
                string serverPath = HttpContext.Current.Server.MapPath(path);
                File.Delete(serverPath);
                return "Dosya Silindi";

            }
            catch (Exception e)
            {

                return e.Message;
            }
            
        }
    }
}