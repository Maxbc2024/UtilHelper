using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Windows.Forms;
using System.Data;

namespace DLL_UTIL
{

    public static class HelperUtil
    {



        public static double convert_StringToDouble(string p_valorCadena)
        {
            double p_valor = 0.0;
            double.TryParse(p_valorCadena, out p_valor);
            return p_valor;
        }

        public static int convert_StringToInt(string p_valorCadena)
        {
            int p_valor = 0;
            int.TryParse(p_valorCadena, out p_valor);
            return p_valor;
        }

        //public static double ValidDoubleObligatory(double p_valor)
        //{
        //    if (p_valor == 0.0)
        //    {
        //        new Exception("El valor  :" + p_valor + " tiene valor cero ");
        //    }
        //    return p_valor;
        //}

        public static bool ValidFechaInicial_MenorIgual_FechaFin(DateTime p_fechaInicial, DateTime p_fechaFinal)
        {
            if (p_fechaInicial > p_fechaFinal)
            {
                throw new Exception("Error Fecha Inicial es mayor que la Final !");
            }
            return true;
        }

        //******************************************************************************
        //*********************************** Negocio **********************************
        //******************************************************************************

        public static string ValidarOldCodigo_4_y_9_digitos(string p_OldCodigo)
        {
            p_OldCodigo = p_OldCodigo.ToUpper();// regla de negocio - oldcodigo son mayusculas
            // mayusculas
            if (Regex.IsMatch(p_OldCodigo, "^[A-Z0-9]-[A-Z0-9][A-Z0-9]-[0-9][0-9][0-9][0-9]$") ||
                Regex.IsMatch(p_OldCodigo, "^[0-9][0-9][0-9][0-9]$"))
            {
                return p_OldCodigo;
            }
            else
            {
                throw new Exception("El OldCodigo  :" + p_OldCodigo + " no tiene el formato correcto ");
            }
        }

        //'**********************************************************************************************************************************************************************
        //'****************************************************** TIMES  *************************************************************************************
        //'**********************************************************************************************************************************************************************
        //'**********************************************************************************************************************************************************************

         
        //--------------
        public static string Format_FechaPeru(DateTime p_fecha)
        {
            return CompletarCeroIzq(p_fecha.Day.ToString(), 2) + "/" + CompletarCeroIzq(p_fecha.Month.ToString(), 2) + "/" + p_fecha.Year.ToString();
        }

        public static string Format_Fecha_utltimoDia_Peru(DateTime p_fecha)
        {
            return CompletarCeroIzq(DateTime.DaysInMonth(p_fecha.Year, p_fecha.Month).ToString(), 2) + "/" + CompletarCeroIzq(p_fecha.Month.ToString(), 2) + "/" + p_fecha.Year.ToString();
        }

        public static string Format_FechaHoraMinutoPeru(DateTime p_fecha)
        {
            return CompletarCeroIzq(p_fecha.Day.ToString(), 2) + "/" +
                    CompletarCeroIzq(p_fecha.Month.ToString(), 2) + "/" +
                    p_fecha.Year.ToString() + " " +
                     CompletarCeroIzq(p_fecha.Hour.ToString(), 2) + ":" + CompletarCeroIzq(p_fecha.Minute.ToString(), 2);
        }

        public static string Format_HoraMinutoPeru(DateTime p_fecha)
        {
            return CompletarCeroIzq(p_fecha.Hour.ToString(), 2) + ":" + CompletarCeroIzq(p_fecha.Minute.ToString(), 2);
        }

        // probar
        public static string GetNameMonthPeru(DateTime p_fecha)
        {
            CultureInfo culture = new CultureInfo("es-PE");
            return p_fecha.ToString("MMMM", culture);
        }

        public static int getDaysForMonth(DateTime p_fecha)
        {
            return DateTime.DaysInMonth(p_fecha.Year, p_fecha.Month);
        }

        public static DateTime getDatetime_FechaIni_WithHourPeru(DateTime p_fecha)
        {
            return new DateTime(p_fecha.Year, p_fecha.Month, p_fecha.Day, 0, 0, 0);
        }

