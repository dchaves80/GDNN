﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Data2.Connection
{
    public class D_Factura
    {

        public static DataTable GetFacturasTarjetasBetweenDates(int IdUser, DateTime Start, DateTime End)
        {
            GestionDataSetTableAdapters.SELECT_Facturas_Tarjetas_betweenDatesTableAdapter TA = new GestionDataSetTableAdapters.SELECT_Facturas_Tarjetas_betweenDatesTableAdapter();
            GestionDataSet.SELECT_Facturas_Tarjetas_betweenDatesDataTable DT = new GestionDataSet.SELECT_Facturas_Tarjetas_betweenDatesDataTable();

            TA.Fill(DT, IdUser, Start.ToString(), End.ToString());

            if (DT.Rows.Count > 0)
            {
                return DT;
            }
            else
            {
                return null;
            }
        }
        public List<DataTable> GetFacturasBetweenDates(int Iduser, DateTime Start, DateTime End,String TIPO, bool Printed) 
        {
            

            GestionDataSetTableAdapters.SELECT_FACTURASBetweenDatesTableAdapter TA = new GestionDataSetTableAdapters.SELECT_FACTURASBetweenDatesTableAdapter();
            GestionDataSet.SELECT_FACTURASBetweenDatesDataTable DT = new GestionDataSet.SELECT_FACTURASBetweenDatesDataTable();
            GestionDataSetTableAdapters.SELECT_REMITOSBetweenDatesTableAdapter TA2 = new GestionDataSetTableAdapters.SELECT_REMITOSBetweenDatesTableAdapter();
            GestionDataSet.SELECT_REMITOSBetweenDatesDataTable DT2 = new GestionDataSet.SELECT_REMITOSBetweenDatesDataTable();
            

            List<DataTable> DT_List = new List<DataTable>();
            
            
            TA.Fill(DT, Iduser, Statics.Conversion.DateTimeToSql(Start), Statics.Conversion.DateTimeToSql(End), TIPO, Printed);
            if (DT.Rows.Count > 0)
            {


                DT_List.Add(DT);


            }
            else 
            {
                DT_List.Add(null);
            }
            //Statics.Log.ADD("Creo que llega", null);
           TA2.Fill(DT2,Iduser, Statics.Conversion.DateTimeToSql(Start), Statics.Conversion.DateTimeToSql(End));
                if (DT2.Rows.Count > 0) 
                {
                    DT_List.Add(DT2);
                }
                else
                {
                    DT_List.Add(null);
                }
                return DT_List;
        }

        public void InsertarDetalleFactura(Class.Struct_Factura p_F) 
        {
            if (p_F.FacturaTipo != Class.Struct_Factura.TipoDeFactura.Presupuesto)
            {

                for (int a = 0; a < p_F.GetDetalle().Count; a++)
                {
                    GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
                    //Si el detalle no es producto
                    if (p_F.GetDetalle()[a].PRODUCTO == null)
                    {
                        //Insertar detalle factura tratamiento
                        QTA.Insert_DetalleFacturaTratamiento(
                            p_F.Id,
                            p_F.GetDetalle()[a].TRATAMIENTO.Id,
                            p_F.GetDetalle()[a].TRATAMIENTO.Precio);
                    }
                    else
                    {
                        //Insertar detalle factura producto
                        if (p_F.GetDetalle()[a].PRODUCTO.MateriaPrima == 0)
                        {
                            QTA.Insert_DetalleFactura(
                                p_F.Id,
                                p_F.GetDetalle()[a].PRODUCTO.Id,
                                p_F.GetDetalle()[a].PRODUCTO.PrecioNeto,
                                p_F.GetDetalle()[a].PRODUCTO.IVA,
                                p_F.GetDetalle()[a].PRODUCTO.PrecioCompra,
                                p_F.GetDetalle()[a].PRODUCTO.PorcentajeGanancia,
                                p_F.GetDetalle()[a].PRODUCTO.PrecioFinal,
                                p_F.GetDetalle()[a].DETALLEINT,
                                p_F.GetDetalle()[a].DETALLEDEC);
                        }
                        else
                        {
                            QTA.Insert_DetalleFacturaConMateriaPrima
                                (
                                p_F.Id,
                                p_F.GetDetalle()[a].PRODUCTO.Id,
                                p_F.GetDetalle()[a].PRODUCTO.MateriaPrima,
                                p_F.GetDetalle()[a].PRODUCTO.PrecioNeto,
                                p_F.GetDetalle()[a].PRODUCTO.IVA,
                                p_F.GetDetalle()[a].PRODUCTO.PrecioCompra,
                                p_F.GetDetalle()[a].PRODUCTO.PorcentajeGanancia,
                                p_F.GetDetalle()[a].PRODUCTO.PrecioFinal,
                                p_F.GetDetalle()[a].DETALLEINT,
                                p_F.GetDetalle()[a].DETALLEDEC

                                );
                        }
                    }
                }
            }
            else
            {
                for (int a = 0; a < p_F.GetDetalle().Count; a++)
                {
                    GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
                    QTA.Insert_DetallePresupuesto(
                        p_F.Id,
                        p_F.GetDetalle()[a].PRODUCTO.Id,
                        p_F.GetDetalle()[a].PRODUCTO.PrecioNeto,
                        p_F.GetDetalle()[a].PRODUCTO.IVA,
                        p_F.GetDetalle()[a].PRODUCTO.PrecioCompra,
                        p_F.GetDetalle()[a].PRODUCTO.PorcentajeGanancia,
                        p_F.GetDetalle()[a].PRODUCTO.PrecioFinal,
                        p_F.GetDetalle()[a].DETALLEINT,
                        p_F.GetDetalle()[a].DETALLEDEC);
                }
            }
        }

        public DataRow GetFacturaById(int p_iduser, int p_idfactura) 
        {
            GestionDataSet.GetFacturaFromIdDataTable DT = new GestionDataSet.GetFacturaFromIdDataTable();
            GestionDataSetTableAdapters.GetFacturaFromIdTableAdapter TA = new GestionDataSetTableAdapters.GetFacturaFromIdTableAdapter();
            TA.Fill(DT, p_iduser, p_idfactura);
            if (DT.Rows.Count > 0)
            {
                return DT.Rows[0];
            }
            else 
            {
                return null;
            }
        }

        public bool InsertVendedorEnFactura(int IdUser, int IdVendedor, int IdFactura) 
        {
            GestionDataSetTableAdapters.QueriesTableAdapter QTA = new GestionDataSetTableAdapters.QueriesTableAdapter();
            int _change = QTA.Insert_FacturaEnVendedor(IdUser, IdVendedor, IdFactura);
            if (_change != 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public int InsertFactura(
            int p_IdUser,
            string p_SerialAFIP,
            string p_SerialTICKET,
            DateTime p_Fecha,
            string p_Tipo,
            string p_Nombre,
            string p_Domicilio,
            string p_Telefono,
            string p_Localidad,
            string p_Cuit,
            bool p_RespInsc,
            bool p_RespNoInsc,
            bool p_Exento,
            bool p_ConsumidorFinal,
            bool p_RespMonotributo,
            bool p_Contado,
            bool p_CtaCte,
            int p_IdCtaCte,
            bool p_Cheque,
            string p_DNI,
            bool p_Tarjeta,
            int p_IdTarjeta,
            string p_NumeroTarjeta,
            string p_Observaciones,
            decimal p_SubTotal,
            bool p_Ivas,
            decimal p_Total
            
            
            ) 
        {
            /*@IdUser bigint,
@SerialAFIP varchar(255),@SerialTICKET varchar(255),
@Printed bit,@PrintAgain bit,
@Transmitida bit,@Fecha datetime,
@Tipo varchar(5),@Nombre varchar(255),
@Domicilio varchar(255),@Telefono varchar(255),
@Localidad varchar(255),@Cuit varchar(255),
@RepInsc bit,@RespNoInsc bit,
@Exento bit,@ConsumidorFinal bit,
@RespMonotributo bit,@Contado bit,
@CtaCte bit,@IdCtaCte bigint,
@Cheque bit,@Dni varchar(255),
@Tarjeta bit,@Idtargeta bigint,
@NumeroTarjeta varchar(50),@Observaciones text,
@SubTotal decimal(18,4),@Ivas bit,
@Total decimal(18,4))*/

            GestionDataSet.insert_FacturaDataTable DT = new GestionDataSet.insert_FacturaDataTable();
            GestionDataSetTableAdapters.insert_FacturaTableAdapter TA = new GestionDataSetTableAdapters.insert_FacturaTableAdapter();
            try
            {
                TA.Fill(DT, p_IdUser, p_SerialAFIP, p_SerialTICKET, false, false, false, DateTime.Now, p_Tipo, p_Nombre, p_Domicilio, p_Telefono, p_Localidad, p_Cuit, p_RespInsc, p_RespNoInsc, p_Exento, p_ConsumidorFinal, p_RespMonotributo, p_Contado, p_CtaCte, p_IdCtaCte, p_Cheque, p_DNI, p_Tarjeta, p_IdTarjeta, p_NumeroTarjeta, p_Observaciones, p_SubTotal, p_Ivas, p_Total);
                if (DT.Rows.Count > 0)
                {
                    return int.Parse( DT.Rows[0][0].ToString());
                }
                else { return 0; }
            }
            catch (Exception E)
            {
                //Statics.Log.ADD("[" + E.Message + "]" + E.StackTrace, null);
                return 0;
            }
            
            
        }
    }
}
