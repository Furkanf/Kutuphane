using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Tesseract;


//Internetten alinmistir. tesseract githubtan alinmistir.
namespace Kutuphane
{
    public class Ocr
    {
        public static string Doit(Bitmap image)
        {
            string text = "";
            string lng = "eng";
          
            try
            {
               
                using (var engine = new TesseractEngine(HttpContext.Current.Server.MapPath(@"~/tessdata"), lng, EngineMode.Default))
                {
                    using (var img = PixConverter.ToPix(image))
                    {
                        using (var page = engine.Process(img))
                        {

                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {


                                                string buffer = iter.GetText(PageIteratorLevel.Word);
                                                if (!(buffer == "" || buffer == null || buffer == string.Empty || buffer == " "))
                                                {
                                                    text += " " + iter.GetText(PageIteratorLevel.Word);
                                                }


                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    if (!(text == " " || text == String.Empty))
                                                    {
                                                        text += " ";
                                                    }

                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                if (!(text == "" || text == String.Empty))
                                                {
                                                    text += " ";
                                                }

                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                text = "";
            }
            return text;
        }
    }
}