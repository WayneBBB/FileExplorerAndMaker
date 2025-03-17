using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;


//using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bold = DocumentFormat.OpenXml.Wordprocessing.Bold;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using FontSize = DocumentFormat.OpenXml.Wordprocessing.FontSize;
using ParagraphProperties = DocumentFormat.OpenXml.Wordprocessing.ParagraphProperties;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
//using Run = DocumentFormat.OpenXml.Drawing.Run;

namespace SimpleFileManager
{
    internal class MakeFirstDocument
    {
    
        string filepath = string.Empty;
        
        public static void CreateWordDoc(string filepath, string msg, string headname)
        {
            //if (File.Exists( filepath)) 
            //{
            //    DialogResult dialogResult= MessageBox.Show("The File Already Exisits. Do you want to continue and overwite it?","", MessageBoxButtons.YesNo);
            //    if (dialogResult == DialogResult.Yes)
            //    {

            //    }

            //}

            using (WordprocessingDocument doc = WordprocessingDocument.Create(filepath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                // Add a main document part. 
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                
                // Create the document structure and add some text.
                mainPart.Document = new Document();

                Body body = mainPart.Document.AppendChild(new Body());

                // Get the Styles part for this document.
                StyleDefinitionsPart part = mainPart?.StyleDefinitionsPart;


                // If the Styles part does not exist, add it and then add the style.

                string styleidTop = "BoldTop";
                string stylenameTop = "Boldtop";
                string styleidBoldBody = "BoldBody";
                string stylenameBody = "Boldbody";
                string styleidPlainBody = "PlainBody";
                string stylenamePlainBody = "Plainbody";

                if (part is null)
                {
                    part = AddStylesPartToPackage(doc);

                    AddNewTopStyle(part, styleidTop, stylenameTop);
                    AddNewBoldBodyStyle(part, styleidBoldBody, stylenameBody);
                    AddNewBoldBodyStyle(part, styleidPlainBody, stylenamePlainBody);
                }
                else
                {
                    // If the style is not in the document, add it.
                    if (IsStyleIdInDocument(doc, styleidTop) != true)
                    {
                        // No match on styleid, so let's try style name.
                        string? styleidFromName = GetStyleIdFromStyleName(doc, stylenameTop);

                        if (styleidFromName is null)
                        {
                            AddNewTopStyle(part, styleidTop, stylenameTop);
                        }
                        else
                            styleidTop = styleidFromName;
                    }
                    if (IsStyleIdInDocument(doc, styleidBoldBody) != true)
                    {
                        // No match on styleid, so let's try style name.
                        string? styleidFromName = GetStyleIdFromStyleName(doc, stylenameBody);

                        if (styleidFromName is null)
                        {

                            AddNewBoldBodyStyle(part, styleidBoldBody, stylenameBody);
                        }
                        else
                            styleidBoldBody = styleidFromName;
                    }
                    if (IsStyleIdInDocument(doc, styleidPlainBody) != true)
                    {
                        // No match on styleid, so let's try style name.
                        string? styleidFromName = GetStyleIdFromStyleName(doc, stylenamePlainBody);

                        if (styleidFromName is null)
                        {

                            AddNewBoldBodyStyle(part, styleidBoldBody, stylenameBody);
                        }
                        else
                            styleidPlainBody = styleidFromName;
                    }
                }

                //new SdtBlock(
                //    new SdtProperties(
                //        new Lock { Val = LockingValues.SdtContentLocked }),
                //    new SdtContentBlock()

                //        Paragraph para0 = new Paragraph();
                            //Create the first Paragraph element and apply top style.
                            Paragraph para0 = body.AppendChild(new Paragraph());

                // If the paragraph has no ParagraphProperties object, create one.
                if (para0.Elements<ParagraphProperties>().Count() == 0)
                {
                    para0.PrependChild<ParagraphProperties>(new ParagraphProperties());
                }

                //// Get the paragraph properties element of the paragraph.
                ParagraphProperties pPr = para0.Elements<ParagraphProperties>().First();
                pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleidTop };
                Run run0 = para0.AppendChild(new Run());

                // String msg contains the text from the msg parameter"
                run0.AppendChild(new Text(msg));
                //run0.AppendChild(new Break());
                

                //CreateNext paragarph and apply body bold style
                DocumentFormat.OpenXml.Wordprocessing.Paragraph para1 = body.AppendChild(new Paragraph());
                if (para1.Elements<ParagraphProperties>().Count() == 0)
                {
                    para1.PrependChild<ParagraphProperties>(new ParagraphProperties());
                }
                ParagraphProperties pPr1 = para1.Elements<ParagraphProperties>().First();
                pPr1.ParagraphStyleId = new ParagraphStyleId() { Val = styleidBoldBody };
                Run run1 = para1.AppendChild(new Run());
                run1.AppendChild(new Text("___________________________"));
                run1.AppendChild(new Break());
                run1.AppendChild(new Text("NOTES FOR THE DAY"));
                


                //Create Next Paragraph without formating
                Paragraph para2 = body.AppendChild(new Paragraph());
                if (para2.Elements<ParagraphProperties>().Count() == 0)
                {
                    para2.PrependChild<ParagraphProperties>(new ParagraphProperties());
                }
                ParagraphProperties pPr2 = para2.Elements<ParagraphProperties>().First();
                pPr2.ParagraphStyleId = new ParagraphStyleId() { Val = styleidPlainBody };
                Run run2 = para2.AppendChild(new Run());
                RunProperties runProps2 = new RunProperties();
                Bold bold = new Bold() { Val = false };
                runProps2.Append(bold);
                run2.AppendChild(new RunProperties(runProps2));
                run2.AppendChild(new Break());
                //run2.AppendChild(new Text(" "));
                run2.AppendChild(new Break());
                //run2.AppendChild(new Text(" "));
                run2.AppendChild(new Break());
                //run2.AppendChild(new Text(" "));
                run2.AppendChild(new Break());
                //run2.AppendChild(new Text(" "));
                run2.AppendChild(new Break());
                //run2.AppendChild(new Text(" "));
                run2.AppendChild(new Break());

                //Create next paragraph with bold body text
                Paragraph para3 = body.AppendChild(new Paragraph());
                if (para3.Elements<ParagraphProperties>().Count() == 0)
                {
                    para3.PrependChild<ParagraphProperties>(new ParagraphProperties());
                }
                ParagraphProperties pPr3 = para3.Elements<ParagraphProperties>().First();
                pPr3.ParagraphStyleId = new ParagraphStyleId() { Val = styleidBoldBody };
                Run run3 = para3.AppendChild(new Run());
                run3.AppendChild(new Text("___________________________"));
                run3.AppendChild(new Break());
                run3.AppendChild(new Text("HEADER"));
               
                Paragraph para4 = body.AppendChild(new Paragraph());
                if (para4.Elements<ParagraphProperties>().Count() == 0)
                {
                    para4.PrependChild<ParagraphProperties>(new ParagraphProperties());
                }
                ParagraphProperties pPr4 = para4.Elements<ParagraphProperties>().First();
                pPr4.ParagraphStyleId = new ParagraphStyleId() { Val = styleidPlainBody };

                Run run4 = para4.AppendChild(new Run()); 
                RunProperties runProps4 = new RunProperties();
                Bold bold1 = new Bold() { Val = false };
                runProps4.Append(bold1);
                run4.AppendChild(new RunProperties(runProps4));
                //run4.AppendChild(new Text(" "));
                run4.AppendChild(new Break());
                run4.AppendChild(new Text(headname));
                run4.AppendChild(new Break());
                //run4.AppendChild(new Text(" "));
                run4.AppendChild(new Break());
                //run4.AppendChild(new Text(" "));
                run4.AppendChild(new Break());
                //run4.AppendChild(new Text(" "));
                run4.AppendChild(new Break());

                Paragraph para5 = body.AppendChild(new Paragraph());
                if (para5.Elements<ParagraphProperties>().Count() == 0)
                {
                    para5.PrependChild<ParagraphProperties>(new ParagraphProperties());
                }
                ParagraphProperties pPr5 = para5.Elements<ParagraphProperties>().First();
                pPr5.ParagraphStyleId = new ParagraphStyleId() { Val = styleidBoldBody };
                Run run5 = para5.AppendChild(new Run());
                run5.AppendChild(new Text("___________________________"));

            }
        }


        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        static StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            MainDocumentPart mainDocumentPart = doc.MainDocumentPart ?? doc.AddMainDocumentPart();
            StyleDefinitionsPart part = mainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            Styles root = new Styles();

