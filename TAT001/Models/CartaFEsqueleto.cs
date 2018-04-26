using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using TAT001.Models;

namespace TAT001.Models
{
    public class CartaFEsqueleto
    {
        iTextSharp.text.Font negritaPeque = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
        iTextSharp.text.Font normalPeque = FontFactory.GetFont(FontFactory.HELVETICA, 11);
        iTextSharp.text.Font normalMasPeque = FontFactory.GetFont(FontFactory.HELVETICA, 8);
        PdfPTable tablaDatos = new PdfPTable(1);
        PdfPTable tablaDatos1 = new PdfPTable(2);
        PdfPTable tablaDatos2 = new PdfPTable(2);
        PdfPTable tablaDatos3 = new PdfPTable(2);
        public int a, b, r;

        Entities.TEXTOCARTAF foo = new Entities.TEXTOCARTAF();


        public void crearPDF(CartaF c, Entities.TEXTOCARTAF f)
        {
            HeaderFooter hfClass = new HeaderFooter(c);
            DateTime fechaCreacion = DateTime.Now;
            string nombreArchivo = string.Format("{0}.pdf", fechaCreacion.ToString(@"yyyyMMdd") + "_" + fechaCreacion.ToString(@"HHmmss"));
            string rutaCompleta = HttpContext.Current.Server.MapPath("~/PdfTemp/" + nombreArchivo);
            FileStream fsDocumento = new FileStream(rutaCompleta, FileMode.Create);
            //PASO UNO DEMINIMOS EL TIPO DOCUMENTO CON LOS RESPECTIVOS MARGENES (A4,IZQ,DER,TOP,BOT)
            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 40f, 100f);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, fsDocumento);
            pdfWriter.PageEvent = new HeaderFooter();


