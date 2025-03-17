using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Drawing;
using System.IO;
using System.Xml;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using Path = System.IO.Path;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using Convert = DocumentFormat.OpenXml.Int64Value;
using System.Windows.Forms;
namespace SimpleFileManager
{

    enum ImageTypeEnum
    {
        PNG,
        JPEG,
        GIF,
        JPG
    }
    public class AddPictures
    {

        public static void InsertPictures(string document, FileInfo[] files, string filespath)
        {
            using (WordprocessingDocument wordprocessingDocument = WordprocessingDocument.Open(document, true))
            {

                if (wordprocessingDocument.MainDocumentPart is null)
                {
                    MessageBox.Show("There was no document found that could be opened.");
                  
                }

                //MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;
               
                //Add reference to the main document
                MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart ?? wordprocessingDocument.AddMainDocumentPart();
                mainPart.Document ??= new Document();
                mainPart.Document.Body ??= mainPart.Document.AppendChild(new Body());
                Body body = wordprocessingDocument.MainDocumentPart!.Document!.Body!;

                string fileExtension = "";
                //listView1.Items.Clear();
                ImageTypeEnum imagetype = ImageTypeEnum.PNG;

               
                for (int i = 0; i < files.Length; i++)
                {

                    bool FoundPicture = false;
                    fileExtension = files[i].Extension.ToUpper();
                    string fileName = files[i].Name;
                    switch (fileExtension)
                    {


                        case ".PNG":
                            {
                                FoundPicture = true;
                                imagetype = ImageTypeEnum.PNG;
                                break;
                            }
                        case ".JPG":
                            {
                                FoundPicture = true;
                                imagetype = ImageTypeEnum.JPEG;
                                break;
                            }
                        case ".JPEG":
                            {
                                FoundPicture = true;
                                imagetype = ImageTypeEnum.JPEG;
                                break;
                            }
                        default:

                            break;
                    }

                    if (FoundPicture == true)
                    {

                        //Add the Image name before the picture is inserted.
                        Paragraph para = body.AppendChild(new Paragraph());
                        Run run = para.AppendChild(new Run());
                        RunProperties runProps = new RunProperties();
                        Bold bold = new Bold() { Val = true };
                        runProps.Append(bold);
                        run.AppendChild(new RunProperties(runProps));
                        run.AppendChild(new Text(fileName));


                        //The thumbs are set up to be a constant height and will adjust to the height/width ratio of the picture. If need
                        //to adjust size of thumbs, just change the below height value.  The pictures are expressed English Metric Units and 
                        //have a value of 914,000 emus per inch.

                        Int64 Int64thumbWidth;
                        Int64 Int64thumbHeight = 950000; 

                        string PicFilePath = Path.Combine(filespath,fileName);

                        using (Image image = Image.FromFile(PicFilePath))
                        {
                            int width =  image.Width;
                            int height = image.Height;

                            double ThumbPicRatio =(double) width / height;

                           
                            double thumbWidth = Int64thumbHeight * ThumbPicRatio;
                            Int64thumbWidth = Convert.ToInt64((Convert)thumbWidth);
                           
                        }

                        if (imagetype == ImageTypeEnum.JPEG)
                        {
                            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                           
                            using (FileStream stream = new FileStream(PicFilePath, FileMode.Open))
                            {
                                imagePart.FeedData(stream);
                            }

                            AddImageToBody(wordprocessingDocument, mainPart.GetIdOfPart(imagePart), Int64thumbHeight, Int64thumbWidth);
                        }

                        if (imagetype == ImageTypeEnum.PNG)
                        {
                            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);

                            using (FileStream stream = new FileStream(PicFilePath, FileMode.Open))
                            {
                                imagePart.FeedData(stream);
                            }

                            AddImageToBody(wordprocessingDocument, mainPart.GetIdOfPart(imagePart), Int64thumbHeight, Int64thumbWidth);
                        }

                        //Add the blank lines and separators before next image
                       Paragraph para1 = body.AppendChild(new Paragraph());
                        Run run1 = para1.AppendChild(new Run());
                        RunProperties runProps1 = new RunProperties();
                        Bold bold1 = new Bold() { Val = false };
                        runProps1.Append(bold1);
                        run1.AppendChild(new RunProperties(runProps1));
                        run1.AppendChild(new Break());
                        run1.AppendChild(new Break());
                        run1.AppendChild(new Break());
                       


                        //Insert separator line
                        Run run2 = para1.AppendChild(new Run());
                        RunProperties runProps2 = new RunProperties();
                        Bold bold2 = new Bold() { Val = true };
                        runProps2.Append(bold2);
                        run2.AppendChild(new RunProperties(runProps2));
                        run2.AppendChild(new Text("_________________________"));
                       

                    }
                }


            }
        }



        static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId,Int64 height, Int64 width)
        {
            // Define the reference of the image.            

            var element =
                 new Drawing(
                     new DW.Inline(
                         new DW.Extent() { Cx = width, Cy = height },
                         new DW.EffectExtent()
                         {
                             LeftEdge = 0L,
                             TopEdge = 0L,
                             RightEdge = 0L,
                             BottomEdge = 0L
                         },
                         new DW.DocProperties()
                         {
                             Id = (UInt32Value)1U,
                             Name = "Picture 1"
                         },
                         new DW.NonVisualGraphicFrameDrawingProperties(
                             new A.GraphicFrameLocks() { NoChangeAspect = true }),
                         new A.Graphic(
                             new A.GraphicData(
                                 new PIC.Picture(
                                     new PIC.NonVisualPictureProperties(
                                         new PIC.NonVisualDrawingProperties()
                                         {
                                             Id = (UInt32Value)0U,
                                             Name = "New Bitmap Image.jpg"
                                         },
                                         new PIC.NonVisualPictureDrawingProperties()),
                                     new PIC.BlipFill(
                                         new A.Blip(
                                             new A.BlipExtensionList(
                                                 new A.BlipExtension()
                                                 {
                                                     Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                 })
                                         )
                                         {
                                             Embed = relationshipId,
                                             CompressionState =
                                             A.BlipCompressionValues.Print
                                         },
                                         new A.Stretch(
                                             new A.FillRectangle())),
                                     new PIC.ShapeProperties(
                                         new A.Transform2D(
                                             new A.Offset() { X = 0L, Y = 0L },                                            
                                             new A.Extents() { Cx = width, Cy = height }),  //{ Cx = 990000L, Cy = 792000L })
                                         new A.PresetGeometry(
                                             new A.AdjustValueList()
                                         )
                                         { Preset = A.ShapeTypeValues.Rectangle }))
                             )
                             { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                     )
                     {
                         DistanceFromTop = (UInt32Value)0U,
                         DistanceFromBottom = (UInt32Value)0U,
                         DistanceFromLeft = (UInt32Value)0U,
                         DistanceFromRight = (UInt32Value)0U,
                         EditId = "50D07946"
                     });

            if (wordDoc.MainDocumentPart is null || wordDoc.MainDocumentPart.Document.Body is null)
            {
                throw new ArgumentNullException("MainDocumentPart and/or Body is null.");
            }

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
        }


    }
}