        public static DateTime getDatetime_FechaFin_WithHourPeru(DateTime p_fecha)
        {
            return new DateTime(p_fecha.Year, p_fecha.Month, p_fecha.Day, 23, 59, 59);
        }

        //'**********************************************************************************************************************************************************************
        //'****************************************************** FORMAT DECIMAL  ***********************************************************************************************
        //'**********************************************************************************************************************************************************************
        //'**********************************************************************************************************************************************************************

        public static string Format_SinDecimalesPeru(double p_Valor)
        {
            //return string.Format(p_Valor.ToString(), "##,##0.00");
            return string.Format("{0:0,0}", p_Valor);
        }

        public static string Format_DosDecimalesPeru(double p_Valor)
        {
            //return string.Format(p_Valor.ToString(), "##,##0.00");
            return string.Format( "{0:0,0.00}", p_Valor);
        }

        public static string Format_TresDecimalesPeru(double p_Valor)
        {
            //return string.Format(p_Valor.ToString(), "##,##0.000");
            return string.Format("{0:0,0.000}", p_Valor);
        }

        public static string Format_SieteDecimalesPeru(double p_Valor)
        {
            //return string.Format(p_Valor.ToString(), "##,##0.0000000");
            return string.Format("{0:0,0.0000000}", p_Valor);
        }

        //'**********************************************************************************************************************************************************************
        //'****************************************************** OTROS  ********************************************************************************************************
        //'**********************************************************************************************************************************************************************
        //'**********************************************************************************************************************************************************************



        //  ( ' ) 
        public static string textoLimpiarCaracteresEspeciales(string p_valor)
        {
            return p_valor.ToString().Replace("'", "");
        }




        public static string CompletarCeroIzq(string p_valor, int p_TamanioTotalCadena)
        {
            if (p_valor == null) throw new Exception("valor no puede ser null");
            if (p_TamanioTotalCadena < 2) throw new Exception("Tamanio debe ser mayor de 1");


            p_valor = p_valor.ToString().Trim();
            string p_NuevoValor = p_valor;
            for (int i = p_valor.Length; i < p_TamanioTotalCadena; i++)
            {
                p_NuevoValor = "0" + p_NuevoValor;
            }
            return p_NuevoValor;
        }


        public static List<CodigoValor> getMeses(bool p_IncluirSelecccione = false)
        {
            var listado = Enumerable.Range(1, 12).Select(i => new CodigoValor { Id = i, Nombre = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) }).ToList();
            if (p_IncluirSelecccione)
            {
                listado.Add(new CodigoValor { Id = 0, Nombre = "(Seleccione)" });
            }
            return listado.OrderBy(x => x.Id).ToList();
        }

        public static List<CodigoValor> getAnio(int p_AnioInicio, bool p_IncluirSelecccione = false)
        {
            if ((DateTime.Now.Year - p_AnioInicio) < 1) throw new Exception("El año actual no puede ser menor o igual que el años Inicio");

            var listadoSeleccione = new List<CodigoValor>();
            if (p_IncluirSelecccione)
            {
                listadoSeleccione.Add(new CodigoValor { Id = 0, Nombre = "(Seleccione)" });
            }
            var listadoYears = Enumerable.Range(p_AnioInicio, (DateTime.Now.Year + 1 - p_AnioInicio))
                            .Select(i => new CodigoValor { Id = i, Nombre = i.ToString() })
                            .OrderByDescending(x => x.Id)
                            .ToList();

            var unionList = listadoSeleccione.Union(listadoYears);
            return unionList.ToList();
        }

      
         public static string Cortar_String(string p_valor,int p_tamanio)
         {
             return p_valor.Substring(0,p_tamanio);
         }


         //'**********************************************************************************************************************************************************************
         //'****************************************************** OTROS  DATATABLE **********************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************