            try
            {
                pdfDoc.Open();
                Paragraph frase1, frase2;

                if (c.company_x == true)
                {
                    frase1 = new Paragraph(c.company, negritaPeque);
                    a = 18;
                }
                else
                {
                    frase1 = new Paragraph("", negritaPeque);
                    a = 0;
                }
                frase1.Alignment = Element.ALIGN_RIGHT;
                pdfDoc.Add(frase1);
                pdfDoc.Add(new Chunk(""));

                if (c.taxid_x == true)
                {
                    frase2 = new Paragraph(c.taxid, negritaPeque);
                    b = 18;
                }
                else
                {
                    frase2 = new Paragraph("", negritaPeque);
                    b = 0;
                }
                frase2.Alignment = Element.ALIGN_RIGHT;
                pdfDoc.Add(frase2);
                r = a + b;

                //AQUI VA LA LINEA 2
                pdfDoc.Add(new Chunk(""));
                PdfPTable tabla = new PdfPTable(5);
                PdfPCell celdaColor = new PdfPCell(new Paragraph(""));
                PdfPCell celdaColor2 = new PdfPCell(new Paragraph(""));
                PdfPCell celdaColor3 = new PdfPCell(new Paragraph(""));
                PdfPCell celdaColor4 = new PdfPCell(new Paragraph(""));
                PdfPCell celdaColor5 = new PdfPCell(new Paragraph(""));
                celdaColor.BackgroundColor = new BaseColor(181, 25, 70);
                celdaColor2.BackgroundColor = new BaseColor(150, 23, 46);
                celdaColor3.BackgroundColor = new BaseColor(238, 175, 48);
                celdaColor4.BackgroundColor = new BaseColor(224, 0, 52);
                celdaColor5.BackgroundColor = new BaseColor(252, 217, 0);
                celdaColor.FixedHeight = 10f;
                tabla.AddCell(celdaColor);
                tabla.AddCell(celdaColor2);
                tabla.AddCell(celdaColor3);
                tabla.AddCell(celdaColor4);
                tabla.AddCell(celdaColor5);
                tabla.SetWidthPercentage(new float[] { 400, 50, 80, 25, 110 }, PageSize.A4);
                for (int i = 0; i < tabla.Rows.Count; i++)
                {
                    if (i <= 4)
                    {
                        hfClass.quitaBordes(i, tabla);
                    }
                }
                pdfDoc.Add(tabla);

                //AQUI EMPIEZA APARTADO DE DATOS
                pdfDoc.Add(new Chunk(""));
                tablaDatos1.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaDatos1.SetWidthPercentage(new float[] { 298, 298 }, PageSize.A4);

                if (c.concepto_x == true)
                { PdfPCell celda1 = new PdfPCell(new Paragraph(c.concepto, negritaPeque)); celda1.Border = 0; tablaDatos1.AddCell(celda1); }
                else
                { PdfPCell celda1 = new PdfPCell(new Paragraph("", negritaPeque)); celda1.Border = 0; tablaDatos1.AddCell(celda1); }

                if (c.folio_x == true)
                { PdfPCell celda2 = new PdfPCell(new Paragraph(f.FOLIO + " " + c.folio, negritaPeque)); celda2.HorizontalAlignment = Element.ALIGN_RIGHT; celda2.Border = 0; tablaDatos1.AddCell(celda2); }
                else
                { PdfPCell celda2 = new PdfPCell(new Paragraph("", normalPeque)); celda2.HorizontalAlignment = Element.ALIGN_RIGHT; celda2.Border = 0; tablaDatos1.AddCell(celda2); }

                PdfPCell celdaB1 = new PdfPCell(new Paragraph("\n", negritaPeque)); celdaB1.Border = 0; tablaDatos1.AddCell(celdaB1);
                PdfPCell celdaB2 = new PdfPCell(new Paragraph("\n", negritaPeque)); celdaB2.Border = 0; tablaDatos1.AddCell(celdaB2);

                if (c.payerNom_x == true)
                { PdfPCell celda3 = new PdfPCell(new Paragraph(c.payerNom, negritaPeque)); celda3.Border = 0; tablaDatos1.AddCell(celda3); }
                else
                { PdfPCell celda3 = new PdfPCell(new Paragraph("", normalPeque)); celda3.Border = 0; tablaDatos1.AddCell(celda3); }

                if (c.lugarFech_x == true)
                { PdfPCell celda4 = new PdfPCell(new Paragraph(c.lugarFech, negritaPeque)); celda4.HorizontalAlignment = Element.ALIGN_RIGHT; celda4.Border = 0; tablaDatos1.AddCell(celda4); }
                else
                { PdfPCell celda4 = new PdfPCell(new Paragraph("", normalPeque)); celda4.HorizontalAlignment = Element.ALIGN_RIGHT; celda4.Border = 0; tablaDatos1.AddCell(celda4); }

                if (c.cliente_x == true)
                { PdfPCell celda5 = new PdfPCell(new Paragraph(c.cliente, negritaPeque)); celda5.Border = 0; tablaDatos1.AddCell(celda5); }
                else
                { PdfPCell celda5 = new PdfPCell(new Paragraph("", negritaPeque)); celda5.Border = 0; tablaDatos1.AddCell(celda5); }

                if (c.lugar_x == true)
                { PdfPCell celda6 = new PdfPCell(new Paragraph(c.lugar, negritaPeque)); celda6.HorizontalAlignment = Element.ALIGN_RIGHT; celda6.Border = 0; tablaDatos1.AddCell(celda6); }
                else
                { PdfPCell celda6 = new PdfPCell(new Paragraph("", negritaPeque)); celda6.HorizontalAlignment = Element.ALIGN_RIGHT; celda6.Border = 0; tablaDatos1.AddCell(celda6); }

                if (c.puesto_x == true)
                { PdfPCell celda7 = new PdfPCell(new Paragraph(c.puesto, normalPeque)); celda7.Border = 0; tablaDatos1.AddCell(celda7); }
                else
                { PdfPCell celda7 = new PdfPCell(new Paragraph("", normalPeque)); celda7.Border = 0; tablaDatos1.AddCell(celda7); }

                if (c.payerId_x == true)
                { PdfPCell celda8 = new PdfPCell(new Paragraph(f.CONTROL, negritaPeque)); celda8.BackgroundColor = new BaseColor(204, 204, 204); tablaDatos1.AddCell(celda8); }
                else
                { PdfPCell celda8 = new PdfPCell(new Paragraph("", negritaPeque)); celda8.Border = 0; tablaDatos1.AddCell(celda8); }

                if (c.direccion_x == true)
                { PdfPCell celda9 = new PdfPCell(new Paragraph(c.direccion, normalPeque)); celda9.Border = 0; tablaDatos1.AddCell(celda9); }
                else
                { PdfPCell celda9 = new PdfPCell(new Paragraph("", normalPeque)); celda9.Border = 0; tablaDatos1.AddCell(celda9); }

                if (c.payerId_x == true)
                { PdfPCell celda10 = new PdfPCell(new Paragraph(f.PAYER + " " + c.payerId, normalPeque)); tablaDatos1.AddCell(celda10); }
                else
                { PdfPCell celda10 = new PdfPCell(new Paragraph("", normalPeque)); celda10.Border = 0; tablaDatos1.AddCell(celda10); }

                float var = tablaDatos1.TotalHeight;
                pdfDoc.Add(tablaDatos1);

                //APARTIR DE AQUI VA EL ESTIMADO
                pdfDoc.Add(new Chunk("\n"));
                Phrase fraseEstimado = new Phrase();

                if (c.estimado_x == true)
                {
                    fraseEstimado.Add(new Paragraph(f.ESTIMADO + " " + c.estimado, negritaPeque));
                }
                else
                {
                    fraseEstimado.Add("");
                }
                pdfDoc.Add(fraseEstimado);

                //APARTIR DE AQUI VA LA MECANICA
                pdfDoc.Add(new Chunk("\n"));
                pdfDoc.Add(new Chunk("\n"));
                Phrase miFrase = new Phrase();

                if (c.mecanica_x == true)
                {
                    miFrase.Add(new Paragraph(c.mecanica, normalPeque));
                }
                else
                {
                    miFrase.Add("");
                }
                pdfDoc.Add(miFrase);

                //APARTIR DE AQUI VA EL MONTO
                pdfDoc.Add(new Chunk("\n"));
                miFrase.Clear();
                miFrase.Add(new Paragraph(f.MONTO + " " + c.monto + " " + c.moneda, normalPeque));
                pdfDoc.Add(miFrase);

                //LINEAS PARA LA FIRMA EN UNA TABLA
                PdfPCell celFirma1 = new PdfPCell();
                PdfPCell celFirma2 = new PdfPCell();

                PdfPTable tabFirma1 = new PdfPTable(1);
                PdfPCell celFirmita1 = new PdfPCell();
                if (c.nombreE_x == true | c.puestoE_x == true | c.companyC_x == true)
                { celFirmita1.AddElement(new Paragraph("\n", normalPeque)); celFirmita1.Border = 2; }
                else
                { celFirmita1.AddElement(new Paragraph("", normalPeque)); celFirmita1.Border = 0; }
                tabFirma1.AddCell(celFirmita1);
                tabFirma1.SetWidthPercentage(new float[] { 450 }, PageSize.A4);

                PdfPTable tabFirma2 = new PdfPTable(1);
                PdfPCell celFirmita2 = new PdfPCell();
                if (c.nombreC_x == true | c.puestoC_x == true | c.companyCC_x == true)
                { celFirmita2.AddElement(new Paragraph("\n", normalPeque)); celFirmita2.Border = 2; }
                else
                { celFirmita2.AddElement(new Paragraph("", normalPeque)); celFirmita2.Border = 0; }
                tabFirma2.AddCell(celFirmita2);
                tabFirma2.SetWidthPercentage(new float[] { 450 }, PageSize.A4);

                celFirma1.AddElement(tabFirma1);
                celFirma1.Border = 0;

                celFirma2.AddElement(tabFirma2);
                celFirma2.Border = 0;

                tablaDatos2.AddCell(celFirma1);
                tablaDatos2.AddCell(celFirma2);
                tablaDatos2.SetWidthPercentage(new float[] { 300, 300 }, PageSize.A4);

                pdfDoc.Add(tablaDatos2);

                //pdfDoc.Add(new Chunk("\n"));
                //TABLA DE DATOS 3
                tablaDatos3.HorizontalAlignment = Element.ALIGN_LEFT;
                tablaDatos3.SetWidthPercentage(new float[] { 298, 298 }, PageSize.A4);

                if (c.nombreE_x == true)
                { PdfPCell celda1Dat3 = new PdfPCell(new Paragraph(c.nombreE, negritaPeque)); celda1Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda1Dat3); }
                else
                { PdfPCell celda1Dat3 = new PdfPCell(new Paragraph("", negritaPeque)); celda1Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda1Dat3); }

                if (c.nombreC_x == true)
                { PdfPCell celda2Dat3 = new PdfPCell(new Paragraph(c.nombreC, negritaPeque)); celda2Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda2Dat3); }
                else
                { PdfPCell celda2Dat3 = new PdfPCell(new Paragraph("", negritaPeque)); celda2Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda2Dat3); }

                if (c.puestoE_x == true)
                { PdfPCell celda3Dat3 = new PdfPCell(new Paragraph(c.puestoE, normalPeque)); celda3Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda3Dat3); }
                else
                { PdfPCell celda3Dat3 = new PdfPCell(new Paragraph("", normalPeque)); celda3Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda3Dat3); }

                if (c.puestoC_x == true)
                { PdfPCell celda4Dat3 = new PdfPCell(new Paragraph(c.puestoC, normalPeque)); celda4Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda4Dat3); }
                else
                { PdfPCell celda4Dat3 = new PdfPCell(new Paragraph("", normalPeque)); celda4Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda4Dat3); }

                if (c.companyC_x == true)
                { PdfPCell celda5Dat3 = new PdfPCell(new Paragraph(c.companyC, negritaPeque)); celda5Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda5Dat3); }
                else
                { PdfPCell celda5Dat3 = new PdfPCell(new Paragraph("", negritaPeque)); celda5Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda5Dat3); }

                if (c.companyCC_x == true)
                { PdfPCell celda6Dat3 = new PdfPCell(new Paragraph(c.companyCC, negritaPeque)); celda6Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda6Dat3); }
                else
                { PdfPCell celda6Dat3 = new PdfPCell(new Paragraph("", negritaPeque)); celda6Dat3.HorizontalAlignment = Element.ALIGN_CENTER; tablaDatos3.AddCell(celda6Dat3); }

                for (int i = 0; i < tablaDatos3.Rows.Count; i++)
                {
                    if (i <= 4)
                    {
                        hfClass.quitaBordes(i, tablaDatos3);
                    }
                }
                pdfDoc.Add(tablaDatos3);

                pdfDoc.Close();

                string rutaf = "../PdfTemp/" + nombreArchivo;
                HttpContext.Current.Session["rutaCompletaf"] = rutaf;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}