using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using System.Drawing;

namespace SimpleFileManager
{
    internal class MakePicThumbnail
    {
        public MakePicThumbnail()
        {

        }

        public Image.GetThumbnailImageAbort ThumbnailCallback { get; private set; }

        public string MakeThumbnail(string path)
        {
            //Source Page: https://stackoverflow.com/questions/1009693/generate-a-thumbnail-of-a-word-document

            byte[] docContent = File.ReadAllBytes(path);

            using (MemoryStream ms = new MemoryStream(docContent))
            {
                // Creates a Spire.Doc object to work with
                Spire.Doc.Document doc = new Spire.Doc.Document(ms, Spire.Doc.FileFormat.Auto);
                // SaveToImages creates an array of System.Drawing.Image, we take only the 1st element
                System.Drawing.Image img = doc.SaveToImages(0, 1, Spire.Doc.Documents.ImageType.Bitmap)[0];

                using (var ms2 = new MemoryStream())
                {
                    // Auxiliary object needed for GetThumbnailImage
                    System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    // We create a thumbnail (0.5 width and height = 50%)
                    img.GetThumbnailImage((int)(img.Width * 0.5), (int)(img.Height * 0.5), myCallback, IntPtr.Zero).Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
                    // Convert to Base64 string representation of the image
                    return Convert.ToBase64String(ms2.ToArray());
                }
            }

        }
    }
}