         //example: SumarizarDataTableCompute(dtListadoImportarExcel, "Sum(Cantidad)")
         public static double SumarizarDataTableCompute(DataTable p_data,String p_columnaFormula,string p_filter ="" )
         {
            double p_resultadoFinal = 0.0;
            double.TryParse(p_data.Compute(p_columnaFormula, p_filter).ToString(), out p_resultadoFinal);
            return p_resultadoFinal;
         }

            
         public static DataTable filterDataTable(DataTable p_data, string p_queryFilter, string p_querySort)
         {
             /////////////////////////////////////////////////////////////////////
             p_data.CaseSensitive = false;
             /////////////////////////////////////////////////////////////////////
             DataView dv = new DataView(p_data);
             if (string.IsNullOrEmpty(p_queryFilter) == false)
             {
                 dv.RowFilter = p_queryFilter;
             }
             if (string.IsNullOrEmpty(p_querySort) == false)
             {
                 dv.Sort = p_querySort;
             }
             /////////////////////////////////////////////////////////////////////
             return dv.ToTable();
         }


         //public double GetAsDouble(DataRow dr,string nombreCampo )
         //{
         //    double resultado = 0;
         //    try
         //    {
         //        if (dr.Table.Columns.Contains(nombreCampo.Trim()))
         //        {
         //            if (!(dr[nombreCampo.Trim()] is DBNull)) 
         //                resultado = double.Parse(dr[nombreCampo.Trim()].ToString());
         //        }
         //        else throw new Exception("El Campo, " + nombreCampo.Trim() + ", No Existe");
         //    }
         //    catch (Exception)
         //    {
         //        throw;
         //    }
         //    return resultado;
         //}