            return part;
        }

        // Create a new style with the specified styleid and stylename and add it to the specified
        // style definitions part.
        static void AddNewTopStyle(StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename, string  aliases = "")
        {
            // Get access to the root element of the styles part.
            styleDefinitionsPart.Styles ??= new Styles();
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            // Create and add the child elements (properties of the style).
            Aliases aliases1 = new Aliases() { Val = aliases };
            AutoRedefine autoredefine1 = new AutoRedefine() { Val = OnOffOnlyValues.Off };
            BasedOn basedon1 = new BasedOn() { Val = "Normal" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "OverdueAmountChar" };
            Locked locked1 = new Locked() { Val = OnOffOnlyValues.Off };
            PrimaryStyle primarystyle1 = new PrimaryStyle() { Val = OnOffOnlyValues.On };
            StyleHidden stylehidden1 = new StyleHidden() { Val = OnOffOnlyValues.Off };
            SemiHidden semihidden1 = new SemiHidden() { Val = OnOffOnlyValues.Off };
            StyleName styleName1 = new StyleName() { Val = stylename };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            UIPriority uipriority1 = new UIPriority() { Val = 1 };
            UnhideWhenUsed unhidewhenused1 = new UnhideWhenUsed() { Val = OnOffOnlyValues.On };

            if (string.IsNullOrWhiteSpace(aliases))
            {
                style.Append(aliases1);
            }

            style.Append(autoredefine1);
            style.Append(basedon1);
            style.Append(linkedStyle1);
            style.Append(locked1);
            style.Append(primarystyle1);
            style.Append(stylehidden1);
            style.Append(semihidden1);
            style.Append(styleName1);
            style.Append(nextParagraphStyle1);
            style.Append(uipriority1);
            style.Append(unhidewhenused1);


            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
                Bold bold1 = new Bold();
                Color color1 = new Color() { Val = "000000" };

                RunFonts font1 = new RunFonts() { Ascii = "Arial" };
                //Italic italic1 = new Italic();
                // Specify a 12 point size.
                FontSize fontSize1 = new FontSize() { Val = "36" };
                styleRunProperties1.Append(bold1);
                styleRunProperties1.Append(color1);
                styleRunProperties1.Append(font1);
                styleRunProperties1.Append(fontSize1);
                //styleRunProperties1.Append(italic1);

                // Add the run properties to the style.
                style.Append(styleRunProperties1);
            
            
            
            // Add the style to the styles part.
            styles.Append(style);
        }

