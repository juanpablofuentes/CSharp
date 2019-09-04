using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Common.Dictionaries
{
    public static class MimeTypesDictionary
    {        
        public static Dictionary<string, string> mimeTypes = new Dictionary<string, string>() {
            {".doc", "application/msword"},
            {".dot", "application/msword"},
            {".txt", "text/plain"},
            {".xla", "application/excel"},
            {".xls", "application/excel"},
            {".xld", "application/excel"},
             {".xlv", "application/excel"},
            {".pdf", "text/pdf"},
            {".png", "image/png"},
            {".xml", "text/xml"},
            {".zip", "application/x-compressed"},
            {".word", "application/msword"},
            {".tif", "image/tiff"},
            {".rgb", "image/x-rgb"},
            {".ppt", "application/powerpoint"},
            {".pps", "application/mspowerpoint"},
            {".ppz", "application/mspowerpoint"},
            {".pot", "application/mspowerpoint"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".gif", "image/gif"},
            {".pic", "image/pic"},
            {".pict", "image/pict"},
            {".html", "text/html"},
            {".bm","image/bmp"},
            {".bmp","image/bmp"}
        };
      
        public static string GetMimeType(string format)
        {
            
            if (mimeTypes[format] != null)
            {
                return mimeTypes[format];
            }
            else
            {
                return mimeTypes[".txt"];
            }
        }       
    }
}