         public static double GetDouble_FromDataRow(DataRow p_dataRow, string p_nombreCampo)
         {
             try
             {
                 if (p_dataRow.Table.Columns.Contains(p_nombreCampo.Trim()))
                 {
                     return Convert_BDNULL_To_Double(p_dataRow[p_nombreCampo.Trim()]);
                 }
                 else
                 {
                     throw new Exception("La columna [" + p_nombreCampo.Trim() + "] No Existe");
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("Error GetDouble_FromDataRow > ", ex);
             }
         }

         public static string GetString_FromDataRow(DataRow p_dataRow, string p_nombreCampo)
         {
             try
             {
                 if (p_dataRow.Table.Columns.Contains(p_nombreCampo.Trim()))
                 {
                     return Convert_BDNULL_To_String(p_dataRow[p_nombreCampo.Trim()]);
                 }
                 else
                 {
                     throw new Exception("La columna [" + p_nombreCampo.Trim() + "] No Existe");
                 }
             }
             catch (Exception ex)
             {
                 throw new Exception("Error GetDouble_FromDataRow > ", ex);
             }
         }

         //public string GetValorFromDT(string nombreCampo, DataTable dt, int posicion = 0)
         //{
         //    string resultado = "";
         //    try
         //    {
         //        if (!dt.Columns.Contains(nombreCampo.Trim())) throw new Exception("El Campo, " + nombreCampo.Trim() + ", No Existe");
         //        resultado = dt.Rows[posicion][nombreCampo.Trim()].ToString();
         //    }
         //    catch (Exception)
         //    {

         //        throw;
         //    }
         //    return resultado;
         //}

         //public static AutoCompleteStringCollection LoadAutoComplete()
         //{
         //    DataTable dt = LoadDataTable();
         //    AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
         //    foreach (DataRow row in dt.Rows)
         //    {
         //        stringCol.Add(Convert.ToString(row["Descripcion"]));
         //    }
         //    return stringCol;
         //}


        //'**********************************************************************************************************************************************************************
        //'********************************************************* CONVERT DBNull TO  *****************************************************************************************
        //'**********************************************************************************************************************************************************************
        //'**********************************************************************************************************************************************************************
 

        public static string Convert_BDNULL_To_String(object p_valor)
        {
            if (p_valor == DBNull.Value)
            {
                return "";
            }
            else 
            {
                return p_valor.ToString();
            }
        }

        public static int Convert_BDNULL_To_Int(object p_valor)
        {
            if (p_valor == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return int.Parse(p_valor.ToString());
            }
        }

        public static double Convert_BDNULL_To_Double(object p_valor)
        {
            if (p_valor == DBNull.Value)
            {
                return 0.0;
            }
            else
            {
                return double.Parse(p_valor.ToString());
            }
        }

        //'**********************************************************************************************************************************************************************
        //'******************************************************************** VALID   *****************************************************************************************
        //'**********************************************************************************************************************************************************************
        //'**********************************************************************************************************************************************************************

        public static bool IsValidEmail(string p_email)
        {
            return Regex.IsMatch(p_email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

         public static int valid_Obligatory_Int(int p_valor)
         {
             if (p_valor == 0)
             {
                 throw new Exception("Tiene valor: 0");
             }
             else
             {
                 return p_valor;
             }
         }

         public static string valid_Obligatory_String(string p_valor)
         {
             if (string.IsNullOrEmpty(p_valor) == true)
             {
                 throw new Exception("Tiene valor vacío");
             }
             else
             {
                 return p_valor;
             }
         }

         public static double valid_Obligatory_Double(double p_valor)
         {
             if (p_valor == 0.0)
             {
                 throw new Exception("Tiene valor: 0.0");
             }
             else
             {
                 return p_valor;
             }
         }

         public static decimal valid_Obligatory_Decimal(decimal p_valor)
         {
             if (Convert.ToDecimal(p_valor) == 0)
             {
                 throw new Exception("Tiene valor: 0.0");
             }
             else
             {
                 return p_valor;
             }
         }

         public static DateTime valid_Obligatory_datetime(DateTime p_valor)
         {
             if (p_valor== DateTime.MinValue)
             {
                 throw new Exception("No existe Fecha");
             }
             else
             {
                 return p_valor;
             }
         }

         //**************** tamaños

         public static string valid_String_Tamanio_Maximo(string p_valor,int p_tamanioMaximo) 
         {
             if (p_valor.Trim().Length > p_tamanioMaximo)
             {
                 throw new Exception("El valor:" + p_valor + " ha excedido el tamaño máximo: " + p_tamanioMaximo.ToString() + " de caracteres");
             }
             return p_valor;
         }

         public static string valid_String_Tamanio(string p_valor, int p_tamanio)
         {
             if (p_valor.Trim().Length == p_tamanio)
             {
                 throw new Exception("El valor:" + p_valor + " no tiene el tamaño exacto: " + p_tamanio.ToString() + " de caracteres");
             }
             return p_valor;
         }

         //**************** only number 

         public static string valid_string_only_Number(string p_valor)
         {
             // mayusculas
             if (Regex.IsMatch(p_valor, "^[0-9]+$"))
             {
                 return p_valor;
             }
             else
             {
                 throw new Exception("El valor:" + p_valor.ToString() + " debe tener solo numeros ");
             }
         }

         //public static object valid_object_only_Number_and_decimal(object p_valor)
         //{
         //    if (Regex.IsMatch(p_valor.ToString(), "^[0-9]+$") == false & Regex.IsMatch(p_valor.ToString(), "^\d*\.\d+$") == false )
         //    {
         //       throw new Exception("El valor:" + p_valor.ToString() + " debe tener solo numeros ");
         //    }
         //    else
         //    {
         //        return p_valor;
         //    }
         //}

            
         //**************** positive 

         public static int valid_only_int_Positive(int p_valor) 
         {
             if (p_valor > 0) 
             {
                 return p_valor;
             }
             else
             {
                 throw new Exception("El valor:" + p_valor.ToString() + " tiene valor Negativo ");
             }
         }

         public static double valid_only_double_Positive(double p_valor)
         {
             if (p_valor > 0.0)
             {
                 return p_valor;
             }
             else
             {
                 throw new Exception("El valor:" + p_valor.ToString() + " tiene valor Negativo ");
             }
         }


         //'**********************************************************************************************************************************************************************
         //'******************************************************************** VALID RETURN  BOOL  *****************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************

         public static bool IsValid_Double_postitive(double p_valor) 
         {
             if (p_valor > 0)
             {
                 return true;
             }
             else {
                 return false;
             }
         }

         //public static bool IsValid_Double_postitive<T>(T p_valor) 
         //{
         //    var valueType = p_valor.GetType();
         //      if (typeof(T).IsAssignableFrom(valueType))
         //    {
         //        if (p_valor > 0)
         //        {
         //            return true;
         //        }
         //    }
         //    else
         //    {
         //        return false;
         //    }
         //}

       

         //'**********************************************************************************************************************************************************************
         //'******************************************************************** VALID EXTENSIONS  *******************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************


         //**************** Column
         public static string valid_Column_Obligatory_String(string p_Valor, string p_NombreColumna)
         {
             try
             {
                 return valid_Obligatory_String(p_Valor);
             }
             catch (Exception ex)
             {
                 throw new Exception("El valor de la columna:[" + p_NombreColumna + "] " + ex.Message);
             }
         }

         public static int valid_Column_Obligatory_int(int p_Valor, string p_NombreColumna)
         {
             try
             {
                 return valid_Obligatory_Int(p_Valor);
             }
             catch (Exception ex)
             {
                 throw new Exception("El valor de la columna:[" + p_NombreColumna + "] " + ex.Message);
             }
         }

         public static double valid_Column_Obligatory_double(double p_Valor, string p_NombreColumna)
         {
             try
             {
                 return valid_Obligatory_Double(p_Valor);
             }
             catch (Exception ex)
             {
                 throw new Exception("El valor de la columna:[" + p_NombreColumna + "] " + ex.Message);
             }
         }

         public static decimal valid_Column_Obligatory_decimal(decimal p_Valor, string p_NombreColumna)
         {
             try
             {
                 return valid_Obligatory_Decimal(p_Valor);
             }
             catch (Exception ex)
             {
                 throw new Exception("El valor de la columna:[" + p_NombreColumna + "] " + ex.Message);
             }
         }

        //'**********************************************************************************************************************************************************************
        //'******************************************************************** VALID ROUND  ************************************************************************************
        //'**********************************************************************************************************************************************************************
        //'**********************************************************************************************************************************************************************


        // falta Probar
         public static bool Valid_round_TwoDecimal(double p_Valor_1, double p_Valor_2) 
         {
             double VALOR_TOLERANCIA  = 0.5;
             int DECIMALES_REDONDEAR = 2;

              double aaaaa1  = Math.Round(p_Valor_1, DECIMALES_REDONDEAR);
              double bbbbb1 = Math.Round(p_Valor_2, DECIMALES_REDONDEAR);

              double ccccc1 = Math.Abs(aaaaa1 - bbbbb1);

              if (ccccc1 > VALOR_TOLERANCIA)
              {
                  throw new Exception("No coincide los valores:[" + aaaaa1.ToString() + " != " + bbbbb1.ToString()+"]"  );
              }
              else 
              {
                  return true;
              }
         }




         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'****************************************************** OTROS UTIL METODO WINFORM *************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************
         //'**********************************************************************************************************************************************************************


         public static void MostrarMensajeInformacion(string Mensaje, string titulo = "")
         {
             MessageBox.Show(Mensaje, titulo.ToUpper(), MessageBoxButtons.OK, MessageBoxIcon.Information);
         }

         //public static DialogResult MostrarMensajePregunta(string Mensaje, string titulo = "FAVOR CONFIRMAR")
         //{
         //    return MessageBox.Show(Mensaje.ToUpper(), titulo.ToUpper(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
         //}

         public static bool MostrarMensajePreguntaV2(string Mensaje, string titulo = "FAVOR CONFIRMAR")
         {
            DialogResult result = MessageBox.Show(Mensaje, titulo.ToUpper(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            return false;
         }

         public static void MostrarMensajeError(string Mensaje, string titulo = "ERROR INESPERADO")
         {
             //Mandamos un Mensaje de Error a Correos
             try
             {
                 //TITULO_MENSAJE = FORM_NAME + ", ERROR DEL SISTEMA";
                 //MENSAJE = titulo + ", " + Mensaje;
                 ////MANDAMOS UN CORREO CON LOS DATOS DE INICIO DE SESSION
                 //Thread tAux = new Thread(new ThreadStart(EnviarEMail));
                 //tAux.Start();
                 //while (!tAux.IsAlive) ;
             }
             catch (Exception)
             {

             }
             MessageBox.Show(Mensaje, titulo.ToUpper(), MessageBoxButtons.OK, MessageBoxIcon.Error);
         }

         public static void MostrarMensajeAdvertencia(string Mensaje, string titulo = "AVERTENCIA!!")
         {
             MessageBox.Show(Mensaje, titulo.ToUpper(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
         }
        
        //'**********************************************************************************************************************************************
        //'**************************************************************** EVENTO **********************************************************************
        //'**********************************************************************************************************************************************

         //solo numeros positivos
         public static void Valid_events_WinForms_OnlyNumeros_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
             {
                 e.Handled = true;
             }
         }

         //solo numeros positivos
         //AddHandler Me.TextBox1.KeyPress, AddressOf HelperUtil.Valid_events_WinForms_OnlyDecimales_KeyPress
         public static void Valid_events_WinForms_OnlyDecimales_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
             {
                 e.Handled = true;
             }

             // solo 1 punto decimal
             if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
             {
                 e.Handled = true;
             }
         }

         public static void Valid_events_WinForms_OnlyDecimales_Inc_Neg_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
             {
                 e.Handled = true;
             }

             // solo 1 punto decimal
             if ((e.KeyChar == '.') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1))
             {
                 e.Handled = true;
             }

             // solo 1 negativo
             if ((e.KeyChar == '-') && ((sender as System.Windows.Forms.TextBox).Text.IndexOf('-') > -1))
             {
                 e.Handled = true;
             }


         }
         
         // falta
         //AddHandler Me.TextBox1.Click, AddressOf HelperUtil.Valid_events_WinForms_SelectAllFocusTextBox_Click
         public static void Valid_events_WinForms_SelectAllFocusTextBox_Click(System.Object sender, System.EventArgs e) 
         {
             if (sender is TextBox)
             {
                 TextBox txt = (TextBox)sender;
                 txt.SelectAll();
                 txt.Focus();
             }
         }



         //*******************************************************************************************************************************
         //*********************************************** WIN CONTROLES COMUNES *************************************************************
         //*******************************************************************************************************************************

        // CARGA COMBO AÑO
         public static void getComboBox_Years_SelectedAnioActual(ComboBox p_ComboBox, int p_AnioInicio)
         {
             p_ComboBox.DataSource = HelperUtil.getAnio(p_AnioInicio, true);
             p_ComboBox.DisplayMember = "Nombre";
             p_ComboBox.ValueMember = "Id";

             p_ComboBox.SelectedValue = DateTime.Today.Year;
         }

         // CARGA COMBO MES
         public static void getComboBox_Months_SelectedMonthActual(ComboBox p_ComboBox)
         {
             p_ComboBox.DataSource = HelperUtil.getMeses(true);
             p_ComboBox.DisplayMember = "Nombre";
             p_ComboBox.ValueMember = "Id";

             p_ComboBox.SelectedValue = DateTime.Today.Month;
         }



         // GRIDVIEW
         public static void GridView_SelectUnselectAllCheckBoxColumn(DataGridView p_GridView, string p_NameColumnCheck, bool p_Flag_select)
         {
             DataGridViewCheckBoxCell ckbCelda = new DataGridViewCheckBoxCell();
             for (int i = 0; i < p_GridView.Rows.Count; i++)
             {
                 ckbCelda = (DataGridViewCheckBoxCell)p_GridView.Rows[i].Cells[p_NameColumnCheck];
                 ckbCelda.Value = p_Flag_select;
                
             }
         }

         //'**********************************************************************************************************************************************
         //'********************************************************** LEER ERRORES MENSAJE **************************************************************
         //'**********************************************************************************************************************************************

         public static void show_msg_errors(ExceptionDominio p_ex)
         {
             StringBuilder p_mensajesErrores = new StringBuilder("");
             foreach (var p_mensaje in p_ex.MensagesErrores)
             {
                 p_mensajesErrores.AppendLine(p_mensaje);
             }
             HelperUtil.MostrarMensajeError(p_mensajesErrores.ToString());
         }


    }


    public struct CodigoValor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }




}