        static void AddNewBoldBodyStyle(StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename, string aliases = "")
        {
            // Get access to the root element of the styles part.
            styleDefinitionsPart.Styles ??= new Styles();
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            // Create and add the child elements (properties of the style).
            Aliases aliases1 = new Aliases() { Val = aliases };
            AutoRedefine autoredefine1 = new AutoRedefine() { Val = OnOffOnlyValues.Off };
            BasedOn basedon1 = new BasedOn() { Val = "Normal" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "OverdueAmountChar" };
            Locked locked1 = new Locked() { Val = OnOffOnlyValues.Off };
            PrimaryStyle primarystyle1 = new PrimaryStyle() { Val = OnOffOnlyValues.On };
            StyleHidden stylehidden1 = new StyleHidden() { Val = OnOffOnlyValues.Off };
            SemiHidden semihidden1 = new SemiHidden() { Val = OnOffOnlyValues.Off };
            StyleName styleName1 = new StyleName() { Val = stylename };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            UIPriority uipriority1 = new UIPriority() { Val = 1 };
            UnhideWhenUsed unhidewhenused1 = new UnhideWhenUsed() { Val = OnOffOnlyValues.On };

            if (string.IsNullOrWhiteSpace(aliases))
            {
                style.Append(aliases1);
            }

            style.Append(autoredefine1);
            style.Append(basedon1);
            style.Append(linkedStyle1);
            style.Append(locked1);
            style.Append(primarystyle1);
            style.Append(stylehidden1);
            style.Append(semihidden1);
            style.Append(styleName1);
            style.Append(nextParagraphStyle1);
            style.Append(uipriority1);
            style.Append(unhidewhenused1);


            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            Bold bold1 = new Bold();
            Color color1 = new Color() { Val = "000000" };
            Lock lock1 = new Lock() { Val = LockingValues.SdtLocked };
            
            RunFonts font1 = new RunFonts() { Ascii = "Arial" };
           // Italic italic1 = new Italic();
            // Specify a 12 point size.
            FontSize fontSize1 = new FontSize() { Val = "24" };
            styleRunProperties1.Append(bold1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(font1);
            styleRunProperties1.Append(fontSize1);
            //styleRunProperties1.Append(italic1);
            //styleRunProperties1.Append(lock1);
            // Add the run properties to the style.
            style.Append(styleRunProperties1);



            // Add the style to the styles part.
            styles.Append(style);
        }

        static void AddPlainBodyStyle(StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename, string aliases = "")
        {
            // Get access to the root element of the styles part.
            styleDefinitionsPart.Styles ??= new Styles();
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            // Create and add the child elements (properties of the style).
            Aliases aliases1 = new Aliases() { Val = aliases };
            AutoRedefine autoredefine1 = new AutoRedefine() { Val = OnOffOnlyValues.Off };
            BasedOn basedon1 = new BasedOn() { Val = "Normal" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "OverdueAmountChar" };
            Locked locked1 = new Locked() { Val = OnOffOnlyValues.Off };
            PrimaryStyle primarystyle1 = new PrimaryStyle() { Val = OnOffOnlyValues.On };
            StyleHidden stylehidden1 = new StyleHidden() { Val = OnOffOnlyValues.Off };
            SemiHidden semihidden1 = new SemiHidden() { Val = OnOffOnlyValues.Off };
            StyleName styleName1 = new StyleName() { Val = stylename };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            UIPriority uipriority1 = new UIPriority() { Val = 1 };
            UnhideWhenUsed unhidewhenused1 = new UnhideWhenUsed() { Val = OnOffOnlyValues.On };

            if (string.IsNullOrWhiteSpace(aliases))
            {
                style.Append(aliases1);
            }

            style.Append(autoredefine1);
            style.Append(basedon1);
            style.Append(linkedStyle1);
            style.Append(locked1);
            style.Append(primarystyle1);
            style.Append(stylehidden1);
            style.Append(semihidden1);
            style.Append(styleName1);
            style.Append(nextParagraphStyle1);
            style.Append(uipriority1);
            style.Append(unhidewhenused1);


            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            Bold bold1 = new Bold() { Val= DocumentFormat.OpenXml.OnOffValue.FromBoolean(false) };

            BoldComplexScript boldCS = new BoldComplexScript();
            //bold1.Val = false;
            Color color1 = new Color() { Val = "000000" };

            RunFonts font1 = new RunFonts() { Ascii = "Arial" };

           
            // Italic italic1 = new Italic();
            // Specify a 12 point size.
            FontSize fontSize1 = new FontSize() { Val = "24" };
            //FontStyle fontweight ;
            //styleRunProperties1.Append(bold1);
            //styleRunProperties1.Append(boldCS);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(font1);
            styleRunProperties1.Append(fontSize1);
            //styleRunProperties1.Append(italic1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);



            // Add the style to the styles part.
            styles.Append(style);
        }
        // Return true if the style id is in the document, false otherwise.
        static bool IsStyleIdInDocument(WordprocessingDocument doc, string styleid)
        {
            // Get access to the Styles element for this document.
            Styles? s = doc.MainDocumentPart?.StyleDefinitionsPart?.Styles;

            if (s is null)
            {
                return false;
            }

            // Check that there are styles and how many.
            int n = s.Elements<Style>().Count();

            if (n == 0)
            {
                return false;
            }

            // Look for a match on styleid.
            Style? style = s.Elements<Style>()
                .Where(st => (st.StyleId is not null && st.StyleId == styleid) && (st.Type is not null && st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style is null)
            {
                return false;
            }

            return true;
        }

        // Return styleid that matches the styleName, or null when there's no match.
        static string? GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart? stylePart = doc.MainDocumentPart?.StyleDefinitionsPart;
            string? styleId = stylePart?.Styles?.Descendants<StyleName>()
                .Where(s =>
                {
                    OpenXmlElement? p = s.Parent;
                    EnumValue<StyleValues>? styleValue = p is null ? null : ((Style)p).Type;

                    return s.Val is not null && s.Val.Value is not null && s.Val.Value.Equals(styleName) &&
                    (styleValue is not null && styleValue == StyleValues.Paragraph);
                })
                .Select(n =>
                {

                    OpenXmlElement? p = n.Parent;
                    return p is null ? null : ((Style)p).StyleId;
                }).FirstOrDefault();

            return styleId;
        }

    }
}
